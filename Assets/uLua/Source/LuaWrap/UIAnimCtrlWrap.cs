using System;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class UIAnimCtrlWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("SetStateCallback", SetStateCallback),
		new LuaMethod("Play", Play),
		new LuaMethod("New", _CreateUIAnimCtrl),
		new LuaMethod("GetClassType", GetClassType),
		new LuaMethod("__eq", Lua_Eq),
	};

	static LuaField[] fields = new LuaField[]
	{
		new LuaField("IsAutoPlay", get_IsAutoPlay, set_IsAutoPlay),
		new LuaField("LayerContainerCount", get_LayerContainerCount, set_LayerContainerCount),
		new LuaField("LayerContainerList", get_LayerContainerList, set_LayerContainerList),
		new LuaField("CallbackMap", get_CallbackMap, set_CallbackMap),
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUIAnimCtrl(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UIAnimCtrl class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(UIAnimCtrl));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UIAnimCtrl", typeof(UIAnimCtrl), regs, fields, typeof(UnityEngine.MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IsAutoPlay(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIAnimCtrl obj = (UIAnimCtrl)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name IsAutoPlay");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index IsAutoPlay on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.IsAutoPlay);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LayerContainerCount(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIAnimCtrl obj = (UIAnimCtrl)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LayerContainerCount");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LayerContainerCount on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.LayerContainerCount);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LayerContainerList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIAnimCtrl obj = (UIAnimCtrl)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LayerContainerList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LayerContainerList on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.LayerContainerList);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CallbackMap(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIAnimCtrl obj = (UIAnimCtrl)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CallbackMap");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CallbackMap on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.CallbackMap);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_IsAutoPlay(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIAnimCtrl obj = (UIAnimCtrl)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name IsAutoPlay");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index IsAutoPlay on a nil value");
			}
		}

		obj.IsAutoPlay = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_LayerContainerCount(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIAnimCtrl obj = (UIAnimCtrl)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LayerContainerCount");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LayerContainerCount on a nil value");
			}
		}

		obj.LayerContainerCount = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_LayerContainerList(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIAnimCtrl obj = (UIAnimCtrl)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LayerContainerList");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LayerContainerList on a nil value");
			}
		}

		obj.LayerContainerList = LuaScriptMgr.GetNetObject<List<UILayerContainerAnim>>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_CallbackMap(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIAnimCtrl obj = (UIAnimCtrl)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CallbackMap");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CallbackMap on a nil value");
			}
		}

		obj.CallbackMap = LuaScriptMgr.GetNetObject<Dictionary<UILayerContainerType,UtilCommon.VoidDelegate>>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetStateCallback(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UIAnimCtrl obj = LuaScriptMgr.GetUnityObject<UIAnimCtrl>(L, 1);
		UILayerContainerType arg0 = LuaScriptMgr.GetNetObject<UILayerContainerType>(L, 2);
		UtilCommon.VoidDelegate arg1 = LuaScriptMgr.GetNetObject<UtilCommon.VoidDelegate>(L, 3);
		obj.SetStateCallback(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Play(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIAnimCtrl obj = LuaScriptMgr.GetUnityObject<UIAnimCtrl>(L, 1);
		UILayerContainerType arg0 = LuaScriptMgr.GetNetObject<UILayerContainerType>(L, 2);
		obj.Play(arg0);
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

