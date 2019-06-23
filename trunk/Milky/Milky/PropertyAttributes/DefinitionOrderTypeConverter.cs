using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Milky.PropertyAttributes
{
	/// <summary>
	/// プロパティグリッドのプロパティの並び順をソート
	/// </summary>
	public class DefinitionOrderTypeConverter : TypeConverter
	{
		/// <summary>
		/// プロパティ編集
		/// </summary>
		/// <param name="context">コンテキスト</param>
		/// <param name="value">変更値</param>
		/// <param name="attributes">属性</param>
		/// <returns>変更後の値</returns>
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			// TypeDescriptorを使用してプロパティ一覧を取得する
			PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(value, attributes);

			// プロパティ一覧をリフレクションから取得
			Type type = value.GetType();
			List<string> list = new List<string>();
			foreach (PropertyInfo propertyInfo in type.GetProperties())
			{
				list.Add(propertyInfo.Name);
			}

			// リフレクションから取得した順でソート
			return pdc.Sort(list.ToArray());
		}

		/// <summary>
		/// GetPropertiesをサポートしていることを表明する。
		/// </summary>
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}