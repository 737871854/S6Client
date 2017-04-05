using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LuaInterface;

public class ListStringWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("Add", Add),
		new LuaMethod("AddRange", AddRange),
		new LuaMethod("AsReadOnly", AsReadOnly),
		new LuaMethod("BinarySearch", BinarySearch),
		new LuaMethod("Clear", Clear),
		new LuaMethod("Contains", Contains),
		new LuaMethod("CopyTo", CopyTo),
		new LuaMethod("Exists", Exists),
		new LuaMethod("Find", Find),
		new LuaMethod("FindAll", FindAll),
		new LuaMethod("FindIndex", FindIndex),
		new LuaMethod("FindLast", FindLast),
		new LuaMethod("FindLastIndex", FindLastIndex),
		new LuaMethod("ForEach", ForEach),
		new LuaMethod("GetEnumerator", GetEnumerator),
		new LuaMethod("GetRange", GetRange),
		new LuaMethod("IndexOf", IndexOf),
		new LuaMethod("Insert", Insert),
		new LuaMethod("InsertRange", InsertRange),
		new LuaMethod("LastIndexOf", LastIndexOf),
		new LuaMethod("Remove", Remove),
		new LuaMethod("RemoveAll", RemoveAll),
		new LuaMethod("RemoveAt", RemoveAt),
		new LuaMethod("RemoveRange", RemoveRange),
		new LuaMethod("Reverse", Reverse),
		new LuaMethod("Sort", Sort),
		new LuaMethod("ToArray", ToArray),
		new LuaMethod("TrimExcess", TrimExcess),
		new LuaMethod("TrueForAll", TrueForAll),
		new LuaMethod("get_Item", get_Item),
		new LuaMethod("set_Item", set_Item),
		new LuaMethod("New", _CreateListString),
		new LuaMethod("GetClassType", GetClassType),
	};

	static LuaField[] fields = new LuaField[]
	{
		new LuaField("Capacity", get_Capacity, set_Capacity),
		new LuaField("Count", get_Count, null),
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateListString(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);


		if (count == 0)
		{
			List<string> obj = new List<string>();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else if (count == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(int)))
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			List<string> obj = new List<string>(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else if (count == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(IEnumerable<string>)))
		{
			IEnumerable<string> arg0 = LuaScriptMgr.GetNetObject<IEnumerable<string>>(L, 1);
			List<string> obj = new List<string>(arg0);
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: List<string>.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(List<string>));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "ListString", typeof(List<string>), regs, fields, typeof(System.Object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Capacity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		List<string> obj = (List<string>)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Capacity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Capacity on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Capacity);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Count(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		List<string> obj = (List<string>)o;

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
	static int set_Capacity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		List<string> obj = (List<string>)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Capacity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Capacity on a nil value");
			}
		}

		obj.Capacity = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Add(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		obj.Add(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddRange(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
		IEnumerable<string> arg0 = LuaScriptMgr.GetNetObject<IEnumerable<string>>(L, 2);
		obj.AddRange(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AsReadOnly(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
		ReadOnlyCollection<string> o = obj.AsReadOnly();
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BinarySearch(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		if (count == 2)
		{
			List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
			string arg0 = LuaScriptMgr.GetLuaString(L, 2);
			int o = obj.BinarySearch(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3)
		{
			List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
			string arg0 = LuaScriptMgr.GetLuaString(L, 2);
			IComparer<string> arg1 = LuaScriptMgr.GetNetObject<IComparer<string>>(L, 3);
			int o = obj.BinarySearch(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 5)
		{
			List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			string arg2 = LuaScriptMgr.GetLuaString(L, 4);
			IComparer<string> arg3 = LuaScriptMgr.GetNetObject<IComparer<string>>(L, 5);
			int o = obj.BinarySearch(arg0,arg1,arg2,arg3);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: List<string>.BinarySearch");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Clear(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
		obj.Clear();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Contains(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.Contains(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CopyTo(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		if (count == 2)
		{
			List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
			string[] objs0 = LuaScriptMgr.GetArrayString(L, 2);
			obj.CopyTo(objs0);
			return 0;
		}
		else if (count == 3)
		{
			List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
			string[] objs0 = LuaScriptMgr.GetArrayString(L, 2);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			obj.CopyTo(objs0,arg1);
			return 0;
		}
		else if (count == 5)
		{
			List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			string[] objs1 = LuaScriptMgr.GetArrayString(L, 3);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 4);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 5);
			obj.CopyTo(arg0,objs1,arg2,arg3);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: List<string>.CopyTo");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Exists(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
		Predicate<string> arg0 = LuaScriptMgr.GetNetObject<Predicate<string>>(L, 2);
		bool o = obj.Exists(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Find(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
		Predicate<string> arg0 = LuaScriptMgr.GetNetObject<Predicate<string>>(L, 2);
		string o = obj.Find(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindAll(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
		Predicate<string> arg0 = LuaScriptMgr.GetNetObject<Predicate<string>>(L, 2);
		List<string> o = obj.FindAll(arg0);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindIndex(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		if (count == 2)
		{
			List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
			Predicate<string> arg0 = LuaScriptMgr.GetNetObject<Predicate<string>>(L, 2);
			int o = obj.FindIndex(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3)
		{
			List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			Predicate<string> arg1 = LuaScriptMgr.GetNetObject<Predicate<string>>(L, 3);
			int o = obj.FindIndex(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4)
		{
			List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			Predicate<string> arg2 = LuaScriptMgr.GetNetObject<Predicate<string>>(L, 4);
			int o = obj.FindIndex(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: List<string>.FindIndex");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindLast(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
		Predicate<string> arg0 = LuaScriptMgr.GetNetObject<Predicate<string>>(L, 2);
		string o = obj.FindLast(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindLastIndex(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		if (count == 2)
		{
			List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
			Predicate<string> arg0 = LuaScriptMgr.GetNetObject<Predicate<string>>(L, 2);
			int o = obj.FindLastIndex(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3)
		{
			List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			Predicate<string> arg1 = LuaScriptMgr.GetNetObject<Predicate<string>>(L, 3);
			int o = obj.FindLastIndex(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4)
		{
			List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			Predicate<string> arg2 = LuaScriptMgr.GetNetObject<Predicate<string>>(L, 4);
			int o = obj.FindLastIndex(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: List<string>.FindLastIndex");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ForEach(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
		Action<string> arg0 = LuaScriptMgr.GetNetObject<Action<string>>(L, 2);
		obj.ForEach(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEnumerator(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
		List<string>.Enumerator o = obj.GetEnumerator();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRange(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
		List<string> o = obj.GetRange(arg0,arg1);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IndexOf(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		if (count == 2)
		{
			List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
			string arg0 = LuaScriptMgr.GetLuaString(L, 2);
			int o = obj.IndexOf(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3)
		{
			List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
			string arg0 = LuaScriptMgr.GetLuaString(L, 2);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			int o = obj.IndexOf(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4)
		{
			List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
			string arg0 = LuaScriptMgr.GetLuaString(L, 2);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 4);
			int o = obj.IndexOf(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: List<string>.IndexOf");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Insert(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		string arg1 = LuaScriptMgr.GetLuaString(L, 3);
		obj.Insert(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InsertRange(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		IEnumerable<string> arg1 = LuaScriptMgr.GetNetObject<IEnumerable<string>>(L, 3);
		obj.InsertRange(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LastIndexOf(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		if (count == 2)
		{
			List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
			string arg0 = LuaScriptMgr.GetLuaString(L, 2);
			int o = obj.LastIndexOf(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3)
		{
			List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
			string arg0 = LuaScriptMgr.GetLuaString(L, 2);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			int o = obj.LastIndexOf(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4)
		{
			List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
			string arg0 = LuaScriptMgr.GetLuaString(L, 2);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 4);
			int o = obj.LastIndexOf(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: List<string>.LastIndexOf");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Remove(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		bool o = obj.Remove(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveAll(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
		Predicate<string> arg0 = LuaScriptMgr.GetNetObject<Predicate<string>>(L, 2);
		int o = obj.RemoveAll(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveAt(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		obj.RemoveAt(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveRange(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
		obj.RemoveRange(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Reverse(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		if (count == 1)
		{
			List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
			obj.Reverse();
			return 0;
		}
		else if (count == 3)
		{
			List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			obj.Reverse(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: List<string>.Reverse");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Sort(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
			obj.Sort();
			return 0;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(List<string>), typeof(Comparison<string>)))
		{
			List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
			Comparison<string> arg0 = LuaScriptMgr.GetNetObject<Comparison<string>>(L, 2);
			obj.Sort(arg0);
			return 0;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(List<string>), typeof(IComparer<string>)))
		{
			List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
			IComparer<string> arg0 = LuaScriptMgr.GetNetObject<IComparer<string>>(L, 2);
			obj.Sort(arg0);
			return 0;
		}
		else if (count == 4)
		{
			List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			IComparer<string> arg2 = LuaScriptMgr.GetNetObject<IComparer<string>>(L, 4);
			obj.Sort(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: List<string>.Sort");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ToArray(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
		string[] o = obj.ToArray();
		LuaScriptMgr.PushArray(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TrimExcess(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
		obj.TrimExcess();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TrueForAll(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
		Predicate<string> arg0 = LuaScriptMgr.GetNetObject<Predicate<string>>(L, 2);
		bool o = obj.TrueForAll(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Item(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		string o = obj[arg0];
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Item(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		List<string> obj = LuaScriptMgr.GetNetObject<List<string>>(L, 1);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
		string arg1 = LuaScriptMgr.GetLuaString(L, 3);
		obj[arg0] = arg1;
		return 0;
	}
}

