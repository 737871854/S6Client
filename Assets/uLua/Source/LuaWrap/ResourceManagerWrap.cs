using System;
using System.Collections;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class ResourceManagerWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("initialize", initialize),
		new LuaMethod("LoadAsset", LoadAsset),
		new LuaMethod("ReleaseAsset", ReleaseAsset),
		new LuaMethod("ClearAllAsset", ClearAllAsset),
		new LuaMethod("CheckAsyncResult", CheckAsyncResult),
		new LuaMethod("LoadAssetAsync", LoadAssetAsync),
		new LuaMethod("excuteShader", excuteShader),
		new LuaMethod("New", _CreateResourceManager),
		new LuaMethod("GetClassType", GetClassType),
		new LuaMethod("__eq", Lua_Eq),
	};

	static LuaField[] fields = new LuaField[]
	{
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateResourceManager(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ResourceManager class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(ResourceManager));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "ResourceManager", typeof(ResourceManager), regs, fields, typeof(UnityEngine.MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int initialize(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ResourceManager obj = LuaScriptMgr.GetUnityObject<ResourceManager>(L, 1);
		obj.initialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadAsset(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		ResourceManager obj = LuaScriptMgr.GetUnityObject<ResourceManager>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		Type arg1 = LuaScriptMgr.GetTypeObject(L, 3);
		ResourceMisc.AssetWrapper o = obj.LoadAsset(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReleaseAsset(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ResourceManager obj = LuaScriptMgr.GetUnityObject<ResourceManager>(L, 1);
		ResourceMisc.AssetWrapper arg0 = LuaScriptMgr.GetNetObject<ResourceMisc.AssetWrapper>(L, 2);
		obj.ReleaseAsset(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearAllAsset(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ResourceManager obj = LuaScriptMgr.GetUnityObject<ResourceManager>(L, 1);
		obj.ClearAllAsset();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CheckAsyncResult(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ResourceManager obj = LuaScriptMgr.GetUnityObject<ResourceManager>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		ResourceMisc.AssetWrapper o = obj.CheckAsyncResult(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadAssetAsync(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		ResourceManager obj = LuaScriptMgr.GetUnityObject<ResourceManager>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		Type arg1 = LuaScriptMgr.GetTypeObject(L, 3);
		IEnumerator o = obj.LoadAssetAsync(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int excuteShader(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameObject arg0 = LuaScriptMgr.GetUnityObject<GameObject>(L, 1);
		ResourceManager.excuteShader(arg0);
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

