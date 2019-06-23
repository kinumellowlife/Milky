using System.Windows.Forms;
using Milky.Windows.Forms;

namespace MilkyTestApp.UI
{
	public partial class FCheckBoxTest : Form
	{
		private MilkyCheckBoxGroup group1 = new MilkyCheckBoxGroup();
		private MilkyCheckBoxGroup group2 = new MilkyCheckBoxGroup();
		private MilkyCheckBoxGroup group3 = new MilkyCheckBoxGroup();
		private MilkyCheckBoxGroup group4 = new MilkyCheckBoxGroup();
		private MilkyCheckBoxGroup group5 = new MilkyCheckBoxGroup();
		private MilkyCheckBoxGroup group6 = new MilkyCheckBoxGroup();

		public FCheckBoxTest()
		{
			InitializeComponent();

			//create groups
			group1.Add(milkyCheckBox1, milkyCheckBox2, milkyCheckBox3, milkyCheckBox4, milkyCheckBox5);
			group2.Add(milkyCheckBox6, milkyCheckBox7, milkyCheckBox8, milkyCheckBox9, milkyCheckBox10);
			group3.Add(milkyCheckBox11, milkyCheckBox12, milkyCheckBox13, milkyCheckBox14, milkyCheckBox15);
			group4.Add(milkyCheckBox16, milkyCheckBox17, milkyCheckBox18, milkyCheckBox19, milkyCheckBox20);
			group5.Add(milkyCheckBox21, milkyCheckBox22, milkyCheckBox23, milkyCheckBox24, milkyCheckBox25);
			group6.Add(milkyCheckBox26, milkyCheckBox27, milkyCheckBox28, milkyCheckBox29, milkyCheckBox30);

			button1.Click += (_s, _e) => group1.Do(MilkyCheckBoxGroup.Operation.And, group1[0]);
			button2.Click += (_s, _e) => group2.Do(MilkyCheckBoxGroup.Operation.Or, group2[0]);
			button3.Click += (_s, _e) => group3.Do(MilkyCheckBoxGroup.Operation.ToggleAnd, group3[0]);
			button4.Click += (_s, _e) => group4.Do(MilkyCheckBoxGroup.Operation.OnAll);
			button5.Click += (_s, _e) => group5.Do(MilkyCheckBoxGroup.Operation.OffAll);
			button6.Click += (_s, _e) => group6.Do(MilkyCheckBoxGroup.Operation.ToggleAll);
		}
	}
}