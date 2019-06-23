using System;
using System.Windows.Forms;

namespace Milky.Windows
{
	public static class ControlInvoker
	{
		public static void Invoke<T>(this T control, Action<T> action) where T : Control
		{
			if (control.IsHandleCreated)
			{
				if (control.Parent.InvokeRequired)
				{
					control.Parent.Invoke((MethodInvoker)(() => action(control)));
				}
				else
				{
					action(control);
				}
			}
		}

		public static void Invoke<T>(this T control, Action<T, T> action, T target = null) where T : Control
		{
			if (control.IsHandleCreated)
			{
				if (control.Parent.InvokeRequired)
				{
					control.Parent.Invoke(new Action<T, T>(action));
				}
				else
				{
					action(control, target);
				}
			}
		}
	}
}