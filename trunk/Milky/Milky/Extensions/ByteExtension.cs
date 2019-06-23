using System;

namespace Milky.Extensions
{
	public static class ByteExtension
	{
		/// <summary>
		/// Byte配列をHex文字列に変換
		/// </summary>
		/// <param name="bytes">byte配列</param>
		/// <returns>16進文字列</returns>
		public static string ToHexString(this byte[] bytes)
		{
			return BitConverter.ToString(bytes).Replace("-", "");
		}
	}
}