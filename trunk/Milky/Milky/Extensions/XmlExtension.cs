using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

namespace Milky.Extensions
{
	public static class XmlExtension
	{
		/// <summary>
		///
		/// </summary>
		/// <param name="xmlDoc"></param>
		/// <param name="xpath"></param>
		/// <returns></returns>
		public static XmlNode Node(this XmlDocument xmlDoc, string xpath)
		{
			if (xmlDoc == null)
			{
				return null;
			}
			XmlNodeList childNodes = xmlDoc.SelectNodes(xpath);
			if (childNodes == null)
			{
				return null;
			}
			else if (childNodes.Count == 0)
			{
				return null;
			}
			return childNodes[0];
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="xmlDoc"></param>
		/// <param name="xpath"></param>
		/// <param name="beginIndex"></param>
		/// <returns></returns>
		public static IEnumerable<XmlNode> Nodes(this XmlDocument xmlDoc, string xpath, int beginIndex = 0)
		{
			if (xmlDoc == null)
			{
				yield return null;
			}
			XmlNodeList childNodes = xmlDoc.SelectNodes(xpath);
			if (childNodes == null)
			{
				yield return null;
			}
			else
			{
				for (beginIndex = 0; beginIndex < childNodes.Count; beginIndex++)
				{
					yield return childNodes[beginIndex];
				}
			}
		}

		/// <summary>
		/// ユーザ型への変換処理用デリゲート
		/// </summary>
		/// <typeparam name="T">変換先の型</typeparam>
		/// <param name="value">変換元の文字列</param>
		/// <returns>変換後の値</returns>
		public delegate T XmlAttributeTypeConverter<T>(string value);

		/// <summary>
		/// 属性値の取得
		/// </summary>
		/// <typeparam name="T">変換する型</typeparam>
		/// <param name="node">XMLノード</param>
		/// <param name="key">属性キー</param>
		/// <param name="converter">変換処理</param>
		/// <returns>属性値</returns>
		public static T Attr<T>(this XmlNode node, string key, XmlAttributeTypeConverter<T> converter)
		{
			if (node == null)
				return default(T);
			XmlAttribute attr = node.Attributes[key];
			if (attr == null)
				return default(T);
			else
				return converter(attr.Value);
		}

		/// <summary>
		/// 属性値の取得
		/// </summary>
		/// <param name="node"></param>
		/// <param name="key"></param>
		/// <param name="def"></param>
		/// <returns></returns>
		public static string Attr(this XmlNode node, string key, string def)
		{
			if (node == null)
				return def;
			XmlAttribute attr = node.Attributes[key];
			if (attr == null)
				return def;
			else
				return attr.Value;
		}

		/// <summary>
		/// 属性値の文字列からColorインスタンスの生成
		/// </summary>
		/// <param name="node"></param>
		/// <param name="key"></param>
		/// <param name="def"></param>
		/// <returns></returns>
		public static Color AttrToColor(this XmlNode node, string key, Color def)
		{
			if (node == null)
				return def;
			XmlAttribute attr = node.Attributes[key];
			if (attr == null)
			{
				return def;
			}
			else
			{
				try
				{
					Color check = ColorTranslator.FromHtml(attr.Value);
					if (check.Name.ToLower().IndexOf("ff") == 0)
					{
						//色名として認識できない
						if (attr.Value.IndexOf(',') == -1)
						{
							int value = Convert.ToInt32(attr.Value.Replace("0x", ""), 16);
							int r = ((value >> 16) & 0xFF);
							int g = ((value >> 8) & 0xFF);
							int b = ((value >> 0) & 0xFF);
							return Color.FromArgb(r, g, b);
						}
						else
						{
							string[] codes = attr.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
							string rCode = (codes.Length > 0) ? codes[0] : "0";
							string gCode = (codes.Length > 1) ? codes[1] : "0";
							string bCode = (codes.Length > 2) ? codes[2] : "0";

							int r = Convert.ToInt32(rCode, (rCode.IndexOf("0x") != -1) ? 16 : 10);
							int g = Convert.ToInt32(gCode, (rCode.IndexOf("0x") != -1) ? 16 : 10);
							int b = Convert.ToInt32(bCode, (rCode.IndexOf("0x") != -1) ? 16 : 10);
							return Color.FromArgb(r, g, b);
						}
					}
					else
					{
						return check;
					}
				}
				catch (Exception)
				{
					return def;
				}
			}
		}

		/// <summary>
		/// 属性値からboolへの変換
		/// </summary>
		/// <param name="node"></param>
		/// <param name="key"></param>
		/// <param name="def"></param>
		/// <returns></returns>
		public static bool AttrToBool(this XmlNode node, string key, bool def)
		{
			if (node == null)
				return def;
			XmlAttribute attr = node.Attributes[key];
			if (attr == null)
			{
				return def;
			}
			else
			{
				bool value;
				bool check = bool.TryParse(attr.Value, out value);
				if (check)
				{
					return value;
				}
				else
				{
					return def;
				}
			}
		}

		/// <summary>
		/// 属性値からDateTimeへの変換
		/// </summary>
		/// <param name="node"></param>
		/// <param name="key"></param>
		/// <param name="def"></param>
		/// <returns></returns>
		public static DateTime AttrToDateTime(this XmlNode node, string key, DateTime def)
		{
			if (node == null)
				return def;
			XmlAttribute attr = node.Attributes[key];
			if (attr == null)
			{
				return def;
			}
			else
			{
				DateTime d;
				if (DateTime.TryParse(attr.Value, out d))
				{
					return d;
				}
				else
				{
					return def;
				}
			}
		}

		/// <summary>
		/// 属性値からfloatへの変換
		/// </summary>
		/// <param name="node"></param>
		/// <param name="key"></param>
		/// <param name="def"></param>
		/// <returns></returns>
		public static float AttrToFloat(this XmlNode node, string key, float def)
		{
			if (node == null)
				return def;
			XmlAttribute attr = node.Attributes[key];
			if (attr == null)
			{
				return def;
			}
			else
			{
				float value;
				if (attr.Value.IndexOf("0x") != -1)
				{
					value = attr.Value.HexToFloat(def);
					return value;
				}
				else
				{
					bool check = float.TryParse(attr.Value, out value);
					if (check)
					{
						return value;
					}
					else
					{
						return def;
					}
				}
			}
		}

		/// <summary>
		/// 属性値からintへの変換
		/// </summary>
		/// <param name="node"></param>
		/// <param name="key"></param>
		/// <param name="def"></param>
		/// <returns></returns>
		public static int AttrToInt(this XmlNode node, string key, int def)
		{
			if (node == null)
				return def;
			XmlAttribute attr = node.Attributes[key];
			if (attr == null)
			{
				return def;
			}
			else
			{
				if (attr.Value.IndexOf("0x") == -1)
				{
					int value;
					bool check = int.TryParse(attr.Value, out value);
					if (check)
					{
						return value;
					}
					else
					{
						return def;
					}
				}
				else
				{
					return attr.Value.HexToInt(def);
				}
			}
		}

		/// <summary>
		/// 属性値からintへの変換
		/// </summary>
		/// <param name="node"></param>
		/// <param name="key"></param>
		/// <param name="def"></param>
		/// <returns></returns>
		public static int AttrHexToInt(this XmlNode node, string key, int def)
		{
			if (node == null)
				return def;
			XmlAttribute attr = node.Attributes[key];
			if (attr == null)
			{
				return def;
			}
			else
			{
				return attr.Value.HexToInt(def);
			}
		}
	}
}