using System;
using System.ComponentModel;

namespace Milky.PropertyAttributes
{
	/// <summary>
	/// プロパティ表示名を外部から設定するための属性。
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class PropertyDisplayNameAttribute : Attribute
	{
		/// <summary>
		///
		/// </summary>
		private string myPropertyDisplayName;

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
		///
		/// </summary>
		public PropertyDisplayConverter()
		{
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="context"></param>
		/// <param name="instance"></param>
		/// <param name="filters"></param>
		/// <returns></returns>
		public override PropertyDescriptorCollection
			GetProperties(ITypeDescriptorContext context,
				object instance, Attribute[] filters)
		{
			PropertyDescriptorCollection collection = new PropertyDescriptorCollection(null);
			if ((context == null) || (instance == null) || (filters == null))
			{
				return collection;
			}

			PropertyDescriptorCollection properies =
				TypeDescriptor.GetProperties(instance, filters, true);
			foreach (PropertyDescriptor desc in properies)
			{
				collection.Add(new PropertyDisplayPropertyDescriptor(desc));
			}

			return collection;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
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
		/// <summary></summary>
		private PropertyDescriptor oneProperty;

		/// <summary>
		///
		/// </summary>
		/// <param name="desc"></param>
		public PropertyDisplayPropertyDescriptor(PropertyDescriptor desc)
			: base(desc)
		{
			oneProperty = desc;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="component"></param>
		/// <returns></returns>
		public override bool CanResetValue(object component)
		{
			return oneProperty.CanResetValue(component);
		}

		/// <summary>
		///
		/// </summary>
		public override Type ComponentType {
			get
			{
				return oneProperty.ComponentType;
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="component"></param>
		/// <returns></returns>
		public override object GetValue(object component)
		{
			if (component == null)
			{
				return oneProperty.GetValue(component);
			}
			return null;
		}

		/// <summary>
		///
		/// </summary>
		public override string Description {
			get
			{
				return oneProperty.Description;
			}
		}

		/// <summary>
		///
		/// </summary>
		public override string Category {
			get
			{
				return oneProperty.Category;
			}
		}

		/// <summary>
		///
		/// </summary>
		public override bool IsReadOnly {
			get
			{
				return oneProperty.IsReadOnly;
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="component"></param>
		public override void ResetValue(object component)
		{
			if (component != null)
			{
				oneProperty.ResetValue(component);
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="component"></param>
		/// <returns></returns>
		public override bool ShouldSerializeValue(object component)
		{
			if (component == null)
			{
				return oneProperty.ShouldSerializeValue(component);
			}
			return false;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="component"></param>
		/// <param name="value"></param>
		public override void SetValue(object component, object value)
		{
			if ((component == null) || (value == null))
			{
				return;
			}
			oneProperty.SetValue(component, value);
		}

		/// <summary>
		///
		/// </summary>
		public override Type PropertyType {
			get
			{
				return oneProperty.PropertyType;
			}
		}

		/// <summary>
		///
		/// </summary>
		public override string DisplayName {
			get
			{
				PropertyDisplayNameAttribute attrib =
					(PropertyDisplayNameAttribute)oneProperty.Attributes[
						typeof(PropertyDisplayNameAttribute)];
				if (attrib != null)
				{
					return attrib.PropertyDisplayName;
				}

				return oneProperty.DisplayName;
			}
		}
	}
}