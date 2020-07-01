using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Milky.Windows.Forms.Controls
{
	public class MilkyCheckBox : CheckBox, INotifyPropertyChanged
	{
		#region fields

		public bool DefaultChecked { get; set; } = false;

		#endregion fields

		#region properties

		public new string Text {
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;

				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Text"));
			}
		}

		public new bool Checked {
			get
			{
				return base.Checked;
			}
			set
			{
				base.Checked = value;

				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Checked"));
			}
		}

		#endregion properties

		#region events

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion events

		#region construct

		public MilkyCheckBox() : base()
		{
		}

		#endregion construct

		#region bind

		private readonly ControlBinder binder = new ControlBinder();

		/// <summary>
		/// bind to property
		/// </summary>
		/// <param name="propertyName">bind property name</param>
		/// <param name="attachObject">bind object(from)</param>
		/// <param name="attachPropertyName">bind property name(from)</param>
		public void Bind(string propertyName, object attachObject, string attachPropertyName, Func<object, object> callback = null)
			=> this.binder.Bind(this, propertyName, attachObject, attachPropertyName, callback);

		/// <summary>
		/// un-bind property
		/// </summary>
		/// <param name="propertyName">bind property name</param>
		/// <param name="attachObject">bind object(from)</param>
		/// <param name="attachPropertyName">bind property name(from)</param>
		public void UnBind(string propertyName, object attachObject, string attachPropertyName, Func<object, object> callback = null)
			=> this.binder.UnBind(propertyName, attachObject, attachPropertyName, callback);

		#endregion bind

		public void Reset()
		{
			this.Checked = this.DefaultChecked;
		}
	}
}