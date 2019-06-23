using System;
using System.Collections.Generic;
using System.Linq;

namespace Milky.Extensions
{
	static public class ListExtension
	{
		static public T Find<T>(this T[] array, Predicate<T> match)
		{
			return Array.Find(array, match);
		}

		static public int FindIndex<T>(this T[] array, Predicate<T> match)
		{
			List<int> a;
			return Array.FindIndex<T>(array, match);
		}

		static public void ForEach<T>(this IEnumerable<T> list, Action<T> action)
		{
			foreach (var elem in list)
			{
				action(elem);
			}
		}

		static public void ForEachIf<T>(this IEnumerable<T> list, Predicate<T> match, Action<T> action)
		{
			foreach (var elem in list)
			{
				if (match(elem))
					action(elem);
			}
		}

		static public void ForAll<T>(this IEnumerable<T> list, Action<int, T> action)
		{
			if (action == null)
				return;
			foreach (var ai in list.Select((data, index) => new { data, index }))
			{
				action(ai.index, ai.data);
			}
		}

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

		static public bool Has<T>(this List<T> list, T elem)
		{
			if (list.Find(l => l.Equals(elem)) != null)
				return true;
			return false;
		}

		static public bool Has<T>(this List<T> list, Predicate<T> match)
		{
			if (list.Find(l => match(l)) != null)
				return true;
			return false;
		}

		static public bool Has<T>(this List<T> list, Func<int, T, bool> match)
		{
			foreach (var ai in list.Select((data, index) => new { data, index }))
			{
				if (match(ai.index, ai.data))
					return true;
			}
			return false;
		}

		static public bool DontHave<T>(this List<T> list, T elem)
		{
			return !list.Has(elem);
		}

		static public bool DontHave<T>(this List<T> list, Predicate<T> match)
		{
			return !list.Has(match);
		}

		static public bool DontHave<T>(this List<T> list, Func<int, T, bool> match)
		{
			return !list.Has(match);
		}
	}
}