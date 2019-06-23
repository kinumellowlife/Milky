using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Milky.Extensions;

namespace Milky.IO
{
	/// <summary>
	/// Json形式で記述されたファイル内容を見やすい形に再フォーマットする
	/// </summary>
	public class JsonFormatter : IDisposable
	{
		#region fields

		private char _indentChar = '\t';
		private int _indentSize = 4;
		private char _sparateChar = ' ';

		#endregion fields

		#region property

		/// <summary>
		/// インデントに使う文字の取得と設定
		/// </summary>
		public char IndentChar { get { return this._indentChar; } set { this._indentChar = value; } }

		/// <summary>
		/// インデントサイズの取得と設定
		/// </summary>
		public int IndentSize { get { return this._indentSize; } set { this._indentSize = value; } }

		/// <summary>
		/// キーと要素の間に使うセパレート文字の取得と設定
		/// </summary>
		public char SeparateChar { get { return this._sparateChar; } set { this._sparateChar = value; } }

		/// <summary>
		/// インデントテキストの取得
		/// </summary>
		private string IndentText {
			get
			{
				if (_indentChar == '\t')
					return "" + _indentChar;

				string s = "";
				for (int index = 0; index < _indentSize; index++)
				{
					s += _indentChar;
				}
				return s;
			}
		}

		#endregion property

		#region construct

		/// <summary>
		/// 構築
		/// </summary>
		public JsonFormatter()
		{
		}

		/// <summary>
		/// 構築
		/// </summary>
		/// <param name="indentChar">インデント文字</param>
		/// <param name="indentSize">インデントサイズ</param>
		public JsonFormatter(char indentChar, int indentSize)
		{
			this.IndentChar = indentChar;
			this.IndentSize = indentSize;
		}

		/// <summary>
		/// 構築
		/// </summary>
		/// <param name="indentChar">インデント文字</param>
		/// <param name="indentSize">インデントサイズ</param>
		/// <param name="separateChar">セパレート文字</param>
		public JsonFormatter(char indentChar, int indentSize, char separateChar) : this(indentChar, indentSize)
		{
			this.SeparateChar = separateChar;
		}

		/// <summary>
		/// 破棄
		/// </summary>
		public void Dispose()
		{
		}

		#endregion construct

		#region API

		/// <summary>
		/// 指定したファイルを再フォーマットする
		/// </summary>
		/// <param name="path"></param>
		public void FormatFile(string path)
		{
			string json = File.ReadAllText(path);
			json = Format(json);
			System.IO.File.Delete(path);
			File.WriteAllText(path, json, Encoding.Default);
		}

		/// <summary>
		/// 指定したファイルに、指定したJSONテキストを再フォーマットして書き込む
		/// </summary>
		/// <param name="path">書き込むファイル</param>
		/// <param name="json">JSONテキスト</param>
		public void FormatFile(string path, string json)
		{
			json = Format(json);
			if (Directory.Exists(Path.GetDirectoryName(path)) == false)
			{
				Directory.CreateDirectory(Path.GetDirectoryName(path));
			}
			File.WriteAllText(path, json, Encoding.UTF8);
		}

		/// <summary>
		/// 指定したJSONテキストを再フォーマットする
		/// </summary>
		/// <param name="text">JSONテキスト</param>
		/// <returns>再フォーマット後のJSONテキスト</returns>
		public string Format(string text)
		{
			StringBuilder s = new StringBuilder();
			StringWriter json = new StringWriter(s);

			string value = "";
			int indent = 0;
			for (int index = 0; index < text.Length; index++)
			{
				char c = text[index];
				switch (c)
				{
					case '[':
						json.WriteLine(c);
						break;

					case '{':
						json.WriteLine(c);
						break;

					case ']':
						json.WriteLine();
						json.Write(c);
						break;

					case '}':
						json.WriteLine();
						json.Write(c);
						break;

					case ',':
						json.WriteLine(",");
						break;

					case ':':
						json.Write("{0}:{0}", this._sparateChar);
						break;

					case '\"':
						value = "";
						index++;
						for (; index < text.Length; index++)
						{
							c = text[index];
							if (c == '\\')
							{
								value += c;
								index++;
								if (index < text.Length)
								{
									c = text[index];
									value += c;
								}
							}
							else if (c == '\"')
							{
								break;
							}
							else
							{
								value += c;
							}
						}

						json.Write("\"{0}\"", value);
						break;

					default:
						json.Write(c);
						break;
				}
			}

			List<string> lines = new List<string>();
			lines.AddRange(json.ToString().Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
			indent = 0;
			for (int index = 0; index < lines.Count; index++)
			{
				string line = lines[index];

				if (line.Right(1) == "{")
				{
					lines[index] = GetIndent(indent) + line;
					indent++;
				}
				else if (line.Right(1) == "[")
				{
					lines[index] = GetIndent(indent) + line;
					indent++;
				}
				else if (line.Right(1) == "}")
				{
					indent--;
					lines[index] = GetIndent(indent) + line;
				}
				else if (line.Right(2) == "},")
				{
					indent--;
					lines[index] = GetIndent(indent) + line;
				}
				else if (line.Right(1) == "]")
				{
					indent--;
					lines[index] = GetIndent(indent) + line;
				}
				else if (line.Right(2) == "],")
				{
					indent--;
					lines[index] = GetIndent(indent) + line;
				}
				else if (line.Right(1) == ",")
				{
					lines[index] = GetIndent(indent) + line;
				}
				else
				{
					//最後の要素
					lines[index] = GetIndent(indent) + line;
				}
			}

			StringBuilder t = new StringBuilder();
			StringWriter jsonText = new StringWriter(t);

			string ret = "";
			foreach (var line in lines)
			{
				jsonText.WriteLine(line);
				//ret += line;
				//ret += "\n";
			}
			ret = jsonText.ToString();
			return ret;
		}

		/// <summary>
		/// インデントテキストを取得する
		/// </summary>
		/// <param name="count">インデント数</param>
		/// <returns>インデントテキスト</returns>
		private string GetIndent(int count)
		{
			string s = "";
			try
			{
				while (count > 0)
				{
					s += this.IndentText;
					count--;
				}
			}
			catch (Exception)
			{
			}
			return s;
		}

		#endregion API
	}
}