using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Milky.Windows.Forms.Controls
{
	public class MilkyButton : Button, IControlBinder, INotifyPropertyChanged
	{
		#region construct

		public MilkyButton() : base()
		{
		}

		#endregion construct

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

		#endregion properties

		#region events

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion events

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

		public void Bind2<T1, T2>(ref T1 from, ref T2 to)
		{
			var fname = nameof(from);
			var tname = nameof(to);
		}

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