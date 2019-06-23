using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Milky.IO
{
	public class ZipArchiver
	{
		public delegate void DecompressDelegate(int count, int max);

		public DecompressDelegate OnDecompress;

		/// <summary>
		/// 指定パス以下に含まれるファイルパス一覧の作成
		/// </summary>
		/// <param name="files">ファイルパス一覧</param>
		/// <param name="root">一覧を作るディレクトリパス</param>
		private void GetFiles(List<string> files, string root)
		{
			if (Directory.Exists(root))
			{
				files.AddRange(Directory.GetFiles(root));
				foreach (var dir in Directory.GetDirectories(root))
				{
					GetFiles(files, dir);
				}
			}
			else if (File.Exists(root))
			{
				files.Add(root);
			}
		}

		/// <summary>
		/// 圧縮する
		/// </summary>
		/// <param name="zipFile">圧縮ファイルパス</param>
		/// <param name="basePath">圧縮対象の基準ディレクトリ</param>
		/// <param name="srcFiles">圧縮対象のファイル・ディレクトリパス達</param>
		/// <param name="compressionLevel">圧縮レベル。未指定でOptimal。</param>
		/// <returns>成功すればtrue</returns>
		public bool Compreession(string zipFile, string basePath, string[] srcFiles, CompressionLevel compressionLevel = CompressionLevel.Optimal)
		{
			if (!Directory.Exists(basePath))
				return false;
			if (File.Exists(zipFile))
			{
				//if( MessageBox.Show( "存在しています。上書きしますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.No )
				//return false;
			}
			if ((basePath[basePath.Length - 1] != '/') && (basePath[basePath.Length - 1] != '\\'))
			{
				basePath += "\\";
			}
			//ファイル一覧の作成
			List<string> files = new List<string>();
			foreach (var path in srcFiles)
			{
				GetFiles(files, path);
			}
			using (var zip = ZipFile.Open(zipFile, ZipArchiveMode.Update))
			{
				foreach (var path in files)
				{
					string compressPath = path.Substring(basePath.Length, path.Length - basePath.Length);
					zip.CreateEntryFromFile(path, compressPath, compressionLevel);
				}
			}

			return true;
		}

		/// <summary>
		/// 解凍する
		/// </summary>
		/// <param name="zipFile">圧縮ファイルのパス</param>
		/// <param name="basePath">解凍先ディレクトリパス</param>
		/// <returns>成功したらtrue</returns>
		public bool Decompression(string zipFile, string basePath)
		{
			if (!File.Exists(zipFile))
				return false;

			if (!Directory.Exists(basePath))
			{
				Directory.CreateDirectory(basePath);
			}

			int files = 0;
			using (var zip = ZipFile.OpenRead(zipFile))
			{
				foreach (ZipArchiveEntry entry in zip.Entries)
				{
					files++;
				}
			}

			if (OnDecompress != null)
			{
				OnDecompress(0, files);
			}

			int count = 0;
			using (var zip = ZipFile.OpenRead(zipFile))
			{
				foreach (ZipArchiveEntry entry in zip.Entries)
				{
					string decompFilePath = Path.Combine(basePath, entry.FullName);
					string decompPath = Path.GetDirectoryName(decompFilePath);
					if (!Directory.Exists(decompPath))
					{
						Directory.CreateDirectory(decompPath);
					}
					string decompFile = Path.Combine(basePath, entry.FullName);
					if (File.Exists(decompFile))
					{
						File.Delete(decompFile);
					}
					if (!File.Exists(decompFile))
					{
						entry.ExtractToFile(Path.Combine(basePath, entry.FullName));
					}
					if (OnDecompress != null)
					{
						OnDecompress(count, files);
					}
					count++;
				}
			}
			return true;
		}
	}
}
