using System;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class UITabsControlWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("AddEntry", AddEntry),
		new LuaMethod("Init", Init),
		new LuaMethod("ActivePanel", ActivePanel),
		new LuaMethod("New", _CreateUITabsControl),
		new LuaMethod("GetClassType", GetClassType),
		new LuaMethod("__eq", Lua_Eq),
	};

	static LuaField[] fields = new LuaField[]
	{
		new LuaField("Entries", get_Entries, set_Entries),
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUITabsControl(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UITabsControl class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(UITabsControl));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UITabsControl", typeof(UITabsControl), regs, fields, typeof(UnityEngine.MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Entries(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITabsControl obj = (UITabsControl)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Entries");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Entries on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.Entries);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Entries(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITabsControl obj = (UITabsControl)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Entries");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Entries on a nil value");
			}
		}

		obj.Entries = LuaScriptMgr.GetNetObject<List<UITabControlEntry>>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddEntry(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UITabsControl obj = LuaScriptMgr.GetUnityObject<UITabsControl>(L, 1);
		GameObject arg0 = LuaScriptMgr.GetUnityObject<GameObject>(L, 2);
		GameObject arg1 = LuaScriptMgr.GetUnityObject<GameObject>(L, 3);
		obj.AddEntry(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UITabsControl obj = LuaScriptMgr.GetUnityObject<UITabsControl>(L, 1);
		obj.Init();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ActivePanel(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UITabsControl obj = LuaScriptMgr.GetUnityObject<UITabsControl>(L, 1);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		obj.ActivePanel(arg0);
		return 0;
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

