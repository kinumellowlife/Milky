using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Milky.Extensions;
using Milky.Windows.Forms.Controls;

namespace Milky.Windows.Forms.Controls
{
	public class MilkyCheckBoxGroup : List<CheckBox>
	{
		#region operation flags

		public enum Operation
		{
			And,
			Or,
			ToggleAnd,
			OnAll,
			OffAll,
			ToggleAll,
		}

		#endregion operation flags

		#region constructs

		public MilkyCheckBoxGroup()
		{
		}

		#endregion constructs

		public new void Add(CheckBox control)
		{
			if (this.DontHave(control))
			{
				base.Add(control);
			}
		}

		public void Add(params CheckBox[] controls)
		{
			controls.ForEach(control => Add(control));
		}

		public new void Remove(CheckBox control)
		{
			if (this.Has(control))
			{
				this.Remove(control);
			}
		}

		public new void RemoveAt(int index)
		{
			if (this.Count > index)
			{
				this.RemoveAt(index);
			}
		}

		private void And(CheckBox control, CheckBox target)
		{
			if (control.Equals(target))
				control.Checked = true;
			else
				control.Checked = false;
		}

		private void Or(CheckBox control, CheckBox target)
		{
			target.Checked = true;
		}

		private void ToggleAnd(CheckBox control, CheckBox target)
		{
			if (control.Equals(target))
				control.Checked = !control.Checked;
			else
				control.Checked = false;
		}

		private void OnAll(CheckBox control, CheckBox target)
		{
			control.Checked = true;
		}

		private void OffAll(CheckBox control, CheckBox target)
		{
			control.Checked = false;
		}

		private void ToggleAll(CheckBox control, CheckBox target)
		{
			control.Checked = !control.Checked;
		}

		private bool IsChecked(CheckBox control)
		{
			return control.Checked;
		}

		public void Do(Operation method, CheckBox target = null)
		{
			switch (method)
			{
				case Operation.And:
					if (target != null)
					{
						this.ForEach(c => c.Invoke(And, target));
					}
					break;

				case Operation.Or:
					if (target != null)
					{
						this.ForEach(c => c.Invoke(Or, target));
					}
					break;

				case Operation.ToggleAnd:
					if (target != null)
					{
						this.ForEach(c => c.Invoke(ToggleAnd, target));
					}
					break;

				case Operation.OnAll:
					this.ForEach(c => c.Invoke(OnAll));
					break;

				case Operation.OffAll:
					this.ForEach(c => c.Invoke(OffAll));
					break;

				case Operation.ToggleAll:
					this.ForEach(c => c.Invoke(ToggleAll));
					break;
			}
		}

		public List<CheckBox> Select(Func<CheckBox, bool> pred)
		{
			var result = this.Where(control => pred(control)).ToList();

			return result;
		}

		public List<CheckBox> Select()
		{
			return Select(control => control.Checked);
		}

		public CheckBox SelectOne(Func<CheckBox, bool> pred)
		{
			var result = this.Where(control => pred(control)).ToList();

			if (result.Count > 0)
				return result[0];
			else
				return default;
		}

		public CheckBox SelectOne()
		{
			return SelectOne(control => control.Checked);
		}
	}
}