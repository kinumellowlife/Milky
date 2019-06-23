using System;
using System.Collections.Generic;

namespace Milky.Collections
{
	public class TupleDictionary<Key1, Key2, Key3, Value> : Dictionary<Tuple<Key1, Key2, Key3>, Value>
	{
		public Value this[Key1 key1, Key2 key2, Key3 key3] {
			get
			{
				var key = Tuple.Create(key1, key2, key3);
				if (ContainsKey(key))
					return base[key];
				else
					return default(Value);
			}
			set
			{
				var key = Tuple.Create(key1, key2, key3);
				if (ContainsKey(key))
					base[key] = value;
				else
					Add(key, value);
			}
		}

		public bool ContainsKey(Key1 key1, Key2 key2, Key3 key3)
		{
			return base.ContainsKey(Tuple.Create(key1, key2, key3));
		}

		public void Add(Key1 key1, Key2 key2, Key3 key3, Value value)
		{
			var key = Tuple.Create(key1, key2, key3);
			if (!base.ContainsKey(key))
				base.Add(key, value);
		}

		public void Remove(Key1 key1, Key2 key2, Key3 key3)
		{
			var key = Tuple.Create(key1, key2, key3);
			if (base.ContainsKey(key))
				base.Remove(key);
		}
	}

	public class TupleDictionary<Key1, Key2, Value> : Dictionary<Tuple<Key1, Key2>, Value>
	{
		public Value this[Key1 key1, Key2 key2] {
			get
			{
				var key = Tuple.Create(key1, key2);
				if (ContainsKey(key))
					return base[key];
				else
					return default(Value);
			}
			set
			{
				var key = Tuple.Create(key1, key2);
				if (ContainsKey(key))
					base[key] = value;
				else
					Add(key, value);
			}
		}

		public bool ContainsKey(Key1 key1, Key2 key2)
		{
			return base.ContainsKey(Tuple.Create(key1, key2));
		}

		public void Add(Key1 key1, Key2 key2, Value value)
		{
			var key = Tuple.Create(key1, key2);
			if (!base.ContainsKey(key))
				base.Add(key, value);
		}

		public void Remove(Key1 key1, Key2 key2)
		{
			var key = Tuple.Create(key1, key2);
			if (base.ContainsKey(key))
				base.Remove(key);
		}
	}
}