/*    
 * Copyright (c) 2014.9 , 蒙占志 (Zhanzhi Meng) topameng@gmail.com
 * All rights reserved.
 * Use, modification and distribution are subject to the "New BSD License"
*/

using UnityEngine;
using System;
using System.Collections;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using LuaInterface;

using Object = UnityEngine.Object;
using System.IO;
using System.Text.RegularExpressions;

public enum MetaOp
{
    None = 0,
    Add = 1,
    Sub = 2,
    Mul = 4,
    Div = 8,
    Eq = 16,
    Neg = 32,
    ALL = Add | Sub | Mul | Div | Eq | Neg,
}

public enum ObjAmbig
{
    None = 0, 
    U3dObj = 1,
    NetObj = 2,
    All = 3
}

public static class ToLua 
{
    public static string className = string.Empty;
    public static Type type = null;
    
    public static string baseClassName = null;
    public static bool isStaticClass = true;    

    static HashSet<string> usingList = new HashSet<string>();
    static MetaOp op = MetaOp.None;    
    static StringBuilder sb = null;
    static MethodInfo[] methods = null;
    static Dictionary<string, int> nameCounter = null;
    static FieldInfo[] fields = null;
    static PropertyInfo[] props = null;
    static List<PropertyInfo> propList = new List<PropertyInfo>();  //非静态属性

    static BindingFlags binding = BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase;
        
    static ObjAmbig ambig = ObjAmbig.NetObj;
    //wrapClaaName + "Wrap" = 导出文件名，导出类名
    public static string wrapClassName = "";

    public static string libClassName = "";

    public static List<string> memberFilter = new List<string>
    {
        "AnimationClip.averageDuration",
        "AnimationClip.averageAngularSpeed",
        "AnimationClip.averageSpeed",
        "AnimationClip.apparentSpeed",
        "AnimationClip.isLooping",
        "AnimationClip.isAnimatorMotion",
        "AnimationClip.isHumanMotion",
        "AnimatorOverrideController.PerformOverrideClipListCleanup",
        "Caching.SetNoBackupFlag",
        "Caching.ResetNoBackupFlag",
        "Light.areaSize",
        "Security.GetChainOfTrustValue",
        "Texture2D.alphaIsTransparency",
        "WWW.movie",
        "WebCamTexture.MarkNonReadable",
        "WebCamTexture.isReadable",
		// i don't why below 2 functions missed in iOS platform
		"Graphic.OnRebuildRequested",
		"Text.OnRebuildRequested",
        //NGUI
        "UIInput.ProcessEvent",
        "UIWidget.showHandlesWithMoveTool",
        "UIWidget.showHandles",
        "Application.ExternalEval",
        "Resources.LoadAssetAtPath",
        "Input.IsJoystickPreconfigured",
    };

    public static bool IsMemberFilter(MemberInfo mi)
    {
        return memberFilter.Contains(type.Name + "." + mi.Name);
    }

    static ToLua()
    {
                
    }

    public static void Clear()
    {
        className = null;
        type = null;
        isStaticClass = false;
        baseClassName = null;
        usingList.Clear();
        op = MetaOp.None;    
        sb = new StringBuilder();
        methods = null;        
        fields = null;
        props = null;
        propList.Clear();
        ambig = ObjAmbig.NetObj;
        wrapClassName = "";
        libClassName = "";
    }

    private static MetaOp GetOp(string name)
    {
        if (name == "op_Addition")
        {
            return MetaOp.Add;
        }
        else if (name == "op_Subtraction")
        {
            return MetaOp.Sub;
        }
        else if (name == "op_Equality")
        {
            return MetaOp.Eq;
        }
        else if (name == "op_Multiply")
        {
            return MetaOp.Mul;
        }
        else if (name == "op_Division")
        {
            return MetaOp.Div;
        }
        else if (name == "op_UnaryNegation")
        {
            return MetaOp.Neg;
        }

        return MetaOp.None;
    }

    //操作符函数无法通过继承metatable实现
    static void GenBaseOpFunction(List<MethodInfo> list)
    {        
        Type baseType = type.BaseType;

        while (baseType != null)
        {
            MethodInfo[] methods = baseType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);

            for (int i = 0; i < methods.Length; i++)
            {
                MetaOp baseOp = GetOp(methods[i].Name);

                if (baseOp != MetaOp.None && (op & baseOp) == 0)
                {
                    list.Add(methods[i]);
                    op |= baseOp;
                }
            }

            baseType = baseType.BaseType;
        }
    }

    public static void Generate(params string[] param)
    {
        Debugger.Log("Begin Generate lua Wrap for class {0}\n", className);
        sb = new StringBuilder();
        usingList.Add("System");

        if (type.Namespace != null && type.Namespace != string.Empty)
        {
            usingList.Add(type.Namespace);
        }

        if (wrapClassName == "")
        {
            wrapClassName = className;
        }

        if (libClassName == "")
        {
            libClassName = className;
        }

        if (type.IsEnum)
        {
            GenEnum();
            GenEnumTranslator();
            sb.Append("}\n\n");
            SaveFile();
            return;
        }

        nameCounter = new Dictionary<string, int>();        
        List<MethodInfo> list = new List<MethodInfo>();

        if (baseClassName != null)
        {
            binding |= BindingFlags.DeclaredOnly;
        }
        else if (baseClassName == null && isStaticClass)
        {
            binding |= BindingFlags.DeclaredOnly;
        }

        if (type.IsInterface)
        {
            list.AddRange(type.GetMethods());
        }
        else
        {
            list.AddRange(type.GetMethods(BindingFlags.Instance | binding));            

            for (int i = list.Count - 1; i >= 0; --i)
            {
                //先去掉操作符函数
                if (list[i].Name.Contains("op_") || list[i].Name.Contains("add_") || list[i].Name.Contains("remove_"))
                {
                    if (!IsNeedOp(list[i].Name))
                    {
                        list.RemoveAt(i);
                    }

                    continue;
                }

                //扔掉 unity3d 废弃的函数                
                if (IsObsolete(list[i]))
                {
                    list.RemoveAt(i);
                }
            }
        }        

        PropertyInfo[] ps = type.GetProperties();

        for (int i = 0; i < ps.Length; i++)
        {
            int index = list.FindIndex((m) => { return m.Name == "get_" + ps[i].Name; });

            if (index >= 0 && list[index].Name != "get_Item")
            {
                list.RemoveAt(index);
            }

            index = list.FindIndex((m) => { return m.Name == "set_" + ps[i].Name; });

            if (index >= 0 && list[index].Name != "set_Item")
            {
                list.RemoveAt(index);
            }
        }

        GenBaseOpFunction(list);
        methods = list.ToArray();
        
        sb.AppendFormat("public class {0}Wrap\n", wrapClassName);
        sb.Append("{\n");
        
        AddRegVar();
        GenConstruct();
        GenGetType();
        GenRegFunc();
        GenIndexFunc();
        GenNewIndexFunc();
        GenToStringFunc();
        GenFunction();        

        sb.Append("}\n\n");
        //Debugger.Log(sb.ToString());                
        SaveFile();     
    }

    static void SaveFile()
    {
        //string file = Application.dataPath + "/Source/LuaWrap/" + wrapClassName + "Wrap.cs";
        string file = Application.dataPath + "/uLua/Source/LuaWrap/" + wrapClassName + "Wrap.cs";

        using (StreamWriter textWriter = new StreamWriter(file, false, Encoding.ASCII))
        {            
            StringBuilder usb = new StringBuilder();

            foreach (string str in usingList)
            {
                usb.AppendFormat("using {0};\n", str);
            }

            usb.Append("using LuaInterface;\n");

            if (ambig == ObjAmbig.All)
            {
                usb.Append("using Object = UnityEngine.Object;\n");
            }

            usb.Append("\n");

            textWriter.Write(usb.ToString());
            textWriter.Write(sb.ToString());
            textWriter.Flush();
            textWriter.Close();
        }  
    }

    static void AddRegVar()
    {
        sb.Append("\tpublic static LuaMethod[] regs = new LuaMethod[]\n");
        sb.Append("\t{\n");

        //注册库函数
        for (int i = 0; i < methods.Length; i++)
        {
            MethodInfo m = methods[i];
            int count = 1;

            if (m.IsGenericMethod)
            {
                continue;
            }                       

            if (!nameCounter.TryGetValue(m.Name, out count))
            {
                if (!m.Name.Contains("op_"))
                {
                    sb.AppendFormat("\t\tnew LuaMethod(\"{0}\", {0}),\n", m.Name);
                }

                nameCounter[m.Name] = 1;
            }
            else
            {
                nameCounter[m.Name] = count + 1;
            }
        }        

        sb.AppendFormat("\t\tnew LuaMethod(\"New\", _Create{0}),\n", wrapClassName);
        sb.Append("\t\tnew LuaMethod(\"GetClassType\", GetClassType),\n");

        int index = Array.FindIndex<MethodInfo>(methods, (p) => { return p.Name == "ToString"; });

        if (index >= 0 && !isStaticClass)
        {
            sb.Append("\t\tnew LuaMethod(\"__tostring\", Lua_ToString),\n");
        }

        GenOperatorReg();

        sb.Append("\t};\n\n");
        fields = type.GetFields(BindingFlags.GetField | BindingFlags.SetField | BindingFlags.Instance | binding);
        props = type.GetProperties(BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.Instance | binding);
        propList.AddRange(type.GetProperties(BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase));

        List<FieldInfo> fieldList = new List<FieldInfo>();
        fieldList.AddRange(fields);

        for (int i = fieldList.Count - 1; i >= 0; i--)
        {
            if (IsObsolete(fieldList[i]))
            {
                fieldList.RemoveAt(i);
            }
        }

        fields = fieldList.ToArray();

        List<PropertyInfo> piList = new List<PropertyInfo>();
        piList.AddRange(props);

        for (int i = piList.Count - 1; i >= 0; i--)
        {            
            if (piList[i].Name == "Item" || IsObsolete(piList[i]))
            {
                piList.RemoveAt(i);                
            }
        }

        props = piList.ToArray();

        for (int i = propList.Count - 1; i >= 0; i--)
        {            
            if (propList[i].Name == "Item" || IsObsolete(propList[i]))
            {
                propList.RemoveAt(i);                
            }
        }

        if (fields.Length == 0 && props.Length == 0 && isStaticClass && baseClassName == null)
        {
            return;
        }

        sb.Append("\tstatic LuaField[] fields = new LuaField[]\n");
        sb.Append("\t{\n");

        for (int i = 0; i < fields.Length; i++)
        {
            if (fields[i].IsLiteral || fields[i].IsPrivate || fields[i].IsInitOnly)
            {
                sb.AppendFormat("\t\tnew LuaField(\"{0}\", get_{0}, null),\n", fields[i].Name);
            }
            else
            {
                sb.AppendFormat("\t\tnew LuaField(\"{0}\", get_{0}, set_{0}),\n", fields[i].Name);
            }
        }

        for (int i = 0; i < props.Length; i++)
        {                     
            if (props[i].CanRead && props[i].CanWrite && props[i].GetSetMethod(true).IsPublic)
            {
                sb.AppendFormat("\t\tnew LuaField(\"{0}\", get_{0}, set_{0}),\n", props[i].Name);
            }
            else if (props[i].CanRead)
            {
                sb.AppendFormat("\t\tnew LuaField(\"{0}\", get_{0}, null),\n", props[i].Name);
            }
            else if (props[i].CanWrite)
            {
                sb.AppendFormat("\t\tnew LuaField(\"{0}\", null, set_{0}),\n", props[i].Name);
            }
        }

        sb.Append("\t};\n");
    }

    static void GenOperatorReg()
    {
        if ((op & MetaOp.Add) != 0)
        {            
            sb.Append("\t\tnew LuaMethod(\"__add\", Lua_Add),\n");                                            
        }

        if ((op & MetaOp.Sub) != 0)
        {
            sb.Append("\t\tnew LuaMethod(\"__sub\", Lua_Sub),\n");
        }

        if ((op & MetaOp.Mul) != 0)
        {
            sb.Append("\t\tnew LuaMethod(\"__mul\", Lua_Mul),\n");
        }

        if ((op & MetaOp.Div) != 0)
        {
            sb.Append("\t\tnew LuaMethod(\"__div\", Lua_Div),\n");
        }

        if ((op & MetaOp.Eq) != 0)
        {
            sb.Append("\t\tnew LuaMethod(\"__eq\", Lua_Eq),\n");    
        }

        if ((op & MetaOp.Neg) != 0)
        {
            sb.Append("\t\tnew LuaMethod(\"__unm\", Lua_Neg),\n");    
        }
    }

    static void GenRegFunc()
    {
        sb.Append("\n\tpublic static void Register(IntPtr L)\n");
        sb.Append("\t{\n");        

        if (baseClassName == null)
        {
            if (isStaticClass && fields.Length == 0 && props.Length == 0)
            {
                sb.AppendFormat("\t\tLuaScriptMgr.RegisterLib(L, \"{0}\", regs);\n", libClassName);
            }
            else
            {
                sb.AppendFormat("\t\tLuaScriptMgr.RegisterLib(L, \"{0}\", typeof({1}), regs, fields, null);\n", libClassName, className);
            }
        }
        else
        {
            sb.AppendFormat("\t\tLuaScriptMgr.RegisterLib(L, \"{0}\", typeof({2}), regs, fields, typeof({1}));\n", libClassName, baseClassName, className);
        }

        sb.Append("\t}\n");
    }

    static bool IsParams(ParameterInfo param)
    {
        return param.GetCustomAttributes(typeof(ParamArrayAttribute), false).Length > 0;
    }

    static void GenFunction()
    {
        HashSet<string> set = new HashSet<string>();

        for (int i = 0; i < methods.Length; i++)
        {
            MethodInfo m = methods[i];

            if (m.IsGenericMethod)
            {
                Debugger.Log("Generic Method {0} cannot be export to lua", m.Name);
                continue;
            }

            if (nameCounter[m.Name] > 1)
            {
                if (!set.Contains(m.Name))
                {
                    MethodInfo mi = GenOverrideFunc(m.Name);

                    if (mi == null)
                    {
                        set.Add(m.Name);
                        continue;
                    }
                    else
                    {
                        m = mi;
                    }
                }
                else
                {
                    continue;
                }
            }
            
            set.Add(m.Name);
            sb.Append("\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]\n");
            sb.AppendFormat("\tstatic int {0}(IntPtr L)\n", GetFuncName(m.Name));
            sb.Append("\t{\n");
            
            ParameterInfo[] paramInfos = m.GetParameters();            
            int offset = m.IsStatic ? 1 : 2;
            bool haveParams = HasOptionalParam(paramInfos);

            if (!haveParams)
            {
                int count = paramInfos.Length + offset - 1;

                //for (int j = 0; j < paramInfos.Length - 1; j++)
                //{
                //    if (paramInfos[j].Attributes == ParameterAttributes.Out)
                //    {
                //        --count;
                //    }
                //}

                sb.AppendFormat("\t\tLuaScriptMgr.CheckArgsCount(L, {0});\n", count);
            }
            else
            {
                sb.Append("\t\tint count = LuaDLL.lua_gettop(L);\n");
            }

            int rc = m.ReturnType == typeof(void) ? 0 : 1;
            rc += ProcessParams(m, 2, false, false);            
            sb.AppendFormat("\t\treturn {0};\n", rc);
            sb.Append("\t}\n");
        }
    }

    static void NoConsturct()
    {
        sb.Append("\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]\n");
        sb.AppendFormat("\tstatic int _Create{0}(IntPtr L)\n", wrapClassName);
        sb.Append("\t{\n");
        sb.AppendFormat("\t\tLuaDLL.luaL_error(L, \"{0} class does not have a constructor function\");\n", className);
        sb.Append("\t\treturn 0;\n");
        sb.Append("\t}\n");
    }

    static string GetPushFunction(Type t)
    {        
        if (t.IsEnum)
        {
            return "Push";
        }
        else if (t == typeof(bool) || t.IsPrimitive || t == typeof(string) || t == typeof(LuaTable) || t == typeof(LuaCSFunction) || t == typeof(LuaFunction) || 
            typeof(UnityEngine.Object).IsAssignableFrom(t) || t == typeof(Type) || t == typeof(IntPtr) || typeof(Delegate).IsAssignableFrom(t) ||
            typeof(UnityEngine.TrackedReference).IsAssignableFrom(t) || typeof(IEnumerator).IsAssignableFrom(t))
        {
            return "Push";
        }
        else if (t == typeof(Vector3) || t == typeof(Vector2) || t == typeof(Vector4) || t == typeof(Quaternion) || t == typeof(Color))
        {
            return "Push";
        }
        else if (t == typeof(RaycastHit) || t == typeof(Ray) || t == typeof(Touch))
        {
            return "Push";
        }
        else if (t == typeof(object))
        {            
            return "PushVarObject";
        }
        else if (t.IsValueType)
        {
            return "PushValue";
        }
        else if (t.IsArray)
        {
            return "PushArray";
        }

        return "PushObject";
    }

    static void DefaultConstruct()
    {
        sb.Append("\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]\n");
        sb.AppendFormat("\tstatic int _Create{0}(IntPtr L)\n", wrapClassName);
        sb.Append("\t{\n");
        sb.Append("\t\tLuaScriptMgr.CheckArgsCount(L, 0);\n");
        sb.AppendFormat("\t\t{0} obj = new {0}();\n", className);
        string str = GetPushFunction(type);
        sb.AppendFormat("\t\tLuaScriptMgr.{0}(L, obj);\n", str);
        sb.Append("\t\treturn 1;\n");
        sb.Append("\t}\n");
    }

    static string GetCountStr(int count)
    {
        if (count != 0)
        {
            return string.Format("count - {0}", count);
        }

        return "count";
    }

    static void GenGetType()
    {
        sb.Append("\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]\n");
        sb.AppendFormat("\tstatic int {0}(IntPtr L)\n", "GetClassType");
        sb.Append("\t{\n");
        sb.AppendFormat("\t\tLuaScriptMgr.Push(L, typeof({0}));\n", className);
        sb.Append("\t\treturn 1;\n");
        sb.Append("\t}\n");
    }

    static void GenConstruct()
    {        
        if (isStaticClass || typeof(MonoBehaviour).IsAssignableFrom(type))
        {
            NoConsturct();
            return;
        }

        ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.Instance | binding);

        if (constructors.Length == 0)
        {
            if (!type.IsValueType)
            {
                NoConsturct();
            }
            else
            {
                DefaultConstruct();
            }

            return;
        }

        List<ConstructorInfo> list = new List<ConstructorInfo>();        

        for (int i = 0; i < constructors.Length; i++)
        {
            //c# decimal 参数类型扔掉了
            if (HasDecimal(constructors[i].GetParameters())) continue;

            if (IsObsolete(constructors[i]))
            {
                continue;
            }

            ConstructorInfo r = constructors[i];
            int index = list.FindIndex((p) => { return CompareMethod(p, r) >= 0; });

            if (index >= 0)
            {
                if (CompareMethod(list[index], r) == 2)
                {
                    list.RemoveAt(index);
                    list.Add(r);                    
                }
            }
            else
            {
                list.Add(r);
            }
        }

        if (list.Count == 0)
        {
            if (!type.IsValueType)
            {
                NoConsturct();
            }
            else
            {
                DefaultConstruct();
            }

            return;
        }

        list.Sort(Compare);

        sb.Append("\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]\n");
        sb.AppendFormat("\tstatic int _Create{0}(IntPtr L)\n", wrapClassName);
        sb.Append("\t{\n");
        sb.Append("\t\tint count = LuaDLL.lua_gettop(L);\n");          
        sb.Append("\n");

        List<ConstructorInfo> countList = new List<ConstructorInfo>();

        for (int i = 0; i < list.Count; i++)
        {
            int index = list.FindIndex((p) => { return p != list[i] && p.GetParameters().Length == list[i].GetParameters().Length; });

            if (index >= 0 || (HasOptionalParam(list[i].GetParameters()) && list[i].GetParameters().Length > 1))
            {
                countList.Add(list[i]);
            }
        }

        if (countList.Count > 1)
        {
            sb.Append("\n");
        }

        MethodBase md = list[0];
        bool hasEmptyCon = list[0].GetParameters().Length == 0 ? true : false;                

        if (HasOptionalParam(md.GetParameters()))
        {
            ParameterInfo[] paramInfos = md.GetParameters();
            ParameterInfo param = paramInfos[paramInfos.Length - 1];
            string str = param.ParameterType.GetElementType().ToString();
            str = _C(str);

            if (paramInfos.Length > 1)
            {                
                string strParams = GenParamTypes(paramInfos, true);
                sb.AppendFormat("\t\tif (LuaScriptMgr.CheckTypes(L, 1, {0}) && LuaScriptMgr.CheckParamsType(L, typeof({1}), {2}, {3}))\n", strParams, str, paramInfos.Length, GetCountStr(paramInfos.Length - 1));
            }
            else
            {
                sb.AppendFormat("\t\tif (LuaScriptMgr.CheckParamsType(L, typeof({0}), {1}, {2}))\n", str, paramInfos.Length, GetCountStr(paramInfos.Length - 1));
            }
        }
        else
        {
            ParameterInfo[] paramInfos = md.GetParameters();

            if (list.Count == 1 || md.GetParameters().Length != list[1].GetParameters().Length)
            {
                sb.AppendFormat("\t\tif (count == {0})\n", paramInfos.Length);
            }
            else
            {                
                string strParams = GenParamTypes(paramInfos, true);
                sb.AppendFormat("\t\tif (count == {0} && LuaScriptMgr.CheckTypes(L, 1, {1}))\n", paramInfos.Length, strParams);
            }
        }

        sb.Append("\t\t{\n");
        int rc = ProcessParams(md, 3, true, list.Count > 1);
        sb.AppendFormat("\t\t\treturn {0};\n", rc);
        sb.Append("\t\t}\n");

        for (int i = 1; i < list.Count; i++)
        {
            hasEmptyCon = list[i].GetParameters().Length == 0 ? true : hasEmptyCon;
            md = list[i];
            ParameterInfo[] paramInfos = md.GetParameters();

            if (!HasOptionalParam(md.GetParameters()))
            {
                if (countList.Contains(list[i]))
                {
                    //sb.AppendFormat("\t\telse if (count == {0} && LuaScriptMgr.CheckTypes(L, types{1}, {2}))\n", paramInfos.Length, i, 1);
                    string strParams = GenParamTypes(paramInfos, true);
                    sb.AppendFormat("\t\telse if (count == {0} && LuaScriptMgr.CheckTypes(L, 1, {1}))\n", paramInfos.Length, strParams);
                }
                else
                {
                    sb.AppendFormat("\t\telse if (count == {0})\n", paramInfos.Length);
                }
            }
            else
            {                
                ParameterInfo param = paramInfos[paramInfos.Length - 1];
                string str = param.ParameterType.GetElementType().ToString();
                str = _C(str);

                if (paramInfos.Length > 1)
                {                    
                    //sb.AppendFormat("\t\telse if (LuaScriptMgr.CheckTypes(L, types{2}, 1) && LuaScriptMgr.CheckParamsType(L, typeof({0}), {1}, {3}))\n", str, paramInfos.Length, i, GetCountStr(paramInfos.Length - 1));
                    string strParams = GenParamTypes(paramInfos, true);
                    sb.AppendFormat("\t\telse if (LuaScriptMgr.CheckTypes(L, 1, {0}) && LuaScriptMgr.CheckParamsType(L, typeof({1}), {2}, {3}))\n", strParams, str, paramInfos.Length, GetCountStr(paramInfos.Length - 1));
                }
                else
                {
                    sb.AppendFormat("\t\telse if (LuaScriptMgr.CheckParamsType(L, typeof({0}), {1}, {2}))\n", str, paramInfos.Length, GetCountStr(paramInfos.Length - 1));
                }
            }

            sb.Append("\t\t{\n");            
            rc = ProcessParams(md, 3, true, true);
            sb.AppendFormat("\t\t\treturn {0};\n", rc);
            sb.Append("\t\t}\n");
        }

        if (type.IsValueType && !hasEmptyCon)
        {
            sb.Append("\t\telse if (count == 0)\n");
            sb.Append("\t\t{\n");
            sb.AppendFormat("\t\t\t{0} obj = new {0}();\n", className);            
            string str = GetPushFunction(type);
            sb.AppendFormat("\t\t\tLuaScriptMgr.{0}(L, obj);\n", str);
            sb.Append("\t\t\treturn 1;\n");
            sb.Append("\t\t}\n");
        }

        sb.Append("\t\telse\n");
        sb.Append("\t\t{\n");
        sb.AppendFormat("\t\t\tLuaDLL.luaL_error(L, \"invalid arguments to method: {0}.New\");\n", className);
        sb.Append("\t\t}\n");

        sb.Append("\n");        
        sb.Append("\t\treturn 0;\n");
        sb.Append("\t}\n");
    }

    static int GetOptionalParamPos(ParameterInfo[] infos)
    {
        for (int i = 0; i < infos.Length; i++)
        {
            if (IsParams(infos[i]))
            {
                return i;
            }
        }

        return -1;
    }

    static int Compare(MethodBase lhs, MethodBase rhs)
    {
        int off1 = lhs.IsStatic ? 0 : 1;
        int off2 = rhs.IsStatic ? 0 : 1;

        ParameterInfo[] lp = lhs.GetParameters();
        ParameterInfo[] rp = rhs.GetParameters();

        int pos1 = GetOptionalParamPos(lp);
        int pos2 = GetOptionalParamPos(rp);

        if (pos1 >= 0 && pos2 < 0)
        {
            return 1;
        }
        else if (pos1 < 0 && pos2 >= 0)
        {
            return -1;
        }
        else if(pos1 >= 0 && pos2 >= 0)
        {
            pos1 += off1;
            pos2 += off2;

            if (pos1 != pos2)
            {
                return pos1 > pos2 ? -1 : 1;
            }
            else
            {
                pos1 -= off1;
                pos2 -= off2;

                if (lp[pos1].ParameterType.GetElementType() == typeof(object) && rp[pos2].ParameterType.GetElementType() != typeof(object))
                {
                    return 1;
                }
                else if (lp[pos1].ParameterType.GetElementType() != typeof(object) && rp[pos2].ParameterType.GetElementType() == typeof(object))
                {
                    return -1;
                }
            }
        }

        int c1 = off1 + lp.Length;
        int c2 = off2 + rp.Length;

        if (c1 > c2)
        {
            return 1;
        }
        else if (c1 == c2)
        {
            List<ParameterInfo> list1 = new List<ParameterInfo>(lp);
            List<ParameterInfo> list2 = new List<ParameterInfo>(rp);

            if (list1.Count > list2.Count)
            {
                if (list1[0].ParameterType == typeof(object))
                {
                    return 1;
                }

                list1.RemoveAt(0);
            }
            else if (list2.Count > list1.Count)
            {
                if (list2[0].ParameterType == typeof(object))
                {
                    return -1;
                }

                list2.RemoveAt(0);
            }

            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].ParameterType == typeof(object) && list2[i].ParameterType != typeof(object))
                {
                    return 1;
                }
                else if (list1[i].ParameterType != typeof(object) && list2[i].ParameterType == typeof(object))
                {
                    return -1;
                }
            }

            return 0;
        }
        else
        {
            return -1;
        }
    }

    static bool HasOptionalParam(ParameterInfo[] infos)
    {        
        for (int i = 0; i < infos.Length; i++)
        {
            if (IsParams(infos[i]))
            {
                return true;
            }
        }

        return false;
    }

    static Type GetRefBaseType(string str)
    {
        int index = str.IndexOf("&");
        string ss = index >= 0 ? str.Remove(index): str;
        Type t = Type.GetType(ss);

        if (t == null)
        {
            t = Type.GetType(ss + ", UnityEngine");
        }

        return t;
    }



    static int ProcessParams(MethodBase md, int tab, bool beConstruct, bool beOverride)
    {
        ParameterInfo[] paramInfos = md.GetParameters();
        int count = paramInfos.Length;
        string head = string.Empty;        

        for (int i = 0; i < tab; i++)
        {
            head += "\t";
        }
        

        if (!md.IsStatic && !beConstruct)
        {            
            if (md.Name == "Equals")
            {
                if (!type.IsValueType)
                {
                    sb.AppendFormat("{0}{1} obj = LuaScriptMgr.GetVarObject(L, 1) as {1};\n", head, className);
                }
                else
                {
                    sb.AppendFormat("{0}{1} obj = ({1})LuaScriptMgr.GetVarObject(L, 1);\n", head, className);
                }
            }
            else if (className != "Type")
            {
                if (typeof(UnityEngine.Object).IsAssignableFrom(type))
                {
                    sb.AppendFormat("{0}{1} obj = LuaScriptMgr.GetUnityObject<{1}>(L, 1);\n", head, className);
                }
                else if(typeof(UnityEngine.TrackedReference).IsAssignableFrom(type))
                {
                    sb.AppendFormat("{0}{1} obj = LuaScriptMgr.GetTrackedObject<{1}>(L, 1);\n", head, className);
                }
                else
                {
                    sb.AppendFormat("{0}{1} obj = LuaScriptMgr.GetNetObject<{1}>(L, 1);\n", head, className);
                }
            }
            else
            {
                sb.AppendFormat("{0}{1} obj = LuaScriptMgr.GetTypeObject(L, 1);\n", head, className);
            }
        }

        for (int j = 0; j < count; j++)
        {
            ParameterInfo param = paramInfos[j];
            string str = param.ParameterType.ToString();
            str = _C(str);

            string arg = "arg" + j;
            int offset = (md.IsStatic || beConstruct) ? 1 : 2;

            if (param.Attributes == ParameterAttributes.Out)
            {                
                Type outType = GetRefBaseType(param.ParameterType.ToString());

                if (outType.IsValueType)
                {
                    sb.AppendFormat("{0}{1} {2};\n", head, str, arg);
                }
                else
                {
                    sb.AppendFormat("{0}{1} {2} = null;\n", head, str, arg);
                }
            }
            else if (param.ParameterType == typeof(bool))
            {
                sb.AppendFormat("{2}bool {0} = LuaScriptMgr.GetBoolean(L, {1});\n", arg, j + offset, head);
            }
            else if (param.ParameterType == typeof(string))
            {
                string getStr = beOverride ? "GetString" : "GetLuaString";
                sb.AppendFormat("{2}string {0} = LuaScriptMgr.{3}(L, {1});\n", arg, j + offset, head, getStr);
            }
            else if (param.ParameterType.IsPrimitive)
            {
                sb.AppendFormat("{3}{0} {1} = ({0})LuaScriptMgr.GetNumber(L, {2});\n", str, arg, j + offset, head);
            }
            else if (param.ParameterType == typeof(LuaFunction))
            {
                sb.AppendFormat("{2}LuaFunction {0} = LuaScriptMgr.GetLuaFunction(L, {1});\n", arg, j + offset, head);
            }
            else if (param.ParameterType == typeof(LuaTable))
            {
                sb.AppendFormat("{2}LuaTable {0} = LuaScriptMgr.GetLuaTable(L, {1});\n", arg, j + offset, head);
            }
            else if (param.ParameterType == typeof(Vector2) || GetRefBaseType(param.ParameterType.ToString()) == typeof(Vector2))
            {
                sb.AppendFormat("{2}Vector2 {0} = LuaScriptMgr.GetVector2(L, {1});\n", arg, j + offset, head);
            }
            else if (param.ParameterType == typeof(Vector3) || GetRefBaseType(param.ParameterType.ToString()) == typeof(Vector3))
            {
                sb.AppendFormat("{2}Vector3 {0} = LuaScriptMgr.GetVector3(L, {1});\n", arg, j + offset, head);
            }
            else if (param.ParameterType == typeof(Vector4) || GetRefBaseType(param.ParameterType.ToString()) == typeof(Vector4))
            {
                sb.AppendFormat("{2}Vector4 {0} = LuaScriptMgr.GetVector4(L, {1});\n", arg, j + offset, head);
            }
            else if (param.ParameterType == typeof(Quaternion) || GetRefBaseType(param.ParameterType.ToString()) == typeof(Quaternion))
            {
                sb.AppendFormat("{2}Quaternion {0} = LuaScriptMgr.GetQuaternion(L, {1});\n", arg, j + offset, head);
            }
            else if (param.ParameterType == typeof(Color) || GetRefBaseType(param.ParameterType.ToString()) == typeof(Color))
            {
                sb.AppendFormat("{2}Color {0} = LuaScriptMgr.GetColor(L, {1});\n", arg, j + offset, head);
            }
            else if (param.ParameterType == typeof(Ray) || GetRefBaseType(param.ParameterType.ToString()) == typeof(Ray))
            {
                sb.AppendFormat("{2}Ray {0} = LuaScriptMgr.GetRay(L, {1});\n", arg, j + offset, head);
            }
            else if (param.ParameterType == typeof(object))
            {
                sb.AppendFormat("{2}object {0} = LuaScriptMgr.GetVarObject(L, {1});\n", arg, j + offset, head);
            }
            else if (param.ParameterType == typeof(Type))
            {
                sb.AppendFormat("{0}{1} {2} = LuaScriptMgr.GetTypeObject(L, {3});\n", head, str, arg, j + offset);
            }
            else if (param.ParameterType == typeof(LuaStringBuffer))
            {
                sb.AppendFormat("{2}LuaStringBuffer {0} = LuaScriptMgr.GetStringBuffer(L, {1});\n", arg, j + offset, head);
            }
            else if (param.ParameterType.IsArray)
            {
                Type et = param.ParameterType.GetElementType();
                string atstr = _C(et.ToString());
                string fname = "GetArrayObject";
                bool flag = false;
                bool optional = false;
                bool isObject = false;

                if (et == typeof(bool))
                {
                    fname = "GetArrayBool";
                }
                else if (et.IsPrimitive)
                {
                    flag = true;
                    fname = "GetArrayNumber";
                }
                else if (et == typeof(string))
                {
                    optional = IsParams(param);
                    fname = optional ? "GetParamsString" : "GetArrayString";
                }
                else //if (et == typeof(object))
                {
                    flag = true;
                    optional = IsParams(param);
                    fname = optional ? "GetParamsObject" : "GetArrayObject";

                    if (et == typeof(object))
                    {
                        ambig |= ObjAmbig.NetObj;
                        isObject = true;
                    }

                    if (et == typeof(UnityEngine.Object))
                    {
                        ambig |= ObjAmbig.U3dObj;
                    }
                }

                if (flag)
                {
                    if (optional)
                    {
                        if (!isObject)
                        {
                            sb.AppendFormat("{5}{0}[] objs{2} = LuaScriptMgr.{4}<{0}>(L, {1}, {3});\n", atstr, j + offset, j, GetCountStr(j + offset - 1), fname, head);
                        }
                        else
                        {
                            sb.AppendFormat("{4}object[] objs{1} = LuaScriptMgr.{3}(L, {0}, {2});\n", j + offset, j, GetCountStr(j + offset - 1), fname, head);
                        }
                    }
                    else
                    {
                        sb.AppendFormat("{4}{0}[] objs{2} = LuaScriptMgr.{3}<{0}>(L, {1});\n", atstr, j + offset, j, fname, head);
                    }
                }
                else
                {
                    if (optional)
                    {
                        sb.AppendFormat("{5}{0}[] objs{2} = LuaScriptMgr.{4}(L, {1}, {3});\n", atstr, j + offset, j, GetCountStr(j + offset - 1), fname, head);
                    }
                    else
                    {
                        sb.AppendFormat("{5}{0}[] objs{2} = LuaScriptMgr.{4}(L, {1});\n", atstr, j + offset, j, j + offset - 1, fname, head);
                    }
                }
            }
            else //if (param.ParameterType == typeof(object))
            {
                if (md.Name == "op_Equality")
                {
                    if (!type.IsValueType)
                    {
                        sb.AppendFormat("{3}{0} {1} = LuaScriptMgr.GetVarObject(L, {2}) as {0};\n", str, arg, j + offset, head);
                    }
                    else
                    {
                        sb.AppendFormat("{3}{0} {1} = ({0})LuaScriptMgr.GetVarObject(L, {2});\n", str, arg, j + offset, head);
                    }
                }
                else
                {
                    if (typeof(UnityEngine.Object).IsAssignableFrom(param.ParameterType))
                    {                        
                        sb.AppendFormat("{3}{0} {1} = LuaScriptMgr.GetUnityObject<{0}>(L, {2});\n", str, arg, j + offset, head);
                    }
                    else if (typeof(UnityEngine.TrackedReference).IsAssignableFrom(param.ParameterType))
                    {
                        sb.AppendFormat("{3}{0} {1} = LuaScriptMgr.GetTrackedObject<{0}>(L, {2});\n", str, arg, j + offset, head);
                    }
                    else
                    {
                        sb.AppendFormat("{3}{0} {1} = LuaScriptMgr.GetNetObject<{0}>(L, {2});\n", str, arg, j + offset, head);
                    }
                }
            }
        }

        StringBuilder sbArgs = new StringBuilder();
        List<string> refList = new List<string>();
        List<Type> refTypes = new List<Type>();

        for (int j = 0; j < count - 1; j++)
        {
            ParameterInfo param = paramInfos[j];

            if (!param.ParameterType.IsArray)
            {
                if (!param.ParameterType.ToString().Contains("&"))
                {
                    sbArgs.Append("arg");
                }
                else
                {
                    if (param.Attributes == ParameterAttributes.Out)
                    {
                        sbArgs.Append("out arg");
                    }
                    else
                    {
                        sbArgs.Append("ref arg");
                    }

                    refList.Add("arg" + j);
                    refTypes.Add(GetRefBaseType(param.ParameterType.ToString()));
                }
            }
            else
            {
                sbArgs.Append("objs");
            }

            sbArgs.Append(j);
            sbArgs.Append(",");
        }

        if (count > 0)
        {
            ParameterInfo param = paramInfos[count - 1];

            if (!param.ParameterType.IsArray)
            {
                if (!param.ParameterType.ToString().Contains("&"))
                {
                    sbArgs.Append("arg");
                }
                else
                {
                    if (param.Attributes == ParameterAttributes.Out)
                    {
                        sbArgs.Append("out arg");
                    }
                    else
                    {
                        sbArgs.Append("ref arg");
                    }

                    refList.Add("arg" + (count - 1));
                    refTypes.Add(GetRefBaseType(param.ParameterType.ToString()));
                }
            }
            else
            {
                sbArgs.Append("objs");
            }

            sbArgs.Append(count - 1);
        }

        if (beConstruct)
        {
            sb.AppendFormat("{2}{0} obj = new {0}({1});\n", className, sbArgs.ToString(), head);
            string str = GetPushFunction(type);
            sb.AppendFormat("{0}LuaScriptMgr.{1}(L, obj);\n", head, str);

            for (int i = 0; i < refList.Count; i++)
            {
                str = GetPushFunction(refTypes[i]);
                sb.AppendFormat("{1}LuaScriptMgr.{2}(L, {0});\n", refList[i], head, str);
            }
            
            return refList.Count + 1;
        }

        string obj = md.IsStatic ? className : "obj";
        MethodInfo m = md as MethodInfo;

        if (m.ReturnType == typeof(void))
        {
            if (md.Name == "set_Item")
            {
                if (count == 2)
                {
                    sb.AppendFormat("{0}{1}[arg0] = arg1;\n", head, obj);
                }
                else if (count == 3)
                {
                    sb.AppendFormat("{0}{1}[arg0, arg1] = arg2;\n", head, obj);
                }
            }
            else
            {
                sb.AppendFormat("{3}{0}.{1}({2});\n", obj, md.Name, sbArgs.ToString(), head);
            }

            if (!md.IsStatic && type.IsValueType)
            {
                sb.AppendFormat("{0}LuaScriptMgr.SetValueObject(L, 1, obj);\n", head);
            }
        }
        else
        {
            string ret = "object";

            if (m.ReturnType.IsArray)
            {
                Type et = m.ReturnType.GetElementType();
                ret = _C(et.ToString());                
                ret += "[]";
            }
            else
            {
                ret = _C(m.ReturnType.ToString());
            }

            if (md.Name.Contains("op_"))
            {
                CallOpFunction(md.Name, tab, ret);
            }
            else if (md.Name == "get_Item")
            {
                sb.AppendFormat("{4}{3} o = {0}[{2}];\n", obj, md.Name, sbArgs.ToString(), ret, head); 
            }
            else
            {
                sb.AppendFormat("{4}{3} o = {0}.{1}({2});\n", obj, md.Name, sbArgs.ToString(), ret, head);
            }

            string str = GetPushFunction(m.ReturnType);
            sb.AppendFormat("{0}LuaScriptMgr.{1}(L, o);\n", head, str);            
        }

        for (int i = 0; i < refList.Count; i++)
        {
            string str = GetPushFunction(refTypes[i]);
            sb.AppendFormat("{1}LuaScriptMgr.{2}(L, {0});\n", refList[i], head, str);            
        }

        return refList.Count;
    }

    static bool CompareParmsCount(MethodBase l, MethodBase r)
    {
        if (l == r)
        {
            return false;
        }

        int c1 = l.IsStatic ? 0 : 1;
        int c2 = r.IsStatic ? 0 : 1;

        c1 += l.GetParameters().Length;
        c2 += r.GetParameters().Length;

        return c1 == c2;
    }

    //decimal 类型扔掉了
    static Dictionary<Type, int> typeSize = new Dictionary<Type, int>()
    {
        {typeof(bool), 1},
        { typeof(char), 2},
        { typeof(byte), 3 },
        { typeof(sbyte), 4 },
        { typeof(ushort),5 },      
        { typeof(short), 6 },        
        { typeof(uint), 7 },
        {typeof(int), 8},        
        {typeof(float), 9},
        { typeof(ulong), 10},
        { typeof(long), 11},                                                  
        { typeof(double), 12 },

    };

    //-1 不存在替换, 1 保留左面， 2 保留右面
    static int CompareMethod(MethodBase l, MethodBase r)
    {
        int s = 0;

        if (!CompareParmsCount(l,r))
        {
            return -1;
        }
        else
        {
            ParameterInfo[] lp = l.GetParameters();
            ParameterInfo[] rp = r.GetParameters();

            List<Type> ll = new List<Type>();
            List<Type> lr = new List<Type>();

            if (!l.IsStatic)
            {
                ll.Add(type);
            }

            if (!r.IsStatic)
            {
                lr.Add(type);
            }

            for (int i = 0; i < lp.Length; i++)
            {
                ll.Add(lp[i].ParameterType);
            }

            for (int i = 0; i < rp.Length; i++)
            {
                lr.Add(rp[i].ParameterType);
            }

            for (int i = 0; i < ll.Count; i++)
            {
                if (!typeSize.ContainsKey(ll[i]) || !typeSize.ContainsKey(lr[i]))
                {
                    if (ll[i] == lr[i])
                    {
                        continue;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else if (ll[i].IsPrimitive && lr[i].IsPrimitive && s == 0)
                {
                    s = typeSize[ll[i]] >= typeSize[lr[i]] ? 1 : 2;
                }
                else if (ll[i] != lr[i])
                {
                    return -1;
                }
            }

            if (s == 0 && l.IsStatic)
            {
                s = 2;
            }
        }

        return s;
    }

    static void Push(List<MethodInfo> list, MethodInfo r)
    {
        int index = list.FindIndex((p) => { return p.Name == r.Name && CompareMethod(p, r) >= 0; });

        if (index >= 0)
        {
            if (CompareMethod(list[index], r) == 2)
            {
                list.RemoveAt(index);        
                list.Add(r);
                return;
            }
            else
            {
                return;
            }
        }

        list.Add(r);        
    }

    static bool HasDecimal(ParameterInfo[] pi)
    {
        for (int i = 0; i < pi.Length; i++)
        {
            if (pi[i].ParameterType == typeof(decimal))
            {
                return true;
            }
        }

        return false;
    }

    public static MethodInfo GenOverrideFunc(string name)
    {
        List<MethodInfo> list = new List<MethodInfo>();        

        for (int i = 0; i < methods.Length; i++)
        {
            if (methods[i].Name == name && !methods[i].IsGenericMethod && !HasDecimal(methods[i].GetParameters()))
            {
                Push(list, methods[i]);
            }
        }

        if (list.Count == 1)
        {
            return list[0];
        }

        list.Sort(Compare);

        sb.Append("\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]\n");
        sb.AppendFormat("\tstatic int {0}(IntPtr L)\n", GetFuncName(name));
        sb.Append("\t{\n");
        sb.Append("\t\tint count = LuaDLL.lua_gettop(L);\n");        

        List<MethodInfo> countList = new List<MethodInfo>();

        for (int i = 0; i < list.Count; i++)
        {
            int index = list.FindIndex((p) => { return CompareParmsCount(p, list[i]); });

            if (index >= 0 || (HasOptionalParam(list[i].GetParameters()) && list[i].GetParameters().Length > 1))
            {
                countList.Add(list[i]);                
            }            
        }

        if (countList.Count >= 1)
        {
            sb.Append("\n");
        }

        MethodInfo md = list[0];
        int ret = md.ReturnType == typeof(void) ? 0 : 1;
        int offset = md.IsStatic ? 0 : 1;
        int c1 = md.GetParameters().Length + offset;
        int c2 = list[1].GetParameters().Length + (list[1].IsStatic ? 0 : 1);
        bool noLuaString = true;

        if (HasOptionalParam(md.GetParameters()))
        {
            ParameterInfo[] paramInfos = md.GetParameters();
            ParameterInfo param = paramInfos[paramInfos.Length - 1];
            string str = param.ParameterType.GetElementType().ToString();
            str = _C(str);

            if (paramInfos.Length > 1)
            {
                //sb.AppendFormat("\t\tif (LuaScriptMgr.CheckTypes(L, types0, 1) && LuaScriptMgr.CheckParamsType(L, typeof({0}), {1}, {2}))\n", str, paramInfos.Length + offset, GetCountStr(paramInfos.Length + offset - 1));
                string strParams = GenParamTypes(paramInfos, md.IsStatic);
                sb.AppendFormat("\t\tif (LuaScriptMgr.CheckTypes(L, 1, {0}) && LuaScriptMgr.CheckParamsType(L, typeof({1}), {2}, {3}))\n", strParams, str, paramInfos.Length + offset, GetCountStr(paramInfos.Length + offset - 1));
            }
            else
            {
                sb.AppendFormat("\t\tif (LuaScriptMgr.CheckParamsType(L, typeof({0}), {1}, {2}))\n", str, paramInfos.Length + offset, GetCountStr(paramInfos.Length + offset - 1));
            }
        }
        else
        {
            if (c1 != c2)
            {
                sb.AppendFormat("\t\tif (count == {0})\n", md.GetParameters().Length + offset);
                noLuaString = false;
            }
            else
            {                
                //sb.AppendFormat("\t\tif (count == {0} && LuaScriptMgr.CheckTypes(L, types0, {1}))\n", md.GetParameters().Length + offset, 1);
                ParameterInfo[] paramInfos = md.GetParameters();
                string strParams = GenParamTypes(paramInfos, md.IsStatic);
                sb.AppendFormat("\t\tif (count == {0} && LuaScriptMgr.CheckTypes(L, 1, {1}))\n", paramInfos.Length + offset, strParams);
            }
        }

        sb.Append("\t\t{\n");
        int count = ProcessParams(md, 3, false, (list.Count > 1) && noLuaString);
        sb.AppendFormat("\t\t\treturn {0};\n", ret + count);
        sb.Append("\t\t}\n");
        //int offset = md.IsStatic ? 1 : 2;

        for (int i = 1; i < list.Count; i++)
        {
            noLuaString = true;
            md = list[i];
            offset = md.IsStatic ? 0 : 1;
            ret = md.ReturnType == typeof(void) ? 0 : 1;

            if (!HasOptionalParam(md.GetParameters()))
            {
                ParameterInfo[] paramInfos = md.GetParameters();

                if (countList.Contains(list[i]))
                {
                    //sb.AppendFormat("\t\telse if (count == {0} && LuaScriptMgr.CheckTypes(L, types{1}, {2}))\n", md.GetParameters().Length + offset, i, 1);                    
                    string strParams = GenParamTypes(paramInfos, md.IsStatic);
                    sb.AppendFormat("\t\telse if (count == {0} && LuaScriptMgr.CheckTypes(L, 1, {1}))\n", paramInfos.Length + offset, strParams);
                }
                else
                {
                    sb.AppendFormat("\t\telse if (count == {0})\n", paramInfos.Length + offset);
                    noLuaString = false;
                }
            }
            else
            {
                ParameterInfo[] paramInfos = md.GetParameters();
                ParameterInfo param = paramInfos[paramInfos.Length - 1];
                string str = param.ParameterType.GetElementType().ToString();
                str = _C(str);

                if (paramInfos.Length > 1)
                {
                    //sb.AppendFormat("\t\telse if (LuaScriptMgr.CheckTypes(L, types{2}, 1) && LuaScriptMgr.CheckParamsType(L, typeof({0}), {1}, {3}))\n", str, paramInfos.Length + offset, i, GetCountStr(paramInfos.Length + offset - 1));                    
                    string strParams = GenParamTypes(paramInfos, md.IsStatic);
                    sb.AppendFormat("\t\telse if (LuaScriptMgr.CheckTypes(L, 1, {0}) && LuaScriptMgr.CheckParamsType(L, typeof({1}), {2}, {3}))\n", strParams, str, paramInfos.Length + offset, GetCountStr(paramInfos.Length + offset - 1));
                }
                else
                {
                    sb.AppendFormat("\t\telse if (LuaScriptMgr.CheckParamsType(L, typeof({0}), {1}, {2}))\n", str, paramInfos.Length + offset, GetCountStr(paramInfos.Length + offset - 1));
                }
            }

            sb.Append("\t\t{\n");
            count = ProcessParams(md, 3, false, noLuaString);
            sb.AppendFormat("\t\t\treturn {0};\n", ret + count);
            sb.Append("\t\t}\n");
        }

        sb.Append("\t\telse\n");
        sb.Append("\t\t{\n");
        sb.AppendFormat("\t\t\tLuaDLL.luaL_error(L, \"invalid arguments to method: {0}.{1}\");\n", className, name);
        sb.Append("\t\t}\n");        

        sb.Append("\n");
        sb.Append("\t\treturn 0;\n");
        sb.Append("\t}\n");

        return null;
    }

    //转模版内参数
    public static string _TC(string str)
    {
        if (str.Contains("[]"))
        {
            str = str.Substring(0, str.Length - 2);
            str = _C(str);
            str += "[]";
            return str;
        }

        return _C(str);
    }

    static string _C(string str)
    {
        if (str.Length > 1 && str[str.Length - 1] == '&')
        {
            str = str.Remove(str.Length - 1);
            return _C(str);
        }
        else if (str == "System.Single" || str == "Single")
        {            
            return "float";
        }
        else if (str == "System.String" || str == "String")
        {
            return "string";
        }
        else if (str == "System.Int32" || str == "Int32")
        {
            return "int";
        }
        else if (str == "System.Int64" || str == "Int64")
        {
            return "long";
        }
        else if (str == "System.SByte" || str == "SByte")
        {
            return "sbyte";
        }
        else if (str == "System.Byte" || str == "Byte")
        {
            return "byte";
        }
        else if (str == "System.Int16" || str == "Int16")
        {
            return "short";
        }
        else if (str == "System.UInt16" || str == "UInt16")
        {
            return "ushort";
        }
        else if (str == "System.Char" || str == "Char")
        {
            return "char";
        }
        else if (str == "System.UInt32" || str == "UInt32")
        {
            return "uint";
        }
        else if (str == "System.UInt64" || str == "UInt64")
        {
            return "ulong";
        }
        else if (str == "System.Decimal" || str == "Decimal")
        {
            return "decimal";
        }
        else if (str == "System.Double" || str == "Double")
        {
            return "double";
        }
        else if (str == "System.Boolean" || str == "Boolean")
        {
            return "bool";
        }
        else if (str == "System.Object")
        {
            usingList.Add("System");
            ambig |= ObjAmbig.NetObj;
            return "object";
        }
        else if (str.Contains("`"))
        {
            string regStr = @"^(?<s0>.*?)\.?(?<s1>\w*)`[1-9]\[(?<s2>.*?)\]$";            
            Regex r = new Regex(regStr, RegexOptions.None);
            Match mc = r.Match(str);
            bool beMember = false;

            if (!mc.Success)
            {
                regStr = @"^(?<s0>.*?)\.?(?<s1>\w*)`[1-9]\+(?<s3>.*?)\[(?<s2>.*?)\]$";
                r = new Regex(regStr, RegexOptions.None);
                mc = r.Match(str);
                beMember = true;
            }

            string s0 = mc.Groups["s0"].Value;            
            string s1 = mc.Groups["s1"].Value;
            string s2 = mc.Groups["s2"].Value;
            string[] ss = s2.Split(new char[] {','}, System.StringSplitOptions.RemoveEmptyEntries);
            s2 = string.Empty;

            for (int i = 0; i < ss.Length; i++)
            {
                ss[i] = _TC(ss[i]);
            }

            for (int i = 0; i < ss.Length - 1; i++)
            {
                s2 += ss[i];
                s2 += ",";
            }

            s2 += ss[ss.Length - 1];

            if (s0 != string.Empty)
            {
                usingList.Add(s0);                              
            }
                       
            string s4 = string.Format("{0}<{1}>", s1, s2);

            if (beMember)
            {
                s4 += ".";
                s4 += mc.Groups["s3"].Value;
            }

            return s4;
        }
        else if (str.Contains("+"))
        {
            int pos1 = str.LastIndexOf('.');

            if (pos1 > 0)
            {
                usingList.Add(str.Substring(0, pos1));
            }

            return str.Replace('+', '.');
        }   
        else if (str.Length > 12 && str.Substring(0, 12) == "UnityEngine.")
        {
            //usingList.Add("UnityEngine");
            //str = str.Remove(0, 12);

            if (str.Contains("Object"))
            {
                ambig |= ObjAmbig.U3dObj;
            }

            int pos = str.LastIndexOf('.');

            if (pos > 0)
            {
                usingList.Add(str.Substring(0, pos));
                str = str.Substring(pos + 1);
            }

            return _C(str);
        }
        else if (str.Length > 7 && str.Substring(0, 7) == "System.")
        {
            int pos1 = str.LastIndexOf('.');
            usingList.Add(str.Substring(0, pos1));
            str = str.Remove(0, pos1+1);

            if (str.Contains("Object"))
            {
                str.Replace("Object", "object");
            }

            return _C(str);
        }

        return str;
    }

    //转checktypes数组参数
    static string _PT(Type t)
    {
        if (t.IsArray)
        {
            t = t.GetElementType();
            string str = _C(t.ToString());
            str += "[]";
            return str;
        }

        return _C(t.ToString());
    }

    static void CreateParamTypes(int pos, ParameterInfo[] p, bool beAddSelf)
    {
        List<Type> a = new List<Type>();

        if (!beAddSelf)
        {
            a.Add(type);
        }

        for (int i = 0; i < p.Length; i++)
        {
            if (!IsParams(p[i]))
            {
                a.Add(p[i].ParameterType);
            }
        }

        if (a.Count == 0)
        {
            return;
        }
        else if (a.Count == 1)
        {
            sb.AppendFormat("\t\tType[] types{0} = {{typeof({1})}};\n", pos, _PT(a[0]));
        }
        else if (a.Count == 2)
        {
            sb.AppendFormat("\t\tType[] types{0} = {{typeof({1}), typeof({2})}};\n", pos, _PT(a[0]), _PT(a[1]));
        }
        else if (a.Count == 3)
        {
            sb.AppendFormat("\t\tType[] types{0} = {{typeof({1}), typeof({2}), typeof({3})}};\n", pos, _PT(a[0]), _PT(a[1]), _PT(a[2]));
        }
        else if (a.Count == 4)
        {
            sb.AppendFormat("\t\tType[] types{0} = {{typeof({1}), typeof({2}), typeof({3}), typeof({4})}};\n", pos, _PT(a[0]), _PT(a[1]), _PT(a[2]), _PT(a[3]));
        }
        else if (a.Count == 5)
        {
            sb.AppendFormat("\t\tType[] types{0} = {{typeof({1}), typeof({2}), typeof({3}), typeof({4}), typeof({5})}};\n", pos, _PT(a[0]), _PT(a[1]), _PT(a[2]), _PT(a[3]), _PT(a[4]));
        }
        else if (a.Count == 6)
        {
            sb.AppendFormat("\t\tType[] types{0} = {{typeof({1}), typeof({2}), typeof({3}), typeof({4}), typeof({5}), typeof({6})}};\n", pos, _PT(a[0]), _PT(a[1]), _PT(a[2]), _PT(a[3]), _PT(a[4]), _PT(a[5]));
        }
        else if (a.Count == 7)
        {
            sb.AppendFormat("\t\tType[] types{0} = {{typeof({1}), typeof({2}), typeof({3}), typeof({4}), typeof({5}), typeof({6}), typeof({7})}};\n", pos, _PT(a[0]), _PT(a[1]), _PT(a[2]), _PT(a[3]), _PT(a[4]), _PT(a[5]), _PT(a[6]));
        }
        else if (a.Count == 8)
        {
            sb.AppendFormat("\t\tType[] types{0} = {{typeof({1}), typeof({2}), typeof({3}), typeof({4}), typeof({5}), typeof({6}), typeof({7}), typeof({8})}};\n", pos, _PT(a[0]), _PT(a[1]), _PT(a[2]), _PT(a[3]), _PT(a[4]), _PT(a[5]), _PT(a[6]), _PT(a[7]));
        }
        else if (a.Count == 9)
        {
            sb.AppendFormat("\t\tType[] types{0} = {{typeof({1}), typeof({2}), typeof({3}), typeof({4}), typeof({5}), typeof({6}), typeof({7}), typeof({8}), typeof({9})}};\n", pos, _PT(a[0]), _PT(a[1]), _PT(a[2]), _PT(a[3]), _PT(a[4]), _PT(a[5]), _PT(a[6]), _PT(a[7]), _PT(a[8]));
        }
        else
        {
            Debugger.LogError("CreateParamTypes i am not defined it: " + a.Count);
        }
    }

    static bool IsLuaTableType(Type t)
    {
        if (t.IsArray)
        {
            t = t.GetElementType();
        }

        return t == typeof(Vector3) || t == typeof(Vector2) || t == typeof(Vector4) || t == typeof(Quaternion) || t == typeof(Color) || t == typeof(Ray);
    }

    static string GetTypeOf(Type t, string sep)
    {
        string str;

        if (t == null)
        {
            str = string.Format("null{0}", sep);
        }
        else if (IsLuaTableType(t))
        {
            str = string.Format("typeof(LuaTable{1}){0}", sep, t.IsArray ? "[]" : "");
        }
        else
        {
           str = string.Format("typeof({0}){1}", _PT(t), sep);
        }

        return str;
    }

    static string GenParamTypes(ParameterInfo[] p, bool notAddSelf)
    {
        StringBuilder sb = new StringBuilder();
        List<Type> list = new List<Type>();

        if (!notAddSelf)
        {
            list.Add(type);
        }

        for (int i = 0; i < p.Length; i++)
        {
            if (IsParams(p[i]))
            {
                continue;                
            }

            if (p[i].Attributes != ParameterAttributes.Out)
            {
                list.Add(p[i].ParameterType);
            }
            else
            {
                list.Add(null);
            }
        }

        for (int i = 0; i < list.Count - 1; i++)
        {
            sb.Append(GetTypeOf(list[i], ", "));
        }

        sb.Append(GetTypeOf(list[list.Count - 1], ""));
        return sb.ToString();
    }

    static void CheckObjectNull()
    {
        if (type.IsValueType)
        {
            sb.Append("\t\tif (o == null)\n");
        }
        else
        {
            sb.Append("\t\tif (obj == null)\n");
        }
    }

    static void GenIndexFunc()
    {
        for(int i = 0; i < fields.Length; i++)
        {
            sb.Append("\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]\n");
            sb.AppendFormat("\tstatic int get_{0}(IntPtr L)\n", fields[i].Name);
            sb.Append("\t{\n");

            string str = GetPushFunction(fields[i].FieldType);

            if (fields[i].IsStatic)
            {
                //if (str == "PushObject")
                //{
                //    sb.AppendFormat("\t\tLuaScriptMgr.{2}(L, {0}.{1} == null ? null : {0}.{1});\n", className, fields[i].Name, str);                    
                //}
                //else
                //{
                    sb.AppendFormat("\t\tLuaScriptMgr.{2}(L, {0}.{1});\n", className, fields[i].Name, str);
                //}
            }
            else
            {
                sb.AppendFormat("\t\tobject o = LuaScriptMgr.GetLuaObject(L, 1);\n");

                if (!type.IsValueType)
                {
                    sb.AppendFormat("\t\t{0} obj = ({0})o;\n", className);
                }

                sb.Append("\n");
                CheckObjectNull();
                sb.Append("\t\t{\n");
                sb.Append("\t\t\tLuaTypes types = LuaDLL.lua_type(L, 1);\n");
                sb.Append("\n");
                sb.Append("\t\t\tif (types == LuaTypes.LUA_TTABLE)\n");
                sb.Append("\t\t\t{\n");
                sb.AppendFormat("\t\t\t\tLuaDLL.luaL_error(L, \"unknown member name {0}\");\n", fields[i].Name);
                sb.Append("\t\t\t}\n");
                sb.Append("\t\t\telse\n");
                sb.Append("\t\t\t{\n");
                sb.AppendFormat("\t\t\t\tLuaDLL.luaL_error(L, \"attempt to index {0} on a nil value\");\n", fields[i].Name);
                sb.Append("\t\t\t}\n");
                sb.Append("\t\t}\n");
                sb.Append("\n");

                if (type.IsValueType)
                {
                    sb.AppendFormat("\t\t{0} obj = ({0})o;\n", className);
                }

                //if (str == "PushObject")
                //{
                //    sb.AppendFormat("\t\tLuaScriptMgr.{1}(L, obj.{0} == null ? null : obj.{0});\n", fields[i].Name, str);                    
                //}
                //else
                //{
                    sb.AppendFormat("\t\tLuaScriptMgr.{1}(L, obj.{0});\n", fields[i].Name, str);
                //}
            }

            sb.Append("\t\treturn 1;\n");
            sb.Append("\t}\n");
        }

        for (int i = 0; i < props.Length; i++)
        {
            if (!props[i].CanRead)
            {
                continue;
            }

            bool isStatic = true;
            int index = propList.IndexOf(props[i]);

            if (index >= 0)
            {
                isStatic = false;
            }

            sb.Append("\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]\n");
            sb.AppendFormat("\tstatic int get_{0}(IntPtr L)\n", props[i].Name);
            sb.Append("\t{\n");

            string str = GetPushFunction(props[i].PropertyType);

            if (isStatic)
            {               
                //if (str == "PushObject")
                //{
                //    sb.AppendFormat("\t\tLuaScriptMgr.{2}(L, {0}.{1} == null ? null : {0}.{1});\n", className, props[i].Name, str);                    
                //}
                //else
                //{
                    sb.AppendFormat("\t\tLuaScriptMgr.{2}(L, {0}.{1});\n", className, props[i].Name, str);
                //}
            }
            else
            {
                sb.AppendFormat("\t\tobject o = LuaScriptMgr.GetLuaObject(L, 1);\n");                
                
                if (!type.IsValueType)
                {
                    sb.AppendFormat("\t\t{0} obj = ({0})o;\n", className);
                }

                sb.Append("\n");
                CheckObjectNull();
                sb.Append("\t\t{\n");
                sb.Append("\t\t\tLuaTypes types = LuaDLL.lua_type(L, 1);\n");
                sb.Append("\n");
                sb.Append("\t\t\tif (types == LuaTypes.LUA_TTABLE)\n");
                sb.Append("\t\t\t{\n");
                sb.AppendFormat("\t\t\t\tLuaDLL.luaL_error(L, \"unknown member name {0}\");\n", props[i].Name);
                sb.Append("\t\t\t}\n");
                sb.Append("\t\t\telse\n");
                sb.Append("\t\t\t{\n");
                sb.AppendFormat("\t\t\t\tLuaDLL.luaL_error(L, \"attempt to index {0} on a nil value\");\n", props[i].Name);
                sb.Append("\t\t\t}\n");
                sb.Append("\t\t}\n");
                sb.Append("\n");

                if (type.IsValueType)
                {
                    sb.AppendFormat("\t\t{0} obj = ({0})o;\n", className);
                }

                //if (str == "PushObject")
                //{
                //    sb.AppendFormat("\t\tLuaScriptMgr.{1}(L, obj.{0} == null ? null : obj.{0});\n", props[i].Name, str);                    
                //}
                //else
                //{
                    sb.AppendFormat("\t\tLuaScriptMgr.{1}(L, obj.{0});\n", props[i].Name, str);
                //}

            }

            sb.Append("\t\treturn 1;\n");
            sb.Append("\t}\n");
        }
    }

    static void GenNewIndexFunc()
    {
        for (int i = 0; i < fields.Length; i++)
        {
            if (fields[i].IsLiteral || fields[i].IsInitOnly || fields[i].IsPrivate)
            {
                continue;
            }

            sb.Append("\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]\n");
            sb.AppendFormat("\tstatic int set_{0}(IntPtr L)\n", fields[i].Name);
            sb.Append("\t{\n");
            string o = fields[i].IsStatic ? className : "obj";                        

            if (!fields[i].IsStatic)
            {                                
                sb.AppendFormat("\t\tobject o = LuaScriptMgr.GetLuaObject(L, 1);\n");                

                if (!type.IsValueType)
                {
                    sb.AppendFormat("\t\t{0} obj = ({0})o;\n", className);
                }

                sb.Append("\n");
                CheckObjectNull();
                sb.Append("\t\t{\n");
                sb.Append("\t\t\tLuaTypes types = LuaDLL.lua_type(L, 1);\n");
                sb.Append("\n");
                sb.Append("\t\t\tif (types == LuaTypes.LUA_TTABLE)\n");
                sb.Append("\t\t\t{\n");
                sb.AppendFormat("\t\t\t\tLuaDLL.luaL_error(L, \"unknown member name {0}\");\n", fields[i].Name);
                sb.Append("\t\t\t}\n");
                sb.Append("\t\t\telse\n");
                sb.Append("\t\t\t{\n");
                sb.AppendFormat("\t\t\t\tLuaDLL.luaL_error(L, \"attempt to index {0} on a nil value\");\n", fields[i].Name);
                sb.Append("\t\t\t}\n");
                sb.Append("\t\t}\n");

                sb.Append("\n");

                if (type.IsValueType)
                {
                    sb.AppendFormat("\t\t{0} obj = ({0})o;\n", className);
                }
            }

            NewIndexSetValue(fields[i].FieldType, o, fields[i].Name);

            if (!fields[i].IsStatic && type.IsValueType)
            {
                sb.Append("\t\tLuaScriptMgr.SetValueObject(L, 1, obj);\n");
            }

            sb.Append("\t\treturn 0;\n");
            sb.Append("\t}\n");
        }

        for (int i = 0; i < props.Length; i++)
        {
            if (!props[i].CanWrite || !props[i].GetSetMethod(true).IsPublic)
            {
                continue;
            }

            bool isStatic = true;
            int index = propList.IndexOf(props[i]);

            if (index >= 0)
            {
                isStatic = false;
            }

            sb.Append("\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]\n");
            sb.AppendFormat("\tstatic int set_{0}(IntPtr L)\n", props[i].Name);
            sb.Append("\t{\n");
            string o = isStatic ? className : "obj";

            if (!isStatic)
            {
                sb.AppendFormat("\t\tobject o = LuaScriptMgr.GetLuaObject(L, 1);\n");

                if (!type.IsValueType)
                {
                    sb.AppendFormat("\t\t{0} obj = ({0})o;\n", className);
                }
    
                sb.Append("\n");
                CheckObjectNull();
                sb.Append("\t\t{\n");
                sb.Append("\t\t\tLuaTypes types = LuaDLL.lua_type(L, 1);\n");
                sb.Append("\n");
                sb.Append("\t\t\tif (types == LuaTypes.LUA_TTABLE)\n");
                sb.Append("\t\t\t{\n");
                sb.AppendFormat("\t\t\t\tLuaDLL.luaL_error(L, \"unknown member name {0}\");\n", props[i].Name);
                sb.Append("\t\t\t}\n");
                sb.Append("\t\t\telse\n");
                sb.Append("\t\t\t{\n");
                sb.AppendFormat("\t\t\t\tLuaDLL.luaL_error(L, \"attempt to index {0} on a nil value\");\n", props[i].Name);
                sb.Append("\t\t\t}\n");
                sb.Append("\t\t}\n");
                sb.Append("\n");

                if (type.IsValueType)
                {
                    sb.AppendFormat("\t\t{0} obj = ({0})o;\n", className);
                }
            }

            NewIndexSetValue(props[i].PropertyType, o, props[i].Name);

            if (!isStatic && type.IsValueType)
            {
                sb.Append("\t\tLuaScriptMgr.SetValueObject(L, 1, obj);\n");
            }

            sb.Append("\t\treturn 0;\n");
            sb.Append("\t}\n");
        }
    }    
   

    static void NewIndexSetValue(Type t, string o, string name)
    {
        if (t == typeof(bool))
        {
            sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetBoolean(L, 3);\n", o, name);
        }
        else if (t == typeof(string))
        {            
            sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetString(L, 3);\n", o, name);
        }
        else if (t.IsPrimitive)
        {            
            sb.AppendFormat("\t\t{0}.{1} = ({2})LuaScriptMgr.GetNumber(L, 3);\n", o, name, _C(t.ToString()));
        }
        else if (t == typeof(LuaFunction))
        {            
            sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetLuaFunction(L, 3);\n", o, name);
        }
        else if (t == typeof(LuaTable))
        {
            sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetLuaTable(L, 3);\n", o, name);
        }
        else if (t == typeof(object))
        {
            sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetVarObject(L, 3);\n", o, name);
        }
        else if (t == typeof(Vector3))
        {
            sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetVector3(L, 3);\n", o, name);
        }
        else if (t == typeof(Quaternion))
        {
            sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetQuaternion(L, 3);\n", o, name);
        }
        else if (t == typeof(Vector2))
        {
            sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetVector2(L, 3);\n", o, name);
        }
        else if (t == typeof(Vector4))
        {
            sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetVector4(L, 3);\n", o, name);
        }
        else if (t == typeof(Color))
        {
            sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetColor(L, 3);\n", o, name);
        }
        else if (t == typeof(Ray))
        {
            sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetRay(L, 3);\n", o, name);
        }
        else if (typeof(UnityEngine.TrackedReference).IsAssignableFrom(t))
        {
            sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetTrackedObject<{2}>(L, 3);\n", o, name, _C(t.ToString()));
        }
        else if (typeof(UnityEngine.Object).IsAssignableFrom(t))
        {
            sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetUnityObject<{2}>(L, 3);\n", o, name, _C(t.ToString()));
        }
        else if (typeof(object).IsAssignableFrom(t) || t.IsEnum)
        {
            sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetNetObject<{2}>(L, 3);\n", o, name, _C(t.ToString()));
        }
        else if (t == typeof(Type))
        {
            sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetTypeObject(L, 3);\n", o, name);
        }
        else
        {
            Debugger.LogError("not defined type {0}", t);
        }
    }

    static void GenToStringFunc()
    {
        int index = Array.FindIndex<MethodInfo>(methods, (p) => { return p.Name == "ToString"; });
        if (index < 0 || isStaticClass) return;

        sb.Append("\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]\n");
        sb.Append("\tstatic int Lua_ToString(IntPtr L)\n");
        sb.Append("\t{\n");
        sb.Append("\t\tobject obj = LuaScriptMgr.GetLuaObject(L, 1);\n");

        sb.Append("\t\tif (obj != null)\n");
        sb.Append("\t\t{\n");
        sb.Append("\t\t\tLuaScriptMgr.Push(L, obj.ToString());\n");
        sb.Append("\t\t}\n");
        sb.Append("\t\telse\n");
        sb.Append("\t\t{\n");
        sb.AppendFormat("\t\t\tLuaScriptMgr.Push(L, \"Table: {0}\");\n", libClassName);
        sb.Append("\t\t}\n");
        sb.Append("\n");
        sb.Append("\t\treturn 1;\n");
        sb.Append("\t}\n");
    }

    static bool IsNeedOp(string name)
    {
        if (name == "op_Addition")
        {
            op |= MetaOp.Add;
        }
        else if (name == "op_Subtraction")
        {
            op |= MetaOp.Sub;
        }
        else if (name == "op_Equality")
        {
            op |= MetaOp.Eq;
        }
        else if (name == "op_Multiply")
        {
            op |= MetaOp.Mul;
        }
        else if (name == "op_Division")
        {
            op |= MetaOp.Div;
        }
        else if (name == "op_UnaryNegation")
        {
            op |= MetaOp.Neg;
        }
        else
        {
            return false;
        }
        

        return true;
    }

    static void CallOpFunction(string name, int count, string ret)
    {
        string head = string.Empty;

        for (int i = 0; i < count; i++)
        {
            head += "\t";
        }

        if (name == "op_Addition")
        {
            sb.AppendFormat("{0}{1} o = arg0 + arg1;\n", head, ret);
        }
        else if (name == "op_Subtraction")
        {
            sb.AppendFormat("{0}{1} o = arg0 - arg1;\n", head, ret);            
        }
        else if (name == "op_Equality")
        {
            sb.AppendFormat("{0}bool o = arg0 == arg1;\n", head);
        }
        else if (name == "op_Multiply")
        {
            sb.AppendFormat("{0}{1} o = arg0 * arg1;\n", head, ret);
        }
        else if (name == "op_Division")
        {
            sb.AppendFormat("{0}{1} o = arg0 / arg1;\n", head, ret);            
        }
        else if (name == "op_UnaryNegation")
        {
            sb.AppendFormat("{0}{1} o = -arg0;\n", head, ret);
        }
    }

    static string GetFuncName(string name)
    {
        if (name == "op_Addition")
        {
            return "Lua_Add";
        }
        else if (name == "op_Subtraction")
        {
            return "Lua_Sub";
        }
        else if (name == "op_Equality")
        {
            return "Lua_Eq";
        }
        else if (name == "op_Multiply")
        {
            return "Lua_Mul";
        }
        else if (name == "op_Division")
        {
            return "Lua_Div";
        }
        else if (name == "op_UnaryNegation")
        {
            return "Lua_Neg";
        }

        return name;
    }

    public static bool IsObsolete(MemberInfo mb)
    {
        object[] attrs = mb.GetCustomAttributes(true);

        for (int j = 0; j < attrs.Length; j++)
        {
            Type t = attrs[j].GetType() ;

            if (t == typeof(System.ObsoleteAttribute) || t == typeof(NoToLuaAttribute)) // || t.ToString() == "UnityEngine.WrapperlessIcall")
            {
                return true;               
            }
        }

        if (IsMemberFilter(mb))
        {
            return true;
        }

        return false;
    }

    static void GenEnum()
    {
        fields = type.GetFields(BindingFlags.GetField | BindingFlags.Public | BindingFlags.Static);
        List<FieldInfo> list = new List<FieldInfo>(fields);

        for (int i = list.Count - 1; i > 0; i--)
        {
            if (IsObsolete(list[i]))
            {
                list.RemoveAt(i);
            }
        }

        fields = list.ToArray();
        sb.AppendFormat("public class {0}Wrap\n", wrapClassName);
        sb.Append("{\n");
        sb.Append("\tstatic LuaMethod[] enums = new LuaMethod[]\n");
        sb.Append("\t{\n");

        for (int i = 0; i < fields.Length; i++)
        {
            sb.AppendFormat("\t\tnew LuaMethod(\"{0}\", Get{0}),\n", fields[i].Name);
        }

        sb.AppendFormat("\t\tnew LuaMethod(\"IntToEnum\", IntToEnum),\n");
        sb.Append("\t};\n");

        sb.Append("\n\tpublic static void Register(IntPtr L)\n");
        sb.Append("\t{\n");
        sb.AppendFormat("\t\tLuaScriptMgr.RegisterLib(L, \"{0}\", typeof({1}), enums);\n", libClassName, className);        
        sb.Append("\t}\n");        

        for (int i = 0; i < fields.Length; i++)
        {
            sb.Append("\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]\n");
            sb.AppendFormat("\tstatic int Get{0}(IntPtr L)\n", fields[i].Name);
            sb.Append("\t{\n");
            sb.AppendFormat("\t\tLuaScriptMgr.Push(L, {0}.{1});\n", className, fields[i].Name);
            sb.Append("\t\treturn 1;\n");
            sb.Append("\t}\n");            
        }
    }

    static void GenEnumTranslator()
    {
        sb.Append("\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]\n");
        sb.Append("\tstatic int IntToEnum(IntPtr L)\n");
        sb.Append("\t{\n");
        sb.Append("\t\tint arg0 = (int)LuaDLL.lua_tonumber(L, 1);\n");
        sb.AppendFormat("\t\t{0} o = ({0})arg0;\n", className);
        sb.Append("\t\tLuaScriptMgr.Push(L, o);\n");
        sb.Append("\t\treturn 1;\n");
        sb.Append("\t}\n");        
    }
}
