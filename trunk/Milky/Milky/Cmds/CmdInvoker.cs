using System.Collections.Generic;
using System.Windows.Forms;

namespace Milky.Cmds
{
	public class CmdInvoker
	{
		private static CmdInvoker theInstance = new CmdInvoker();
		static public CmdInvoker Instance { get { return theInstance; } }

		private List<ICmdListener> attached = new List<ICmdListener>();

		public delegate void CommandEvent(ICmdListener f, string cmd, object[] args);

		public void Attach(ICmdListener listener) => this.attached.Add(listener);

		public void Detach(ICmdListener listener) => this.attached.Remove(listener);

		private void EventKicker(ICmdListener listener, string cmd, object[] args = null)
		{
			if (listener is Control)
			{
				Control c = listener as Control;
				if (c.InvokeRequired)
				{
					c.Invoke(new CommandEvent(this.EventKicker), listener, cmd, args);
					return;
				}
				else
				{
					listener.CommandInvoke(cmd, args);
				}
			}
			else
			{
				listener.CommandInvoke(cmd, args);
			}
		}

		public void Invoke(string cmd, object[] args = null)
		{
			List<ICmdListener> shadow = new List<ICmdListener>(this.attached);
			foreach (var f in shadow)
			{
				EventKicker(f, cmd, args);
			}
		}

		public void Invoke(string cmd, object args1) => Invoke(cmd, new[] { args1 });

		public void Invoke(string cmd, object args1, object args2) => Invoke(cmd, new[] { args1, args2 });

		public void Invoke(string cmd, object args1, object args2, object args3) => Invoke(cmd, new[] { args1, args2, args3 });

		public void Invoke(string cmd, object args1, object args2, object args3, object args4) => Invoke(cmd, new[] { args1, args2, args3, args4 });
	}
}