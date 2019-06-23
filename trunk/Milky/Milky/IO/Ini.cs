using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Milky.IO
{
	public class Ini
	{
		private class IniFileHandler
		{
			[DllImport("KERNEL32.DLL", SetLastError = false)]
			public static extern Int32 GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, Int32 nSize, string lpFileName);

			[DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileStringA", SetLastError = false)]
			public static extern Int32 GetPrivateProfileStringByByteArray(string lpAppName, string lpKeyName, string lpDefault, byte[] lpReturnedString, Int32 nSize, string lpFileName);

			[DllImport("KERNEL32.DLL", SetLastError = false)]
			public static extern Int32 GetPrivateProfileInt(string lpAppName, string lpKeyName, Int32 nDefault, string lpFileName);

			[DllImport("KERNEL32.DLL", SetLastError = false)]
			public static extern Int32 WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
		}

		#region API

		/// <summary>
		/// INI形式のファイルから目的のデータを取得する。
		/// </summary>
		public long GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, long lSize, string lpFileName)
		{
			return (long)IniFileHandler.GetPrivateProfileString(
						lpAppName,
						lpKeyName,
						lpDefault,
						lpReturnedString,
						(int)lSize,
						lpFileName);
		}

		/// <summary>
		/// INI形式のファイルから目的のデータを取得する。
		/// </summary>
		public long GetPrivateProfileStringByByteArray(string lpAppName, string lpKeyName, string lpDefault, byte[] lpReturnedString, long lSize, string lpFileName)
		{
			return (long)IniFileHandler.GetPrivateProfileStringByByteArray(
						lpAppName,
						lpKeyName,
						lpDefault,
						lpReturnedString,
						(int)lSize,
						lpFileName);
		}

		/// <summary>
		/// INI形式のファイルから目的のデータを取得する。
		/// </summary>
		public long GetPrivateProfileInt(string lpAppName, string lpKeyName, long lDefault, string lpFileName)
		{
			return (long)IniFileHandler.GetPrivateProfileInt(
						lpAppName,
						lpKeyName,
						(int)lDefault,
						lpFileName);
		}

		/// <summary>
		/// INI形式のファイルから目的のデータを取得する。
		/// </summary>
		public long WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName)
		{
			return (long)IniFileHandler.WritePrivateProfileString(
						lpAppName,
						lpKeyName,
						lpString,
						lpFileName);
		}

		/// <summary>
		/// INI形式のファイルから目的のデータを取得する。
		/// </summary>
		public long WritePrivateProfileInt(string lpAppName, string lpKeyName, long lValue, string lpFileName)
		{
			return (long)WritePrivateProfileString(
				lpAppName,
				lpKeyName,
				((uint)lValue).ToString(),
				lpFileName);
		}

		#endregion API
	}
}