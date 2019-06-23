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

namespace MilkyTestApp
{
	public partial class FMain : Form
	{
		public FMain()
		{
			InitializeComponent();
		}

		private void CheckBoxTestButton_Click(object sender, EventArgs e)
		{
			using (var f = new UI.FCheckBoxTest())
			{
				this.Hide();
				f.ShowDialog(this);
				this.Show();
			}
		}

		private void BindTest_Click(object sender, EventArgs e)
		{
			using (var f = new UI.FBindTest())
			{
				this.Hide();
				f.ShowDialog(this);
				this.Show();
			}
		}
	}
}
