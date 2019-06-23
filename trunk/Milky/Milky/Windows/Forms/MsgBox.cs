using System;
using System.Windows.Forms;

namespace Milky.Windows.Forms
{
	public abstract class MsgBox
	{
		public virtual string Caption { get; set; }
		public virtual string Body { get; set; }
		public virtual MessageBoxIcon Icon { get; set; }
		public virtual MessageBoxButtons Buttons { get; set; }
		public virtual IWin32Window Owner { get; set; } = null;

		public abstract DialogResult Show();

		protected DialogResult Popup()
		{
			DialogResult result;
			if (this.Owner != null)
			{
				result = MessageBox.Show(this.Owner, this.Body, this.Caption, this.Buttons, this.Icon);
			}
			else
			{
				result = MessageBox.Show(this.Body, this.Caption, this.Buttons, this.Icon);
			}

			return result;
		}
	}

	public class YesNoMessageBox : MsgBox
	{
		public override MessageBoxIcon Icon { get => base.Icon; set => base.Icon = MessageBoxIcon.Question; }
		public override MessageBoxButtons Buttons { get => base.Buttons; set => base.Buttons = MessageBoxButtons.YesNo; }

		public Action OnYes;
		public Action OnNo;

		public override DialogResult Show()
		{
			var result = Popup();

			if (result == DialogResult.Yes)
			{
				OnYes?.Invoke();
			}
			else if (result == DialogResult.No)
			{
				OnNo?.Invoke();
			}
			return result;
		}

		public void Show(Action yes, Action no)
		{
			var result = Popup();

			if (result == DialogResult.Yes)
			{
				yes?.Invoke();
			}
			else if (result == DialogResult.No)
			{
				no?.Invoke();
			}
		}
	}

	public class YesNoCancelMessageBox : MsgBox
	{
		public override MessageBoxIcon Icon { get => base.Icon; set => base.Icon = MessageBoxIcon.Question; }
		public override MessageBoxButtons Buttons { get => base.Buttons; set => base.Buttons = MessageBoxButtons.YesNoCancel; }

		public Action OnYes;
		public Action OnNo;
		public Action OnCancel;

		public override DialogResult Show()
		{
			var result = Popup();

			if (result == DialogResult.Yes)
			{
				OnYes?.Invoke();
			}
			else if (result == DialogResult.No)
			{
				OnNo?.Invoke();
			}
			else if (result == DialogResult.Cancel)
			{
				OnCancel?.Invoke();
			}
			return result;
		}

		public void Show(Action yes, Action no, Action cancel)
		{
			var result = Popup();

			if (result == DialogResult.Yes)
			{
				yes?.Invoke();
			}
			else if (result == DialogResult.No)
			{
				no?.Invoke();
			}
			else if (result == DialogResult.Cancel)
			{
				cancel?.Invoke();
			}
		}
	}

	public class OKCancelMessageBox : MsgBox
	{
		public override MessageBoxIcon Icon { get => base.Icon; set => base.Icon = MessageBoxIcon.Question; }
		public override MessageBoxButtons Buttons { get => base.Buttons; set => base.Buttons = MessageBoxButtons.OKCancel; }

		public Action OnOK;
		public Action OnCancel;

		public override DialogResult Show()
		{
			var result = Popup();

			if (result == DialogResult.OK)
			{
				OnOK?.Invoke();
			}
			else if (result == DialogResult.Cancel)
			{
				OnCancel?.Invoke();
			}
			return result;
		}

		public void Show(Action ok, Action cancel)
		{
			var result = Popup();

			if (result == DialogResult.OK)
			{
				ok?.Invoke();
			}
			else if (result == DialogResult.Cancel)
			{
				cancel?.Invoke();
			}
		}
	}
}