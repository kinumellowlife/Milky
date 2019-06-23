using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MilkyTestApp.UI
{
	public partial class FBindTest : Form
	{
		private class BindableObject : INotifyPropertyChanged
		{
			private string _text = "";
			public string Text {
				get
				{
					return this._text;
				}
				set
				{
					this._text = value;

					this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Text"));
				}
			}

			private bool _check = false;
			public bool Checked {
				get
				{
					return this._check;
				}
				set
				{
					this._check = value;
					this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Checked"));
				}
			}

			public event PropertyChangedEventHandler PropertyChanged;
		}

		private BindableObject bo = new BindableObject();
		private Thread bindTestThread = null;
		private int count = 0;

		public FBindTest()
		{
			InitializeComponent();

			//I want to do this : milkyButton1.Text = this.bo.Text;
			milkyButton1.Bind("Text", this.bo, "Text");

			//I want to do this : milkyCheckBox1.Text = this.bo.Text;
			milkyCheckBox1.Bind("Text", this.bo, "Text");

			//I want to do this : milkyCheckBox1.Checked = this.bo.Checked;
			milkyCheckBox1.Bind("Checked", this.bo, "Checked");

			//I want to do this : milkyCheckBox2.Text = this.bo.Text;
			milkyCheckBox2.Bind("Text", this.bo, "Text");

			//I want to think Checked property is true or false by this.count value.
			//Property update torigger is when this.bo.Text is changed.
			milkyCheckBox2.Bind("Checked", this.bo, "Text", (o) =>
			{
				if((this.count % 3) == 0)
				{
					return true;
				}
				return false;
			});
		}

		private void StartThreadButton_Click(object sender, EventArgs e)
		{
			if( this.bindTestThread == null)
			{
				StartThreadButton.Enabled = false;

				this.bindTestThread = new Thread(() =>
				{
					while (true)
					{
						this.bo.Text = count.ToString();
						this.bo.Checked = ((count % 2) == 0);
						count++;
						Thread.Sleep(1000);
					}
				})
				{ IsBackground = true };
				this.bindTestThread.Start();
			}
		}
	}
}
