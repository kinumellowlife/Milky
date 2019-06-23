using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Milky.Extensions;

namespace Milky.IO
{
	/// <summary>
	/// ディレクトリ以下のファイルをバックグランドで検索する。
	/// </summary>
	public class FilePicker
	{
		#region delegates

		/// <summary>
		/// ファイルを見つけたときに呼ばれるデリゲート
		/// </summary>
		/// <param name="fileName">ファイルパス</param>
		public delegate void FindFileDelegate(string fileName);

		/// <summary>
		/// ファイル検索終了時に呼ばれるデリゲート
		/// </summary>
		public delegate void FindFileCompleteDelegate();

		/// <summary>
		/// ファイル検索終了
		/// </summary>
		public FindFileCompleteDelegate OnFindComplete;

		/// <summary>
		/// ファイル発見
		/// </summary>
		public FindFileDelegate OnFindFile;

		#endregion delegates

		#region フィールド

		/// <summary>ロック用オブジェクト</summary>
		private readonly Object lockFilelist = new Object();

		/// <summary>
		/// ファイルリストアクセス用インデクサ
		/// </summary>
		/// <remarks>
		/// ファイルリスト内、次のファイル名格納位置を示す
		/// </remarks>
		protected int nextFilePos;

		/// <summary>基準ディレクトリ</summary>
		protected string baseDir = "c:\\";

		/// <summary>検索フィルター</summary>
		protected string filters = "*.*";

		/// <summary>対象ファイル名リスト</summary>
		protected List<string> findFiles;

		/// <summary>
		/// 検索スレッド
		/// </summary>
		private Thread findThread;

		#endregion フィールド

		#region 構築

		/// <summary>
		/// 構築
		/// </summary>
		public FilePicker()
		{
			findFiles = new List<string>();
		}

		#endregion 構築

		#region プロパティ

		/// <summary>
		/// 動作中かどうか
		/// </summary>
		public bool OperationFlag { get; set; } = false;

		/// <summary>
		/// 対象ファイル検索中かどうか
		/// </summary>
		public bool NowSearching { get; private set; } = false;

		/// <summary>
		/// 基準ディレクトリ
		/// </summary>
		public string BaseDir {
			set
			{
				if (Directory.Exists(value))
					this.baseDir = value;
				else if (File.Exists(value))
					this.baseDir = Path.GetDirectoryName(value);
			}
			get
			{
				return this.baseDir;
			}
		}

		/// <summary>
		/// 検索フィルタ
		/// </summary>
		public string Filters {
			set
			{
				if (value == "")
				{
					this.filters = "*.*";
				}
				else
				{
					this.filters = value;
					if (value[value.Length - 1] != ';')
						this.filters += ";";
				}
			}
			get
			{
				return this.filters;
			}
		}

		/// <summary>
		/// 対象ファイル総数
		/// </summary>
		public int Count {
			get
			{
				return findFiles.Count;
			}
		}

		/// <summary>
		/// まだ処理していないファイルの数
		/// </summary>
		public int Remains {
			get
			{
				return this.Count - nextFilePos;
			}
		}

		/// <summary>
		/// 次処理するファイルを取得する。なければ空文字が返る。
		/// </summary>
		public string NextFile {
			get
			{
				lock (lockFilelist)
				{
					if (nextFilePos < this.Count)
					{
						string target = findFiles[nextFilePos];
						nextFilePos++;
						return target;
					}
					else
					{
						return "";
					}
				}
			}
		}

		/// <summary>
		/// サブディレクトリも検索するかどうか
		/// </summary>
		public bool SearchSubDir { get; set; } = true;

		#endregion プロパティ

		#region 内部操作

		/// <summary>
		/// 対象ファイル検索初期化処理
		/// </summary>
		protected void Init()
		{
			if (findThread != null)
			{
				if (OnFindComplete != null)
				{
					OnFindComplete();
				}
				findThread.Abort();
				findThread = null;
			}
			lock (lockFilelist)
			{
				findFiles.Clear();
				nextFilePos = 0;
			}
		}

		#endregion 内部操作

		#region 操作

		/// <summary>
		/// 検索スレッド
		/// </summary>
		private void FindTH()
		{
			// 検索開始
			NowSearching = true;

			// 対象ファイルフィルターの分割
			string[] extensions = this.filters.SplitTrim(new char[] { ';', ':', ',' });
			// ディレクトリキュー
			Stack<string> dirStack = new Stack<string>();
			// とりあえず基準ディレクトリを格納
			dirStack.Push(baseDir);

			// スタック内、未検索ディレクトリが空っぽになるまで処理
			while (dirStack.Count > 0)
			{
				// キューから一個取り出す
				string currentDir = dirStack.Pop();
				if (Directory.Exists(currentDir))
				{
					// 対象フィルタ一つずつ処理
					foreach (string ext in extensions)
					{
						// 対象ファイルを取得
						string[] targetFiles = Directory.GetFiles(currentDir, ext);
						foreach (string file in targetFiles)
						{
							// ロックしながら
							lock (lockFilelist)
							{
								// リストに格納
								findFiles.Add(file);
							}
							// ファイル見つかった！
							if (OnFindFile != null)
							{
								OnFindFile(file);
							}
						}
					}
				}
				if (this.SearchSubDir)
				{
					// ディレクトリリストを取得
					string[] diveDirs = Directory.GetDirectories(currentDir);
					// キューに溜める
					foreach (string dir in diveDirs)
					{
						dirStack.Push(dir);
					}
				}
			}

			// 検索終了
			NowSearching = false;

			// 全てのファイルが取得されるまで待つ
			while ((Remains > 0) || (OperationFlag))
			{
				System.Threading.Thread.Sleep(50);
			}

			// 終了通知
			if (OnFindComplete != null)
			{
				OnFindComplete();
			}

			findThread = null;
		}

		/// <summary>
		/// 対象ファイル検索処理
		/// </summary>
		public void Start()
		{
			// 初期化
			Init();

			if (string.IsNullOrEmpty(this.baseDir))
				return;

			findThread = new Thread(FindTH);
			findThread.IsBackground = true;
			findThread.Start();
		}

		/// <summary>
		/// 中段する
		/// </summary>
		public void Abort()
		{
			this.OperationFlag = false;
		}

		#endregion 操作
	}
}