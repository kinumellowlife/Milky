using System;

namespace Milky.Extensions
{
	public static class NumberExtension
	{
		/// <summary>
		/// 素数かどうか判定する
		/// </summary>
		/// <param name="val">チェック対象</param>
		/// <returns>素数ならtrue</returns>
		public static bool IsPrime(this int val)
		{
			if ((val & 1) == 0)
			{
				return val == 2;
			}
			int num = (int)Math.Sqrt((double)val);
			for (int i = 3; i <= num; i += 2)
			{
				if ((val % i) == 0)
					return false;
			}
			return true;
		}

		/// <summary>
		/// 階乗計算
		/// </summary>
		/// <param name="val">階乗</param>
		/// <returns>階乗結果</returns>
		public static double Factorial(this int val)
		{
			if (val <= 1)
				return 1;
			else
				return val * Factorial(val - 1);
		}

		/// <summary>
		/// 割り合い計算
		/// </summary>
		/// <param name="val">対象</param>
		/// <param name="value">母数</param>
		/// <param name="roundOffTo">小数部の桁数</param>
		/// <returns>割り合い</returns>
		public static double PercentOf(this double val, double value, int roundOffTo)
		{
			return Math.Round((val / 100d) * value, roundOffTo);
		}

		/// <summary>
		/// 割り合い計算
		/// </summary>
		/// <param name="val">対象</param>
		/// <param name="value">母数</param>
		/// <param name="roundOffTo">小数部の桁数</param>
		/// <returns>割り合い</returns>
		public static double PercentOf(this int val, double value, int roundOffTo)
		{
			return Math.Round((val / 100d) * value, roundOffTo);
		}

		/// <summary>
		/// 累乗計算
		/// </summary>
		/// <param name="val">対象</param>
		/// <param name="off">何乗するか</param>
		/// <returns>累乗した値</returns>
		public static double PowerOf(this int val, int off)
		{
			return Math.Pow(val, off);
		}

		/// <summary>
		/// 指定範囲内かどうか。from,toは含む。
		/// </summary>
		/// <param name="val">対象</param>
		/// <param name="from">最小値</param>
		/// <param name="to">最大値</param>
		/// <returns>範囲内ならtrue</returns>
		public static bool IsIn<T>(this T val, T from, T to) where T : IComparable<T>
		{
			if (val.CompareTo(from) < 0)
				return false;
			if (val.CompareTo(to) > 0)
				return false;
			return true;
		}

		/// <summary>
		/// 範囲内かどうか。from, toを含む。
		/// </summary>
		/// <param name="val">対象</param>
		/// <param name="from">最小値判断処理</param>
		/// <param name="to">最大値判断処理</param>
		/// <returns>範囲内ならtrue</returns>
		public static bool IsIn<T>(this T val, Func<T, int> from, Func<T, int> to) where T : IComparable<T>
		{
			if (from(val) < 0)
				return false;
			if (to(val) > 0)
				return false;
			return true;
		}

		/// <summary>
		/// 指定範囲外かどうか。from, toは含まない。
		/// </summary>
		/// <param name="val">対象</param>
		/// <param name="from">最小値</param>
		/// <param name="to">最大値</param>
		/// <returns>範囲外ならtrue</returns>
		public static bool IsOut<T>(this T val, T from, T to) where T : IComparable<T>
		{
			return !val.IsIn(from, to);
		}

		/// <summary>
		/// 指定範囲外かどうか。from, toは含まない。
		/// </summary>
		/// <param name="val">対象</param>
		/// <param name="from">最小値</param>
		/// <param name="to">最大値</param>
		/// <returns>範囲外ならtrue</returns>
		public static bool IsOut<T>(this T val, Func<T, int> from, Func<T, int> to) where T : IComparable<T>
		{
			return !val.IsIn(from, to);
		}
	}
}