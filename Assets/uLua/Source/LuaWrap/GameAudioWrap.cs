using System;
using LuaInterface;

public class GameAudioWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("InitSceneAudios", InitSceneAudios),
		new LuaMethod("InitBetAudios", InitBetAudios),
		new LuaMethod("PlayAudio", PlayAudio),
		new LuaMethod("Pause", Pause),
		new LuaMethod("UnPause", UnPause),
		new LuaMethod("New", _CreateGameAudio),
		new LuaMethod("GetClassType", GetClassType),
	};

	static LuaField[] fields = new LuaField[]
	{
		new LuaField("Instance", get_Instance, null),
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGameAudio(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			GameAudio obj = new GameAudio();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GameAudio.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(GameAudio));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "GameAudio", typeof(GameAudio), regs, fields, typeof(System.Object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Instance(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, GameAudio.Instance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InitSceneAudios(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameAudio obj = LuaScriptMgr.GetNetObject<GameAudio>(L, 1);
		obj.InitSceneAudios();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InitBetAudios(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameAudio obj = LuaScriptMgr.GetNetObject<GameAudio>(L, 1);
		obj.InitBetAudios();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayAudio(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameAudio obj = LuaScriptMgr.GetNetObject<GameAudio>(L, 1);
        int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		obj.PlayAudio((AudioName)arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Pause(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameAudio obj = LuaScriptMgr.GetNetObject<GameAudio>(L, 1);
        int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
        obj.Pause((AudioName)arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnPause(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameAudio obj = LuaScriptMgr.GetNetObject<GameAudio>(L, 1);
        int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
        obj.UnPause((AudioName)arg0);
		return 0;
	}
}

