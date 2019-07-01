using System.Collections.Generic;
using System.IO;

namespace Milky.Extensions
{
	public static class StringWriterExtension
	{
		/// <summary>
		/// 改行する
		/// </summary>
		/// <param name="writer">インスタンス</param>
		/// <returns>StringWriter自身</returns>
		public static StringWriter LineBreak(this StringWriter writer)
		{
			writer.WriteLine("");
			return writer;
		}

		/// <summary>
		/// 改行する
		/// </summary>
		/// <param name="writer">StringWriterインスタンス</param>
		/// <returns>StringWriter自身</returns>
		public static StringWriter CR(this StringWriter writer)
		{
			writer.WriteLine("");
			return writer;
		}

		/// <summary>
		/// 文字列を追加し、自身のインスタンスを返すことでメソッド連結できるようにした。
		/// </summary>
		/// <param name="writer">StringWriterインスタンス</param>
		/// <param name="text">書き込む文字列</param>
		/// <returns>StringWriter自身</returns>
		public static StringWriter Append(this StringWriter writer, string text)
		{
			writer.Write(text);
			return writer;
		}

		/// <summary>
		/// 文字列を追加し、自身のインスタンスを返すことでメソッド連結できるようにした。
		/// </summary>
		/// <param name="writer">StringWriterインスタンス</param>
		/// <param name="text">書き込む文字列</param>
		/// <returns>StringWriter自身</returns>
		public static StringWriter AppendLine(this StringWriter writer, string text)
		{
			writer.WriteLine(text);
			return writer;
		}

		/// <summary>
		/// 指定リストの内容をセパレータつけながら文字列化して書き込む
		/// </summary>
		/// <typeparam name="T">リストの型</typeparam>
		/// <param name="writer">StringBuilderインスタンス</param>
		/// <param name="list">リスト</param>
		/// <param name="separator">セパレータ</param>
		/// <returns>StringWriter自身</returns>
		public static StringWriter Append<T>(this StringWriter writer, IEnumerable<T> list, string separator = "")
		{
			list.ForEach(elem => writer.Write(elem.ToString() + separator));
			return writer;
		}

		/// <summary>
		/// 指定リストの内容をセパレータつけながら文字列化して書き込む。
		/// 要素一つずつ改行される。
		/// </summary>
		/// <typeparam name="T">リストの型</typeparam>
		/// <param name="writer">StringBuilderインスタンス</param>
		/// <param name="list">リスト</param>
		/// <param name="separator">セパレータ</param>
		/// <returns>StringWriter自身</returns>
		public static StringWriter AppendLine<T>(this StringWriter writer, IEnumerable<T> list, string separator = "")
		{
			list.ForEach(elem => writer.WriteLine(elem.ToString() + separator));
			return writer;
		}
	}
}