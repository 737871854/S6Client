using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using LuaInterface;
using Object = UnityEngine.Object;

public class ScrollRectWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("Rebuild", Rebuild),
		new LuaMethod("LayoutComplete", LayoutComplete),
		new LuaMethod("GraphicUpdateComplete", GraphicUpdateComplete),
		new LuaMethod("IsActive", IsActive),
		new LuaMethod("StopMovement", StopMovement),
		new LuaMethod("OnScroll", OnScroll),
		new LuaMethod("OnInitializePotentialDrag", OnInitializePotentialDrag),
		new LuaMethod("OnBeginDrag", OnBeginDrag),
		new LuaMethod("OnEndDrag", OnEndDrag),
		new LuaMethod("OnDrag", OnDrag),
		new LuaMethod("CalculateLayoutInputHorizontal", CalculateLayoutInputHorizontal),
		new LuaMethod("CalculateLayoutInputVertical", CalculateLayoutInputVertical),
		new LuaMethod("SetLayoutHorizontal", SetLayoutHorizontal),
		new LuaMethod("SetLayoutVertical", SetLayoutVertical),
		new LuaMethod("New", _CreateScrollRect),
		new LuaMethod("GetClassType", GetClassType),
		new LuaMethod("__eq", Lua_Eq),
	};

	static LuaField[] fields = new LuaField[]
	{
		new LuaField("content", get_content, set_content),
		new LuaField("horizontal", get_horizontal, set_horizontal),
		new LuaField("vertical", get_vertical, set_vertical),
		new LuaField("movementType", get_movementType, set_movementType),
		new LuaField("elasticity", get_elasticity, set_elasticity),
		new LuaField("inertia", get_inertia, set_inertia),
		new LuaField("decelerationRate", get_decelerationRate, set_decelerationRate),
		new LuaField("scrollSensitivity", get_scrollSensitivity, set_scrollSensitivity),
		new LuaField("viewport", get_viewport, set_viewport),
		new LuaField("horizontalScrollbar", get_horizontalScrollbar, set_horizontalScrollbar),
		new LuaField("verticalScrollbar", get_verticalScrollbar, set_verticalScrollbar),
		new LuaField("horizontalScrollbarVisibility", get_horizontalScrollbarVisibility, set_horizontalScrollbarVisibility),
		new LuaField("verticalScrollbarVisibility", get_verticalScrollbarVisibility, set_verticalScrollbarVisibility),
		new LuaField("horizontalScrollbarSpacing", get_horizontalScrollbarSpacing, set_horizontalScrollbarSpacing),
		new LuaField("verticalScrollbarSpacing", get_verticalScrollbarSpacing, set_verticalScrollbarSpacing),
		new LuaField("onValueChanged", get_onValueChanged, set_onValueChanged),
		new LuaField("velocity", get_velocity, set_velocity),
		new LuaField("normalizedPosition", get_normalizedPosition, set_normalizedPosition),
		new LuaField("horizontalNormalizedPosition", get_horizontalNormalizedPosition, set_horizontalNormalizedPosition),
		new LuaField("verticalNormalizedPosition", get_verticalNormalizedPosition, set_verticalNormalizedPosition),
		new LuaField("minWidth", get_minWidth, null),
		new LuaField("preferredWidth", get_preferredWidth, null),
		new LuaField("flexibleWidth", get_flexibleWidth, null),
		new LuaField("minHeight", get_minHeight, null),
		new LuaField("preferredHeight", get_preferredHeight, null),
		new LuaField("flexibleHeight", get_flexibleHeight, null),
		new LuaField("layoutPriority", get_layoutPriority, null),
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateScrollRect(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ScrollRect class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(ScrollRect));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.UI.ScrollRect", typeof(ScrollRect), regs, fields, typeof(UnityEngine.EventSystems.UIBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_content(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name content");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index content on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.content);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_horizontal(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name horizontal");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index horizontal on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.horizontal);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_vertical(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name vertical");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index vertical on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.vertical);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_movementType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name movementType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index movementType on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.movementType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_elasticity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name elasticity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index elasticity on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.elasticity);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_inertia(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name inertia");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index inertia on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.inertia);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_decelerationRate(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name decelerationRate");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index decelerationRate on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.decelerationRate);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_scrollSensitivity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scrollSensitivity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scrollSensitivity on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.scrollSensitivity);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_viewport(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name viewport");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index viewport on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.viewport);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_horizontalScrollbar(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name horizontalScrollbar");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index horizontalScrollbar on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.horizontalScrollbar);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_verticalScrollbar(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name verticalScrollbar");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index verticalScrollbar on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.verticalScrollbar);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_horizontalScrollbarVisibility(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name horizontalScrollbarVisibility");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index horizontalScrollbarVisibility on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.horizontalScrollbarVisibility);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_verticalScrollbarVisibility(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name verticalScrollbarVisibility");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index verticalScrollbarVisibility on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.verticalScrollbarVisibility);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_horizontalScrollbarSpacing(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name horizontalScrollbarSpacing");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index horizontalScrollbarSpacing on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.horizontalScrollbarSpacing);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_verticalScrollbarSpacing(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name verticalScrollbarSpacing");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index verticalScrollbarSpacing on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.verticalScrollbarSpacing);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onValueChanged(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onValueChanged");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onValueChanged on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.onValueChanged);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_velocity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name velocity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index velocity on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.velocity);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_normalizedPosition(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name normalizedPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index normalizedPosition on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.normalizedPosition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_horizontalNormalizedPosition(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name horizontalNormalizedPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index horizontalNormalizedPosition on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.horizontalNormalizedPosition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_verticalNormalizedPosition(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name verticalNormalizedPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index verticalNormalizedPosition on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.verticalNormalizedPosition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_minWidth(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name minWidth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index minWidth on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.minWidth);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_preferredWidth(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name preferredWidth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index preferredWidth on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.preferredWidth);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_flexibleWidth(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name flexibleWidth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index flexibleWidth on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.flexibleWidth);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_minHeight(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name minHeight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index minHeight on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.minHeight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_preferredHeight(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name preferredHeight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index preferredHeight on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.preferredHeight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_flexibleHeight(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name flexibleHeight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index flexibleHeight on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.flexibleHeight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_layoutPriority(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name layoutPriority");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index layoutPriority on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.layoutPriority);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_content(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name content");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index content on a nil value");
			}
		}

		obj.content = LuaScriptMgr.GetUnityObject<RectTransform>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_horizontal(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name horizontal");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index horizontal on a nil value");
			}
		}

		obj.horizontal = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_vertical(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name vertical");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index vertical on a nil value");
			}
		}

		obj.vertical = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_movementType(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name movementType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index movementType on a nil value");
			}
		}

		obj.movementType = LuaScriptMgr.GetNetObject<UnityEngine.UI.ScrollRect.MovementType>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_elasticity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name elasticity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index elasticity on a nil value");
			}
		}

		obj.elasticity = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_inertia(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name inertia");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index inertia on a nil value");
			}
		}

		obj.inertia = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_decelerationRate(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name decelerationRate");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index decelerationRate on a nil value");
			}
		}

		obj.decelerationRate = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_scrollSensitivity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scrollSensitivity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scrollSensitivity on a nil value");
			}
		}

		obj.scrollSensitivity = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_viewport(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name viewport");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index viewport on a nil value");
			}
		}

		obj.viewport = LuaScriptMgr.GetUnityObject<RectTransform>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_horizontalScrollbar(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name horizontalScrollbar");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index horizontalScrollbar on a nil value");
			}
		}

		obj.horizontalScrollbar = LuaScriptMgr.GetUnityObject<Scrollbar>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_verticalScrollbar(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name verticalScrollbar");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index verticalScrollbar on a nil value");
			}
		}

		obj.verticalScrollbar = LuaScriptMgr.GetUnityObject<Scrollbar>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_horizontalScrollbarVisibility(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name horizontalScrollbarVisibility");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index horizontalScrollbarVisibility on a nil value");
			}
		}

		obj.horizontalScrollbarVisibility = LuaScriptMgr.GetNetObject<UnityEngine.UI.ScrollRect.ScrollbarVisibility>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_verticalScrollbarVisibility(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name verticalScrollbarVisibility");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index verticalScrollbarVisibility on a nil value");
			}
		}

		obj.verticalScrollbarVisibility = LuaScriptMgr.GetNetObject<UnityEngine.UI.ScrollRect.ScrollbarVisibility>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_horizontalScrollbarSpacing(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name horizontalScrollbarSpacing");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index horizontalScrollbarSpacing on a nil value");
			}
		}

		obj.horizontalScrollbarSpacing = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_verticalScrollbarSpacing(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name verticalScrollbarSpacing");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index verticalScrollbarSpacing on a nil value");
			}
		}

		obj.verticalScrollbarSpacing = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onValueChanged(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onValueChanged");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onValueChanged on a nil value");
			}
		}

		obj.onValueChanged = LuaScriptMgr.GetNetObject<UnityEngine.UI.ScrollRect.ScrollRectEvent>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_velocity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name velocity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index velocity on a nil value");
			}
		}

		obj.velocity = LuaScriptMgr.GetVector2(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_normalizedPosition(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name normalizedPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index normalizedPosition on a nil value");
			}
		}

		obj.normalizedPosition = LuaScriptMgr.GetVector2(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_horizontalNormalizedPosition(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name horizontalNormalizedPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index horizontalNormalizedPosition on a nil value");
			}
		}

		obj.horizontalNormalizedPosition = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_verticalNormalizedPosition(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ScrollRect obj = (ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name verticalNormalizedPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index verticalNormalizedPosition on a nil value");
			}
		}

		obj.verticalNormalizedPosition = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Rebuild(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ScrollRect obj = LuaScriptMgr.GetUnityObject<ScrollRect>(L, 1);
		CanvasUpdate arg0 = LuaScriptMgr.GetNetObject<CanvasUpdate>(L, 2);
		obj.Rebuild(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LayoutComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ScrollRect obj = LuaScriptMgr.GetUnityObject<ScrollRect>(L, 1);
		obj.LayoutComplete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GraphicUpdateComplete(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ScrollRect obj = LuaScriptMgr.GetUnityObject<ScrollRect>(L, 1);
		obj.GraphicUpdateComplete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsActive(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ScrollRect obj = LuaScriptMgr.GetUnityObject<ScrollRect>(L, 1);
		bool o = obj.IsActive();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopMovement(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ScrollRect obj = LuaScriptMgr.GetUnityObject<ScrollRect>(L, 1);
		obj.StopMovement();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnScroll(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ScrollRect obj = LuaScriptMgr.GetUnityObject<ScrollRect>(L, 1);
		PointerEventData arg0 = LuaScriptMgr.GetNetObject<PointerEventData>(L, 2);
		obj.OnScroll(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnInitializePotentialDrag(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ScrollRect obj = LuaScriptMgr.GetUnityObject<ScrollRect>(L, 1);
		PointerEventData arg0 = LuaScriptMgr.GetNetObject<PointerEventData>(L, 2);
		obj.OnInitializePotentialDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnBeginDrag(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ScrollRect obj = LuaScriptMgr.GetUnityObject<ScrollRect>(L, 1);
		PointerEventData arg0 = LuaScriptMgr.GetNetObject<PointerEventData>(L, 2);
		obj.OnBeginDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEndDrag(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ScrollRect obj = LuaScriptMgr.GetUnityObject<ScrollRect>(L, 1);
		PointerEventData arg0 = LuaScriptMgr.GetNetObject<PointerEventData>(L, 2);
		obj.OnEndDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDrag(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ScrollRect obj = LuaScriptMgr.GetUnityObject<ScrollRect>(L, 1);
		PointerEventData arg0 = LuaScriptMgr.GetNetObject<PointerEventData>(L, 2);
		obj.OnDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CalculateLayoutInputHorizontal(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ScrollRect obj = LuaScriptMgr.GetUnityObject<ScrollRect>(L, 1);
		obj.CalculateLayoutInputHorizontal();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CalculateLayoutInputVertical(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ScrollRect obj = LuaScriptMgr.GetUnityObject<ScrollRect>(L, 1);
		obj.CalculateLayoutInputVertical();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetLayoutHorizontal(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ScrollRect obj = LuaScriptMgr.GetUnityObject<ScrollRect>(L, 1);
		obj.SetLayoutHorizontal();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetLayoutVertical(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ScrollRect obj = LuaScriptMgr.GetUnityObject<ScrollRect>(L, 1);
		obj.SetLayoutVertical();
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

