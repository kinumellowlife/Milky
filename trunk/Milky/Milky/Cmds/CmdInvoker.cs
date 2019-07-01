using System.Collections.Generic;
using System.Windows.Forms;
using Milky.Algorithm;

namespace Milky.Cmds
{
	/// <summary>
	/// コマンドを実行するクラス
	/// クラスやフォームをまたがってコマンドを投げ合うため、
	/// グローバル空間に一つだけインスタンスが存在するシングルトンクラスとして動作する。
	/// </summary>
	public class CmdInvoker : Singleton<CmdInvoker>
	{
		/// <summary>
		/// コマンドリスナー達
		/// </summary>
		private List<ICmdListener> attached = new List<ICmdListener>();

		/// <summary>
		/// コマンド実行のためのイベント
		/// </summary>
		/// <param name="f">リスナー</param>
		/// <param name="cmd">コマンド</param>
		/// <param name="args">コマンド引数</param>
		public delegate void CommandEvent(ICmdListener f, string cmd, object[] args);

		/// <summary>
		/// リスナー登録
		/// </summary>
		/// <param name="listener">コマンドを受け付けるリスナー</param>
		public void Attach(ICmdListener listener) => this.attached.Add(listener);

		/// <summary>
		/// リスナー解除
		/// </summary>
		/// <param name="listener">リスナー</param>
		public void Detach(ICmdListener listener) => this.attached.Remove(listener);

		/// <summary>
		/// イベント起動
		/// </summary>
		/// <param name="listener">リスナー</param>
		/// <param name="cmd">コマンド</param>
		/// <param name="args">コマンド引数</param>
		private void EventKicker(ICmdListener listener, string cmd, object[] args = null)
		{
			if (listener is Control c)
			{
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

		/// <summary>
		/// コマンド実行
		/// </summary>
		/// <param name="cmd">コマンド</param>
		/// <param name="args">コマンド引数</param>
		public void Invoke(string cmd, object[] args = null)
		{
			List<ICmdListener> shadow = new List<ICmdListener>(this.attached);
			foreach (var f in shadow)
			{
				EventKicker(f, cmd, args);
			}
		}

		/// <summary>
		/// コマンド実行
		/// </summary>
		/// <param name="cmd">コマンド</param>
		/// <param name="args">コマンド引数１</param>
		public void Invoke(string cmd, object args1) => Invoke(cmd, new[] { args1 });

		/// <summary>
		/// コマンド実行
		/// </summary>
		/// <param name="cmd">コマンド</param>
		/// <param name="args1">コマンド引数１</param>
		/// <param name="args2">コマンド引数２</param>
		public void Invoke(string cmd, object args1, object args2) => Invoke(cmd, new[] { args1, args2 });

		/// <summary>
		/// コマンド実行
		/// </summary>
		/// <param name="cmd">コマンド</param>
		/// <param name="args1">コマンド引数１</param>
		/// <param name="args2">コマンド引数２</param>
		/// <param name="args3">コマンド引数３</param>
		public void Invoke(string cmd, object args1, object args2, object args3) => Invoke(cmd, new[] { args1, args2, args3 });

		/// <summary>
		/// コマンド実行
		/// </summary>
		/// <param name="cmd">コマンド</param>
		/// <param name="args1">コマンド引数１</param>
		/// <param name="args2">コマンド引数２</param>
		/// <param name="args3">コマンド引数３</param>
		/// <param name="args4">コマンド引数４</param>
		public void Invoke(string cmd, object args1, object args2, object args3, object args4) => Invoke(cmd, new[] { args1, args2, args3, args4 });

		/// <summary>
		/// コマンド実行
		/// </summary>
		/// <param name="cmd">コマンド</param>
		/// <param name="args1">コマンド引数１</param>
		/// <param name="args2">コマンド引数２</param>
		/// <param name="args3">コマンド引数３</param>
		/// <param name="args4">コマンド引数４</param>
		/// <param name="args5">コマンド引数５</param>
		public void Invoke(string cmd, object args1, object args2, object args3, object args4, object args5) => Invoke(cmd, new[] { args1, args2, args3, args4, args5 });

		/// <summary>
		/// コマンド実行
		/// </summary>
		/// <param name="cmd">コマンド</param>
		/// <param name="args1">コマンド引数１</param>
		/// <param name="args2">コマンド引数２</param>
		/// <param name="args3">コマンド引数３</param>
		/// <param name="args4">コマンド引数４</param>
		/// <param name="args5">コマンド引数５</param>
		/// <param name="args6">コマンド引数６</param>
		public void Invoke(string cmd, object args1, object args2, object args3, object args4, object args5, object args6) => Invoke(cmd, new[] { args1, args2, args3, args4, args5, args6 });

		/// <summary>
		/// コマンド実行
		/// </summary>
		/// <param name="cmd">コマンド</param>
		/// <param name="args1">コマンド引数１</param>
		/// <param name="args2">コマンド引数２</param>
		/// <param name="args3">コマンド引数３</param>
		/// <param name="args4">コマンド引数４</param>
		/// <param name="args5">コマンド引数５</param>
		/// <param name="args6">コマンド引数６</param>
		/// <param name="args7">コマンド引数７</param>
		public void Invoke(string cmd, object args1, object args2, object args3, object args4, object args5, object args6, object args7) => Invoke(cmd, new[] { args1, args2, args3, args4, args5, args6, args7 });

		/// <summary>
		/// コマンド実行
		/// </summary>
		/// <param name="cmd">コマンド</param>
		/// <param name="args1">コマンド引数１</param>
		/// <param name="args2">コマンド引数２</param>
		/// <param name="args3">コマンド引数３</param>
		/// <param name="args4">コマンド引数４</param>
		/// <param name="args5">コマンド引数５</param>
		/// <param name="args6">コマンド引数６</param>
		/// <param name="args7">コマンド引数７</param>
		/// <param name="args8">コマンド引数８</param>
		public void Invoke(string cmd, object args1, object args2, object args3, object args4, object args5, object args6, object args7, object args8) => Invoke(cmd, new[] { args1, args2, args3, args4, args5, args6, args7, args8 });

		/// <summary>
		/// コマンド実行
		/// </summary>
		/// <param name="cmd">コマンド</param>
		/// <param name="args1">コマンド引数１</param>
		/// <param name="args2">コマンド引数２</param>
		/// <param name="args3">コマンド引数３</param>
		/// <param name="args4">コマンド引数４</param>
		/// <param name="args5">コマンド引数５</param>
		/// <param name="args6">コマンド引数６</param>
		/// <param name="args7">コマンド引数７</param>
		/// <param name="args8">コマンド引数８</param>
		/// <param name="args9">コマンド引数９</param>
		public void Invoke(string cmd, object args1, object args2, object args3, object args4, object args5, object args6, object args7, object args8, object args9) => Invoke(cmd, new[] { args1, args2, args3, args4, args5, args6, args7, args8, args9 });

		/// <summary>
		/// コマンド実行
		/// </summary>
		/// <param name="cmd">コマンド</param>
		/// <param name="args1">コマンド引数１</param>
		/// <param name="args2">コマンド引数２</param>
		/// <param name="args3">コマンド引数３</param>
		/// <param name="args4">コマンド引数４</param>
		/// <param name="args5">コマンド引数５</param>
		/// <param name="args6">コマンド引数６</param>
		/// <param name="args7">コマンド引数７</param>
		/// <param name="args8">コマンド引数８</param>
		/// <param name="args9">コマンド引数９</param>
		/// <param name="args10">コマンド引数１０</param>
		public void Invoke(string cmd, object args1, object args2, object args3, object args4, object args5, object args6, object args7, object args8, object args9, object args10) => Invoke(cmd, new[] { args1, args2, args3, args4, args5, args6, args7, args8, args9, args10 });
	}
}