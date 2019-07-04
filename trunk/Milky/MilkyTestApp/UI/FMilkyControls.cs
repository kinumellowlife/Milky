using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Milky.Windows.Forms.Jornal;

namespace MilkyTestApp.UI
{
	public partial class FMilkyControls : Form
	{
		public FMilkyControls()
		{
			InitializeComponent();
		}

		private void MilkyButton1_Click(object sender, EventArgs e)
		{
			MilkyLog.AddItem(new MilkyLogListItem()
			{
				LogType = MilkyLogItem.MilkyLogTypeFlags.Debug,
				DateTime = DateTime.Now,
				Message = "this is test",
				Tag = null
			});
		}

		private void MilkyButton2_Click(object sender, EventArgs e)
		{
			MilkyLog.AddItem(new MilkyLogListItem()
			{
				LogType = MilkyLogItem.MilkyLogTypeFlags.Message,
				DateTime = DateTime.Now,
				Message = "this is test",
				Tag = null
			});
		}

		private int viewCount = 0;
		private void MilkyButton3_Click(object sender, EventArgs e)
		{
			switch( viewCount)
			{
				case 0:
					MilkyLog.ViewFlag = MilkyLogItem.MilkyLogTypeFlags.Debug;
					viewCount++;
					break;
				case 1:
					MilkyLog.ViewFlag = MilkyLogItem.MilkyLogTypeFlags.Message;
					viewCount = 0;
					break;
			}
		}
	}
}
