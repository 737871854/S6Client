using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class UITimeDestoryWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("New", _CreateUITimeDestory),
		new LuaMethod("GetClassType", GetClassType),
		new LuaMethod("__eq", Lua_Eq),
	};

	static LuaField[] fields = new LuaField[]
	{
		new LuaField("Time", get_Time, set_Time),
		new LuaField("DestoryDelegate", get_DestoryDelegate, set_DestoryDelegate),
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUITimeDestory(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UITimeDestory class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(UITimeDestory));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UITimeDestory", typeof(UITimeDestory), regs, fields, typeof(UnityEngine.MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITimeDestory obj = (UITimeDestory)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Time on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Time);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DestoryDelegate(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITimeDestory obj = (UITimeDestory)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name DestoryDelegate");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index DestoryDelegate on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.DestoryDelegate);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITimeDestory obj = (UITimeDestory)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Time on a nil value");
			}
		}

		obj.Time = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_DestoryDelegate(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UITimeDestory obj = (UITimeDestory)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name DestoryDelegate");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index DestoryDelegate on a nil value");
			}
		}

		obj.DestoryDelegate = LuaScriptMgr.GetNetObject<UtilCommon.VoidDelegate>(L, 3);
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

