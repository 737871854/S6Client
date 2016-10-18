using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class AudioManagerWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("Init", Init),
		new LuaMethod("PlayOnPoint", PlayOnPoint),
		new LuaMethod("Play", Play),
		new LuaMethod("Play2D", Play2D),
		new LuaMethod("PlayBGM", PlayBGM),
		new LuaMethod("StopBGM", StopBGM),
		new LuaMethod("StopAll", StopAll),
		new LuaMethod("ClearOnInitScene", ClearOnInitScene),
		new LuaMethod("New", _CreateAudioManager),
		new LuaMethod("GetClassType", GetClassType),
		new LuaMethod("__eq", Lua_Eq),
	};

	static LuaField[] fields = new LuaField[]
	{
		new LuaField("m_BGM", get_m_BGM, set_m_BGM),
		new LuaField("isDebug", get_isDebug, set_isDebug),
		new LuaField("m_AudioName", get_m_AudioName, set_m_AudioName),
		new LuaField("m_Pos", get_m_Pos, set_m_Pos),
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateAudioManager(IntPtr L)
	{
		LuaDLL.luaL_error(L, "AudioManager class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(AudioManager));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "AudioManager", typeof(AudioManager), regs, fields, typeof(UnityEngine.MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_BGM(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AudioManager obj = (AudioManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_BGM");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_BGM on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_BGM);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isDebug(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AudioManager obj = (AudioManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isDebug");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isDebug on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isDebug);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_AudioName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AudioManager obj = (AudioManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_AudioName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_AudioName on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_AudioName);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_Pos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AudioManager obj = (AudioManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_Pos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_Pos on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.m_Pos);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_BGM(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AudioManager obj = (AudioManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_BGM");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_BGM on a nil value");
			}
		}

		obj.m_BGM = LuaScriptMgr.GetUnityObject<AudioObject>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isDebug(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AudioManager obj = (AudioManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isDebug");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isDebug on a nil value");
			}
		}

		obj.isDebug = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_AudioName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AudioManager obj = (AudioManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_AudioName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_AudioName on a nil value");
			}
		}

		obj.m_AudioName = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_Pos(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AudioManager obj = (AudioManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_Pos");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_Pos on a nil value");
			}
		}

		obj.m_Pos = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AudioManager obj = LuaScriptMgr.GetUnityObject<AudioManager>(L, 1);
		obj.Init();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayOnPoint(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		AudioManager obj = LuaScriptMgr.GetUnityObject<AudioManager>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		Vector3 arg1 = LuaScriptMgr.GetVector3(L, 3);
		string o = obj.PlayOnPoint(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Play(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		AudioManager obj = LuaScriptMgr.GetUnityObject<AudioManager>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		GameObject arg1 = LuaScriptMgr.GetUnityObject<GameObject>(L, 3);
		string o = obj.Play(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Play2D(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		AudioManager obj = LuaScriptMgr.GetUnityObject<AudioManager>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
		obj.Play2D(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayBGM(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		AudioManager obj = LuaScriptMgr.GetUnityObject<AudioManager>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
		obj.PlayBGM(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopBGM(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AudioManager obj = LuaScriptMgr.GetUnityObject<AudioManager>(L, 1);
		obj.StopBGM();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopAll(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AudioManager obj = LuaScriptMgr.GetUnityObject<AudioManager>(L, 1);
		obj.StopAll();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearOnInitScene(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AudioManager obj = LuaScriptMgr.GetUnityObject<AudioManager>(L, 1);
		obj.ClearOnInitScene();
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

