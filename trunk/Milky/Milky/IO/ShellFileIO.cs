using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Milky.IO
{
	/// <summary>
	/// シェルを利用したファイル操作（コピー、削除、移動）
	/// </summary>
	public class ShellFileIO
	{
		#region shell

		/// <summary>
		/// シェルの動作モード
		/// </summary>
		public enum FileFuncFlags : uint
		{
			/// <summary>
			/// 移動
			/// </summary>
			FO_MOVE = 0x1,

			/// <summary>
			/// コピー
			/// </summary>
			FO_COPY = 0x2,

			/// <summary>
			/// 削除
			/// </summary>
			FO_DELETE = 0x3,

			/// <summary>
			/// リネーム
			/// </summary>
			FO_RENAME = 0x4
		}

		/// <summary>
		/// ドロップ動作定義
		/// </summary>
		[Flags]
		public enum FILEOP_FLAGS : ushort
		{
			/// <summary>
			///
			/// </summary>
			FOF_MULTIDESTFILES = 0x1,

			/// <summary>
			///
			/// </summary>
			FOF_CONFIRMMOUSE = 0x2,

			/// <summary>
			/// Don't create progress/report
			/// </summary>
			FOF_SILENT = 0x4,

			/// <summary>
			///
			/// </summary>
			FOF_RENAMEONCOLLISION = 0x8,

			/// <summary>
			/// Don't prompt the user.
			/// </summary>
			FOF_NOCONFIRMATION = 0x10,

			/// <summary>
			/// Fill in SHFILEOPSTRUCT.hNameMappings.
			/// Must be freed using SHFreeNameMappings
			/// </summary>
			FOF_WANTMAPPINGHANDLE = 0x20,

			/// <summary>
			///
			/// </summary>
			FOF_ALLOWUNDO = 0x40,

			/// <summary>
			/// On *.*, do only files
			/// </summary>
			FOF_FILESONLY = 0x80,

			/// <summary>
			/// Don't show names of files
			/// </summary>
			FOF_SIMPLEPROGRESS = 0x100,

			/// <summary>
			/// Don't confirm making any needed dirs
			/// </summary>
			FOF_NOCONFIRMMKDIR = 0x200,

			/// <summary>
			/// Don't put up error UI
			/// </summary>
			FOF_NOERRORUI = 0x400,

			/// <summary>
			/// Dont copy NT file Security Attributes
			/// </summary>
			FOF_NOCOPYSECURITYATTRIBS = 0x800,

			/// <summary>
			/// Don't recurse into directories.
			/// </summary>
			FOF_NORECURSION = 0x1000,

			/// <summary>
			/// Don't operate on connected elements.
			/// </summary>
			FOF_NO_CONNECTED_ELEMENTS = 0x2000,

			/// <summary>
			/// During delete operation,
			/// warn if nuking instead of recycling (partially overrides FOF_NOCONFIRMATION)
			/// </summary>
			FOF_WANTNUKEWARNING = 0x4000,

			/// <summary>
			/// Treat reparse points as objects, not containers
			/// </summary>
			FOF_NORECURSEREPARSE = 0x8000
		}

		//[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
		//If you use the above you may encounter an invalid memory access exception (when using ANSI
		//or see nothing (when using unicode) when you use FOF_SIMPLEPROGRESS flag.
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct SHFILEOPSTRUCT
		{
			/// <summary>
			/// ウィンドウハンドル
			/// </summary>
			public IntPtr hwnd;

			/// <summary>
			/// 動作フラグ
			/// </summary>
			public FileFuncFlags wFunc;

			/// <summary>
			/// 操作対象元
			/// </summary>
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pFrom;

			/// <summary>
			/// 操作対象先
			/// </summary>
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pTo;

			/// <summary>
			/// ドロップ制御フラグ
			/// </summary>
			public FILEOP_FLAGS fFlags;

			[MarshalAs(UnmanagedType.Bool)]
			public bool fAnyOperationsAborted;

			public IntPtr hNameMappings;

			[MarshalAs(UnmanagedType.LPWStr)]
			public string lpszProgressTitle;
		}

		[DllImport("shell32.dll", CharSet = CharSet.Unicode)]
		private static extern int SHFileOperation([In] ref SHFILEOPSTRUCT lpFileOp);

		/// <summary>
		/// シェルにコピーしてもらう
		/// </summary>
		/// <param name="handle">コピー元ウィンドウハンドル</param>
		/// <param name="from">コピー元ファイル</param>
		/// <param name="to">コピー先</param>
		public void Copy(IntPtr handle, string from, string to)
		{
			Copy(handle, new List<string> { from }, to);
		}

		/// <summary>
		/// シェルにコピーしてもらう
		/// </summary>
		/// <param name="handle">コピー元ウィンドウハンドル</param>
		/// <param name="from">コピー元ファイル</param>
		/// <param name="to">コピー先</param>
		public void Copy(IntPtr handle, List<string> from, string to)
		{
			Operate(handle, from, to, FileFuncFlags.FO_COPY);
		}

		/// <summary>
		/// シェルに移動してもらう
		/// </summary>
		/// <param name="handle">コピー元ウィンドウハンドル</param>
		/// <param name="from">コピー元ファイル</param>
		/// <param name="to">コピー先</param>
		public void Move(IntPtr handle, List<string> from, string to)
		{
			Operate(handle, from, to, FileFuncFlags.FO_MOVE);
		}

		/// <summary>
		/// シェルにリネームしてもらう
		/// </summary>
		/// <param name="handle">リネーム元ウィンドウハンドル</param>
		/// <param name="from">リネーム前</param>
		/// <param name="to">リネーム後</param>
		public void Rename(IntPtr handle, string from, string to)
		{
			Operate(handle, new List<string> { from }, to, FileFuncFlags.FO_RENAME);
		}

		/// <summary>
		/// シェルに削除してもらう
		/// </summary>
		/// <param name="handle">削除元ウィンドウハンドル</param>
		/// <param name="from">削除ファイル</param>
		public void Delete(IntPtr handle, string from)
		{
			Operate(handle, new List<string> { from }, "", FileFuncFlags.FO_DELETE);
		}

		private void Operate(IntPtr handle, List<string> from, string to, FileFuncFlags flag)
		{
			string toDir = to + "\0\0";

			string froms = "";
			foreach (var file in from)
			{
				froms += file + "\0";
			}
			froms += "\0";

			SHFILEOPSTRUCT shfos;
			shfos.hwnd = handle;
			shfos.wFunc = flag;
			shfos.pFrom = froms;
			shfos.pTo = toDir;
			shfos.fFlags = FILEOP_FLAGS.FOF_ALLOWUNDO;
			shfos.fAnyOperationsAborted = true;
			shfos.hNameMappings = IntPtr.Zero;
			shfos.lpszProgressTitle = null;

			SHFileOperation(ref shfos);
		}

		#endregion shell
	}
}