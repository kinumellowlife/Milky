namespace Milky.Algorithm
{
	/// <summary>
	/// ９つの異なる型のペア。
	/// Tupleが値の書き換えができないため作成。
	/// </summary>
	/// <typeparam name="T1">要素１の型</typeparam>
	/// <typeparam name="T2">要素２の型</typeparam>
	/// <typeparam name="T3">要素３の型</typeparam>
	/// <typeparam name="T4">要素４の型</typeparam>
	/// <typeparam name="T5">要素５の型</typeparam>
	/// <typeparam name="T6">要素６の型</typeparam>
	/// <typeparam name="T7">要素７の型</typeparam>
	/// <typeparam name="T8">要素８の型</typeparam>
	/// <typeparam name="T9">要素８の型</typeparam>
	public class Pair<T1, T2, T3, T4, T5, T6, T7, T8, T9>
	{
		#region fields

		/// <summary>要素１</summary>
		private T1 value1;

		/// <summary>要素２</summary>
		private T2 value2;

		/// <summary>要素３</summary>
		private T3 value3;

		/// <summary>要素４</summary>
		private T4 value4;

		/// <summary>要素５</summary>
		private T5 value5;

		/// <summary>要素６</summary>
		private T6 value6;

		/// <summary>要素７</summary>
		private T7 value7;

		/// <summary>要素８</summary>
		private T8 value8;

		/// <summary>要素９</summary>
		private T9 value9;

		#endregion fields

		#region construct

		/// <summary>
		/// 構築
		/// </summary>
		/// <param name="v1">要素１</param>
		/// <param name="v2">要素２</param>
		/// <param name="v3">要素３</param>
		/// <param name="v4">要素４</param>
		/// <param name="v5">要素５</param>
		/// <param name="v6">要素６</param>
		/// <param name="v7">要素７</param>
		/// <param name="v8">要素８</param>
		/// <param name="v9">要素９</param>
		public Pair(T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9)
		{
			value1 = v1;
			value2 = v2;
			value3 = v3;
			value4 = v4;
			value5 = v5;
			value6 = v6;
			value7 = v7;
			value8 = v8;
			value9 = v9;
		}

		#endregion construct

		#region property

		/// <summary>
		/// 名前で要素を取得する
		/// </summary>
		/// <param name="name">名前</param>
		/// <returns>要素</returns>
		public object this[int index] {
			get
			{
				switch (index)
				{
					default:
						return null;

					case 0:
						return value1;

					case 1:
						return value2;

					case 2:
						return value3;

					case 3:
						return value4;

					case 4:
						return value5;

					case 5:
						return value6;

					case 6:
						return value7;

					case 7:
						return value8;

					case 8:
						return value9;
				}
			}
		}

		/// <summary>
		/// 要素１の取得と設定
		/// </summary>
		public T1 Item1 { get { return this.value1; } set { this.value1 = value; } }

		/// <summary>
		/// 要素２の取得と設定
		/// </summary>
		public T2 Item2 { get { return this.value2; } set { this.value2 = value; } }

		/// <summary>
		/// 要素３の取得と設定
		/// </summary>
		public T3 Item3 { get { return this.value3; } set { this.value3 = value; } }

		/// <summary>
		/// 要素４の取得と設定
		/// </summary>
		public T4 Item4 { get { return this.value4; } set { this.value4 = value; } }

		/// <summary>
		/// 要素５の取得と設定
		/// </summary>
		public T5 Item5 { get { return this.value5; } set { this.value5 = value; } }

		/// <summary>
		/// 要素６の取得と設定
		/// </summary>
		public T6 Item6 { get { return this.value6; } set { this.value6 = value; } }

		/// <summary>
		/// 要素７の取得と設定
		/// </summary>
		public T7 Item7 { get { return this.value7; } set { this.value7 = value; } }

		/// <summary>
		/// 要素８の取得と設定
		/// </summary>
		public T8 Item8 { get { return this.value8; } set { this.value8 = value; } }

		/// <summary>
		/// 要素９の取得と設定
		/// </summary>
		public T9 Item9 { get { return this.value9; } set { this.value9 = value; } }

		#endregion property
	}

	/// <summary>
	/// ８つの異なる型のペア。
	/// Tupleが値の書き換えができないため作成。
	/// </summary>
	/// <typeparam name="T1">要素１の型</typeparam>
	/// <typeparam name="T2">要素２の型</typeparam>
	/// <typeparam name="T3">要素３の型</typeparam>
	/// <typeparam name="T4">要素４の型</typeparam>
	/// <typeparam name="T5">要素５の型</typeparam>
	/// <typeparam name="T6">要素６の型</typeparam>
	/// <typeparam name="T7">要素７の型</typeparam>
	/// <typeparam name="T8">要素８の型</typeparam>
	public class Pair<T1, T2, T3, T4, T5, T6, T7, T8>
	{
		#region fields

		/// <summary>要素１</summary>
		private T1 value1;

		/// <summary>要素２</summary>
		private T2 value2;

		/// <summary>要素３</summary>
		private T3 value3;

		/// <summary>要素４</summary>
		private T4 value4;

		/// <summary>要素５</summary>
		private T5 value5;

		/// <summary>要素６</summary>
		private T6 value6;

		/// <summary>要素７</summary>
		private T7 value7;

		/// <summary>要素８</summary>
		private T8 value8;

		#endregion fields

		#region construct

		/// <summary>
		/// 構築
		/// </summary>
		/// <param name="v1">要素１</param>
		/// <param name="v2">要素２</param>
		/// <param name="v3">要素３</param>
		/// <param name="v4">要素４</param>
		/// <param name="v5">要素５</param>
		/// <param name="v6">要素６</param>
		/// <param name="v7">要素７</param>
		/// <param name="v8">要素８</param>
		public Pair(T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8)
		{
			value1 = v1;
			value2 = v2;
			value3 = v3;
			value4 = v4;
			value5 = v5;
			value6 = v6;
			value7 = v7;
			value8 = v8;
		}

		#endregion construct

		#region property

		/// <summary>
		/// 名前で要素を取得する
		/// </summary>
		/// <param name="name">名前</param>
		/// <returns>要素</returns>
		public object this[int index] {
			get
			{
				switch (index)
				{
					default:
						return null;

					case 0:
						return value1;

					case 1:
						return value2;

					case 2:
						return value3;

					case 3:
						return value4;

					case 4:
						return value5;

					case 5:
						return value6;

					case 6:
						return value7;

					case 7:
						return value8;
				}
			}
		}

		/// <summary>
		/// 要素１の取得と設定
		/// </summary>
		public T1 Item1 { get { return this.value1; } set { this.value1 = value; } }

		/// <summary>
		/// 要素２の取得と設定
		/// </summary>
		public T2 Item2 { get { return this.value2; } set { this.value2 = value; } }

		/// <summary>
		/// 要素３の取得と設定
		/// </summary>
		public T3 Item3 { get { return this.value3; } set { this.value3 = value; } }

		/// <summary>
		/// 要素４の取得と設定
		/// </summary>
		public T4 Item4 { get { return this.value4; } set { this.value4 = value; } }

		/// <summary>
		/// 要素５の取得と設定
		/// </summary>
		public T5 Item5 { get { return this.value5; } set { this.value5 = value; } }

		/// <summary>
		/// 要素６の取得と設定
		/// </summary>
		public T6 Item6 { get { return this.value6; } set { this.value6 = value; } }

		/// <summary>
		/// 要素７の取得と設定
		/// </summary>
		public T7 Item7 { get { return this.value7; } set { this.value7 = value; } }

		/// <summary>
		/// 要素８の取得と設定
		/// </summary>
		public T8 Item8 { get { return this.value8; } set { this.value8 = value; } }

		#endregion property
	}

	/// <summary>
	/// ７つの異なる型のペア。
	/// Tupleが値の書き換えができないため作成。
	/// </summary>
	/// <typeparam name="T1">要素１の型</typeparam>
	/// <typeparam name="T2">要素２の型</typeparam>
	/// <typeparam name="T3">要素３の型</typeparam>
	/// <typeparam name="T4">要素４の型</typeparam>
	/// <typeparam name="T5">要素５の型</typeparam>
	/// <typeparam name="T6">要素６の型</typeparam>
	/// <typeparam name="T7">要素７の型</typeparam>
	public class Pair<T1, T2, T3, T4, T5, T6, T7>
	{
		#region fields

		/// <summary>要素１</summary>
		private T1 value1;

		/// <summary>要素２</summary>
		private T2 value2;

		/// <summary>要素３</summary>
		private T3 value3;

		/// <summary>要素４</summary>
		private T4 value4;

		/// <summary>要素５</summary>
		private T5 value5;

		/// <summary>要素６</summary>
		private T6 value6;

		/// <summary>要素７</summary>
		private T7 value7;

		#endregion fields

		#region construct

		/// <summary>
		/// 構築
		/// </summary>
		/// <param name="v1">要素１</param>
		/// <param name="v2">要素２</param>
		/// <param name="v3">要素３</param>
		/// <param name="v4">要素４</param>
		/// <param name="v5">要素５</param>
		/// <param name="v6">要素６</param>
		/// <param name="v7">要素７</param>
		public Pair(T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7)
		{
			value1 = v1;
			value2 = v2;
			value3 = v3;
			value4 = v4;
			value5 = v5;
			value6 = v6;
			value7 = v7;
		}

		#endregion construct

		#region property

		/// <summary>
		/// 名前で要素を取得する
		/// </summary>
		/// <param name="name">名前</param>
		/// <returns>要素</returns>
		public object this[int index] {
			get
			{
				switch (index)
				{
					default:
						return null;

					case 0:
						return value1;

					case 1:
						return value2;

					case 2:
						return value3;

					case 3:
						return value4;

					case 4:
						return value5;

					case 5:
						return value6;

					case 6:
						return value7;
				}
			}
		}

		/// <summary>
		/// 要素１の取得と設定
		/// </summary>
		public T1 Item1 { get { return this.value1; } set { this.value1 = value; } }

		/// <summary>
		/// 要素２の取得と設定
		/// </summary>
		public T2 Item2 { get { return this.value2; } set { this.value2 = value; } }

		/// <summary>
		/// 要素３の取得と設定
		/// </summary>
		public T3 Item3 { get { return this.value3; } set { this.value3 = value; } }

		/// <summary>
		/// 要素４の取得と設定
		/// </summary>
		public T4 Item4 { get { return this.value4; } set { this.value4 = value; } }

		/// <summary>
		/// 要素５の取得と設定
		/// </summary>
		public T5 Item5 { get { return this.value5; } set { this.value5 = value; } }

		/// <summary>
		/// 要素６の取得と設定
		/// </summary>
		public T6 Item6 { get { return this.value6; } set { this.value6 = value; } }

		/// <summary>
		/// 要素７の取得と設定
		/// </summary>
		public T7 Item7 { get { return this.value7; } set { this.value7 = value; } }

		#endregion property
	}

	/// <summary>
	/// ６つの異なる型のペア。
	/// Tupleが値の書き換えができないため作成。
	/// </summary>
	/// <typeparam name="T1">要素１の型</typeparam>
	/// <typeparam name="T2">要素２の型</typeparam>
	/// <typeparam name="T3">要素３の型</typeparam>
	/// <typeparam name="T4">要素４の型</typeparam>
	/// <typeparam name="T5">要素５の型</typeparam>
	/// <typeparam name="T6">要素６の型</typeparam>
	public class Pair<T1, T2, T3, T4, T5, T6>
	{
		#region fields

		/// <summary>要素１</summary>
		private T1 value1;

		/// <summary>要素２</summary>
		private T2 value2;

		/// <summary>要素３</summary>
		private T3 value3;

		/// <summary>要素４</summary>
		private T4 value4;

		/// <summary>要素５</summary>
		private T5 value5;

		/// <summary>要素６</summary>
		private T6 value6;

		#endregion fields

		#region construct

		/// <summary>
		/// 構築
		/// </summary>
		/// <param name="v1">要素１</param>
		/// <param name="v2">要素２</param>
		/// <param name="v3">要素３</param>
		/// <param name="v4">要素４</param>
		/// <param name="v5">要素５</param>
		/// <param name="v6">要素６</param>
		public Pair(T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6)
		{
			value1 = v1;
			value2 = v2;
			value3 = v3;
			value4 = v4;
			value5 = v5;
			value6 = v6;
		}

		#endregion construct

		#region property

		/// <summary>
		/// 名前で要素を取得する
		/// </summary>
		/// <param name="name">名前</param>
		/// <returns>要素</returns>
		public object this[int index] {
			get
			{
				switch (index)
				{
					default:
						return null;

					case 0:
						return value1;

					case 1:
						return value2;

					case 2:
						return value3;

					case 3:
						return value4;

					case 4:
						return value5;

					case 5:
						return value6;
				}
			}
		}

		/// <summary>
		/// 要素１の取得と設定
		/// </summary>
		public T1 Item1 { get { return this.value1; } set { this.value1 = value; } }

		/// <summary>
		/// 要素２の取得と設定
		/// </summary>
		public T2 Item2 { get { return this.value2; } set { this.value2 = value; } }

		/// <summary>
		/// 要素３の取得と設定
		/// </summary>
		public T3 Item3 { get { return this.value3; } set { this.value3 = value; } }

		/// <summary>
		/// 要素４の取得と設定
		/// </summary>
		public T4 Item4 { get { return this.value4; } set { this.value4 = value; } }

		/// <summary>
		/// 要素５の取得と設定
		/// </summary>
		public T5 Item5 { get { return this.value5; } set { this.value5 = value; } }

		/// <summary>
		/// 要素６の取得と設定
		/// </summary>
		public T6 Item6 { get { return this.value6; } set { this.value6 = value; } }

		#endregion property
	}

	/// <summary>
	/// ５つの異なる型のペア。
	/// Tupleが値の書き換えができないため作成。
	/// </summary>
	/// <typeparam name="T1">要素１の型</typeparam>
	/// <typeparam name="T2">要素２の型</typeparam>
	/// <typeparam name="T3">要素３の型</typeparam>
	/// <typeparam name="T4">要素４の型</typeparam>
	/// <typeparam name="T5">要素５の型</typeparam>
	public class Pair<T1, T2, T3, T4, T5>
	{
		#region fields

		/// <summary>要素１</summary>
		private T1 value1;

		/// <summary>要素２</summary>
		private T2 value2;

		/// <summary>要素３</summary>
		private T3 value3;

		/// <summary>要素４</summary>
		private T4 value4;

		/// <summary>要素５</summary>
		private T5 value5;

		#endregion fields

		#region construct

		/// <summary>
		/// 構築
		/// </summary>
		/// <param name="v1">要素１</param>
		/// <param name="v2">要素２</param>
		/// <param name="v3">要素３</param>
		/// <param name="v4">要素４</param>
		/// <param name="v5">要素５</param>
		public Pair(T1 v1, T2 v2, T3 v3, T4 v4, T5 v5)
		{
			value1 = v1;
			value2 = v2;
			value3 = v3;
			value4 = v4;
			value5 = v5;
		}

		#endregion construct

		#region property

		/// <summary>
		/// 名前で要素を取得する
		/// </summary>
		/// <param name="name">名前</param>
		/// <returns>要素</returns>
		public object this[int index] {
			get
			{
				switch (index)
				{
					default:
						return null;

					case 0:
						return value1;

					case 1:
						return value2;

					case 2:
						return value3;

					case 3:
						return value4;

					case 4:
						return value5;
				}
			}
		}

		/// <summary>
		/// 要素１の取得と設定
		/// </summary>
		public T1 Item1 { get { return this.value1; } set { this.value1 = value; } }

		/// <summary>
		/// 要素２の取得と設定
		/// </summary>
		public T2 Item2 { get { return this.value2; } set { this.value2 = value; } }

		/// <summary>
		/// 要素３の取得と設定
		/// </summary>
		public T3 Item3 { get { return this.value3; } set { this.value3 = value; } }

		/// <summary>
		/// 要素４の取得と設定
		/// </summary>
		public T4 Item4 { get { return this.value4; } set { this.value4 = value; } }

		/// <summary>
		/// 要素５の取得と設定
		/// </summary>
		public T5 Item5 { get { return this.value5; } set { this.value5 = value; } }

		#endregion property
	}

	/// <summary>
	/// ４つの異なる型のペア。
	/// Tupleが値の書き換えができないため作成。
	/// </summary>
	/// <typeparam name="T1">要素１の型</typeparam>
	/// <typeparam name="T2">要素２の型</typeparam>
	/// <typeparam name="T3">要素３の型</typeparam>
	/// <typeparam name="T4">要素４の型</typeparam>
	public class Pair<T1, T2, T3, T4>
	{
		#region fields

		/// <summary>要素１</summary>
		private T1 value1;

		/// <summary>要素２</summary>
		private T2 value2;

		/// <summary>要素３</summary>
		private T3 value3;

		/// <summary>要素４</summary>
		private T4 value4;

		#endregion fields

		#region construct

		/// <summary>
		/// 構築
		/// </summary>
		/// <param name="v1">要素１</param>
		/// <param name="v2">要素２</param>
		/// <param name="v3">要素３</param>
		/// <param name="v4">要素４</param>
		public Pair(T1 v1, T2 v2, T3 v3, T4 v4)
		{
			value1 = v1;
			value2 = v2;
			value3 = v3;
			value4 = v4;
		}

		#endregion construct

		#region property

		/// <summary>
		/// 名前で要素を取得する
		/// </summary>
		/// <param name="name">名前</param>
		/// <returns>要素</returns>
		public object this[int index] {
			get
			{
				switch (index)
				{
					default:
					case 0:
						return value1;

					case 1:
						return value2;

					case 2:
						return value3;

					case 3:
						return value4;
				}
			}
		}

		/// <summary>
		/// 要素１の取得と設定
		/// </summary>
		public T1 Item1 { get { return this.value1; } set { this.value1 = value; } }

		/// <summary>
		/// 要素２の取得と設定
		/// </summary>
		public T2 Item2 { get { return this.value2; } set { this.value2 = value; } }

		/// <summary>
		/// 要素３の取得と設定
		/// </summary>
		public T3 Item3 { get { return this.value3; } set { this.value3 = value; } }

		/// <summary>
		/// 要素４の取得と設定
		/// </summary>
		public T4 Item4 { get { return this.value4; } set { this.value4 = value; } }

		#endregion property
	}

	/// <summary>
	/// ３つの異なる型のペア。
	/// Tupleが値の書き換えができないため作成。
	/// </summary>
	/// <typeparam name="T1">要素１の型</typeparam>
	/// <typeparam name="T2">要素２の型</typeparam>
	/// <typeparam name="T3">要素３の型</typeparam>
	public class Pair<T1, T2, T3>
	{
		#region fields

		/// <summary>要素１</summary>
		private T1 value1;

		/// <summary>要素２</summary>
		private T2 value2;

		/// <summary>要素３</summary>
		private T3 value3;

		#endregion fields

		#region construct

		/// <summary>
		/// 構築
		/// </summary>
		/// <param name="v1">要素１</param>
		/// <param name="v2">要素２</param>
		/// <param name="v3">要素３</param>
		public Pair(T1 v1, T2 v2, T3 v3)
		{
			value1 = v1;
			value2 = v2;
			value3 = v3;
		}

		#endregion construct

		#region property

		/// <summary>
		/// 名前で要素を取得する
		/// </summary>
		/// <param name="name">名前</param>
		/// <returns>要素</returns>
		public object this[int index] {
			get
			{
				switch (index)
				{
					default:
						return null;

					case 0:
						return value1;

					case 1:
						return value2;

					case 2:
						return value3;
				}
			}
		}

		/// <summary>
		/// 要素１の取得と設定
		/// </summary>
		public T1 Item1 { get { return this.value1; } set { this.value1 = value; } }

		/// <summary>
		/// 要素２の取得と設定
		/// </summary>
		public T2 Item2 { get { return this.value2; } set { this.value2 = value; } }

		/// <summary>
		/// 要素３の取得と設定
		/// </summary>
		public T3 Item3 { get { return this.value3; } set { this.value3 = value; } }

		#endregion property
	}

	/// <summary>
	/// ２つの異なる型のペア。
	/// Tupleが値の書き換えができないため作成。
	/// </summary>
	/// <typeparam name="T1">要素１の型</typeparam>
	/// <typeparam name="T2">要素２の型</typeparam>
	public class Pair<T1, T2>
	{
		#region fields

		/// <summary>要素１</summary>
		private T1 value1;

		/// <summary>要素２</summary>
		private T2 value2;

		#endregion fields

		#region construct

		/// <summary>
		/// 構築
		/// </summary>
		/// <param name="v1">要素１</param>
		/// <param name="v2">要素２</param>
		public Pair(T1 v1, T2 v2)
		{
			value1 = v1;
			value2 = v2;
		}

		#endregion construct

		#region property

		/// <summary>
		/// 名前で要素を取得する
		/// </summary>
		/// <param name="name">名前</param>
		/// <returns>要素</returns>
		public object this[int index] {
			get
			{
				switch (index)
				{
					default:
						return null;

					case 0:
						return value1;

					case 1:
						return value2;
				}
			}
		}

		/// <summary>
		/// 要素１の取得と設定
		/// </summary>
		public T1 Item1 { get { return this.value1; } set { this.value1 = value; } }

		/// <summary>
		/// 要素２の取得と設定
		/// </summary>
		public T2 Item2 { get { return this.value2; } set { this.value2 = value; } }

		#endregion property
	}
}