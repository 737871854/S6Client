using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class UtilWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("PushBufferToLua", PushBufferToLua),
		new LuaMethod("PosTween", PosTween),
		new LuaMethod("ScaleTween", ScaleTween),
		new LuaMethod("RotTween", RotTween),
		new LuaMethod("ColorTween", ColorTween),
		new LuaMethod("CGAlphaTween", CGAlphaTween),
		new LuaMethod("AlphaTween", AlphaTween),
		new LuaMethod("GetLocalIPAddress", GetLocalIPAddress),
		new LuaMethod("FindTransformByName", FindTransformByName),
		new LuaMethod("TEXT", TEXT),
		new LuaMethod("ScreenPointToLocalPointInRectangle", ScreenPointToLocalPointInRectangle),
		new LuaMethod("New", _CreateUtil),
		new LuaMethod("GetClassType", GetClassType),
	};

	static LuaField[] fields = new LuaField[]
	{
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUtil(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			Util obj = new Util();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Util.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(Util));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "Util", typeof(Util), regs, fields, typeof(UtilCommon));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PushBufferToLua(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		LuaFunction arg0 = LuaScriptMgr.GetLuaFunction(L, 1);
		byte[] objs1 = LuaScriptMgr.GetArrayNumber<byte>(L, 2);
		Util.PushBufferToLua(arg0,objs1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PosTween(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 7);
		GameObject arg0 = LuaScriptMgr.GetUnityObject<GameObject>(L, 1);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
		Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
		int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
		int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
		LuaFunction arg5 = LuaScriptMgr.GetLuaFunction(L, 6);
		bool arg6 = LuaScriptMgr.GetBoolean(L, 7);
		Util.PosTween(arg0,arg1,arg2,arg3,arg4,arg5,arg6);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ScaleTween(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 7);
		GameObject arg0 = LuaScriptMgr.GetUnityObject<GameObject>(L, 1);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
		Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
		int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
		int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
		LuaFunction arg5 = LuaScriptMgr.GetLuaFunction(L, 6);
		bool arg6 = LuaScriptMgr.GetBoolean(L, 7);
		Util.ScaleTween(arg0,arg1,arg2,arg3,arg4,arg5,arg6);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RotTween(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 7);
		GameObject arg0 = LuaScriptMgr.GetUnityObject<GameObject>(L, 1);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
		Quaternion arg2 = LuaScriptMgr.GetQuaternion(L, 3);
		int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
		int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
		LuaFunction arg5 = LuaScriptMgr.GetLuaFunction(L, 6);
		bool arg6 = LuaScriptMgr.GetBoolean(L, 7);
		Util.RotTween(arg0,arg1,arg2,arg3,arg4,arg5,arg6);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ColorTween(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 7);
		GameObject arg0 = LuaScriptMgr.GetUnityObject<GameObject>(L, 1);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
		Color arg2 = LuaScriptMgr.GetColor(L, 3);
		int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
		int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
		LuaFunction arg5 = LuaScriptMgr.GetLuaFunction(L, 6);
		bool arg6 = LuaScriptMgr.GetBoolean(L, 7);
		Util.ColorTween(arg0,arg1,arg2,arg3,arg4,arg5,arg6);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CGAlphaTween(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 7);
		GameObject arg0 = LuaScriptMgr.GetUnityObject<GameObject>(L, 1);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
		float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
		int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
		int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
		LuaFunction arg5 = LuaScriptMgr.GetLuaFunction(L, 6);
		bool arg6 = LuaScriptMgr.GetBoolean(L, 7);
		Util.CGAlphaTween(arg0,arg1,arg2,arg3,arg4,arg5,arg6);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AlphaTween(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 7);
		GameObject arg0 = LuaScriptMgr.GetUnityObject<GameObject>(L, 1);
		float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
		float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
		int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
		int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
		LuaFunction arg5 = LuaScriptMgr.GetLuaFunction(L, 6);
		bool arg6 = LuaScriptMgr.GetBoolean(L, 7);
		Util.AlphaTween(arg0,arg1,arg2,arg3,arg4,arg5,arg6);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLocalIPAddress(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		string o = Util.GetLocalIPAddress();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindTransformByName(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Transform arg0 = LuaScriptMgr.GetUnityObject<Transform>(L, 1);
		string arg1 = LuaScriptMgr.GetLuaString(L, 2);
		Transform o = Util.FindTransformByName(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TEXT(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string o = Util.TEXT(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ScreenPointToLocalPointInRectangle(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		RectTransform arg0 = LuaScriptMgr.GetUnityObject<RectTransform>(L, 1);
		Vector2 arg1 = LuaScriptMgr.GetVector2(L, 2);
		Camera arg2 = LuaScriptMgr.GetUnityObject<Camera>(L, 3);
		Vector2 o = Util.ScreenPointToLocalPointInRectangle(arg0,arg1,arg2);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

