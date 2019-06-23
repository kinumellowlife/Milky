using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Milky.IO
{
	/// <summary>
	/// JSON形式でファイルに書き込む
	/// </summary>
	public class JsonWriter
	{
		/// <summary>
		/// 指定したファイルに書き込む
		/// </summary>
		/// <typeparam name="T">書き込む元となるオブジェクトの型</typeparam>
		/// <param name="json">書き込むオブジェクト</param>
		/// <param name="outFile">書込先のファイルパス</param>
		/// <param name="beautify">見やすいように整形するかどうか</param>
		static public bool Write<T>(T json, string outFile, bool beautify = true) where T : class
		{
			DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));

			try
			{
				var ms = new MemoryStream();
				ser.WriteObject(ms, json);
				if (beautify)
				{
					var formatter = new JsonFormatter();
					formatter.FormatFile(outFile, Encoding.UTF8.GetString(ms.ToArray()));

					formatter.Dispose();
				}
				ms.Dispose();
				return true;
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return false;
			}
		}

		/// <summary>
		/// JSONテキストに変換する
		/// </summary>
		/// <typeparam name="T">オブジェクトの型</typeparam>
		/// <param name="json">変換したいオブジェクト</param>
		/// <returns>JSONテキスト</returns>
		static public string Write<T>(T json) where T : class
		{
			DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));

			string jsonText = "";
			using (MemoryStream ms = new MemoryStream())
			{
				ser.WriteObject(ms, json);

				jsonText = Encoding.UTF8.GetString(ms.ToArray());
			}

			return jsonText;
		}
	}
}