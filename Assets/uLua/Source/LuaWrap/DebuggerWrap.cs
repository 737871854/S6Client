using System;
using System.Collections.Generic;
using LuaInterface;

public class DebuggerWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("Log", Log),
		new LuaMethod("LogWarning", LogWarning),
		new LuaMethod("LogError", LogError),
		new LuaMethod("New", _CreateDebugger),
		new LuaMethod("GetClassType", GetClassType),
	};

	static LuaField[] fields = new LuaField[]
	{
		new LuaField("needAIMsg", get_needAIMsg, set_needAIMsg),
		new LuaField("needSaveFile", get_needSaveFile, set_needSaveFile),
		new LuaField("aiMsgDic", get_aiMsgDic, set_aiMsgDic),
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateDebugger(IntPtr L)
	{
		LuaDLL.luaL_error(L, "Debugger class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(Debugger));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "Debugger", typeof(Debugger), regs, fields, null);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_needAIMsg(IntPtr L)
	{
		LuaScriptMgr.Push(L, Debugger.needAIMsg);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_needSaveFile(IntPtr L)
	{
		LuaScriptMgr.Push(L, Debugger.needSaveFile);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_aiMsgDic(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, Debugger.aiMsgDic);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_needAIMsg(IntPtr L)
	{
		Debugger.needAIMsg = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_needSaveFile(IntPtr L)
	{
		Debugger.needSaveFile = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_aiMsgDic(IntPtr L)
	{
		Debugger.aiMsgDic = LuaScriptMgr.GetNetObject<Dictionary<uint,List<string>>>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Log(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		object[] objs1 = LuaScriptMgr.GetParamsObject(L, 2, count - 1);
		Debugger.Log(arg0,objs1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LogWarning(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		object[] objs1 = LuaScriptMgr.GetParamsObject(L, 2, count - 1);
		Debugger.LogWarning(arg0,objs1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LogError(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		object[] objs1 = LuaScriptMgr.GetParamsObject(L, 2, count - 1);
		Debugger.LogError(arg0,objs1);
		return 0;
	}
}

