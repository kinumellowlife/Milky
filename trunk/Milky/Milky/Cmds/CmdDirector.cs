using System;
using System.Collections.Generic;

namespace Milky.Cmds
{
	public class CmdDirector
	{
		private List<Tuple<string, CmdActor>> actors = new List<Tuple<string, CmdActor>>();

		public void Set(string cmd, CmdActor actor)
		{
			this.actors?.Add(new Tuple<string, CmdActor>(cmd, actor));
		}

		public void Action(string cmd, object[] args)
		{
			var t = this.actors.FindAll(a => a.Item1.Equals(cmd));
			if (t != null)
			{
				foreach (var a in t)
				{
					a.Item2(args);
				}
			}
		}
	}
}