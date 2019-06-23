using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Milky.Windows.Forms
{
	public class MilkyNumericUpDown : NumericUpDown, INotifyPropertyChanged
	{
		#region fields

		public bool DefaultChecked { get; set; } = false;

		#endregion fields

		#region properties

		public new decimal Value {
			get
			{
				return base.Value;
			}
			set
			{
				base.Value = value;

				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
			}
		}

		#endregion properties

		#region events

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion events

		#region construct

		public MilkyNumericUpDown() : base()
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
	}
}