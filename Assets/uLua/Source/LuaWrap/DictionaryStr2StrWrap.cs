using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using LuaInterface;

public class DictionaryStr2StrWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("get_Item", get_Item),
		new LuaMethod("set_Item", set_Item),
		new LuaMethod("Add", Add),
		new LuaMethod("Clear", Clear),
		new LuaMethod("ContainsKey", ContainsKey),
		new LuaMethod("ContainsValue", ContainsValue),
		new LuaMethod("GetObjectData", GetObjectData),
		new LuaMethod("OnDeserialization", OnDeserialization),
		new LuaMethod("Remove", Remove),
		new LuaMethod("TryGetValue", TryGetValue),
		new LuaMethod("GetEnumerator", GetEnumerator),
		new LuaMethod("New", _CreateDictionaryStr2Str),
		new LuaMethod("GetClassType", GetClassType),
	};

	static LuaField[] fields = new LuaField[]
	{
		new LuaField("Count", get_Count, null),
		new LuaField("Comparer", get_Comparer, null),
		new LuaField("Keys", get_Keys, null),
		new LuaField("Values", get_Values, null),
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateDictionaryStr2Str(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);


		if (count == 0)
		{
			Dictionary<string,string> obj = new Dictionary<string,string>();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else if (count == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(int)))
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			Dictionary<string,string> obj = new Dictionary<string,string>(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else if (count == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(IDictionary<string,string>)))
		{
			IDictionary<string,string> arg0 = LuaScriptMgr.GetNetObject<IDictionary<string,string>>(L, 1);
			Dictionary<string,string> obj = new Dictionary<string,string>(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else if (count == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(IEqualityComparer<string>)))
		{
			IEqualityComparer<string> arg0 = LuaScriptMgr.GetNetObject<IEqualityComparer<string>>(L, 1);
			Dictionary<string,string> obj = new Dictionary<string,string>(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(int), typeof(IEqualityComparer<string>)))
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			IEqualityComparer<string> arg1 = LuaScriptMgr.GetNetObject<IEqualityComparer<string>>(L, 2);
			Dictionary<string,string> obj = new Dictionary<string,string>(arg0,arg1);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(IDictionary<string,string>), typeof(IEqualityComparer<string>)))
		{
			IDictionary<string,string> arg0 = LuaScriptMgr.GetNetObject<IDictionary<string,string>>(L, 1);
			IEqualityComparer<string> arg1 = LuaScriptMgr.GetNetObject<IEqualityComparer<string>>(L, 2);
			Dictionary<string,string> obj = new Dictionary<string,string>(arg0,arg1);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Dictionary<string,string>.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(Dictionary<string,string>));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "DictionaryStr2Str", typeof(Dictionary<string,string>), regs, fields, typeof(System.Object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Count(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Dictionary<string,string> obj = (Dictionary<string,string>)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Count");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Count on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Count);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Comparer(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Dictionary<string,string> obj = (Dictionary<string,string>)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Comparer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Comparer on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.Comparer);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Keys(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Dictionary<string,string> obj = (Dictionary<string,string>)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Keys");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Keys on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.Keys);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Values(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		Dictionary<string,string> obj = (Dictionary<string,string>)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Values");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Values on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.Values);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Item(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Dictionary<string,string> obj = LuaScriptMgr.GetNetObject<Dictionary<string,string>>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		string o = obj[arg0];
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Item(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Dictionary<string,string> obj = LuaScriptMgr.GetNetObject<Dictionary<string,string>>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		string arg1 = LuaScriptMgr.GetLuaString(L, 3);
		obj[arg0] = arg1;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Add(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Dictionary<string,string> obj = LuaScriptMgr.GetNetObject<Dictionary<string,string>>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		string arg1 = LuaScriptMgr.GetLuaString(L, 3);
		obj.Add(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Clear(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Dictionary<string,string> obj = LuaScriptMgr.GetNetObject<Dictionary<string,string>>(L, 1);
		obj.Clear();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ContainsKey(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Dictionary<string,string> obj = LuaScriptMgr.GetNetObject<Dictionary<string,string>>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ContainsKey(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ContainsValue(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Dictionary<string,string> obj = LuaScriptMgr.GetNetObject<Dictionary<string,string>>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.ContainsValue(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetObjectData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Dictionary<string,string> obj = LuaScriptMgr.GetNetObject<Dictionary<string,string>>(L, 1);
		SerializationInfo arg0 = LuaScriptMgr.GetNetObject<SerializationInfo>(L, 2);
		StreamingContext arg1 = LuaScriptMgr.GetNetObject<StreamingContext>(L, 3);
		obj.GetObjectData(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDeserialization(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Dictionary<string,string> obj = LuaScriptMgr.GetNetObject<Dictionary<string,string>>(L, 1);
		object arg0 = LuaScriptMgr.GetVarObject(L, 2);
		obj.OnDeserialization(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Remove(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Dictionary<string,string> obj = LuaScriptMgr.GetNetObject<Dictionary<string,string>>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.Remove(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TryGetValue(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Dictionary<string,string> obj = LuaScriptMgr.GetNetObject<Dictionary<string,string>>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		string arg1 = null;
		bool o = obj.TryGetValue(arg0,out arg1);
		LuaScriptMgr.Push(L, o);
		LuaScriptMgr.Push(L, arg1);
		return 2;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEnumerator(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Dictionary<string,string> obj = LuaScriptMgr.GetNetObject<Dictionary<string,string>>(L, 1);
		Dictionary<string,string>.Enumerator o = obj.GetEnumerator();
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

