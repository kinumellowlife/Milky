using System;

namespace Milky.Extensions
{
	public static class DateTimeExtension
	{
		/// <summary>
		/// 時分を１０進数にする
		/// </summary>
		/// <param name="d">時分</param>
		/// <returns>１０進数表記した時分</returns>
		public static double To60Time(this DateTime d)
		{
			return d.Hour + (d.Minute / 60.0D);
		}

		/// <summary>
		/// 指定した曜日かどうか。
		/// </summary>
		/// <param name="d">調べたい日付</param>
		/// <param name="day">曜日</param>
		/// <returns></returns>
		public static bool IsDayOfWeek(this DateTime d, DayOfWeek day)
		{
			return d.DayOfWeek == day;
		}

		/// <summary>
		/// 土曜日かどうか
		/// </summary>
		/// <param name="value">調べたい日付</param>
		/// <returns>土曜日ならTrue</returns>
		public static bool IsSaturday(this DateTime value)
		{
			return (value.DayOfWeek == DayOfWeek.Saturday);
		}

		/// <summary>
		/// 日曜日かどうか
		/// </summary>
		/// <param name="value">調べたい日付</param>
		/// <returns>日曜日ならTrue</returns>
		public static bool IsSunday(this DateTime value)
		{
			return (value.DayOfWeek == DayOfWeek.Sunday);
		}

		/// <summary>
		/// 日付情報から時間情報をクリアした日付情報を生成する
		/// </summary>
		/// <param name="value">日付情報</param>
		/// <returns>クリアされた日付情報</returns>
		public static DateTime CutHHMMSS(this DateTime value)
		{
			return value.ToBinary().ToDateTime(true);
		}

		/// <summary>
		/// シリアル値を日付情報に変換する
		/// </summary>
		/// <param name="value">シリアル値</param>
		/// <param name="cutHHMMSS">変換する際、時刻情報を０にするかどうか</param>
		/// <returns>日付</returns>
		public static DateTime ToDateTime(this long value, bool cutHHMMSS = false)
		{
			if (cutHHMMSS)
			{
				DateTime d = DateTime.FromBinary(value);
				return new DateTime(d.Year, d.Month, d.Day);
			}
			else
			{
				return DateTime.FromBinary(value);
			}
		}
	}
}