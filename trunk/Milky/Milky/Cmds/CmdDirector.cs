using System;
using System.Collections.Generic;

namespace Milky.Cmds
{
	/// <summary>
	/// コマンド名とアクションとを結びつけるためのディレクタクラス
	/// </summary>
	public class CmdDirector
	{
		private List<Tuple<string, CmdActor>> actors = new List<Tuple<string, CmdActor>>();

		/// <summary>
		/// アクションを登録する
		/// </summary>
		/// <param name="cmd">コマンド</param>
		/// <param name="actor">アクション</param>
		public void Set(string cmd, CmdActor actor)
		{
			this.actors?.Add(new Tuple<string, CmdActor>(cmd, actor));
		}

		/// <summary>
		/// 指定コマンドのアクションを実行する
		/// </summary>
		/// <param name="cmd">コマンド名</param>
		/// <param name="args">コマンド引数</param>
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