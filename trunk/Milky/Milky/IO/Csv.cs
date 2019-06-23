using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

//VisualBasic名前空間のクラスなんて使いたくないんだけども
//こいつだけは便利なので使う。
using Microsoft.VisualBasic.FileIO;
using Milky.Extensions;

namespace Milky.IO
{
	/// <summary>
	/// CSVの１つのセルを表現するインタフェイス
	/// </summary>
	public interface ICsvCell
	{
		/// <summary>
		/// 値
		/// </summary>
		object Value { get; set; }

		/// <summary>
		/// CSVテキスト変換
		/// </summary>
		/// <returns></returns>
		object ToCsvText();
	}

	/// <summary>
	/// 文字列として動作するセル
	/// </summary>
	public class CsvStringCell : ICsvCell
	{
		private string value = "";

		#region properties

		/// <summary>
		/// 値の取得と設定
		/// </summary>
		public object Value {
			get
			{
				return this.value;
			}
			set
			{
				if (value is string set)
					this.value = set;
			}
		}

		#endregion properties

		#region construct

		public CsvStringCell()
		{
		}

		/// <summary>
		/// 構築
		/// </summary>
		/// <param name="init">初期値</param>
		public CsvStringCell(string init)
		{
			this.value = init;
		}

		#endregion construct

		/// <summary>
		/// 文字列化
		/// </summary>
		/// <returns>テキスト</returns>
		public override string ToString()
		{
			return this.value;
		}

		/// <summary>
		/// CSV文字列化
		/// </summary>
		/// <returns>CSVフォーマット化されたテキスト</returns>
		public object ToCsvText()
		{
			return $"\"{this.value}\"";
		}
	}

	/// <summary>
	/// 数値として動作するセル
	/// </summary>
	public class CsvNumericCell : ICsvCell
	{
		private long value = 0;

		#region properties

		/// <summary>
		/// 値の取得と設定
		/// </summary>
		public object Value {
			get
			{
				return this.value;
			}
			set
			{
				//内部で保持するlong型に変換できるなら格納する
				if (long.TryParse(value.ToString(), out long set))
				{
					this.value = set;
				}
			}
		}

		#endregion properties

		#region construct

		/// <summary>
		/// 構築
		/// </summary>
		public CsvNumericCell() { }

		/// <summary>
		/// 構築
		/// </summary>
		/// <param name="init">初期値</param>
		public CsvNumericCell(long init) { this.value = init; }

		#endregion construct

		/// <summary>
		/// 文字列化
		/// </summary>
		/// <returns>テキスト</returns>
		public override string ToString()
		{
			return this.value.ToString();
		}

		/// <summary>
		/// CSV文字列化
		/// </summary>
		/// <returns>CSVフォーマット化されたテキスト</returns>
		public object ToCsvText()
		{
			return $"=\"{this.value}\"";
		}
	}

	/// <summary>
	/// １つのCSVファイルを管理するクラス。
	/// 要素へのアクセスは以下の通り。
	/// Csv sheet = new Csv();
	/// sheet.Load( file );
	/// sheet[ row, col ] = 2;
	/// row, col は０はじまります。
	/// セルの要素へはobjectでつっこむので、適当なクラスをつっこんでToString()を実装すれば
	/// なんでもCSVのセルとして動作させれます。
	///
	/// sheet.Attach( row, col, T );で任意のセルをあとから自前クラスにスイッチ可能です。
	/// </summary>
	public class Csv
	{
		#region fields

		/// <summary>
		/// 編集中のファイルパス
		/// </summary>
		private string filePath;

		private int rows = 0;
		private int cols = 0;

		/// <summary>
		/// セル
		/// </summary>
		private SortedList<int, SortedList<int, object>> cells = new SortedList<int, SortedList<int, object>>();

		private Dictionary<string, List<Tuple<int, int>>> keyCells = new Dictionary<string, List<Tuple<int, int>>>();

		#endregion fields

		#region properties

		/// <summary>
		/// 行数を取得
		/// </summary>
		public int RowCount { get { return this.rows + 1; } }

		/// <summary>
		/// 列数を取得
		/// </summary>
		public int ColCount { get { return this.cols + 1; } }

		/// <summary>
		/// 要素の取得
		/// </summary>
		/// <param name="row"></param>
		/// <param name="col"></param>
		/// <returns></returns>
		public object this[int row, int col] {
			get
			{
				return Cells(row, col);
			}
			set
			{
				if (this.rows < row)
					this.rows = row;
				if (this.cols < col)
					this.cols = col;

				var cells = Rows(row);
				if (cells.ContainsKey(col) == false)
				{
					cells.Add(col, value);
				}
				else
				{
					if (value is ICsvCell)
					{
						if (cells[col] is ICsvCell)
						{
							//すでに別のインタフェイスが設定されているので何もしない。
						}
						else
						{
							object o = cells[col];
							((ICsvCell)value).Value = o;
							cells[col] = value;
						}
					}
					else
					{
						if (cells[col] is ICsvCell)
						{
							((ICsvCell)(cells[col])).Value = value;
						}
						else
						{
							cells[col] = value;
						}
					}
				}
			}
		}

		/// <summary>
		/// 指定したキーワードに関連付けられたセルに値をセットする。
		/// </summary>
		/// <param name="key">セル内キーワード</param>
		/// <returns>なし</returns>
		public object this[string key] {
			set
			{
				if (this.keyCells.ContainsKey(key) == false)
					return;
				var cells = this.keyCells[key];
				foreach (var cell in cells)
				{
					this[cell.Item1, cell.Item2] = value;
				}
			}
		}

		/// <summary>
		/// 指定範囲内に値をセットする
		/// </summary>
		/// <param name="x1">開始X</param>
		/// <param name="y1">開始Y</param>
		/// <param name="x2">終了X</param>
		/// <param name="y2">終了Y</param>
		/// <returns>なし</returns>
		public object this[int x1, int y1, int x2, int y2] {
			set
			{
				for (int x = x1; x <= x1; x++)
				{
					for (int y = y1; y <= y1; y++)
					{
						this[x, y] = value;
					}
				}
			}
		}

		#endregion properties

		#region construct

		/// <summary>
		/// 構築
		/// </summary>
		public Csv()
		{
		}

		#endregion construct

		#region API

		/// <summary>
		/// 指定した行を削除する
		/// </summary>
		/// <param name="row">削除する行</param>
		public void DeleteRow(int row)
		{
			if (this.cells.ContainsKey(row))
			{
				this.cells.Remove(row);
			}
			this.rows = this.cells.Count - 1;
		}

		/// <summary>
		/// 指定した行から指定行数削除する
		/// </summary>
		/// <param name="row">削除開始行</param>
		/// <param name="count">削除する行数</param>
		public void DeleteRow(int row, int count)
		{
			for (int at = row + count - 1; at >= row; at--)
			{
				DeleteRow(at);
			}
		}

		/// <summary>
		/// 指定した列を削除する
		/// </summary>
		/// <param name="col">削除する列</param>
		public void DeleteCol(int col)
		{
			int colMax = 0;
			foreach (var row in cells)
			{
				var cols = row.Value;
				if (cols.ContainsKey(col))
				{
					cols.Remove(col);
				}
				if (colMax < cols.Count)
					colMax = cols.Count;
			}
			this.cols = colMax;
		}

		/// <summary>
		/// 指定した列を指定列数削除する
		/// </summary>
		/// <param name="col">削除開始列</param>
		/// <param name="count">削除する列数</param>
		public void DeleteCol(int col, int count)
		{
			for (int at = col + count - 1; at >= col; at--)
			{
				DeleteCol(at);
			}
		}

		/// <summary>
		/// 指定行のデータ一式を取得
		/// </summary>
		/// <param name="row">行番号</param>
		/// <returns>列の集合</returns>
		private SortedList<int, object> Rows(int row)
		{
			if (cells.ContainsKey(row) == false)
			{
				cells.Add(row, new SortedList<int, object>());
			}
			return cells[row];
		}

		/// <summary>
		/// 指定セルの内容を取得
		/// </summary>
		/// <param name="row">行番号</param>
		/// <param name="col">列番号</param>
		/// <returns>セルオブジェクト</returns>
		private object Cells(int row, int col)
		{
			var cell = Rows(row);
			if (cell.ContainsKey(col) == false)
			{
				cell.Add(col, null);
			}
			return cell[col];
		}

		/// <summary>
		/// CSVファイルの読込
		/// </summary>
		/// <param name="csvFile"></param>
		public void Load(string csvFile)
		{
			this.cells.Clear();
			this.keyCells.Clear();
			if (string.IsNullOrEmpty(csvFile))
				return;
			if (!File.Exists(csvFile))
				return;

			this.filePath = csvFile;

			using (FileStream reader = new FileStream(csvFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			using (TextFieldParser parser = new TextFieldParser(reader, System.Text.Encoding.GetEncoding(932)))
			{
				parser.TextFieldType = FieldType.Delimited;
				parser.SetDelimiters(",");
				int row = 0;
				while (!parser.EndOfData)
				{
					try
					{
						string[] rowDatas = parser.ReadFields();
						int col = 0;
						foreach (var rowData in rowDatas)
						{
							if (rowData.Left(2) == "**")
							{
								//キーワードセル
								if (this.keyCells.ContainsKey(rowData) == false)
								{
									List<Tuple<int, int>> cells = new List<Tuple<int, int>>();
									cells.Add(new Tuple<int, int>(row, col));
									this.keyCells.Add(rowData, cells);
								}
								else
								{
									this.keyCells[rowData].Add(new Tuple<int, int>(row, col));
								}
								this[row, col] = "";
							}
							else
							{
								this[row, col] = rowData;
							}
							col++;
						}
						row++;
					}
					catch (Exception)
					{
					}
				}
			}
		}

		/// <summary>
		/// 保存。
		/// </summary>
		/// <param name="csvFile">保存ファイルパス。空だと編集中ファイルへ上書き保存。</param>
		/// <param name="append">追加保存するかどうか</param>
		public void Save(string csvFile = "", bool append = false)
		{
			string saveFile = this.filePath;
			if (string.IsNullOrEmpty(csvFile))
			{
				saveFile = this.filePath;
			}
			else
			{
				saveFile = csvFile;
			}
			if (string.IsNullOrEmpty(saveFile))
				return;

			StringBuilder sb = new StringBuilder();
			StringWriter text = new StringWriter(sb);

			for (int rowIndex = 0; rowIndex < this.RowCount; rowIndex++)
			{
				if (cells.ContainsKey(rowIndex))
				{
					var cols = cells[rowIndex];

					for (int col = 0; col < this.ColCount; col++)
					{
						if (col > 0)
						{
							text.Write(",");
						}

						if (this[rowIndex, col] is ICsvCell)
						{
							text.Write((this[rowIndex, col] as ICsvCell).ToCsvText());
						}
						else if (cols.ContainsKey(col))
						{
							//text.Write(@"=""{0}""", cols[col]);
							text.Write(@"=""{0}""", cols[col]);
						}
						else
						{
							text.Write("");
						}
					}
				}
				else
				{
					for (int col = 0; col < this.ColCount; col++)
					{
						if (col > 0)
						{
							text.Write(",");
						}
					}
				}
				text.WriteLine();
			}
			using (StreamWriter sw = new StreamWriter(csvFile, append, Encoding.GetEncoding(932)))
			{
				sw.Write(text);
			}
		}

		/// <summary>
		/// 指定セルの内容を管理するクラスを追加
		/// </summary>
		/// <param name="row">行番号</param>
		/// <param name="col">列番号</param>
		/// <param name="cell">セルの内容を管理するクラス</param>
		public void Attach(int row, int col, ICsvCell cell)
		{
			if (this[row, col] is ICsvCell)
			{
				//すでに別の何かにアタッチ済み
			}
			else
			{
				object value = this[row, col];
				cell.Value = value;
				this[row, col] = cell;
			}
		}

		#endregion API
	}
}