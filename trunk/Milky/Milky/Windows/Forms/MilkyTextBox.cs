using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Milky.Windows.Forms
{
	public class MilkyTextBox : TextBox, INotifyPropertyChanged
	{
		#region dlls

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, string lParam);

		#endregion dlls

		#region fields

		private const int ECM_FIRST = 0x1500;

		private const int EM_SETCUEBANNER = ECM_FIRST + 1;

		private const int Win32False = 0;

		private const int Win32True = 1;

		private string watermarkText;

		private bool hideWatermark = true;

		#endregion fields

		#region properties

		[Category("表示")]
		[DefaultValue("")]
		[Description("テキストが空の場合に表示する文字列を設定または取得する")]
		public string WatermarkText {
			get { return watermarkText; }
			set
			{
				watermarkText = value;
				this.SetWatermark();
			}
		}

		[Category("動作")]
		[DefaultValue(true)]
		[Description("エディット コントロールがフォーカスを失ったときに透かし文字を非表示にするかどうか")]
		public bool HideWatermark {
			get { return hideWatermark; }
			set
			{
				hideWatermark = value;
				this.SetWatermark();
			}
		}

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

		#region construct

		public MilkyTextBox() : base()
		{
		}

		#endregion construct

		#region privagte API

		private void SetWatermark()
		{
			SendMessage(this.Handle, EM_SETCUEBANNER, this.HideWatermark ? Win32False : Win32True, this.WatermarkText);
		}

		#endregion privagte API

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