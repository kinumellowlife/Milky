using System;
using System.Collections.Generic;
using System.Linq;

namespace Milky.Extensions
{
	static public class ListExtension
	{
		/// <summary>
		/// 指定条件に適合する要素を探す
		/// </summary>
		/// <typeparam name="T">型</typeparam>
		/// <param name="array">配列</param>
		/// <param name="match">条件式</param>
		/// <returns>要素</returns>
		static public T Find<T>(this T[] array, Predicate<T> match)
		{
			return Array.Find(array, match);
		}

		/// <summary>
		/// 指定条件に適合する要素のインデックスを求める
		/// </summary>
		/// <typeparam name="T">型</typeparam>
		/// <param name="array">配列</param>
		/// <param name="match">条件式</param>
		/// <returns>見つかればインデックス、見つからなければ－１</returns>
		static public int FindIndex<T>(this T[] array, Predicate<T> match)
		{
			List<int> a;
			return Array.FindIndex<T>(array, match);
		}

		/// <summary>
		/// 指定要素に対し順番にアクションを実行する
		/// </summary>
		/// <typeparam name="T">型</typeparam>
		/// <param name="list">リスト</param>
		/// <param name="action">アクション</param>
		static public void ForEach<T>(this IEnumerable<T> list, Action<T> action)
		{
			foreach (var elem in list)
			{
				action(elem);
			}
		}

		/// <summary>
		/// 指定条件に合致する要素に対し順次アクションを実行する
		/// </summary>
		/// <typeparam name="T">型</typeparam>
		/// <param name="list">リスト</param>
		/// <param name="match">条件式</param>
		/// <param name="action">アクション</param>
		static public void ForEachIf<T>(this IEnumerable<T> list, Predicate<T> match, Action<T> action)
		{
			foreach (var elem in list)
			{
				if (match(elem))
					action(elem);
			}
		}

		/// <summary>
		/// 指定要素に対し、要素のインデックス付きでアクションを実行する
		/// </summary>
		/// <typeparam name="T">型</typeparam>
		/// <param name="list">リスト</param>
		/// <param name="action">アクション</param>
		static public void ForAll<T>(this IEnumerable<T> list, Action<int, T> action)
		{
			if (action == null)
				return;
			foreach (var ai in list.Select((data, index) => new { data, index }))
			{
				action(ai.index, ai.data);
			}
		}

		/// <summary>
		/// 指定条件に合致する要素に対し、要素のインデックス付きでアクションを実行する
		/// </summary>
		/// <typeparam name="T">型</typeparam>
		/// <param name="list">リスト</param>
		/// <param name="match">条件</param>
		/// <param name="action">アクション</param>
		static public void ForAllIf<T>(this IEnumerable<T> list, Func<int, T, bool> match, Action<int, T> action)
		{
			if (action == null)
				return;
			foreach (var ai in list.Select((data, index) => new { data, index }))
			{
				if (match(ai.index, ai.data))
					action(ai.index, ai.data);
			}
		}

		/// <summary>
		/// 指定要素が存在するかどうか
		/// </summary>
		/// <typeparam name="T">型</typeparam>
		/// <param name="list">リスト</param>
		/// <param name="elem">要素</param>
		/// <returns>存在すればTrue</returns>
		static public bool Has<T>(this IEnumerable<T> list, T elem)
		{
			foreach (var value in list)
			{
				if (value.Equals(elem))
					return true;
			}
			return false;
		}

		/// <summary>
		/// 指定要素が存在するかどうか
		/// </summary>
		/// <typeparam name="T">型</typeparam>
		/// <param name="list">リスト</param>
		/// <param name="match">一致条件</param>
		/// <returns>存在すればTrue</returns>
		static public bool Has<T>(this IEnumerable<T> list, Predicate<T> match)
		{
			foreach (var value in list)
			{
				if (match(value))
					return true;
			}
			return false;
		}

		/// <summary>
		/// 指定要素が存在するかどうか
		/// </summary>
		/// <typeparam name="T">型</typeparam>
		/// <param name="list">リスト</param>
		/// <param name="match">条件式</param>
		/// <returns>存在すればTrue</returns>
		static public bool Has<T>(this IEnumerable<T> list, Func<int, T, bool> match)
		{
			foreach (var ai in list.Select((data, index) => new { data, index }))
			{
				if (match(ai.index, ai.data))
					return true;
			}
			return false;
		}

		/// <summary>
		/// 指定要素が存在しないかどうか
		/// </summary>
		/// <typeparam name="T">型</typeparam>
		/// <param name="list">リスト</param>
		/// <param name="elem">要素</param>
		/// <returns>存在しなければTrue</returns>
		static public bool DontHave<T>(this List<T> list, T elem)
		{
			return !list.Has(elem);
		}

		/// <summary>
		/// 指定要素が存在しないかどうか
		/// </summary>
		/// <typeparam name="T">型</typeparam>
		/// <param name="list">リスト</param>
		/// <param name="match">合致条件</param>
		/// <returns>存在しなければTrue</returns>
		static public bool DontHave<T>(this List<T> list, Predicate<T> match)
		{
			return !list.Has(match);
		}

		/// <summary>
		/// 指定要素が存在しないかどうか
		/// </summary>
		/// <typeparam name="T">型</typeparam>
		/// <param name="list">リスト</param>
		/// <param name="match">合致条件</param>
		/// <returns>存在しなければTrue</returns>
		static public bool DontHave<T>(this List<T> list, Func<int, T, bool> match)
		{
			return !list.Has(match);
		}
	}
}