using System;

namespace Milky.Extensions
{
	public static class DateTimeExtension
	{
		public static double To60Time(this DateTime d)
		{
			return d.Hour + (d.Minute / 60.0D);
		}

		public static bool IsSaturday(this DateTime value)
		{
			return (value.DayOfWeek == DayOfWeek.Saturday);
		}

		public static bool IsSunday(this DateTime value)
		{
			return (value.DayOfWeek == DayOfWeek.Sunday);
		}

		public static DateTime CutHHMMSS(this DateTime value)
		{
			return value.ToBinary().ToDateTime(true);
		}

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