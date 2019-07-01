using System;
using System.Collections.Generic;

namespace Milky.Collections
{
	/// <summary>
	/// 検索が高速なリスト
	/// </summary>
	/// <typeparam name="TKey">検索に用いるキーの型</typeparam>
	/// <typeparam name="TValue">格納する要素の型</typeparam>
	public class MapList<TKey, TValue>
	{
		#region fields

		/// <summary>検索用のマップ</summary>
		private Dictionary<TKey, TValue> map = new Dictionary<TKey, TValue>();

		/// <summary>格納用のリスト</summary>
		private List<TValue> container = new List<TValue>();

		#endregion fields

		#region 構築

		/// <summary>
		///
		/// </summary>
		public MapList()
		{
		}

		#endregion 構築

		#region プロパティ

		/// <summary>
		/// 要素の数を取得する
		/// </summary>
		public int Count { get { return container.Count; } }

		/// <summary>
		/// 指定したキーに関連付けられた要素を取得する
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public TValue this[TKey key] {
			get
			{
				if (ContainsKey(key))
				{
					return map[key];
				}
				else
				{
					return default(TValue);
				}
			}
		}

		/// <summary>
		/// 指定したインデックスの要素を取得する
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public TValue this[int index] {
			get
			{
				if (index < 0)
					return default(TValue);

				if (container.Count > index)
				{
					return container[index];
				}
				else
				{
					return default(TValue);
				}
			}
		}

		/// <summary>
		/// コンテナの要素を取得する
		/// </summary>
		public IEnumerable<TValue> Values {
			get
			{
				foreach (TValue value in container)
				{
					yield return value;
				}
			}
		}

		/// <summary>
		/// コンテナのキーを取得する
		/// </summary>
		public IEnumerable<TKey> Keys {
			get
			{
				foreach (var v in map)
				{
					yield return v.Key;
				}
			}
		}

		#endregion プロパティ

		#region 操作

		/// <summary>
		/// 指定したキーが含まれているかどうか
		/// </summary>
		/// <param name="key">キー</param>
		/// <returns>含まれていればtrue</returns>
		public bool ContainsKey(TKey key)
		{
			return map.ContainsKey(key);
		}

		/// <summary>
		/// 要素を登録する
		/// </summary>
		/// <param name="key">キー</param>
		/// <param name="value">要素</param>
		public void Add(TKey key, TValue value)
		{
			if (ContainsKey(key) == false)
			{
				map.Add(key, value);
				container.Add(value);
			}
		}

		/// <summary>
		/// 指定したキーと要素を削除する
		/// </summary>
		/// <param name="key">キー</param>
		public void RemoveAt(TKey key)
		{
			TValue value = map[key];
			if (value != null)
			{
				container.Remove(value);
				map.Remove(key);
			}
		}

		/// <summary>
		/// 要素を全て消去する
		/// </summary>
		public void Clear()
		{
			map.Clear();
			container.Clear();
		}

		/// <summary>
		/// 指定した論理で要素を検索する
		/// </summary>
		/// <param name="match">検索ロジック</param>
		/// <returns>見つかった要素</returns>
		public TValue Find(Predicate<TValue> match)
		{
			return this.container.Find(match);
		}

		/// <summary>
		/// 指定した論理で見つかった要素全てを取得する
		/// </summary>
		/// <param name="match">検索ロジック</param>
		/// <returns>見つかった要素</returns>
		public List<TValue> FindAll(Predicate<TValue> match)
		{
			return this.container.FindAll(match);
		}

		/// <summary>
		/// 指定した論理で見つかった要素のインデクスを取得
		/// </summary>
		/// <param name="match">検索ロジック</param>
		/// <returns>見つかった要素のインデックス。見つからなかった場合は-1が返る。</returns>
		public int FindIndex(Predicate<TValue> match)
		{
			return this.container.FindIndex(match);
		}

		/// <summary>
		/// 要素１つずつ指定したアクションを実行する
		/// </summary>
		/// <param name="action">アクション</param>
		public void ForEach(Action<TValue> action)
		{
			foreach (TValue value in container)
			{
				action(value);
			}
		}

		#endregion 操作
	}
}