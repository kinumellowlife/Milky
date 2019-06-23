using System;

namespace Milky.Extensions
{
	public static class SwapExtension
	{
		/// <summary>
		/// 指定した型のスワップを行う。
		/// =演算子で参照ではなく値のコピーができることが前提。
		/// </summary>
		/// <typeparam name="T">スワップ対象の型</typeparam>
		/// <param name="own">自分自身</param>
		/// <param name="value">入れ替え先</param>
		public static void Swap<T>(ref T own, ref T value)
		{
			T tmp = own;
			own = value;
			value = tmp;
		}

		/// <summary>
		/// スワップ
		/// </summary>
		/// <param name="own">自分自身</param>
		/// <param name="value">入れ替え先</param>
		public static void Swap(this byte own, ref byte value) { Swap(ref own, ref value); }

		/// <summary>
		/// スワップ
		/// </summary>
		/// <param name="own">自分自身</param>
		/// <param name="value">入れ替え先</param>
		public static void Swap(this int own, ref int value) { Swap(ref own, ref value); }

		/// <summary>
		/// スワップ
		/// </summary>
		/// <param name="own">自分自身</param>
		/// <param name="value">入れ替え先</param>
		public static void Swap(this short own, ref short value) { Swap(ref own, ref value); }

		/// <summary>
		/// スワップ
		/// </summary>
		/// <param name="own">自分自身</param>
		/// <param name="value">入れ替え先</param>
		public static void Swap(this long own, ref long value) { Swap(ref own, ref value); }

		/// <summary>
		/// スワップ
		/// </summary>
		/// <param name="own">自分自身</param>
		/// <param name="value">入れ替え先</param>
		public static void Swap(this float own, ref float value) { Swap(ref own, ref value); }

		/// <summary>
		/// スワップ
		/// </summary>
		/// <param name="own">自分自身</param>
		/// <param name="value">入れ替え先</param>
		public static void Swap(this double own, ref double value) { Swap(ref own, ref value); }

		/// <summary>
		/// スワップ
		/// </summary>
		/// <param name="own">自分自身</param>
		/// <param name="value">入れ替え先</param>
		public static void Swap(this uint own, ref uint value) { Swap(ref own, ref value); }

		/// <summary>
		/// スワップ
		/// </summary>
		/// <param name="own">自分自身</param>
		/// <param name="value">入れ替え先</param>
		public static void Swap(this ushort own, ref ushort value) { Swap(ref own, ref value); }

		/// <summary>
		/// スワップ
		/// </summary>
		/// <param name="own">自分自身</param>
		/// <param name="value">入れ替え先</param>
		public static void Swap(this ulong own, ref ulong value) { Swap(ref own, ref value); }

		/// <summary>
		/// スワップ
		/// </summary>
		/// <param name="own">自分自身</param>
		/// <param name="value">入れ替え先</param>
		public static void Swap(this DateTime own, ref DateTime value) { Swap(ref own, ref value); }
	}
}