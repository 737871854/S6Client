using System;
using LuaInterface;

public class UISpriteStateWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("Normal", GetNormal),
		new LuaMethod("HighLighted", GetHighLighted),
		new LuaMethod("Press", GetPress),
		new LuaMethod("Disable", GetDisable),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UISpriteState", typeof(UISpriteState), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNormal(IntPtr L)
	{
		LuaScriptMgr.Push(L, UISpriteState.Normal);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetHighLighted(IntPtr L)
	{
		LuaScriptMgr.Push(L, UISpriteState.HighLighted);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPress(IntPtr L)
	{
		LuaScriptMgr.Push(L, UISpriteState.Press);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDisable(IntPtr L)
	{
		LuaScriptMgr.Push(L, UISpriteState.Disable);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		UISpriteState o = (UISpriteState)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

