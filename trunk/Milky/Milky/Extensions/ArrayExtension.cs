﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Milky.Extensions
{
	public static class ArrayExtension
	{
		/// <summary>
		/// 配列を文字列に変換する
		/// </summary>
		/// <param name="array">配列</param>
		/// <param name="format">書式</param>
		/// <param name="separator">１バイトごとのセパレータ</param>
		/// <returns>変換後の文字列</returns>
		public static string ToString<T>(this T[] array, string format, string separator = "")
		{
			string ret = "";
			if (array == null)
				return "";
			array.ForAll((index, data) =>
			{
				ret += String.Format(format, data);
				if (index < (array.Length - 1))
				{
					ret += separator;
				}
			});
			return ret;
		}

		/// <summary>
		/// 配列を文字列に変換する
		/// </summary>
		/// <param name="array">配列</param>
		/// <param name="format">書式</param>
		/// <param name="separator">１バイトごとのセパレータ</param>
		/// <returns>変換後の文字列</returns>
		public static string ToString<T>(this T[] array, Func<int,T,string> convert, string separator = "")
		{
			string ret = "";
			if (array == null)
				return "";
			array.ForAll((index, data) =>
			{
				ret += convert(index, data);

				if (index < (array.Length - 1))
				{
					ret += separator;
				}
			});
			return ret;
		}

		/// <summary>
		/// 配列を文字列に変換する
		/// </summary>
		/// <param name="array">配列</param>
		/// <param name="format">書式</param>
		/// <param name="separator">１バイトごとのセパレータ</param>
		/// <returns>変換後の文字列</returns>
		public static string ToString<T>(this IEnumerable<T> list, string format, string separator = "")
		{
			string ret = "";
			int count = list.Count();
			list.ForAll((index, data) =>
			{
				ret += String.Format(format, data);
				if (index < (list.Count() - 1))
				{
					ret += separator;
				}
			});
			return ret;
		}

		/// <summary>
		/// reverse array
		/// </summary>
		/// <typeparam name="T">type of array</typeparam>
		/// <param name="array">target array</param>
		public static void Reverse<T>(this T[] array)
		{
			Array.Reverse(array);
		}

		/// <summary>
		/// convert to byte array
		/// </summary>
		/// <param name="value">convert from value</param>
		/// <returns>byte array</returns>
		public static byte[] ToBytes(this byte value)
		{
			return new byte[] { value };
		}

		/// <summary>
		/// convert to byte array
		/// </summary>
		/// <param name="value">convert from value</param>
		/// <returns>byte array</returns>
		public static byte[] ToBytes(this short value)
		{
			return BitConverter.GetBytes(value);
		}

		/// <summary>
		/// convert to byte array
		/// </summary>
		/// <param name="value">convert from value</param>
		/// <returns>byte array</returns>
		public static byte[] ToBytes(this int value)
		{
			return BitConverter.GetBytes(value);
		}

		/// <summary>
		/// convert to byte array
		/// </summary>
		/// <param name="value">convert from value</param>
		/// <returns>byte array</returns>
		public static byte[] ToBytes(this long value)
		{
			return BitConverter.GetBytes(value);
		}

		/// <summary>
		/// convert to byte array
		/// </summary>
		/// <param name="value">convert from value</param>
		/// <returns>byte array</returns>
		public static byte[] ToBytes(this ushort value)
		{
			return BitConverter.GetBytes(value);
		}

		/// <summary>
		/// convert to byte array
		/// </summary>
		/// <param name="value">convert from value</param>
		/// <returns>byte array</returns>
		public static byte[] ToBytes(this uint value)
		{
			return BitConverter.GetBytes(value);
		}

		/// <summary>
		/// convert to byte array
		/// </summary>
		/// <param name="value">convert from value</param>
		/// <returns>byte array</returns>
		public static byte[] ToBytes(this ulong value)
		{
			return BitConverter.GetBytes(value);
		}

		/// <summary>
		/// convert to byte array
		/// </summary>
		/// <param name="value">convert from value</param>
		/// <returns>byte array</returns>
		public static byte GetByte(this byte[] array)
		{
			if (array.Length < 1)
				return 0;
			return array[0];
		}

		/// <summary>
		/// convert to byte array
		/// </summary>
		/// <param name="value">convert from value</param>
		/// <returns>byte array</returns>
		public static short GetShort(this byte[] array)
		{
			if (array.Length < 2)
				return 0;
			return BitConverter.ToInt16(array, 0);
		}

		/// <summary>
		/// convert to byte array
		/// </summary>
		/// <param name="value">convert from value</param>
		/// <returns>byte array</returns>
		public static int GetInt(this byte[] array)
		{
			if (array.Length < 4)
				return 0;
			return BitConverter.ToInt32(array, 0);
		}

		/// <summary>
		/// convert to byte array
		/// </summary>
		/// <param name="value">convert from value</param>
		/// <returns>byte array</returns>
		public static long GetLong(this byte[] array)
		{
			if (array.Length < 8)
				return 0;
			return BitConverter.ToInt64(array, 0);
		}

		/// <summary>
		/// convert to byte array
		/// </summary>
		/// <param name="value">convert from value</param>
		/// <returns>byte array</returns>
		public static ushort GetUShort(this byte[] array)
		{
			if (array.Length < 2)
				return 0;
			return BitConverter.ToUInt16(array, 0);
		}

		/// <summary>
		/// convert to byte array
		/// </summary>
		/// <param name="value">convert from value</param>
		/// <returns>byte array</returns>
		public static uint GetUInt(this byte[] array)
		{
			if (array.Length < 4)
				return 0;
			return BitConverter.ToUInt32(array, 0);
		}

		/// <summary>
		/// convert to byte array
		/// </summary>
		/// <param name="value">convert from value</param>
		/// <returns>byte array</returns>
		public static ulong GetULong(this byte[] array)
		{
			if (array.Length < 8)
				return 0;
			return BitConverter.ToUInt64(array, 0);
		}
	}
}