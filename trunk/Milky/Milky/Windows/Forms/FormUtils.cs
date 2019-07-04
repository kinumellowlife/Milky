namespace Milky.Windows.Forms
{
	public static class FormUtils
	{
		public static bool IsDesinMode {
			get
			{
				return (System.Reflection.Assembly.GetEntryAssembly() == null);
			}
		}
	}
}