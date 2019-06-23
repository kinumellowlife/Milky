namespace Milky.Cmds
{
	public delegate void CmdActor(object[] args);

	public interface ICmdListener
	{
		void CommandInvoke(string cmd, object[] args);
	}
}