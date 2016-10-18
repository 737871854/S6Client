using System;
using UnityEngine.UI;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class MaskableGraphicWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("GetModifiedMaterial", GetModifiedMaterial),
		new LuaMethod("Cull", Cull),
		new LuaMethod("SetClipRect", SetClipRect),
		new LuaMethod("RecalculateClipping", RecalculateClipping),
		new LuaMethod("RecalculateMasking", RecalculateMasking),
		new LuaMethod("New", _CreateMaskableGraphic),
		new LuaMethod("GetClassType", GetClassType),
		new LuaMethod("__eq", Lua_Eq),
	};

	static LuaField[] fields = new LuaField[]
	{
		new LuaField("onCullStateChanged", get_onCullStateChanged, set_onCullStateChanged),
		new LuaField("maskable", get_maskable, set_maskable),
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMaskableGraphic(IntPtr L)
	{
		LuaDLL.luaL_error(L, "MaskableGraphic class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(MaskableGraphic));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.UI.MaskableGraphic", typeof(MaskableGraphic), regs, fields, typeof(UnityEngine.UI.Graphic));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onCullStateChanged(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MaskableGraphic obj = (MaskableGraphic)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onCullStateChanged");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onCullStateChanged on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.onCullStateChanged);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maskable(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MaskableGraphic obj = (MaskableGraphic)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maskable");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maskable on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.maskable);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onCullStateChanged(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MaskableGraphic obj = (MaskableGraphic)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onCullStateChanged");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onCullStateChanged on a nil value");
			}
		}

		obj.onCullStateChanged = LuaScriptMgr.GetNetObject<UnityEngine.UI.MaskableGraphic.CullStateChangedEvent>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maskable(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		MaskableGraphic obj = (MaskableGraphic)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maskable");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maskable on a nil value");
			}
		}

		obj.maskable = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetModifiedMaterial(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MaskableGraphic obj = LuaScriptMgr.GetUnityObject<MaskableGraphic>(L, 1);
		Material arg0 = LuaScriptMgr.GetUnityObject<Material>(L, 2);
		Material o = obj.GetModifiedMaterial(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Cull(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MaskableGraphic obj = LuaScriptMgr.GetUnityObject<MaskableGraphic>(L, 1);
		Rect arg0 = LuaScriptMgr.GetNetObject<Rect>(L, 2);
		bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
		obj.Cull(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetClipRect(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MaskableGraphic obj = LuaScriptMgr.GetUnityObject<MaskableGraphic>(L, 1);
		Rect arg0 = LuaScriptMgr.GetNetObject<Rect>(L, 2);
		bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
		obj.SetClipRect(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RecalculateClipping(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MaskableGraphic obj = LuaScriptMgr.GetUnityObject<MaskableGraphic>(L, 1);
		obj.RecalculateClipping();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RecalculateMasking(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MaskableGraphic obj = LuaScriptMgr.GetUnityObject<MaskableGraphic>(L, 1);
		obj.RecalculateMasking();
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

