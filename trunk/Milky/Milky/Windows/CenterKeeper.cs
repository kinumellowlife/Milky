using System.Drawing;
using System.Windows.Forms;

namespace Milky.Windows
{
	public class CenterKeeper
	{
		private Control target;
		private Control parent;

		public enum KeepDirection : byte
		{
			Horizonal = 0x01,
			Vertical = 0x02,

			Both = Horizonal | Vertical,
		}

		public KeepDirection Direction { get; set; } = KeepDirection.Both;

		public CenterKeeper(Control target, Control parent)
		{
			this.target = target;
			this.parent = parent;

			parent.Resize += (_s, _e) =>
			{
				Relocate();
			};
			target.Resize += (_s, _e) =>
			{
				Relocate();
			};

			Relocate();
		}

		private void Relocate()
		{
			int x = target.Left;
			int y = target.Top;

			if ((this.Direction & KeepDirection.Vertical) == KeepDirection.Vertical)
			{
				if ((target.Width >= parent.Width))
					x = 0;
				else
					x = (parent.Width - target.Width) / 2;
			}
			if ((this.Direction & KeepDirection.Horizonal) == KeepDirection.Horizonal)
			{
				if ((target.Height >= parent.Height))
					y = 0;
				else
					y = (parent.Height - target.Height) / 2;
			}
			target.Location = new Point(x, y);
		}
	}
}