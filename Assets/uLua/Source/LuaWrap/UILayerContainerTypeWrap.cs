using System;
using LuaInterface;

public class UILayerContainerTypeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("None", GetNone),
		new LuaMethod("Normal", GetNormal),
		new LuaMethod("Active", GetActive),
		new LuaMethod("InActive", GetInActive),
		new LuaMethod("Press", GetPress),
		new LuaMethod("Disable", GetDisable),
		new LuaMethod("COUNT", GetCOUNT),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UILayerContainerType", typeof(UILayerContainerType), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNone(IntPtr L)
	{
		LuaScriptMgr.Push(L, UILayerContainerType.None);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNormal(IntPtr L)
	{
		LuaScriptMgr.Push(L, UILayerContainerType.Normal);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetActive(IntPtr L)
	{
		LuaScriptMgr.Push(L, UILayerContainerType.Active);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetInActive(IntPtr L)
	{
		LuaScriptMgr.Push(L, UILayerContainerType.InActive);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPress(IntPtr L)
	{
		LuaScriptMgr.Push(L, UILayerContainerType.Press);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDisable(IntPtr L)
	{
		LuaScriptMgr.Push(L, UILayerContainerType.Disable);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCOUNT(IntPtr L)
	{
		LuaScriptMgr.Push(L, UILayerContainerType.COUNT);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		UILayerContainerType o = (UILayerContainerType)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

