using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class SimplePoolWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("Release", Release),
		new LuaMethod("CreatePrefabPool", CreatePrefabPool),
		new LuaMethod("Spawn", Spawn),
		new LuaMethod("DeSpawn", DeSpawn),
		new LuaMethod("New", _CreateSimplePool),
		new LuaMethod("GetClassType", GetClassType),
		new LuaMethod("__eq", Lua_Eq),
	};

	static LuaField[] fields = new LuaField[]
	{
		new LuaField("Instance", get_Instance, null),
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSimplePool(IntPtr L)
	{
		LuaDLL.luaL_error(L, "SimplePool class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(SimplePool));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "SimplePool", typeof(SimplePool), regs, fields, typeof(UnityEngine.MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Instance(IntPtr L)
	{
		LuaScriptMgr.Push(L, SimplePool.Instance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Release(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		SimplePool obj = LuaScriptMgr.GetUnityObject<SimplePool>(L, 1);
		obj.Release();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreatePrefabPool(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		SimplePool obj = LuaScriptMgr.GetUnityObject<SimplePool>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
		Transform o = obj.CreatePrefabPool(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Spawn(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		SimplePool obj = LuaScriptMgr.GetUnityObject<SimplePool>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		Vector3 arg1 = LuaScriptMgr.GetVector3(L, 3);
		Quaternion arg2 = LuaScriptMgr.GetQuaternion(L, 4);
		GameObject o = obj.Spawn(arg0,arg1,arg2);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DeSpawn(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SimplePool obj = LuaScriptMgr.GetUnityObject<SimplePool>(L, 1);
		GameObject arg0 = LuaScriptMgr.GetUnityObject<GameObject>(L, 2);
		obj.DeSpawn(arg0);
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

