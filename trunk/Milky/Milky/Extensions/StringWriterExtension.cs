using System.Collections.Generic;
using System.IO;

namespace Milky.Extensions
{
	public static class StringWriterExtension
	{
		public static StringWriter LineBreak(this StringWriter writer)
		{
			writer.WriteLine("");
			return writer;
		}

		public static StringWriter CR(this StringWriter writer)
		{
			writer.WriteLine("");
			return writer;
		}

		public static StringWriter Append(this StringWriter writer, string text)
		{
			writer.Write(text);
			return writer;
		}

		public static StringWriter AppendLine(this StringWriter writer, string text)
		{
			writer.WriteLine(text);
			return writer;
		}

		public static StringWriter Append<T>(this StringWriter writer, IEnumerable<T> list, string delim = "")
		{
			list.ForEach(elem => writer.Write(elem.ToString() + delim));
			return writer;
		}

		public static StringWriter AppendLine<T>(this StringWriter writer, IEnumerable<T> list, string delim = "")
		{
			list.ForEach(elem => writer.WriteLine(elem.ToString() + delim));
			return writer;
		}
	}
}