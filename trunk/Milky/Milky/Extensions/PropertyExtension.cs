using System;
using System.Reflection;

namespace Milky.Extensions
{
	public static class PropertyExtension
	{
		/// <summary>
		/// get properties
		/// </summary>
		public static PropertyInfo[] GetProperties(this object o)
		{
			return o.GetType().GetProperties();
		}

		/// <summary>
		/// get properties with BindingFlag
		/// </summary>
		/// <param name="bindAttr">binding flag</param>
		public static PropertyInfo[] GetProperties(this object o, BindingFlags bindAttr)
		{
			return o.GetType().GetProperties(bindAttr);
		}

		/// <summary>
		/// get property by name
		/// </summary>
		/// <param name="propertyName">property name</param>
		/// <returns></returns>
		public static PropertyInfo GetProperty(this object o, string propertyName)
		{
			PropertyInfo pInfo = o.GetType().GetProperty(propertyName);
			return pInfo;
		}

		/// <summary>
		/// get property by name
		/// </summary>
		/// <param name="propertyName">property name</param>
		/// <returns></returns>
		public static PropertyInfo GetProperty(this object o, string propertyName, BindingFlags bindAttr)
		{
			PropertyInfo pInfo = o.GetType().GetProperty(propertyName, bindAttr);
			return pInfo;
		}

		/// <summary>
		/// カスタム属性を取得する
		/// </summary>
		/// <typeparam name="T">取得するカスタム属性の型</typeparam>
		/// <param name="info">PropertyInfo</param>
		/// <returns>カスタム属性</returns>
		public static T GetCustomAttribute<T>(this PropertyInfo info)
		{
			var attrs = info.GetCustomAttributes(typeof(T), false);

			if (attrs != null && attrs.Length > 0)
			{
				return (T)attrs[0];
			}
			else
			{
				return default(T);
			}
		}

		/// <summary>
		/// カスタム属性を取得する
		/// </summary>
		/// <param name="info">PropertyInfo</param>
		/// <param name="attributeType">検索する属性の型。この型に割り当てることができる属性だけが返されます。</param>
		/// <param name="inherit">このメンバーの継承チェーンを検索して属性を見つけるかどうかを指定します。</param>
		/// <returns>カスタム属性</returns>
		public static object GetCustomAttribute(this PropertyInfo info, Type attributeType, bool inherit)
		{
			var attrs = info.GetCustomAttributes(attributeType, inherit);

			if (attrs != null && attrs.Length > 0)
			{
				return attrs[0];
			}
			else
			{
				return null;
			}
		}
	}
}