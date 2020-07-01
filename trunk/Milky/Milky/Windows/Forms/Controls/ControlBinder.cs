using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using Milky.Extensions;

namespace Milky.Windows.Forms.Controls
{
	/// <summary>
	/// binder interface
	/// </summary>
	public interface IControlBinder
	{
		/// <summary>
		/// bind
		/// </summary>
		void Bind(string propertyName, object attachObject, string attachPropertyName, Func<object, object> callback = null);

		void UnBind(string propertyName, object attachObject, string attachPropertyName, Func<object, object> callback = null);
	}

	public class ControlBinder
	{
		/// <summary>
		/// Bind information
		/// </summary>
		private class BindTo
		{
			/// <summary>
			/// Bind "to" control
			/// </summary>
			public Control BindControl { get; set; }

			/// <summary>
			/// Bind property name of Bind Control's
			/// </summary>
			public string BindPropertyName { get; set; }

			/// <summary>
			/// Bind property information.This is created from BindPropertyName
			/// </summary>
			public PropertyInfo BindProperty { get; set; }

			/// <summary>
			/// Bind "From" object
			/// </summary>
			public object AttachObject { get; set; }

			/// <summary>
			/// Property name of AttachObject
			/// </summary>
			public string AttachPropertyName { get; set; }

			/// <summary>
			/// Property of AttachObject
			/// </summary>
			public PropertyInfo AttachProperty { get; set; }

			/// <summary>
			/// Value callback.Usually, use AttachPropertyName, but if you can not set property, you use this callback.
			/// </summary>
			public Func<object, object> ValueCallback { get; set; }

			public bool Equals(string propertyName, object attachObject, string attachPropertyName, Func<object, object> callback = null)
			{
				if (!this.BindPropertyName.Equals(propertyName))
					return false;

				if (!this.AttachPropertyName.Equals(attachPropertyName))
					return false;

				if (this.ValueCallback != callback)
					return false;

				return true;
			}
		}

		private readonly List<BindTo> binds = new List<BindTo>();

		public ControlBinder()
		{
		}

		public void Bind(Control control, string propertyName, object attachObject, string attachPropertyName, Func<object, object> callback = null)
		{
			if (this.binds.Has(b => b.Equals(propertyName, attachObject, attachPropertyName, callback)))
			{
				return;
			}

			//アタッチされる自分のプロパティ情報
			PropertyInfo pInfo = control.GetProperty(propertyName);
			if (pInfo == null)
				return;

			//アタッチする外部オブジェクトのプロパティ名
			PropertyInfo attachInfo = null;
			if (!string.IsNullOrEmpty(attachPropertyName))
			{
				attachInfo = attachObject.GetProperty(attachPropertyName);
				if (attachInfo == null)
					return;
			}
			else if (callback == null)
			{
				return;
			}

			//バンド情報
			BindTo bind = new BindTo()
			{
				BindControl = control,
				BindPropertyName = propertyName,
				BindProperty = pInfo,
				AttachObject = attachObject,
				AttachPropertyName = attachPropertyName,
				AttachProperty = attachInfo,
				ValueCallback = callback
			};

			//必要なインタフェイスへキャスト
			if (attachObject is INotifyPropertyChanged notify)
			{
				//バインド情報の保持
				this.binds.Add(bind);

				//プロパティの変更をキャッチ
				notify.PropertyChanged += Notify_PropertyChanged;
			}
		}

		public void UnBind(string propertyName, object attachObject, string attachPropertyName, Func<object, object> callback = null)
		{
			var bind = this.binds.Find(b => b.Equals(propertyName, attachObject, attachPropertyName, callback));

			if (bind == null)
				return;

			if (attachObject is INotifyPropertyChanged notify)
			{
				notify.PropertyChanged -= Notify_PropertyChanged;
			}
		}

		private void Notify_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			var translates = this.binds.FindAll(b =>
			{
				if (b.AttachObject != sender)
					return false;
				if (!b.AttachPropertyName.Equals(e.PropertyName))
					return false;
				return true;
			});

			translates.ForEach(translate =>
			{
				object value = (translate.ValueCallback != null) ?
					translate.ValueCallback(translate.AttachObject)
						:
					translate.AttachProperty.GetValue(translate.AttachObject);

				Action translateAction = () => translate.BindProperty.SetValue(translate.BindControl, value);

				if (translate.BindControl.InvokeRequired)
				{
					translate.BindControl.Invoke((o) =>
					{
						translate.BindProperty.SetValue(translate.BindControl, value);
					});
				}
				else
				{
					translate.BindProperty.SetValue(translate.BindControl, value);
				}
			});
		}
	}
}