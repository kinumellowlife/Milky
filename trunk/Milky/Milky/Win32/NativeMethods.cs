using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Milky.Win32
{
	public static class NativeMethods
	{

		///<summary>Window Messages, added as requried</summary>
		public enum Msgs : int
		{

			/// <summary></summary>
			WM_KEYDOWN = 0x100,

			/// <summary></summary>
			WM_CLOSE = 0x0010,

			/// <summary></summary>
			WM_ENDSESSION = 0x16,

			/// <summary></summary>
			WM_SYSCOMMAND = 0x112,

			/// <summary></summary>
			SC_CLOSE = 0xF060,
		}

		/// <summary>
		/// DLLモジュールをマップ
		/// </summary>
		/// <param name="lpFileName"></param>
		/// <returns></returns>
		[DllImport("kernel32",
			EntryPoint = "LoadLibrary",
			SetLastError = true,
			CharSet = CharSet.Auto,
			ExactSpelling = false)]
		public extern static IntPtr LoadLibrary(
			[MarshalAs(UnmanagedType.LPTStr)]string lpFileName);

		/// <summary>
		/// DLLモジュールの参照カウントを1つ減らす
		/// </summary>
		/// <param name="hModule"></param>
		/// <returns></returns>
		[DllImport("kernel32",
			EntryPoint = "FreeLibrary",
			SetLastError = true,
			ExactSpelling = true)]
		public extern static bool FreeLibrary(
			IntPtr hModule);

		/// <summary>
		/// 関数のアドレスを取得
		/// </summary>
		/// <param name="hModule"></param>
		/// <param name="lpProcName"></param>
		/// <returns></returns>
		[DllImport("kernel32",
			EntryPoint = "GetProcAddress",
			SetLastError = true,
			CharSet = CharSet.Ansi,
			ExactSpelling = true)]
		public extern static IntPtr GetProcAddress(
			IntPtr hModule,
			[MarshalAs(UnmanagedType.LPStr)]string lpProcName);

		/// <summary>
		/// ファイルを検索
		/// </summary>
		/// <param name="lpPath"></param>
		/// <param name="lpFileName"></param>
		/// <param name="lpExtension"></param>
		/// <param name="nBufferLength"></param>
		/// <param name="lpBuffer"></param>
		/// <param name="lpFilePart"></param>
		/// <returns></returns>
		[DllImport("kernel32",
			EntryPoint = "SearchPath",
			SetLastError = true,
			CharSet = CharSet.Auto,
			ExactSpelling = false)]
		public static extern uint SearchPath(
			[MarshalAs(UnmanagedType.LPTStr)]string lpPath,
			[MarshalAs(UnmanagedType.LPTStr)]string lpFileName,
			[MarshalAs(UnmanagedType.LPTStr)]string lpExtension,
			uint nBufferLength,
			[MarshalAs(UnmanagedType.LPTStr)]StringBuilder lpBuffer,
			out IntPtr lpFilePart);

		///<summary>
		/// Send Message To a window
		///</summary>
		///<param text="hWnd" type="System.Runtime.InteropServices.HandleRef">
		/// Window Handle to Send Message To</param>
		///<param text="msg" type="int">Message Code</param>
		///<param text="wParam" type="int">WParameter to the Message</param>
		///<param text="lParam" type="int">lParameter to the Message</param>
		///<returns>System.IntPtr</returns>
		///
		[DllImport("user32", SetLastError = false)]
		public static extern IntPtr SendMessage(HandleRef hWnd, int msg,
												int wParam, int lParam);

		/// <summary>
		/// Beep
		/// </summary>
		/// <param name="dwFreq"></param>
		/// <param name="dwDuration"></param>
		/// <returns></returns>
		[DllImport("kernel32.dll", SetLastError = true)]
		private extern static bool Beep(uint dwFreq, uint dwDuration);

		///<summary>
		///Get the Current Caret Position in the box
		///</summary>
		///<param text="lpPoint" type="ref System.Drawing.Point">
		/// Point that is the position</param>
		///<returns>int</returns>
		///
		[DllImport("user32", SetLastError = false)]
		public static extern int GetCaretPos(ref Point lpPoint);

		/// <summary>
		///
		/// </summary>
		/// <param name="dwFlags"></param>
		/// <param name="lpSource"></param>
		/// <param name="dwMessageId"></param>
		/// <param name="dwLanguageId"></param>
		/// <param name="lpBuffer"></param>
		/// <param name="nSize"></param>
		/// <param name="Arguments"></param>
		/// <returns></returns>
		[DllImport("kernel32.dll", SetLastError = false)]
		public static extern uint FormatMessage(
		  Int32 dwFlags, IntPtr lpSource,
		  Int32 dwMessageId, Int32 dwLanguageId,
		  StringBuilder lpBuffer, Int32 nSize,
		  IntPtr Arguments);

		/// <summary>
		///
		/// </summary>
		/// <returns></returns>
		public static int GetLastError()
		{
			return Marshal.GetLastWin32Error();
		}

		/// <summary>
		///
		/// </summary>
		/// <returns></returns>
		public static string GetLastErrorMessage()
		{
			const int FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;
			StringBuilder message = new StringBuilder(255);
			int errCode = GetLastError();

			FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM,
						   IntPtr.Zero,
						   errCode,
						   0,
						   message,
						   message.Capacity,
						   IntPtr.Zero);

			return message.ToString();
		}

		/// <summary>
		/// メモリのコピー
		/// </summary>
		/// <param name="dst">コピー先</param>
		/// <param name="src">コピー元</param>
		/// <param name="size">コピーサイズ</param>
		[DllImport("kernel32.dll", SetLastError = false)]
		public static extern void CopyMemory(IntPtr dst, IntPtr src, int size);
	}
}
