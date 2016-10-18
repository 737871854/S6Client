using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class CanvasWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("GetDefaultCanvasMaterial", GetDefaultCanvasMaterial),
		new LuaMethod("ForceUpdateCanvases", ForceUpdateCanvases),
		new LuaMethod("New", _CreateCanvas),
		new LuaMethod("GetClassType", GetClassType),
		new LuaMethod("__eq", Lua_Eq),
	};

	static LuaField[] fields = new LuaField[]
	{
		new LuaField("renderMode", get_renderMode, set_renderMode),
		new LuaField("isRootCanvas", get_isRootCanvas, null),
		new LuaField("worldCamera", get_worldCamera, set_worldCamera),
		new LuaField("pixelRect", get_pixelRect, null),
		new LuaField("scaleFactor", get_scaleFactor, set_scaleFactor),
		new LuaField("referencePixelsPerUnit", get_referencePixelsPerUnit, set_referencePixelsPerUnit),
		new LuaField("overridePixelPerfect", get_overridePixelPerfect, set_overridePixelPerfect),
		new LuaField("pixelPerfect", get_pixelPerfect, set_pixelPerfect),
		new LuaField("planeDistance", get_planeDistance, set_planeDistance),
		new LuaField("renderOrder", get_renderOrder, null),
		new LuaField("overrideSorting", get_overrideSorting, set_overrideSorting),
		new LuaField("sortingOrder", get_sortingOrder, set_sortingOrder),
		new LuaField("sortingLayerID", get_sortingLayerID, set_sortingLayerID),
		new LuaField("cachedSortingLayerValue", get_cachedSortingLayerValue, null),
		new LuaField("sortingLayerName", get_sortingLayerName, set_sortingLayerName),
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateCanvas(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			Canvas obj = new Canvas();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Canvas.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(Canvas));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.Canvas", typeof(Canvas), regs, fields, typeof(UnityEngine.Behaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_renderMode(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name renderMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index renderMode on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.renderMode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isRootCanvas(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isRootCanvas");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isRootCanvas on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isRootCanvas);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_worldCamera(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name worldCamera");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index worldCamera on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.worldCamera);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pixelRect(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pixelRect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pixelRect on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.pixelRect);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_scaleFactor(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scaleFactor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scaleFactor on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.scaleFactor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_referencePixelsPerUnit(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name referencePixelsPerUnit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index referencePixelsPerUnit on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.referencePixelsPerUnit);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_overridePixelPerfect(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name overridePixelPerfect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index overridePixelPerfect on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.overridePixelPerfect);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pixelPerfect(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pixelPerfect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pixelPerfect on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.pixelPerfect);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_planeDistance(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name planeDistance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index planeDistance on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.planeDistance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_renderOrder(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name renderOrder");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index renderOrder on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.renderOrder);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_overrideSorting(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name overrideSorting");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index overrideSorting on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.overrideSorting);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sortingOrder(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingOrder");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingOrder on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.sortingOrder);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sortingLayerID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingLayerID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingLayerID on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.sortingLayerID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cachedSortingLayerValue(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cachedSortingLayerValue");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cachedSortingLayerValue on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.cachedSortingLayerValue);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sortingLayerName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingLayerName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingLayerName on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.sortingLayerName);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_renderMode(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name renderMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index renderMode on a nil value");
			}
		}

		obj.renderMode = LuaScriptMgr.GetNetObject<RenderMode>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_worldCamera(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name worldCamera");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index worldCamera on a nil value");
			}
		}

		obj.worldCamera = LuaScriptMgr.GetUnityObject<Camera>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_scaleFactor(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scaleFactor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scaleFactor on a nil value");
			}
		}

		obj.scaleFactor = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_referencePixelsPerUnit(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name referencePixelsPerUnit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index referencePixelsPerUnit on a nil value");
			}
		}

		obj.referencePixelsPerUnit = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_overridePixelPerfect(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name overridePixelPerfect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index overridePixelPerfect on a nil value");
			}
		}

		obj.overridePixelPerfect = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pixelPerfect(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pixelPerfect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pixelPerfect on a nil value");
			}
		}

		obj.pixelPerfect = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_planeDistance(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name planeDistance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index planeDistance on a nil value");
			}
		}

		obj.planeDistance = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_overrideSorting(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name overrideSorting");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index overrideSorting on a nil value");
			}
		}

		obj.overrideSorting = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sortingOrder(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingOrder");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingOrder on a nil value");
			}
		}

		obj.sortingOrder = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sortingLayerID(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingLayerID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingLayerID on a nil value");
			}
		}

		obj.sortingLayerID = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sortingLayerName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingLayerName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingLayerName on a nil value");
			}
		}

		obj.sortingLayerName = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDefaultCanvasMaterial(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		Material o = Canvas.GetDefaultCanvasMaterial();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ForceUpdateCanvases(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		Canvas.ForceUpdateCanvases();
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

