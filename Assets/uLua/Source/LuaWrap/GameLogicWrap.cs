using System;
using LuaInterface;

public class GameLogicWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("StartGame", StartGame),
		new LuaMethod("ReadyToGo", ReadyToGo),
		new LuaMethod("EndGame", EndGame),
		new LuaMethod("New", _CreateGameLogic),
		new LuaMethod("GetClassType", GetClassType),
	};

	static LuaField[] fields = new LuaField[]
	{
		new LuaField("state", get_state, set_state),
		new LuaField("Instance", get_Instance, null),
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGameLogic(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			GameLogic obj = new GameLogic();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GameLogic.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(GameLogic));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "GameLogic", typeof(GameLogic), regs, fields, typeof(System.Object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_state(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameLogic obj = (GameLogic)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name state");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index state on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.state);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Instance(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, GameLogic.Instance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_state(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		GameLogic obj = (GameLogic)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name state");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index state on a nil value");
			}
		}

		obj.state = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StartGame(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameLogic obj = LuaScriptMgr.GetNetObject<GameLogic>(L, 1);
		obj.StartGame();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReadyToGo(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameLogic obj = LuaScriptMgr.GetNetObject<GameLogic>(L, 1);
		obj.ReadyToGo();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EndGame(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		GameLogic obj = LuaScriptMgr.GetNetObject<GameLogic>(L, 1);
		obj.EndGame();
		return 0;
	}
}

