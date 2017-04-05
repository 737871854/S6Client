using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class LoadSceneMgrWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("Init", Init),
		new LuaMethod("SetLoadScene", SetLoadScene),
		new LuaMethod("AddPreLoadPrefab", AddPreLoadPrefab),
		new LuaMethod("StartLoad", StartLoad),
		new LuaMethod("OnLoadEmptylevel", OnLoadEmptylevel),
		new LuaMethod("OnLoadScnene", OnLoadScnene),
		new LuaMethod("New", _CreateLoadSceneMgr),
		new LuaMethod("GetClassType", GetClassType),
	};

	static LuaField[] fields = new LuaField[]
	{
		new LuaField("LOADING_UI_RES", get_LOADING_UI_RES, null),
		new LuaField("IsStartLoad", get_IsStartLoad, set_IsStartLoad),
		new LuaField("Instance", get_Instance, null),
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateLoadSceneMgr(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			LoadSceneMgr obj = new LoadSceneMgr();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: LoadSceneMgr.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(LoadSceneMgr));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "LoadSceneMgr", typeof(LoadSceneMgr), regs, fields, typeof(System.Object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LOADING_UI_RES(IntPtr L)
	{
		LuaScriptMgr.Push(L, LoadSceneMgr.LOADING_UI_RES);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IsStartLoad(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LoadSceneMgr obj = (LoadSceneMgr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name IsStartLoad");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index IsStartLoad on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.IsStartLoad);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Instance(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, LoadSceneMgr.Instance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_IsStartLoad(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LoadSceneMgr obj = (LoadSceneMgr)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name IsStartLoad");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index IsStartLoad on a nil value");
			}
		}

		obj.IsStartLoad = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		LoadSceneMgr obj = LuaScriptMgr.GetNetObject<LoadSceneMgr>(L, 1);
		GameObject arg0 = LuaScriptMgr.GetUnityObject<GameObject>(L, 2);
		obj.Init(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetLoadScene(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		if (count == 2)
		{
			LoadSceneMgr obj = LuaScriptMgr.GetNetObject<LoadSceneMgr>(L, 1);
			string arg0 = LuaScriptMgr.GetLuaString(L, 2);
			obj.SetLoadScene(arg0);
			return 0;
		}
		else if (count == 3)
		{
			LoadSceneMgr obj = LuaScriptMgr.GetNetObject<LoadSceneMgr>(L, 1);
			string arg0 = LuaScriptMgr.GetLuaString(L, 2);
			string arg1 = LuaScriptMgr.GetLuaString(L, 3);
			obj.SetLoadScene(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: LoadSceneMgr.SetLoadScene");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddPreLoadPrefab(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		LoadSceneMgr obj = LuaScriptMgr.GetNetObject<LoadSceneMgr>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
		obj.AddPreLoadPrefab(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StartLoad(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LoadSceneMgr obj = LuaScriptMgr.GetNetObject<LoadSceneMgr>(L, 1);
		obj.StartLoad();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnLoadEmptylevel(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		LoadSceneMgr obj = LuaScriptMgr.GetNetObject<LoadSceneMgr>(L, 1);
		obj.OnLoadEmptylevel();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnLoadScnene(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		LoadSceneMgr obj = LuaScriptMgr.GetNetObject<LoadSceneMgr>(L, 1);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		obj.OnLoadScnene(arg0);
		return 0;
	}
}

