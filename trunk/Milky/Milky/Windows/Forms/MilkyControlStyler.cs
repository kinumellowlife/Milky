using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Milky.Extensions;

namespace Milky.Windows.Forms
{
	public class MilkyControlStyler
	{
		private Dictionary<Control, List<Control>> controls = new Dictionary<Control, List<Control>>();

		private void PropertyUpdated(Action<Control> action)
		{
			foreach (var parent in this.controls.Keys)
			{
				if (parent.InvokeRequired)
				{
					parent.Invoke(new Action<Control>(action));
				}
				else
				{
					action(parent);
				}
			}
		}

		#region color

		private ColorPair color = null;

		private void SetColor(Control parent)
		{
			this.controls[parent].ForEach(c =>
			{
				c.ForeColor = this.color.Fore;
				c.BackColor = this.color.Back;
			});
		}

		public ColorPair Color {
			get
			{
				return this.color;
			}
			set
			{
				this.color = value;
				PropertyUpdated(this.SetColor);
			}
		}

		#endregion color

		#region font

		private Font font;

		private void SetFont(Control parent)
		{
			this.controls[parent].ForEach(c =>
			{
				c.Font = this.font;
			});
		}

		public Font Font {
			get
			{
				return this.font;
			}
			set
			{
				this.font = value;
				PropertyUpdated(this.SetFont);
			}
		}

		#endregion font

		public void Attach(Control parent, Control c)
		{
			if (this.controls.ContainsKey(parent))
			{
				if (this.controls[parent].DontHave(c))
				{
					this.controls[parent].Add(c);
				}
			}
			else
			{
				this.controls.Add(parent, new List<Control>() { c });
				parent.Disposed += (_s, _e) => { this.controls.Remove(c); };
			}
		}

		public void Detach(Control target)
		{
			if (this.controls.ContainsKey(target))
			{
				this.controls.Remove(target);
			}
			else
			{
				foreach (var values in this.controls.Values)
				{
					if (values.Has(target))
					{
						values.Remove(target);
					}
				}
			}
		}
	}
}