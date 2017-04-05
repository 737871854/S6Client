using System;
using UnityEngine.EventSystems;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class UIBehaviourWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("IsActive", IsActive),
		new LuaMethod("IsDestroyed", IsDestroyed),
		new LuaMethod("New", _CreateUIBehaviour),
		new LuaMethod("GetClassType", GetClassType),
		new LuaMethod("__eq", Lua_Eq),
	};

	static LuaField[] fields = new LuaField[]
	{
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUIBehaviour(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UIBehaviour class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(UIBehaviour));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.EventSystems.UIBehaviour", typeof(UIBehaviour), regs, fields, typeof(UnityEngine.MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsActive(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIBehaviour obj = LuaScriptMgr.GetUnityObject<UIBehaviour>(L, 1);
		bool o = obj.IsActive();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsDestroyed(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIBehaviour obj = LuaScriptMgr.GetUnityObject<UIBehaviour>(L, 1);
		bool o = obj.IsDestroyed();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Object arg0 = LuaScriptMgr.GetVarObject(L, 1) as Object;
		Object arg1 = LuaScriptMgr.GetVarObject(L, 2) as Object;
		bool o = arg0 == arg1;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

