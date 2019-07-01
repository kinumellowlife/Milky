namespace Milky.Cmds
{
	public delegate void CmdActor(object[] args);

	/// <summary>
	/// コマンドを実行するためのインタフェイス。
	/// このインタフェイスを持つクラスがコマンド実行可能となる。
	/// </summary>
	public interface ICmdListener
	{
		/// <summary>
		/// コマンド実行処理
		/// </summary>
		/// <param name="cmd">コマンド名</param>
		/// <param name="args">コマンド引数</param>
		void CommandInvoke(string cmd, object[] args);
	}
}