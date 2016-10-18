/**
 * Copyright (c) 2015,广州擎天柱网络科技有限公司
 * All rights reserved.
 *
 * 文件名称：BindLua.cs
 * 简    述：自动生成uLua/Source/LuaWrap中的各种封装类对象，便于在lua中调用，
 * LuaBinding.binds 数组会指定要导出的自定义类型。因为框架会自动导出u3dengine的类，
 * 为了防止导出到lua的类过多，在U3dBinding.dropList剔除不需要导出的类型。
 * 创建标识：jyq 2015/XX/XX
 * 修改标识：lxt添加文件头说明 2015/12/23
 */
using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

using Object = UnityEngine.Object;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Reflection;
using UnityEngine.Rendering;
using Junfine.Debuger;
using UnityEngine.UI;
//using Qtz.Q5.Battle;

public static class LuaBinding
{
    public class BindType
    {
        public string name;
        public Type type;
        public bool IsStatic;
        public string baseName = null;
        public string wrapName = "";
        public string libName = "";

        string GetTypeStr(string str)
        {
            if (str.Contains("`"))
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
                string[] ss = s2.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
                s2 = string.Empty;

                for (int i = 0; i < ss.Length; i++)
                {
                    ss[i] = ToLua._TC(ss[i]);
                }

                for (int i = 0; i < ss.Length - 1; i++)
                {
                    s2 += ss[i];
                    s2 += ",";
                }

                s2 += ss[ss.Length - 1];

                string s4 = string.Format("{0}<{1}>", s1, s2);

                if (beMember)
                {
                    s4 += ".";
                    s4 += mc.Groups["s3"].Value;
                }

                str = s0 + "." + s4;
            }
            else if (str.Contains("+"))
            {
                str = str.Replace('+', '.');
            }

            return str;
        }

        public BindType(Type t)
        {
            string str = t.ToString();
            //str = GetTypeStr(str);            
            libName = GetTypeStr(str);
            type = t;

            if (t.IsGenericType) str = libName;

            if (t.BaseType != null)
            {
                baseName = t.BaseType.ToString();

                if (baseName == "System.ValueType")
                {
                    baseName = null;
                }
            }

            if (t.GetConstructor(Type.EmptyTypes) == null && t.IsAbstract && t.IsSealed)
            {
                baseName = null;
                IsStatic = true;
            }

            int index = str.LastIndexOf('.');

            if (index > 0)
            {
                name = str.Substring(index + 1);
                name = name.Replace('+', '.');
                wrapName = name.Replace(".", "");
            }
            else
            {
                name = str.Replace('+', '.');
                wrapName = name.Replace(".", "");
            }
        }

        public BindType SetBaseName(string str)
        {
            baseName = str;
            return this;
        }

        public BindType SetClassName(string str)
        {
            name = str;
            wrapName = GetWrapName();
            return this;
        }

        public BindType SetWrapName(string str)
        {
            wrapName = str;
            return this;
        }

        public BindType SetLibName(string str)
        {
            libName = str;
            return this;
        }

        string GetWrapName()
        {
            string[] ss = name.Split(new char[] { '.' });
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < ss.Length; i++)
            {
                sb.Append(ss[i]);
            }

            return sb.ToString();
        }
    }

    static BindType _GT(Type t)
    {
        return new BindType(t);
    }

    static BindType[] binds = new BindType[]
    { //object 由于跟 Object 文件重名就不加入了     
      /*  #region 测试模板                
        //测试模板
        //_GT(typeof(Dictionary<int,string>)).SetWrapName("DictInt2Str").SetLibName("DictInt2Str"),
        _GT(typeof(Dictionary<string,string>)).SetWrapName("DictionaryStr2Str").SetLibName("DictionaryStr2Str"),
        _GT(typeof(List<string>)).SetWrapName("ListString").SetLibName("ListString"),
        _GT(typeof(AudioManager)),
        //_GT(typeof(LoadSceneMgr)),
        #endregion
        
        #region ui
        _GT(typeof( UnityEngine.EventSystems.UIBehaviour)),
        _GT(typeof(Graphic)),
        _GT(typeof(MaskableGraphic)),
        _GT(typeof(Text)),
        _GT(typeof(Image)),
        _GT(typeof(Sprite)),
        _GT(typeof(Texture)),
        _GT(typeof(Texture2D)),
        _GT(typeof(ScrollRect)),
        _GT(typeof(UnityEngine.EventSystems.PointerEventData)),
        _GT(typeof(Selectable)),
        _GT(typeof(Button)),
        _GT(typeof(Toggle)),
        _GT(typeof(InputField)),
        _GT(typeof(Canvas)),
        
        _GT(typeof(UILayerContainerType)),
        _GT(typeof(UITabsControl)),
        _GT(typeof(UIAnimCtrl)),
        _GT(typeof(UITimeDestory)),
        _GT(typeof(UISpriteChange)),
        _GT(typeof(UISpriteState)),
        #endregion

        #region unity
        //unity                        
        _GT(typeof(Component)),
        _GT(typeof(Behaviour)),
        _GT(typeof(MonoBehaviour)),        
        _GT(typeof(GameObject)),
        _GT(typeof(Transform)),
        _GT(typeof(Space)),

        _GT(typeof(Camera)),   
        _GT(typeof(CameraClearFlags)),           
        _GT(typeof(Material)),
        _GT(typeof(Renderer)),        
        _GT(typeof(MeshRenderer)),
        _GT(typeof(SkinnedMeshRenderer)),
        _GT(typeof(Light)),
        _GT(typeof(LightType)),     
        _GT(typeof(ParticleEmitter)),
        _GT(typeof(ParticleRenderer)),
        _GT(typeof(ParticleAnimator)),        
                
        _GT(typeof(Physics)),
        _GT(typeof(Collider)),
        _GT(typeof(BoxCollider)),
        _GT(typeof(MeshCollider)),
        _GT(typeof(SphereCollider)),
        
        _GT(typeof(CharacterController)),

        _GT(typeof(Animation)),             
        _GT(typeof(AnimationClip)),
        _GT(typeof(TrackedReference)),
        _GT(typeof(AnimationState)),  
        _GT(typeof(QueueMode)),  
        _GT(typeof(PlayMode)),                  
        
        _GT(typeof(AudioClip)),
        _GT(typeof(AudioSource)),                
        
        _GT(typeof(Application)),
        _GT(typeof(Input)),    
        _GT(typeof(TouchPhase)),            
        _GT(typeof(KeyCode)),             
        _GT(typeof(Screen)),
        _GT(typeof(Time)),
        _GT(typeof(RenderSettings)),
        _GT(typeof(SleepTimeout)),        

        _GT(typeof(AsyncOperation)),
        _GT(typeof(AssetBundle)),   
        _GT(typeof(BlendWeights)),   
        _GT(typeof(QualitySettings)),  
        _GT(typeof(Plane)), 
        _GT(typeof(AnimationBlendMode)),    
        _GT(typeof(RenderTexture)),
        _GT(typeof(ParticleSystem)),
        _GT(typeof(Text)),
        
        _GT(typeof(WWW)),
        _GT(typeof(AudioListener)),
        _GT(typeof(LayerMask)),
        _GT(typeof(Animator)),
        //_GT(typeof(AnimationPlayer)),
        #endregion

       	#region 寻路相关
		_GT(typeof(NavMeshHit)),
		_GT(typeof(NavMeshPath)),
		_GT(typeof(NavMeshPathStatus)),
		_GT(typeof(NavMeshAgent)),
		_GT(typeof(NavMesh)),
		#endregion

		#region 碰撞相关
		_GT(typeof(RaycastHit)),
		#endregion

        _GT(typeof(Debugger)),
        _GT(typeof(ioo)),
        _GT(typeof(UtilCommon)),
        _GT(typeof(ByteBuffer)),
        _GT(typeof(NetworkManager)),
        _GT(typeof(ResourceManager)),
        _GT(typeof(TimerManager)),
        _GT(typeof(BaseLua)), 
        _GT(typeof(RectTransform)),
        _GT(typeof(UIFollowTarget)),
        _GT(typeof(UIProgress)),
        _GT(typeof(CamEventCaller)),
		_GT(typeof(TimerOutCtr)),
		_GT(typeof(CombineMesh)),
        _GT(typeof(Global)),
        _GT(typeof(PanelManager)),
        _GT(typeof(Util)),
        _GT(typeof(UIEventListener)),
        _GT(typeof(LuaHelper)),
        _GT(typeof(LeanTween)),
        _GT(typeof(LTDescr)),
        _GT(typeof(LoadSceneMgr)),
      	_GT(typeof(SimplePool)),
        _GT(typeof(GameLogic)),
        
       */ 
        _GT(typeof(UtilMath)),
    };

    [MenuItem("Q5/Lua相关/1. Gen Lua Wrap Files", false, 11)]
    public static void Binding()
    {
        if (!Application.isPlaying)
        {
            EditorApplication.isPlaying = true;
        }

        BindType[] list = binds;

        for (int i = 0; i < list.Length; i++)
        {
            ToLua.Clear();
            ToLua.className = list[i].name;
            ToLua.type = list[i].type;
            ToLua.isStaticClass = list[i].IsStatic;
            ToLua.baseClassName = list[i].baseName;
            ToLua.wrapClassName = list[i].wrapName;
            ToLua.libClassName = list[i].libName;
            ToLua.Generate(null);
        }

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < list.Length; i++)
        {
            sb.AppendFormat("\t\t{0}Wrap.Register();\n", list[i].wrapName);
        }

        EditorApplication.isPlaying = false;
        StringBuilder sb1 = new StringBuilder();
        for (int i = 0; i < binds.Length; i++) {
            sb1.AppendFormat("\t\t{0}Wrap.Register(L);\n", binds[i].wrapName);
        }
        GenLuaBinder();
        Debug.Log("Generate lua binding files over");
        AssetDatabase.Refresh();        
    }

    //[MenuItem("Lua/Gen LuaBinder File", false, 12)]
    static void GenLuaBinder()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("using System;\n");
        sb.Append("\n");
        sb.Append("public static class LuaBinder\n");
        sb.Append("{\n");
        sb.Append("\tpublic static void Bind(IntPtr L)\n");
        sb.Append("\t{\n");
        sb.Append("\t\tobjectWrap.Register(L);\n");
        sb.Append("\t\tObjectWrap.Register(L);\n");
        sb.Append("\t\tTypeWrap.Register(L);\n");
        sb.Append("\t\tDelegateWrap.Register(L);\n");
        sb.Append("\t\tIEnumeratorWrap.Register(L);\n");
        sb.Append("\t\tEnumWrap.Register(L);\n");
        sb.Append("\t\tStringWrap.Register(L);\n");
        sb.Append("\t\tMsgPacketWrap.Register(L);\n");
        

        //string[] files = Directory.GetFiles("Assets/Source/LuaWrap/", "*.cs", SearchOption.TopDirectoryOnly);
        string[] files = Directory.GetFiles("Assets/uLua/Source/LuaWrap/", "*.cs", SearchOption.TopDirectoryOnly);

        for (int i = 0; i < files.Length; i++)
        {
            string wrapName = Path.GetFileName(files[i]);
            int pos = wrapName.LastIndexOf(".");
            wrapName = wrapName.Substring(0, pos);
            sb.AppendFormat("\t\t{0}.Register(L);\n", wrapName);
        }

        sb.Append("\t}\n");
        sb.Append("}\n");

        //string file = Application.dataPath + "/Source/LuaWrap/Base/LuaBinder.cs";
        string file = Application.dataPath + "/uLua/Source/LuaWrap/Base/LuaBinder.cs";

        using (StreamWriter textWriter = new StreamWriter(file, false, Encoding.ASCII))
        {
            textWriter.Write(sb.ToString());
            textWriter.Flush();
            textWriter.Close();
        }
    }

    [MenuItem("Q5/Lua相关/3. Clear LuaBinder File + Wrap Files", false, 43)]
    static void ClearLuaBinder()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("using System;\n");
        sb.Append("\n");
        sb.Append("public static class LuaBinder\n");
        sb.Append("{\n");
        sb.Append("\tpublic static void Bind(IntPtr L)\n");
        sb.Append("\t{\n");
        sb.Append("\t}\n");
        sb.Append("}\n");

        //string file = Application.dataPath + "/Source/LuaWrap/Base/LuaBinder.cs";
        string file = Application.dataPath + "/uLua/Source/LuaWrap/Base/LuaBinder.cs";

        using (StreamWriter textWriter = new StreamWriter(file, false, Encoding.UTF8))
        {
            textWriter.Write(sb.ToString());
            textWriter.Flush();
            textWriter.Close();
        }
        //string path = Application.dataPath + "/Source/LuaWrap/";
        string path = Application.dataPath + "/uLua/Source/LuaWrap/";
        string[] names = Directory.GetFiles(path);
        foreach (var filename in names) {
            File.Delete(filename); //删除缓存
        }
        AssetDatabase.Refresh();
    }

    [MenuItem("Q5/Lua相关/2. Gen u3d Wrap Files", false, 13)]
    public static void U3dBinding()
    {
        List<string> dropList = new List<string>
        {      
            //特殊修改
            "UnityEngine.Object",

            //一般情况不需要的类, 编辑器相关的
            "HideInInspector",
            "ExecuteInEditMode",
            "AddComponentMenu",
            "ContextMenu",
            "RequireComponent",
            "DisallowMultipleComponent",
            "SerializeField",
            "AssemblyIsEditorAssembly",
            "Attribute",  //一些列文件，都是编辑器相关的     
            "FFTWindow",
            "Avatar",
            "CollisionDetectionMode2D",
            "FloatComparer",
            "HumanTrait",
            "PhysicMaterial",
            "Physics2D",
            "AdvertisementSettings",
            "AnimationClipPlayable",
            "AnimationMixerPlayable",
            "AnimationPlayable",
            "AnimatorControllerPlayable",
            "AudioSourceCurveType",
            "BoundingSphere",
            "CameraType",
            "ColorUtility",
            "CullingGroupEvent",
            "CullingGroup",
            "DirectorUpdateMode",
            "FrameData",
            "IAnimatorControllerPlayable",
            "Window",
            "StackTraceLogType",
            "SortingLayer",
            "RuntimeInitializeLoadType",
            "RenderTargetSetup",
            "ReflectionCubemapCompression",
            "QueryTriggerInteraction",
            "Playable",
            "PlayState",
            "StateMachineBehaviour",
            "Color",
            "Color32",
            "MaskableGraphic",

            "Types",
            "UnitySurrogateSelector",            
            "TypeInferenceRules",            
            "ThreadPriority",
            "Debug",        //自定义debugger取代
            "GenericStack",

            //异常，lua无法catch
            "PlayerPrefsException",
            "UnassignedReferenceException",            
            "UnityException",
            "MissingComponentException",
            "MissingReferenceException",

            //RPC网络
            "RPC",
            "Network",
            "MasterServer",
            "BitStream",
            "HostData",
            "ConnectionTesterStatus",

            //unity 自带编辑器GUI
            "GUI",
            "EventType",
            "EventModifiers",
            //"Event",
            "FontStyle",
            "TextAlignment",
            "TextEditor",
            "TextEditorDblClickSnapping",
            "TextGenerator",
            "TextClipping",
            "TextGenerationSettings",
            "TextAnchor",
            "TextAsset",
            "TextWrapMode",
            "Gizmos",
            "ImagePosition",
            "FocusType",
            

            //地形相关
            "Terrain",                            
            "Tree",
            "SplatPrototype",
            "DetailPrototype",
            "DetailRenderMode",

            //其他
            "MeshSubsetCombineUtility",
            "AOT",
            "Random",
            "Mathf",
            "Social",
            "Enumerator",       
            "SendMouseEvents",               
            "Cursor",
            "Flash",
            "ActionScript",
            
    
            //非通用的类
            "ADBannerView",
            "ADInterstitialAd",            
            "Android",
            "jvalue",
            "iPhone",
            "iOS",
            "CalendarIdentifier",
            "CalendarUnit",
            "CalendarUnit",
            "FullScreenMovieControlMode",
            "FullScreenMovieScalingMode",
            "Handheld",
            "LocalNotification",
            "Motion",   //空类
            "NotificationServices",
            "RemoteNotificationType",      
            "RemoteNotification",
            "SamsungTV",
            "TextureCompressionQuality",
            "TextureFormat",
            "ShaderVariantCollection",
            "TouchScreenKeyboardType",
            "TouchScreenKeyboard",
            "MovieTexture",

            //我不需要的
            //2d 类
            "AccelerationEventWrap", //加速
            "AnimatorUtility",
            "AudioChorusFilter",		
		    "AudioDistortionFilter",
		    "AudioEchoFilter",
		    "AudioHighPassFilter",		    
		    "AudioLowPassFilter",
		    "AudioReverbFilter",
		    "AudioReverbPreset",
		    "AudioReverbZone",
		    "AudioRolloffMode",
		    "AudioSettings",		    
		    "AudioSpeakerMode",
		    "AudioType",
		    "AudioVelocityUpdateMode",
            
            "Ping",
            "Profiler",
            "StaticBatchingUtility",
            "Font",
            "Gyroscope",                        //不需要重力感应
            "ISerializationCallbackReceiver",   //u3d 继承的序列化接口，lua不需要
            "ImageEffectOpaque",                //后处理
            "ImageEffectTransformsToLDR",
            "PrimitiveType",                // 暂时不需要 GameObject.CreatePrimitive           
            "Skybox",                       //不会u3d自带的Skybox
            "SparseTexture",                // mega texture 不需要
            "Plane",
            "PlayerPrefs",

            //不用ugui
            "SpriteAlignment",
		    "SpriteMeshType",
		    "SpritePackingMode",
		    "SpritePackingRotation",
		    "SpriteRenderer",
		    "Sprite",
            "UIVertex",
            "CanvasGroup",
            "CanvasRenderer",
            "ICanvasRaycastFilter",
            "Canvas",
            "RectTransform",
            "DrivenRectTransformTracker",
            "DrivenTransformProperties",
            "RectTransformAxis",
		    "RectTransformEdge",
		    "RectTransformUtility",
		    "RectTransform",
            "UICharInfo",
		    "UILineInfo",

            //不需要轮子碰撞体
            "WheelCollider",
		    "WheelFrictionCurve",
		    "WheelHit",

            //手机不适用雾
            "FogMode",

            "UnityEventBase",
		    "UnityEventCallState",
		    "UnityEvent",

            "LightProbeGroup",
            "LightProbes",

            "NPOTSupport", //只是SystemInfo 的一个枚举值

            //没用到substance纹理
            "ProceduralCacheSize",
		    "ProceduralLoadingBehavior",
		    "ProceduralMaterial",
		    "ProceduralOutputType",
		    "ProceduralProcessorUsage",
		    "ProceduralPropertyDescription",
		    "ProceduralPropertyType",
		    "ProceduralTexture",

            //物理关节系统
		    "JointDriveMode",
		    "JointDrive",
		    "JointLimits",		
		    "JointMotor",
		    "JointProjectionMode",
		    "JointSpring",
            "SoftJointLimit",
            "SpringJoint",
            "HingeJoint",
            "FixedJoint",
            "ConfigurableJoint",
            "CharacterJoint",            
		    "Joint",

            "LODGroup",
		    "LOD",

            "WWWForm",
            "WebCamDevice",
            "WebCamFlags",
            "WebCamTexture",
            "WindZoneMode",

            "DataUtility",          //给sprite使用的
            "CrashReport",
            "CombineInstance",
            "AnimatorControllerParameter"

        };

        List<BindType> list = new List<BindType>();
        Assembly assembly = Assembly.Load("UnityEngine");
        Type[] types = assembly.GetExportedTypes();

        for (int i = 0; i < types.Length; i++)
        {
            //不导出： 模版类，event委托, c#协同相关, obsolete 类
            if (!types[i].IsGenericType && types[i].BaseType != typeof(System.MulticastDelegate) &&
                !typeof(YieldInstruction).IsAssignableFrom(types[i]) && !ToLua.IsObsolete(types[i]))
            {
                list.Add(_GT(types[i]));
            }
            else
            {
                Debug.Log("drop generic type " + types[i].ToString());
            }
        }

        for (int i = 0; i < dropList.Count; i++)
        {
            list.RemoveAll((p) => { return p.type.ToString().Contains(dropList[i]); });
        }

        //for (int i = 0; i < list.Count; i++)
        //{
        //    if (!typeof(UnityEngine.Object).IsAssignableFrom(list[i].type) && !list[i].type.IsEnum && !typeof(UnityEngine.TrackedReference).IsAssignableFrom(list[i].type)
        //        && !list[i].type.IsValueType && !list[i].type.IsSealed)            
        //    {
        //        Debug.Log(list[i].type.Name);
        //    }
        //}

        for (int i = 0; i < list.Count; i++)
        {
            try
            {
                ToLua.Clear();
                ToLua.className = list[i].name;
                ToLua.type = list[i].type;
                ToLua.isStaticClass = list[i].IsStatic;
                ToLua.baseClassName = list[i].baseName;
                ToLua.wrapClassName = list[i].wrapName;
                ToLua.libClassName = list[i].libName;
                ToLua.Generate(null);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Generate wrap file error: " + e.ToString());
            }
        }

        GenLuaBinder();
        Debug.Log("Generate lua binding files over， Generate " + list.Count + " files");
        AssetDatabase.Refresh();
    }
}
