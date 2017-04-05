using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class UISpriteChangeWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("SetSprite", SetSprite),
		new LuaMethod("New", _CreateUISpriteChange),
		new LuaMethod("GetClassType", GetClassType),
		new LuaMethod("__eq", Lua_Eq),
	};

	static LuaField[] fields = new LuaField[]
	{
		new LuaField("NormalSprite", get_NormalSprite, set_NormalSprite),
		new LuaField("HighLightedSprite", get_HighLightedSprite, set_HighLightedSprite),
		new LuaField("PressSprite", get_PressSprite, set_PressSprite),
		new LuaField("DisableSprite", get_DisableSprite, set_DisableSprite),
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUISpriteChange(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UISpriteChange class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(UISpriteChange));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UISpriteChange", typeof(UISpriteChange), regs, fields, typeof(UnityEngine.MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NormalSprite(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UISpriteChange obj = (UISpriteChange)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name NormalSprite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index NormalSprite on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.NormalSprite);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_HighLightedSprite(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UISpriteChange obj = (UISpriteChange)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name HighLightedSprite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index HighLightedSprite on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.HighLightedSprite);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PressSprite(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UISpriteChange obj = (UISpriteChange)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name PressSprite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index PressSprite on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.PressSprite);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DisableSprite(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UISpriteChange obj = (UISpriteChange)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name DisableSprite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index DisableSprite on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.DisableSprite);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_NormalSprite(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UISpriteChange obj = (UISpriteChange)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name NormalSprite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index NormalSprite on a nil value");
			}
		}

		obj.NormalSprite = LuaScriptMgr.GetUnityObject<Sprite>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_HighLightedSprite(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UISpriteChange obj = (UISpriteChange)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name HighLightedSprite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index HighLightedSprite on a nil value");
			}
		}

		obj.HighLightedSprite = LuaScriptMgr.GetUnityObject<Sprite>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_PressSprite(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UISpriteChange obj = (UISpriteChange)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name PressSprite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index PressSprite on a nil value");
			}
		}

		obj.PressSprite = LuaScriptMgr.GetUnityObject<Sprite>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_DisableSprite(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UISpriteChange obj = (UISpriteChange)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name DisableSprite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index DisableSprite on a nil value");
			}
		}

		obj.DisableSprite = LuaScriptMgr.GetUnityObject<Sprite>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetSprite(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UISpriteChange obj = LuaScriptMgr.GetUnityObject<UISpriteChange>(L, 1);
		UISpriteState arg0 = LuaScriptMgr.GetNetObject<UISpriteState>(L, 2);
		obj.SetSprite(arg0);
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

