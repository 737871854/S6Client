using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using LuaInterface;
using Object = UnityEngine.Object;

public class SelectableWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("IsInteractable", IsInteractable),
		new LuaMethod("FindSelectable", FindSelectable),
		new LuaMethod("FindSelectableOnLeft", FindSelectableOnLeft),
		new LuaMethod("FindSelectableOnRight", FindSelectableOnRight),
		new LuaMethod("FindSelectableOnUp", FindSelectableOnUp),
		new LuaMethod("FindSelectableOnDown", FindSelectableOnDown),
		new LuaMethod("OnMove", OnMove),
		new LuaMethod("OnPointerDown", OnPointerDown),
		new LuaMethod("OnPointerUp", OnPointerUp),
		new LuaMethod("OnPointerEnter", OnPointerEnter),
		new LuaMethod("OnPointerExit", OnPointerExit),
		new LuaMethod("OnSelect", OnSelect),
		new LuaMethod("OnDeselect", OnDeselect),
		new LuaMethod("Select", Select),
		new LuaMethod("New", _CreateSelectable),
		new LuaMethod("GetClassType", GetClassType),
		new LuaMethod("__eq", Lua_Eq),
	};

	static LuaField[] fields = new LuaField[]
	{
		new LuaField("allSelectables", get_allSelectables, null),
		new LuaField("navigation", get_navigation, set_navigation),
		new LuaField("transition", get_transition, set_transition),
		new LuaField("colors", get_colors, set_colors),
		new LuaField("spriteState", get_spriteState, set_spriteState),
		new LuaField("animationTriggers", get_animationTriggers, set_animationTriggers),
		new LuaField("targetGraphic", get_targetGraphic, set_targetGraphic),
		new LuaField("interactable", get_interactable, set_interactable),
		new LuaField("image", get_image, set_image),
		new LuaField("animator", get_animator, null),
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSelectable(IntPtr L)
	{
		LuaDLL.luaL_error(L, "Selectable class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(Selectable));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.UI.Selectable", typeof(Selectable), regs, fields, typeof(UnityEngine.EventSystems.UIBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_allSelectables(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, Selectable.allSelectables);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_navigation(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Selectable obj = (Selectable)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name navigation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index navigation on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.navigation);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_transition(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Selectable obj = (Selectable)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name transition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index transition on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.transition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_colors(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Selectable obj = (Selectable)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name colors");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index colors on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.colors);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_spriteState(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Selectable obj = (Selectable)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name spriteState");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index spriteState on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.spriteState);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_animationTriggers(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Selectable obj = (Selectable)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name animationTriggers");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index animationTriggers on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.animationTriggers);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_targetGraphic(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Selectable obj = (Selectable)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name targetGraphic");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index targetGraphic on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.targetGraphic);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_interactable(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Selectable obj = (Selectable)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name interactable");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index interactable on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.interactable);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_image(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Selectable obj = (Selectable)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name image");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index image on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.image);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_animator(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Selectable obj = (Selectable)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name animator");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index animator on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.animator);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_navigation(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Selectable obj = (Selectable)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name navigation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index navigation on a nil value");
			}
		}

		obj.navigation = LuaScriptMgr.GetNetObject<Navigation>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_transition(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Selectable obj = (Selectable)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name transition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index transition on a nil value");
			}
		}

		obj.transition = LuaScriptMgr.GetNetObject<UnityEngine.UI.Selectable.Transition>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_colors(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Selectable obj = (Selectable)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name colors");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index colors on a nil value");
			}
		}

		obj.colors = LuaScriptMgr.GetNetObject<ColorBlock>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_spriteState(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Selectable obj = (Selectable)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name spriteState");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index spriteState on a nil value");
			}
		}

		obj.spriteState = LuaScriptMgr.GetNetObject<SpriteState>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_animationTriggers(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Selectable obj = (Selectable)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name animationTriggers");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index animationTriggers on a nil value");
			}
		}

		obj.animationTriggers = LuaScriptMgr.GetNetObject<AnimationTriggers>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_targetGraphic(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Selectable obj = (Selectable)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name targetGraphic");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index targetGraphic on a nil value");
			}
		}

		obj.targetGraphic = LuaScriptMgr.GetUnityObject<Graphic>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_interactable(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Selectable obj = (Selectable)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name interactable");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index interactable on a nil value");
			}
		}

		obj.interactable = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_image(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Selectable obj = (Selectable)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name image");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index image on a nil value");
			}
		}

		obj.image = LuaScriptMgr.GetUnityObject<Image>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsInteractable(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Selectable obj = LuaScriptMgr.GetUnityObject<Selectable>(L, 1);
		bool o = obj.IsInteractable();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindSelectable(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Selectable obj = LuaScriptMgr.GetUnityObject<Selectable>(L, 1);
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 2);
		Selectable o = obj.FindSelectable(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindSelectableOnLeft(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Selectable obj = LuaScriptMgr.GetUnityObject<Selectable>(L, 1);
		Selectable o = obj.FindSelectableOnLeft();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindSelectableOnRight(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Selectable obj = LuaScriptMgr.GetUnityObject<Selectable>(L, 1);
		Selectable o = obj.FindSelectableOnRight();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindSelectableOnUp(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Selectable obj = LuaScriptMgr.GetUnityObject<Selectable>(L, 1);
		Selectable o = obj.FindSelectableOnUp();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindSelectableOnDown(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Selectable obj = LuaScriptMgr.GetUnityObject<Selectable>(L, 1);
		Selectable o = obj.FindSelectableOnDown();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnMove(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Selectable obj = LuaScriptMgr.GetUnityObject<Selectable>(L, 1);
		AxisEventData arg0 = LuaScriptMgr.GetNetObject<AxisEventData>(L, 2);
		obj.OnMove(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerDown(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Selectable obj = LuaScriptMgr.GetUnityObject<Selectable>(L, 1);
		PointerEventData arg0 = LuaScriptMgr.GetNetObject<PointerEventData>(L, 2);
		obj.OnPointerDown(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerUp(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Selectable obj = LuaScriptMgr.GetUnityObject<Selectable>(L, 1);
		PointerEventData arg0 = LuaScriptMgr.GetNetObject<PointerEventData>(L, 2);
		obj.OnPointerUp(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerEnter(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Selectable obj = LuaScriptMgr.GetUnityObject<Selectable>(L, 1);
		PointerEventData arg0 = LuaScriptMgr.GetNetObject<PointerEventData>(L, 2);
		obj.OnPointerEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerExit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Selectable obj = LuaScriptMgr.GetUnityObject<Selectable>(L, 1);
		PointerEventData arg0 = LuaScriptMgr.GetNetObject<PointerEventData>(L, 2);
		obj.OnPointerExit(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnSelect(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Selectable obj = LuaScriptMgr.GetUnityObject<Selectable>(L, 1);
		BaseEventData arg0 = LuaScriptMgr.GetNetObject<BaseEventData>(L, 2);
		obj.OnSelect(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDeselect(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Selectable obj = LuaScriptMgr.GetUnityObject<Selectable>(L, 1);
		BaseEventData arg0 = LuaScriptMgr.GetNetObject<BaseEventData>(L, 2);
		obj.OnDeselect(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Select(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Selectable obj = LuaScriptMgr.GetUnityObject<Selectable>(L, 1);
		obj.Select();
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

