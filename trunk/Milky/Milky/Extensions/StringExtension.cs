using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Milky.Extensions
{
	public static class ExtString
	{
		#region 数値変換関連

		/// <summary>
		/// １６進数表記された文字列から数値への変換
		/// </summary>
		/// <param name="s"></param>
		/// <param name="def"></param>
		/// <returns></returns>
		static public Int32 HexToInt(this String s, Int32 def)
		{
			try
			{
				return System.Convert.ToInt32(s.Replace("0x", ""), 16);
			}
			catch (Exception)
			{
				return def;
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="s"></param>
		/// <param name="def"></param>
		/// <returns></returns>
		static public float HexToFloat(this String s, float def)
		{
			try
			{
				int value = System.Convert.ToInt32(s.Replace("0x", ""), 16);
				return (float)value;
			}
			catch (Exception)
			{
				return def;
			}
		}

		/// <summary>
		/// 色コードから色への変換
		/// </summary>
		/// <param name="s">色コード</param>
		/// <param name="def">コードの基底（１６進数なら１６）</param>
		/// <returns>色</returns>
		static public Color HexToColor(this string s, Int32 def = 16)
		{
			int value = s.HexToInt(def);
			Color c = Color.FromArgb((value & 0xFF0000) >> 16, (value & 0x00FF00) >> 8, (value & 0x0000FF));
			return c;
		}

		/// <summary>
		/// １６進数表記された文字列から数値への変換
		/// </summary>
		/// <param name="s"></param>
		/// <param name="def"></param>
		/// <returns></returns>
		static public Byte HexToByte(this String s, Byte def)
		{
			try
			{
				return System.Convert.ToByte(s.Replace("0x", ""), 16);
			}
			catch (Exception)
			{
				return def;
			}
		}

		/// <summary>
		/// １６進数表記された文字列から数値への変換
		/// </summary>
		/// <param name="s"></param>
		/// <param name="def"></param>
		/// <returns></returns>
		static public Byte OctToByte(this String s, Byte def)
		{
			try
			{
				return System.Convert.ToByte(s, 8);
			}
			catch (Exception)
			{
				return def;
			}
		}

		/// <summary>
		/// １６進数表記された文字列から数値への変換
		/// </summary>
		/// <param name="s"></param>
		/// <param name="def"></param>
		/// <returns></returns>
		static public Byte BinToByte(this String s, Byte def)
		{
			try
			{
				return System.Convert.ToByte(s, 2);
			}
			catch (Exception)
			{
				return def;
			}
		}

		/// <summary>
		/// 年月日時分秒からDateTime型へ変換
		/// </summary>
		/// <param name="s">変換元の文字列</param>
		/// <param name="year">年の開始位置</param>
		/// <param name="month">月の開始位置</param>
		/// <param name="day">日の開始位置</param>
		/// <param name="hour">時の開始位置</param>
		/// <param name="minute">分の開始位置</param>
		/// <param name="second">秒の開始位置</param>
		/// <returns>DateTime</returns>
		static public DateTime ToDateTime(this String s, Int32 year, Int32 month, Int32 day, Int32 hour, Int32 minute, Int32 second)
		{
			int dtYear = 0;
			int dtMonth = 0;
			int dtDay = 0;
			int dtHour = 0;
			int dtMinute = 0;
			int dtSecond = 0;
			int pos = 0;

			if (year > 0)
			{
				dtYear = s.Substring(pos, year).DecToInt(0);
				pos += year;
			}
			if (month > 0)
			{
				dtMonth = s.Substring(pos, month).DecToInt(0);
				pos += month;
			}
			if (day > 0)
			{
				dtDay = s.Substring(pos, day).DecToInt(0);
				pos += day;
			}
			if (hour > 0)
			{
				dtHour = s.Substring(pos, hour).DecToInt(0);
				pos += hour;
			}
			if (minute > 0)
			{
				dtMinute = s.Substring(pos, minute).DecToInt(0);
				pos += minute;
			}
			if (second > 0)
			{
				dtSecond = s.Substring(pos, second).DecToInt(0);
				pos += second;
			}

			DateTime dt = new DateTime(dtYear, dtMonth, dtDay, dtHour, dtMinute, dtSecond);

			return dt;
		}

		/// <summary>
		/// "true"か"false"からboolへの変換
		/// </summary>
		/// <param name="s"></param>
		/// <param name="def"></param>
		/// <returns></returns>
		static public Boolean ToBool(this String s, Boolean def)
		{
			bool b;

			if (bool.TryParse(s, out b))
			{
				return b;
			}
			else
			{
				if (s.Equals("1"))
				{
					return true;
				}
				else if (s.Equals("0"))
				{
					return false;
				}
				else
				{
					return def;
				}
			}
		}

		/// <summary>
		/// １０進数表記された数値文字列から数値への変換
		/// </summary>
		/// <param name="s"></param>
		/// <param name="def"></param>
		/// <returns></returns>
		static public Int32 DecToInt(this string s, int def)
		{
			try
			{
				return System.Convert.ToInt32(s, 10);
			}
			catch (Exception)
			{
				return def;
			}
		}

		/// <summary>
		/// 小数点表記された文字列からfloatへの変換
		/// </summary>
		/// <param name="s"></param>
		/// <param name="def"></param>
		/// <returns></returns>
		static public Double ToDouble(this string s, double def)
		{
			double f;

			if (double.TryParse(s, out f))
			{
				return f;
			}
			else
			{
				return def;
			}
		}

		/// <summary>
		/// 数値を文字列へ変換
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		static public String ValueOf(this short value)
		{
			return string.Format("{0}", value);
		}

		/// <summary>
		/// 数値を文字列へ変換
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		static public String ValueOf(this int value)
		{
			return string.Format("{0}", value);
		}

		/// <summary>
		/// 文字列を数値へ変換
		/// </summary>
		/// <param name="value"></param>
		/// <param name="fromBase"></param>
		/// <returns></returns>
		static public Int32 ToInt16(this string value, int fromBase = 10)
		{
			return Convert.ToInt16(value, fromBase);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="value"></param>
		/// <param name="fromBase"></param>
		/// <returns></returns>
		static public Int32 ToInt32(this string value, int fromBase = 10)
		{
			return Convert.ToInt32(value, fromBase);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="value"></param>
		/// <param name="fromBase"></param>
		/// <returns></returns>
		static public Int64 ToInt64(this string value, int fromBase = 10)
		{
			return Convert.ToInt64(value, fromBase);
		}

		/// <summary>
		/// 文字列を数値へ変換
		/// </summary>
		/// <param name="value"></param>
		/// <param name="fromBase"></param>
		/// <returns></returns>
		static public UInt32 ToUInt16(this string value, int fromBase = 10)
		{
			return Convert.ToUInt16(value, fromBase);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="value"></param>
		/// <param name="fromBase"></param>
		/// <returns></returns>
		static public UInt32 ToUInt32(this string value, int fromBase = 10)
		{
			return Convert.ToUInt32(value, fromBase);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="value"></param>
		/// <param name="fromBase"></param>
		/// <returns></returns>
		static public UInt64 ToUInt64(this string value, int fromBase = 10)
		{
			return Convert.ToUInt64(value, fromBase);
		}

		/// <summary>
		/// 文字列を数値へ変換
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		static public Double ToDouble(this string value)
		{
			return Convert.ToDouble(value);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		static public Decimal ToDecimal(this string value)
		{
			return Convert.ToDecimal(value);
		}

		#endregion 数値変換関連

		#region 操作

		/// <summary>
		/// Splitした上で各要素をさらにTrimする
		/// </summary>
		/// <param name="value"></param>
		/// <param name="args"></param>
		/// <param name="option"></param>
		/// <returns></returns>
		static public string[] SplitTrim(this string value, char[] args, StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries)
		{
			string[] ret = value.Split(args, option);
			for (int index = 0; index < ret.Length; index++)
			{
				ret[index] = ret[index].Trim();
			}
			return ret;
		}

		/// <summary>
		/// Splitした上で書く要素をさらにTrimする
		/// </summary>
		/// <param name="value"></param>
		/// <param name="args"></param>
		/// <param name="option"></param>
		/// <returns></returns>
		static public string[] SplitTrim(this string value, string[] args, StringSplitOptions option = StringSplitOptions.RemoveEmptyEntries)
		{
			string[] ret = value.Split(args, option);
			for (int index = 0; index < ret.Length; index++)
			{
				ret[index] = ret[index].Trim();
			}
			return ret;
		}

		/// <summary>
		/// 右から指定文字数分の文字列を返す
		/// </summary>
		/// <param name="value"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		static public string Right(this string value, int count)
		{
			int startIndex = value.Length - 1 - count + 1;
			if (startIndex < 0)
			{
				startIndex = 0;
				count = value.Length;
			}

			return value.Substring(startIndex, count);
		}

		/// <summary>
		/// 左から指定文字数分の文字列を返す
		/// </summary>
		/// <param name="value"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		static public string Left(this string value, int length)
		{
			return value.Substring(0, (value.Length < length) ? value.Length : length);
		}

		/// <summary>
		/// テキストの表示サイズを計算する
		/// </summary>
		/// <param name="text"></param>
		/// <param name="font"></param>
		/// <returns></returns>
		static public SizeF Measure(this String text, Font font)
		{
			SizeF result;
			using (var image = new Bitmap(1, 1))
			{
				using (var g = Graphics.FromImage(image))
				{
					result = g.MeasureString(text, font);
				}
			}
			return result;
		}

		/// <summary>
		/// Left pads the passed string using the passed pad string for the total number of spaces.
		/// It will not cut-off the pad even if it causes the string to exceed the total width.
		/// Test Coverage: Included
		/// </summary>
		/// <param name="val"></param>
		/// <param name="pad">The pad string</param>
		/// <param name="totalWidth">The total width of the resulting string</param>
		/// <returns>Copy of string with the padding applied</returns>
		public static string PadLeft(this string val, string pad, int totalWidth)
		{
			return val.PadLeft(pad, totalWidth, false);
		}

		/// <summary>
		/// Left pads the passed string using the passed pad string for the total number of spaces.
		/// </summary>
		/// <param name="val"></param>
		/// <param name="pad">The pad string</param>
		/// <param name="totalWidth">The total width of the resulting string</param>
		/// <param name="cutOff">True to cut off the characters if exceeds the specified width</param>
		/// <returns>Copy of string with the padding applied</returns>
		public static string PadLeft(this string val, string pad, int totalWidth, bool cutOff)
		{
			if (val.Length >= totalWidth)
				return val;

			int padCount = pad.Length;
			string paddedString = val;

			while (paddedString.Length < totalWidth)
			{
				paddedString += pad;
			}

			if (cutOff)
				paddedString = paddedString.Substring(0, totalWidth);
			return paddedString;
		}

		/// <summary>
		/// Right pads the passed string using the passed pad string for the total number of spaces.
		/// It will not cut-off the pad even if it causes the string to exceed the total width.
		/// Test Coverage: Included
		/// </summary>
		/// <param name="val"></param>
		/// <param name="pad">The pad string</param>
		/// <param name="totalWidth">The total width of the resulting string</param>
		/// <returns>Copy of string with the padding applied</returns>
		public static string PadRight(this string val, string pad, int totalWidth)
		{
			return val.PadRight(pad, totalWidth, false);
		}

		/// <summary>
		/// Right pads the passed string using the passed pad string for the total number of spaces.
		/// </summary>
		/// <param name="val"></param>
		/// <param name="pad">The pad string</param>
		/// <param name="totalWidth">The total width of the resulting string</param>
		/// <param name="cutOff">True to cut off the characters if exceeds the specified width</param>
		/// <returns>Copy of string with the padding applied</returns>
		public static string PadRight(this string val, string pad, int totalWidth, bool cutOff)
		{
			if (val.Length >= totalWidth)
				return val;

			string paddedString = string.Empty;

			while (paddedString.Length < totalWidth - val.Length)
			{
				paddedString += pad;
			}

			if (cutOff)
				paddedString = paddedString.Substring(0, totalWidth - val.Length);
			paddedString += val;
			return paddedString;
		}

		public static string Reverse(this string val)
		{
			return string.Join("", val.Reverse());
		}

		#endregion 操作

		#region 数学関連

		/// <summary>
		/// Encodes to Base64
		/// Test Coverage: Included
		/// </summary>
		/// <param name="val"></param>
		/// <returns>Base 64 Encoded string</returns>
		public static string Base64StringEncode(this string val)
		{
			byte[] toEncodeAsBytes = ASCIIEncoding.ASCII.GetBytes(val);
			string returnValue = Convert.ToBase64String(toEncodeAsBytes);
			return returnValue;
		}

		/// <summary>
		/// Decodes a Base64 encoded string
		/// Test Coverage: Included
		/// </summary>
		/// <param name="val"></param>
		/// <returns>Base 64 decoded string</returns>
		public static string Base64StringDecode(this string val)
		{
			byte[] encodedDataAsBytes = Convert.FromBase64String(val);
			string returnValue = ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
			return returnValue;
		}

		/// <summary>
		/// Left pads the passed string using the HTML non-breaking space ( ) for the total number of spaces.
		/// Test Coverage: Included
		/// </summary>
		/// <param name="val"></param>
		/// <param name="totalSpaces">Total number of spaces to add</param>
		/// <returns>string after adding the Html Spaces</returns>
		public static string PadLeftHtmlSpaces(this string val, int totalSpaces)
		{
			string space = "&nbsp;";
			return PadLeft(val, space, val.Length + (totalSpaces * space.Length));
		}

		/// <summary>
		/// Right pads the passed string using the HTML non-breaking space ( ) for the total number of spaces.
		/// Test Coverage: Included
		/// </summary>
		/// <param name="val"></param>
		/// <param name="totalSpaces">total number of spaces to add</param>
		/// <returns>string after adding the Html Spaces</returns>
		public static string PadRightHtmlSpaces(this string val, int totalSpaces)
		{
			string space = "&nbsp;";
			return PadRight(val, space, val.Length + (totalSpaces * space.Length));
		}

		/// <summary>
		/// A wrapper around HttpUtility.HtmlEncode
		/// </summary>
		/// <param name="val"></param>
		/// <returns></returns>
		public static string HtmlSpecialEntitiesEncode(this string val)
		{
			return HttpUtility.HtmlEncode(val);
		}

		/// <summary>
		/// A wrapper around HttpUtility.HtmlDecode
		/// </summary>
		/// <param name="val"></param>
		/// <returns></returns>
		public static string HtmlSpecialEntitiesDecode(this string val)
		{
			return HttpUtility.HtmlDecode(val);
		}

		/// <summary>
		/// Encrypts a string to using MD5 algorithm
		/// </summary>
		/// <param name="val"></param>
		/// <returns>string representation of the MD5 encryption</returns>
		public static string MD5String(this string val)
		{
			MD5 md5Hasher = MD5.Create();
			byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(val));

			StringBuilder sBuilder = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}
			return sBuilder.ToString();
		}

		/// <summary>
		/// Verifies the string against the encrypted value for equality
		/// </summary>
		/// <param name="val"></param>
		/// <param name="hash">The encrypted value of the string</param>
		/// <returns>true is the given string is equal to the string encrypted</returns>
		public static bool VerifyMD5String(this string val, string hash)
		{
			string hashOfInput = MD5String(val);
			StringComparer comparer = StringComparer.OrdinalIgnoreCase;
			return 0 == comparer.Compare(hashOfInput, hash) ? true : false;
		}

		/// <summary>
		/// Converts all spaces to HTML non-breaking spaces
		/// Test Coverage: Included
		/// </summary>
		/// <param name="val"></param>
		/// <returns></returns>
		public static string SpaceToNbsp(this string val)
		{
			string space = "&nbsp;";
			return val.Replace(" ", space);
		}

		/// <summary>
		/// Removes all HTML tags from the passed string.
		/// Test Coverage: Included
		/// </summary>
		/// <param name="val"></param>
		/// <returns></returns>
		public static string StripTags(this string val)
		{
			Regex stripTags = new Regex("<(.|\n)+?>");
			return stripTags.Replace(val, "");
		}

		/// <summary>
		/// Converts each new line (\n) and carriage return (\r) symbols to the HTML <br /> tag.
		/// Test Coverage: Included
		/// </summary>
		/// <param name="val"></param>
		/// <returns></returns>
		public static string NewLineToBreak(this string val)
		{
			Regex regEx = new Regex(@"[\n|\r]+");
			return regEx.Replace(val, "<br/>");
		}

		/// <summary>
		/// Test Coverage: Included
		/// </summary>
		/// <param name="val"></param>
		/// <param name="charCount">The number of characters after which it should wrap the text</param>
		/// <returns>The copy of the string after applying the Wrap</returns>
		public static string WordWrap(this string val, int charCount)
		{
			return WordWrap(val, charCount, false, Environment.NewLine);
		}

		/// <summary>
		/// Wraps the passed string at the passed total number of characters (if cuttOff is true)
		/// or at the next whitespace (if cutOff is false).
		/// Uses the environment new line symbol for the break text
		/// </summary>
		/// <param name="val"></param>
		/// <param name="charCount">The number of characters after which to break</param>
		/// <param name="cutOff">true to break at specific</param>
		/// <returns></returns>
		public static string WordWrap(this string val, int charCount, bool cutOff)
		{
			return WordWrap(val, charCount, cutOff, Environment.NewLine);
		}

		private static string WordWrap(this string val, int charCount, bool cutOff, string breakText)
		{
			StringBuilder sb = new StringBuilder(val.Length + 100);
			int counter = 0;

			if (cutOff)
			{
				while (counter < val.Length)
				{
					if (val.Length > counter + charCount)
					{
						sb.Append(val.Substring(counter, charCount));
						sb.Append(breakText);
					}
					else
					{
						sb.Append(val.Substring(counter));
					}
					counter += charCount;
				}
			}
			else
			{
				string[] strings = val.Split(' ');
				for (int i = 0; i < strings.Length; i++)
				{
					// added one to represent the space.
					counter += strings[i].Length + 1;
					if (i != 0 && counter > charCount)
					{
						sb.Append(breakText);
						counter = 0;
					}

					sb.Append(strings[i] + ' ');
				}
			}
			// to get rid of the extra space at the end.
			return sb.ToString().TrimEnd();
		}

		#endregion 数学関連
	}
}