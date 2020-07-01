using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Milky.IO;

namespace MilkyTestApp.UI
{
	public partial class FBindTest : Form
	{
		[DataContract]
		private class BindableObject : INotifyPropertyChanged
		{
			private string _text = "";
			[DataMember]
			public string Text {
				get
				{
					return this._text;
				}
				set
				{
					SetProperty(ref this._text, value);
				}
			}

			private bool _check = false;
			[DataMember]
			public bool Checked {
				get
				{
					return this._check;
				}
				set
				{
					SetProperty(ref this._check, value);
				}
			}

			public event PropertyChangedEventHandler PropertyChanged;

			private void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
			{
				field = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private BindableObject bo = new BindableObject();
		private Thread bindTestThread = null;
		private int count = 0;

		public FBindTest()
		{
			InitializeComponent();

			//I want to do this : milkyButton1.Text = this.bo.Text;
			milkyButton1.Bind(nameof(milkyButton1.Text), this.bo, nameof(this.bo.Text));

			//I want to do this : milkyCheckBox1.Text = this.bo.Text;
			milkyCheckBox1.Bind(nameof(milkyButton1.Text), this.bo, nameof(this.bo.Text));

			//I want to do this : milkyCheckBox1.Checked = this.bo.Checked;
			milkyCheckBox1.Bind(nameof(milkyCheckBox1.Checked), this.bo, nameof(this.bo.Checked));

			//I want to do this : milkyCheckBox2.Text = this.bo.Text;
			milkyCheckBox2.Bind(nameof(this.milkyCheckBox2.Text), this.bo, nameof(this.bo.Text));

			//I want to think Checked property is true or false by this.count value.
			//Property update torigger is when this.bo.Text is changed.
			milkyCheckBox2.Bind( nameof(milkyCheckBox2.Checked), this.bo, nameof(this.bo.Text), (o) =>
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

		public void ReflectionCopy<T>(T dest, T src)
		{
			foreach (var property in dest.GetType().GetProperties())
			{
				property.SetValue(dest, property.GetValue(src));
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var tmp = JsonReader.ReadJson<BindableObject>("{\"Text\":\"aaa\",\"Checked\":true}");
			ReflectionCopy(this.bo, tmp);
		}
	}
}
