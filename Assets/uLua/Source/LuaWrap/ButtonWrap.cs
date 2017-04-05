using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class ButtonWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("OnPointerClick", OnPointerClick),
		new LuaMethod("OnSubmit", OnSubmit),
		new LuaMethod("New", _CreateButton),
		new LuaMethod("GetClassType", GetClassType),
		new LuaMethod("__eq", Lua_Eq),
	};

	static LuaField[] fields = new LuaField[]
	{
		new LuaField("onClick", get_onClick, set_onClick),
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateButton(IntPtr L)
	{
		LuaDLL.luaL_error(L, "Button class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(Button));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.UI.Button", typeof(Button), regs, fields, typeof(UnityEngine.UI.Selectable));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onClick(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Button obj = (Button)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onClick");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onClick on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.onClick);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onClick(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Button obj = (Button)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onClick");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onClick on a nil value");
			}
		}

		obj.onClick = LuaScriptMgr.GetNetObject<UnityEngine.UI.Button.ButtonClickedEvent>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerClick(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Button obj = LuaScriptMgr.GetUnityObject<Button>(L, 1);
		PointerEventData arg0 = LuaScriptMgr.GetNetObject<PointerEventData>(L, 2);
		obj.OnPointerClick(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnSubmit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Button obj = LuaScriptMgr.GetUnityObject<Button>(L, 1);
		BaseEventData arg0 = LuaScriptMgr.GetNetObject<BaseEventData>(L, 2);
		obj.OnSubmit(arg0);
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

