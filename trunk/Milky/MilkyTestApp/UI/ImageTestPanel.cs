using System;
using System.Drawing;
using System.Windows.Forms;

namespace MilkyTestApp.UI
{
	public partial class ImageTestPanel : UserControl
	{
		public delegate Image GetImageDelegate(int value);

		public GetImageDelegate OnGetImage;

		public int Minimum {
			get
			{
				return ValueTracBar.Minimum;
			}
			set
			{
				ValueTracBar.Minimum = value;
			}
		}

		public int Maximum {
			get
			{
				return ValueTracBar.Maximum;
			}
			set
			{
				ValueTracBar.Maximum = value;
			}
		}

		public int Value {
			get
			{
				return ValueTracBar.Value;
			}
			set
			{
				ValueTracBar.Value = value;
			}
		}

		public bool UseTracBar {
			get
			{
				return this.ValueTracBar.Visible;
			}
			set
			{
				this.ValueTracBar.Visible = value;
				this.ValueLabel.Visible = value;
			}
		}

		public ImageTestPanel()
		{
			InitializeComponent();

			if (OnGetImage != null)
			{
				Pict.Image = OnGetImage(this.ValueTracBar.Value);
			}

			this.ValueTracBar.ValueChanged += ValueTracBar_ValueChanged;

			this.Load += (_s, _e) =>
			{
				if (OnGetImage != null)
				{
					Pict.Image = OnGetImage(this.ValueTracBar.Value);
				}
			};
		}	

		private void ValueTracBar_ValueChanged(object sender, EventArgs e)
		{
			if (Pict.Image != null)
			{
				Pict.Image.Dispose();
			}
			if (OnGetImage != null)
			{
				Pict.Image = OnGetImage(this.ValueTracBar.Value);
			}
		}
	}
}