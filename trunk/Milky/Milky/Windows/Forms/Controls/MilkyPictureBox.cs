using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Milky.Windows.Forms.Controls
{
	public class MilkyPictureBox : PictureBox, IControlBinder, INotifyPropertyChanged
	{
		#region construct

		public MilkyPictureBox() : base()
		{
		}

		#endregion construct

		#region properties

		public new Image Image {
			get
			{
				return base.Image;
			}
			set
			{
				base.Image = value;

				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Image"));
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