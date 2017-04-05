using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class UIProgressWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("InitUIProgress", InitUIProgress),
		new LuaMethod("New", _CreateUIProgress),
		new LuaMethod("GetClassType", GetClassType),
		new LuaMethod("__eq", Lua_Eq),
	};

	static LuaField[] fields = new LuaField[]
	{
		new LuaField("TotalTime", get_TotalTime, set_TotalTime),
		new LuaField("IsFinish", get_IsFinish, null),
		new LuaField("CallBack", null, set_CallBack),
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUIProgress(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UIProgress class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(UIProgress));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UIProgress", typeof(UIProgress), regs, fields, typeof(UnityEngine.MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TotalTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIProgress obj = (UIProgress)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TotalTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TotalTime on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.TotalTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IsFinish(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIProgress obj = (UIProgress)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name IsFinish");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index IsFinish on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.IsFinish);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_TotalTime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIProgress obj = (UIProgress)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name TotalTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index TotalTime on a nil value");
			}
		}

		obj.TotalTime = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_CallBack(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIProgress obj = (UIProgress)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name CallBack");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index CallBack on a nil value");
			}
		}

		obj.CallBack = LuaScriptMgr.GetNetObject<UtilCommon.VoidDelegate>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InitUIProgress(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		UIProgress obj = LuaScriptMgr.GetUnityObject<UIProgress>(L, 1);
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 3);
		obj.InitUIProgress(arg0,arg1);
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

