using System;
using System.IO;
using System.Windows.Forms;

namespace Milky.IO
{
	public static class PathHelper
	{
		/// <summary>
		/// パスの結合
		/// </summary>
		/// <param name="appendPaths">くっつける相対パス</param>
		/// <returns>短くしたパス</returns>
		static public string Append(params string[] appendPaths)
		{
			try
			{
				Uri absUri = null;
				foreach (string path in appendPaths)
				{
					if (absUri == null)
					{
						absUri = new Uri(path);
					}
					else
					{
						absUri = new Uri(Path.Combine(absUri.LocalPath, path));
					}
				}
				if (absUri == null)
				{
					return "";
				}
				else
				{
					return absUri.LocalPath;
				}
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
			return "";
		}

		/// <summary>
		/// ファイル名として問題ないかどうかチェックする
		/// </summary>
		/// <param name="check">チェックする対象のファイルパス</param>
		/// <returns>正常ならtrue</returns>
		static public bool IsValidFilename(string check)
		{
			char[] invalidChars = Path.GetInvalidFileNameChars();
			if (check.IndexOfAny(invalidChars) < 0)
			{
				invalidChars = Path.GetInvalidPathChars();
				if (check.IndexOfAny(invalidChars) < 0)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// ファイル名やパスに使ってはいけない文字を使っている場所を取得
		/// </summary>
		/// <param name="path">チェックするファイルパス</param>
		/// <returns>正常なら-1</returns>
		static public int GetInvalidCharIndex(string path)
		{
			if (IsValidFilename(path))
				return -1;

			char[] invalidChars = Path.GetInvalidFileNameChars();
			int index = path.IndexOfAny(invalidChars);
			if (index < 0)
			{
				invalidChars = Path.GetInvalidPathChars();
				index = path.IndexOfAny(invalidChars);
			}
			return index;
		}

		/// <summary>
		/// basePathから見たtargetPathへの相対パスを取得する
		/// </summary>
		/// <param name="basePath">基準となるパス</param>
		/// <param name="targetPath">計算対象のパス</param>
		/// <returns>相対パス</returns>
		static public string GetRelativePath(string basePath, string targetPath)
		{
			Uri u1 = new Uri(basePath);
			Uri u2 = new Uri(targetPath);
			string relativePath = System.Web.HttpUtility.UrlDecode(u1.MakeRelativeUri(u2).ToString()).Replace('/', '\\');

			return relativePath;
		}

		/// <summary>
		/// ファイルパスをクリップボードにコピーする
		/// </summary>
		/// <param name="paths">コピーするファイルパス</param>
		static public void CopyFilesToClipboard(params string[] paths)
		{
			//コピーするファイルのパスをStringCollectionに追加する
			System.Collections.Specialized.StringCollection files = new System.Collections.Specialized.StringCollection();
			files.AddRange(paths);
			//クリップボードにコピーする
			Clipboard.SetFileDropList(files);
		}

		/// <summary>
		/// ファイルパスをクリップボードにカットする
		/// </summary>
		/// <param name="paths">切り取るファイルのパス</param>
		static public void CutFilesToClipboard(params string[] paths)
		{
			//ファイルドロップ形式のDataObjectを作成する
			IDataObject data = new DataObject(DataFormats.FileDrop, paths);

			//DragDropEffects.Moveを設定する（DragDropEffects.Move は 2）
			byte[] bs = new byte[] { (byte)DragDropEffects.Move, 0, 0, 0 };
			MemoryStream ms = new MemoryStream(bs);
			data.SetData("Preferred DropEffect", ms);

			//クリップボードに切り取る
			Clipboard.SetDataObject(data);
		}

		/// <summary>
		/// クリップボードにコピーされたパスを取得する
		/// </summary>
		/// <returns>コピーされたパス</returns>
		static public string[] GetCopiedClipbpardFiles()
		{
			//クリップボードにファイルドロップ形式のデータがあるか確認
			if (Clipboard.ContainsFileDropList())
			{
				//データを取得する（取得できなかった時はnull）
				System.Collections.Specialized.StringCollection files = Clipboard.GetFileDropList();
				string[] results = new string[files.Count];
				for (int index = 0; index < files.Count; index++)
				{
					results[index] = files[index];
				}
				return results;
			}
			return null;
		}

		/// <summary>
		/// ファイルが存在しているかどうか
		/// </summary>
		/// <param name="file">ファイルパス</param>
		/// <returns>存在していればtrue</returns>
		static public bool FileExists(string file)
		{
			if (string.IsNullOrEmpty(file))
				return false;
			if (File.Exists(file) == false)
				return false;

			return true;
		}

		/// <summary>
		/// ディレクトリが存在しているかどうか
		/// </summary>
		/// <param name="path">パス</param>
		/// <returns>存在していればtrue</returns>
		static public bool DirectoryExists(string path)
		{
			if (string.IsNullOrEmpty(path))
				return false;
			if (Directory.Exists(path) == false)
				return false;

			return true;
		}

		/// <summary>
		/// fromのパスのファイル名をchangeFilenameに変更する
		/// </summary>
		/// <param name="from"></param>
		/// <param name="changeFilename"></param>
		/// <returns></returns>
		static public string ChangeFilename(string from, string changeFilename)
		{
			string basePath = Path.GetDirectoryName(from);
			return PathHelper.Append(basePath, changeFilename);
		}

		/// <summary>
		/// 相対パスを絶対パスに変換する
		/// </summary>
		/// <param name="basePath">基準パス</param>
		/// <param name="relativePath">早退パス</param>
		/// <returns>絶対パス</returns>
		static public string PathRelativeToAbs(string basePath, string relativePath)
		{
			if (Path.IsPathRooted(relativePath) == false)
			{
				//相対パスが指定されたら、現在読み込んでるファイルからパスを決める
				string dirPath = Path.GetDirectoryName(basePath);
				return PathHelper.Append(Path.GetDirectoryName(basePath), relativePath);
			}
			return relativePath;
		}

		/// <summary>
		/// 指定したファイルのMD5を取得する
		/// </summary>
		/// <param name="file">ファイル</param>
		/// <returns>MD5</returns>
		private static byte[] GetMD5(string file)
		{
			if (FileExists(file) == false)
				return null;
			//ファイルを開く
			FileStream fs = new System.IO.FileStream(
				file,
				System.IO.FileMode.Open,
				System.IO.FileAccess.Read,
				System.IO.FileShare.Read);

			//MD5CryptoServiceProviderオブジェクトを作成
			System.Security.Cryptography.MD5CryptoServiceProvider md5 =
				new System.Security.Cryptography.MD5CryptoServiceProvider();

			//ハッシュ値を計算する
			byte[] bs = md5.ComputeHash(fs);

			//ファイルを閉じる
			fs.Close();

			return bs;
		}

		/// <summary>
		/// 指定したファイルのSHA1を取得
		/// </summary>
		/// <param name="file">ファイル</param>
		/// <returns>SHA1</returns>
		private static byte[] GetSHA1(string file)
		{
			//ファイルを開く
			System.IO.FileStream fs = new System.IO.FileStream(file, System.IO.FileMode.Open, System.IO.FileAccess.Read);

			//SHA1CryptoServiceProviderオブジェクトを作成
			System.Security.Cryptography.SHA1CryptoServiceProvider sha1 =
				new System.Security.Cryptography.SHA1CryptoServiceProvider();

			//ハッシュ値を計算する
			byte[] bs = sha1.ComputeHash(fs);

			//ファイルを閉じる
			fs.Close();

			return bs;
		}

		/// <summary>
		/// ディレクトリをコピーする
		/// </summary>
		/// <param name="sourceDirName">コピーするディレクトリ</param>
		/// <param name="destDirName">コピー先のディレクトリ</param>
		/// <param name="newerOnly">新しいファイルのみコピーする</param>
		/// <param name="sync">sourceDirNameにないファイルを削除する</param>
		public static void Sync(string sourceDirName, string destDirName, bool newerOnly, bool sync)
		{
			//コピー先のディレクトリがないときは作る
			if (!Directory.Exists(destDirName))
			{
				Directory.CreateDirectory(destDirName);
				//属性もコピー
				File.SetAttributes(destDirName, File.GetAttributes(sourceDirName));
			}

			//コピー先のディレクトリ名の末尾に"\"をつける
			if (destDirName[destDirName.Length - 1] != Path.DirectorySeparatorChar)
			{
				destDirName = destDirName + Path.DirectorySeparatorChar;
			}

			//コピー元のディレクトリにあるファイルをコピー
			if (Directory.Exists(sourceDirName))
			{
				string[] files = Directory.GetFiles(sourceDirName);
				foreach (string f in files)
				{
					string destFileName = destDirName + Path.GetFileName(f);
					//コピー先にファイルが存在し、
					//コピー元より更新日時が古い時はコピーする
					if (!newerOnly ||
						!File.Exists(destFileName) ||
						File.GetLastWriteTime(destFileName) < File.GetLastWriteTime(f))
					{
						File.Copy(f, destFileName, true);
					}
				}
			}
			//コピー先にあってコピー元にないファイルを削除
			if (sync)
			{
				DeleteNotExistFiles(sourceDirName, destDirName);
			}

			//コピー元のディレクトリにあるディレクトリについて、再帰的に呼び出す
			string[] dirs = Directory.GetDirectories(sourceDirName);
			foreach (string dir in dirs)
			{
				Sync(dir, destDirName + Path.GetFileName(dir), newerOnly, sync);
			}
		}

		/// <summary>
		/// destDirNameにありsourceDirNameにないファイルを削除する
		/// </summary>
		/// <param name="sourceDirName">比較先のフォルダ</param>
		/// <param name="destDirName">比較もとのフォルダ</param>
		private static void DeleteNotExistFiles(string sourceDirName, string destDirName)
		{
			//sourceDirNameの末尾に"\"をつける
			if (sourceDirName[sourceDirName.Length - 1] != Path.DirectorySeparatorChar)
			{
				sourceDirName = sourceDirName + Path.DirectorySeparatorChar;
			}

			//destDirNameにありsourceDirNameにないファイルを削除する
			string[] files = Directory.GetFiles(destDirName);
			foreach (string f in files)
			{
				if (!File.Exists(sourceDirName + Path.GetFileName(f)))
				{
					File.Delete(f);
				}
			}

			//destDirNameにありsourceDirNameにないフォルダを削除する
			string[] folders = Directory.GetDirectories(destDirName);
			foreach (string folder in folders)
			{
				if (!Directory.Exists(sourceDirName + Path.GetFileName(folder)))
				{
					Directory.Delete(folder, true);
				}
			}
		}

		/// <summary>
		/// ゴミ箱に入れる
		/// </summary>
		/// <param name="fileName"></param>
		public static void SendToRecycleBin(string fileName)
		{
			try
			{
				Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(fileName, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
			}
			catch (Exception) { }
		}

		/// <summary>
		/// 書込み禁止でも削除する
		/// </summary>
		/// <param name="fileName"></param>
		public static void ForceDelete(string fileName)
		{
			if (FileExists(fileName))
			{
				try
				{
					//読み取り専用を解除
					FileAttributes attr = File.GetAttributes(fileName);
					attr &= ~FileAttributes.ReadOnly;
					File.SetAttributes(fileName, attr);
					//削除
					File.Delete(fileName);
				}
				catch (Exception) { }
			}
		}
	}
}