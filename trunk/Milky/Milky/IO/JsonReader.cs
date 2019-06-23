using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Milky.Extensions;

namespace Milky.IO
{
	/// <summary>
	/// JSON形式で記述された内容をオブジェクトとして読み込む
	/// </summary>
	public class JsonReader
	{
		/// <summary>
		/// 指定したJSON文字列をオブジェクトに変換する
		/// </summary>
		/// <typeparam name="T">変換後のクラスの型</typeparam>
		/// <param name="jsonText">JSONテキスト</param>
		/// <returns>オブジェクト</returns>
		static public T ReadJson<T>(string jsonText) where T : class
		{
			byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonText);
			using (MemoryStream ms = new MemoryStream(jsonBytes))
			{
				var jsonReader = new DataContractJsonSerializer(typeof(T));
				return jsonReader.ReadObject(ms) as T;
			}
		}

		/// <summary>
		/// 指定したファイルを読み込む
		/// </summary>
		/// <typeparam name="T">変換後のクラスの型</typeparam>
		/// <param name="path">読み込むファイル</param>
		/// <returns>オブジェクト</returns>
		static public T Read<T>(string path) where T : class
		{
			if (string.IsNullOrEmpty(path))
				return null;
			if (File.Exists(path) == false)
				return null;

			var enc = File.ReadAllBytes(path).GetEncoding();

			if (enc == null)
				return null;

			using (StreamReader sr = new StreamReader(path, enc))
			{
				try
				{
					string jsonText = sr.ReadToEnd();
					byte[] jsonBytes = enc.GetBytes(jsonText);
					using (MemoryStream ms = new MemoryStream(jsonBytes))
					{
						try
						{
							var jsonReader = new DataContractJsonSerializer(typeof(T));
							return jsonReader.ReadObject(ms) as T;
						}
						catch (Exception)
						{
							return null;
						}
					}
				}
				catch (Exception)
				{
					return null;
				}
			}
		}
	}
}