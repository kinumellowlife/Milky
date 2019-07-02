using System;
using System.ComponentModel;

namespace Milky.Windows.Forms
{
	/// <summary>
	/// プロパティ表示名を外部から設定するための属性。
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class PropertyDisplayNameAttribute : Attribute
	{

		private readonly string myPropertyDisplayName;

		/// <summary>
		///
		/// </summary>
		/// <param name="name"></param>
		public PropertyDisplayNameAttribute(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				myPropertyDisplayName = name;
			}
		}

		/// <summary>
		///
		/// </summary>
		public string PropertyDisplayName {
			get { return myPropertyDisplayName; }
		}
	}

	/// <summary>
	/// プロパティ表示名でPropertyDisplayPropertyDescriptorクラスを使用するために
	/// TypeConverter属性に指定するためのTypeConverter派生クラス。
	/// </summary>
	public class PropertyDisplayConverter : TypeConverter
	{

		/// <summary>
		/// 構築
		/// </summary>
		public PropertyDisplayConverter()
		{
		}

		/// <summary>
		/// プロパティの取得
		/// </summary>
		/// <param name="context">コンテキスト</param>
		/// <param name="instance">オブジェクト</param>
		/// <param name="filters"></param>
		/// <returns>プロパティ定義</returns>
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object instance, Attribute[] filters)
		{
			PropertyDescriptorCollection collection = new PropertyDescriptorCollection(null);
			if ((context == null) || (instance == null) || (filters == null))
			{
				return collection;
			}

			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(instance, filters, true);
			foreach (PropertyDescriptor desc in properties)
			{
				collection.Add(new PropertyDisplayPropertyDescriptor(desc));
			}

			return collection;
		}

		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}

	/// <summary>
	/// プロパティの説明（＝情報）を提供するクラス。DisplayNameをカスタマイズする。
	/// </summary>
	public class PropertyDisplayPropertyDescriptor : PropertyDescriptor
	{

		private readonly PropertyDescriptor oneProperty;

		public PropertyDisplayPropertyDescriptor(PropertyDescriptor desc)
			: base(desc)
		{
			oneProperty = desc;
		}

		public override bool CanResetValue(object component)
		{
			return oneProperty.CanResetValue(component);
		}

		public override Type ComponentType {
			get
			{
				return oneProperty.ComponentType;
			}
		}

		public override object GetValue(object component)
		{
			if (component == null)
			{
				return oneProperty.GetValue(component);
			}
			return null;
		}

		public override string Description {
			get
			{
				return oneProperty.Description;
			}
		}

		public override string Category {
			get
			{
				return oneProperty.Category;
			}
		}

		public override bool IsReadOnly {
			get
			{
				return oneProperty.IsReadOnly;
			}
		}

		public override void ResetValue(object component)
		{
			if (component != null)
			{
				oneProperty.ResetValue(component);
			}
		}

		public override bool ShouldSerializeValue(object component)
		{
			if (component == null)
			{
				return oneProperty.ShouldSerializeValue(component);
			}
			return false;
		}

		public override void SetValue(object component, object value)
		{
			if ((component == null) || (value == null))
			{
				return;
			}
			oneProperty.SetValue(component, value);
		}

		public override Type PropertyType {
			get
			{
				return oneProperty.PropertyType;
			}
		}

		public override string DisplayName {
			get
			{
				PropertyDisplayNameAttribute attrib = (PropertyDisplayNameAttribute)oneProperty.Attributes[ typeof(PropertyDisplayNameAttribute)];
				if (attrib != null)
				{
					return attrib.PropertyDisplayName;
				}

				return oneProperty.DisplayName;
			}
		}
	}
}
