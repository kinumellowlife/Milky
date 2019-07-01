using System;
using System.Collections.Generic;

namespace Milky.Collections
{
	/// <summary>
	/// KeyがTulple<Key1,Key2,Key3>なDictionary</Key1>
	/// </summary>
	/// <typeparam name="Key1">キーの型１</typeparam>
	/// <typeparam name="Key2">キーの型２</typeparam>
	/// <typeparam name="Key3">キーの型３</typeparam>
	/// <typeparam name="Value">値の型</typeparam>
	public class TupleDictionary<Key1, Key2, Key3, Value> : Dictionary<Tuple<Key1, Key2, Key3>, Value>
	{
		#region public API

		/// <summary>
		/// 値の取得
		/// </summary>
		/// <param name="key1">キー１</param>
		/// <param name="key2">キー２</param>
		/// <param name="key3">キー３</param>
		/// <returns>値</returns>
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

		/// <summary>
		/// 指定したキーが存在するかどうか
		/// </summary>
		/// <param name="key1">キー１</param>
		/// <param name="key2">キー２</param>
		/// <param name="key3">キー３</param>
		/// <returns>存在すればTrue</returns>
		public bool ContainsKey(Key1 key1, Key2 key2, Key3 key3)
		{
			return base.ContainsKey(Tuple.Create(key1, key2, key3));
		}

		/// <summary>
		/// 要素を追加する
		/// </summary>
		/// <param name="key1">キー１</param>
		/// <param name="key2">キー２</param>
		/// <param name="key3">キー３</param>
		/// <returns>値</returns>
		public void Add(Key1 key1, Key2 key2, Key3 key3, Value value)
		{
			var key = Tuple.Create(key1, key2, key3);
			if (!base.ContainsKey(key))
				base.Add(key, value);
		}

		/// <summary>
		/// 指定したキーの要素を削除する
		/// </summary>
		/// <param name="key1">キー１</param>
		/// <param name="key2">キー２</param>
		/// <param name="key3">キー３</param>
		public void Remove(Key1 key1, Key2 key2, Key3 key3)
		{
			var key = Tuple.Create(key1, key2, key3);
			if (base.ContainsKey(key))
				base.Remove(key);
		}

		#endregion public API
	}

	/// <summary>
	/// KeyがTulple<Key1,Key2>なDictionary</Key1>
	/// </summary>
	/// <typeparam name="Key1">キーの型１</typeparam>
	/// <typeparam name="Key2">キーの型２</typeparam>
	/// <typeparam name="Value">値の型</typeparam>
	public class TupleDictionary<Key1, Key2, Value> : Dictionary<Tuple<Key1, Key2>, Value>
	{
		#region public API

		/// <summary>
		/// 値の取得
		/// </summary>
		/// <param name="key1">キー１</param>
		/// <param name="key2">キー２</param>
		/// <returns>値</returns>
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

		/// <summary>
		/// 指定したキーが存在するかどうか
		/// </summary>
		/// <param name="key1">キー１</param>
		/// <param name="key2">キー２</param>
		/// <returns>存在すればTrue</returns>
		public bool ContainsKey(Key1 key1, Key2 key2)
		{
			return base.ContainsKey(Tuple.Create(key1, key2));
		}

		/// <summary>
		/// 要素を追加する
		/// </summary>
		/// <param name="key1">キー１</param>
		/// <param name="key2">キー２</param>
		/// <returns>値</returns>
		public void Add(Key1 key1, Key2 key2, Value value)
		{
			var key = Tuple.Create(key1, key2);
			if (!base.ContainsKey(key))
				base.Add(key, value);
		}

		/// <summary>
		/// 指定したキーの要素を削除する
		/// </summary>
		/// <param name="key1">キー１</param>
		/// <param name="key2">キー２</param>
		public void Remove(Key1 key1, Key2 key2)
		{
			var key = Tuple.Create(key1, key2);
			if (base.ContainsKey(key))
				base.Remove(key);
		}

		#endregion public API
	}
}