using System;
using UnityEngine;
using UnityEngine.EventSystems;
using LuaInterface;
using Object = UnityEngine.Object;

public class UIEventListenerWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("Get", Get),
		new LuaMethod("OnPointerClick", OnPointerClick),
		new LuaMethod("OnPointerDown", OnPointerDown),
		new LuaMethod("OnPointerUp", OnPointerUp),
		new LuaMethod("OnPointerEnter", OnPointerEnter),
		new LuaMethod("OnPointerExit", OnPointerExit),
		new LuaMethod("OnBeginDrag", OnBeginDrag),
		new LuaMethod("OnEndDrag", OnEndDrag),
		new LuaMethod("OnDrag", OnDrag),
		new LuaMethod("OnLongPress", OnLongPress),
		new LuaMethod("OnLongPressMoreThanOnce", OnLongPressMoreThanOnce),
		new LuaMethod("AddValueChangeEventListener", AddValueChangeEventListener),
		new LuaMethod("New", _CreateUIEventListener),
		new LuaMethod("GetClassType", GetClassType),
		new LuaMethod("__eq", Lua_Eq),
	};

	static LuaField[] fields = new LuaField[]
	{
		new LuaField("onClick", get_onClick, set_onClick),
		new LuaField("onPointerDown", get_onPointerDown, set_onPointerDown),
		new LuaField("onPointerUp", get_onPointerUp, set_onPointerUp),
		new LuaField("onDragPointerEnter", get_onDragPointerEnter, set_onDragPointerEnter),
		new LuaField("onDragPointerExit", get_onDragPointerExit, set_onDragPointerExit),
		new LuaField("onPointerEnter", get_onPointerEnter, set_onPointerEnter),
		new LuaField("onPointerExit", get_onPointerExit, set_onPointerExit),
		new LuaField("onLongPress", get_onLongPress, set_onLongPress),
		new LuaField("onLongPressMoreThanOnce", get_onLongPressMoreThanOnce, set_onLongPressMoreThanOnce),
		new LuaField("onBeginDrag", get_onBeginDrag, set_onBeginDrag),
		new LuaField("onEndDrag", get_onEndDrag, set_onEndDrag),
		new LuaField("onDrag", get_onDrag, set_onDrag),
		new LuaField("LongPressDuration", get_LongPressDuration, set_LongPressDuration),
		new LuaField("LongPressInterval", get_LongPressInterval, set_LongPressInterval),
		new LuaField("LongPressAcceleration", get_LongPressAcceleration, set_LongPressAcceleration),
		new LuaField("LongPressMinInterval", get_LongPressMinInterval, set_LongPressMinInterval),
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUIEventListener(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UIEventListener class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(UIEventListener));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UIEventListener", typeof(UIEventListener), regs, fields, typeof(UnityEngine.MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onClick(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

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

		LuaScriptMgr.Push(L, obj.onClick);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onPointerDown(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onPointerDown");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onPointerDown on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onPointerDown);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onPointerUp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onPointerUp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onPointerUp on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onPointerUp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onDragPointerEnter(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDragPointerEnter");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDragPointerEnter on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onDragPointerEnter);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onDragPointerExit(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDragPointerExit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDragPointerExit on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onDragPointerExit);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onPointerEnter(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onPointerEnter");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onPointerEnter on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onPointerEnter);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onPointerExit(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onPointerExit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onPointerExit on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onPointerExit);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onLongPress(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onLongPress");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onLongPress on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onLongPress);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onLongPressMoreThanOnce(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onLongPressMoreThanOnce");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onLongPressMoreThanOnce on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onLongPressMoreThanOnce);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onBeginDrag(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onBeginDrag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onBeginDrag on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onBeginDrag);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onEndDrag(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onEndDrag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onEndDrag on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onEndDrag);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onDrag(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDrag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDrag on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.onDrag);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LongPressDuration(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LongPressDuration");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LongPressDuration on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.LongPressDuration);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LongPressInterval(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LongPressInterval");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LongPressInterval on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.LongPressInterval);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LongPressAcceleration(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LongPressAcceleration");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LongPressAcceleration on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.LongPressAcceleration);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LongPressMinInterval(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LongPressMinInterval");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LongPressMinInterval on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.LongPressMinInterval);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onClick(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

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

		obj.onClick = LuaScriptMgr.GetNetObject<UtilCommon.VoidDelegate>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onPointerDown(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onPointerDown");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onPointerDown on a nil value");
			}
		}

		obj.onPointerDown = LuaScriptMgr.GetNetObject<UtilCommon.VoidDelegate>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onPointerUp(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onPointerUp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onPointerUp on a nil value");
			}
		}

		obj.onPointerUp = LuaScriptMgr.GetNetObject<UtilCommon.VoidDelegate>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onDragPointerEnter(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDragPointerEnter");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDragPointerEnter on a nil value");
			}
		}

		obj.onDragPointerEnter = LuaScriptMgr.GetNetObject<UtilCommon.VoidDelegate>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onDragPointerExit(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDragPointerExit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDragPointerExit on a nil value");
			}
		}

		obj.onDragPointerExit = LuaScriptMgr.GetNetObject<UtilCommon.VoidDelegate>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onPointerEnter(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onPointerEnter");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onPointerEnter on a nil value");
			}
		}

		obj.onPointerEnter = LuaScriptMgr.GetNetObject<UtilCommon.VoidDelegate>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onPointerExit(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onPointerExit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onPointerExit on a nil value");
			}
		}

		obj.onPointerExit = LuaScriptMgr.GetNetObject<UtilCommon.VoidDelegate>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onLongPress(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onLongPress");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onLongPress on a nil value");
			}
		}

		obj.onLongPress = LuaScriptMgr.GetNetObject<UtilCommon.VoidDelegate>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onLongPressMoreThanOnce(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onLongPressMoreThanOnce");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onLongPressMoreThanOnce on a nil value");
			}
		}

		obj.onLongPressMoreThanOnce = LuaScriptMgr.GetNetObject<UtilCommon.VoidDelegate>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onBeginDrag(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onBeginDrag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onBeginDrag on a nil value");
			}
		}

		obj.onBeginDrag = LuaScriptMgr.GetNetObject<UtilCommon.DragDelegate>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onEndDrag(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onEndDrag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onEndDrag on a nil value");
			}
		}

		obj.onEndDrag = LuaScriptMgr.GetNetObject<UtilCommon.DragDelegate>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onDrag(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDrag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDrag on a nil value");
			}
		}

		obj.onDrag = LuaScriptMgr.GetNetObject<UtilCommon.DragDelegate>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_LongPressDuration(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LongPressDuration");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LongPressDuration on a nil value");
			}
		}

		obj.LongPressDuration = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_LongPressInterval(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LongPressInterval");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LongPressInterval on a nil value");
			}
		}

		obj.LongPressInterval = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_LongPressAcceleration(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LongPressAcceleration");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LongPressAcceleration on a nil value");
			}
		}

		obj.LongPressAcceleration = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_LongPressMinInterval(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		UIEventListener obj = (UIEventListener)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name LongPressMinInterval");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index LongPressMinInterval on a nil value");
			}
		}

		obj.LongPressMinInterval = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Get(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameObject arg0 = LuaScriptMgr.GetUnityObject<GameObject>(L, 1);
		UIEventListener o = UIEventListener.Get(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerClick(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIEventListener obj = LuaScriptMgr.GetUnityObject<UIEventListener>(L, 1);
		PointerEventData arg0 = LuaScriptMgr.GetNetObject<PointerEventData>(L, 2);
		obj.OnPointerClick(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerDown(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIEventListener obj = LuaScriptMgr.GetUnityObject<UIEventListener>(L, 1);
		PointerEventData arg0 = LuaScriptMgr.GetNetObject<PointerEventData>(L, 2);
		obj.OnPointerDown(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerUp(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIEventListener obj = LuaScriptMgr.GetUnityObject<UIEventListener>(L, 1);
		PointerEventData arg0 = LuaScriptMgr.GetNetObject<PointerEventData>(L, 2);
		obj.OnPointerUp(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerEnter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIEventListener obj = LuaScriptMgr.GetUnityObject<UIEventListener>(L, 1);
		PointerEventData arg0 = LuaScriptMgr.GetNetObject<PointerEventData>(L, 2);
		obj.OnPointerEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerExit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIEventListener obj = LuaScriptMgr.GetUnityObject<UIEventListener>(L, 1);
		PointerEventData arg0 = LuaScriptMgr.GetNetObject<PointerEventData>(L, 2);
		obj.OnPointerExit(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnBeginDrag(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIEventListener obj = LuaScriptMgr.GetUnityObject<UIEventListener>(L, 1);
		PointerEventData arg0 = LuaScriptMgr.GetNetObject<PointerEventData>(L, 2);
		obj.OnBeginDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEndDrag(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIEventListener obj = LuaScriptMgr.GetUnityObject<UIEventListener>(L, 1);
		PointerEventData arg0 = LuaScriptMgr.GetNetObject<PointerEventData>(L, 2);
		obj.OnEndDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDrag(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIEventListener obj = LuaScriptMgr.GetUnityObject<UIEventListener>(L, 1);
		PointerEventData arg0 = LuaScriptMgr.GetNetObject<PointerEventData>(L, 2);
		obj.OnDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnLongPress(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIEventListener obj = LuaScriptMgr.GetUnityObject<UIEventListener>(L, 1);
		obj.OnLongPress();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnLongPressMoreThanOnce(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIEventListener obj = LuaScriptMgr.GetUnityObject<UIEventListener>(L, 1);
		obj.OnLongPressMoreThanOnce();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddValueChangeEventListener(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UIEventListener obj = LuaScriptMgr.GetUnityObject<UIEventListener>(L, 1);
		UtilCommon.VoidDelegate arg0 = LuaScriptMgr.GetNetObject<UtilCommon.VoidDelegate>(L, 2);
		obj.AddValueChangeEventListener(arg0);
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

