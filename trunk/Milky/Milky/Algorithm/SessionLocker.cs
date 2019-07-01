using System;
using System.Threading;

namespace Milky.Algorithm
{
	/// <summary>
	/// Mutexを利用したロック機構
	/// </summary>
	public class SessionLocker : IDisposable
	{
		#region fields

		/// <summary>
		/// ロック用ミューテックス
		/// </summary>
		private Mutex mx;

		#endregion fields

		#region construct

		/// <summary>
		/// 構築
		/// </summary>
		/// <param name="name">ミューテックス名称</param>
		public SessionLocker(string name)
		{
			mx = new Mutex(false, name);
			mx.WaitOne();
		}

		#endregion construct

		#region API

		/// <summary>
		/// 破棄
		/// </summary>
		public void Dispose()
		{
			try
			{
				this.mx.ReleaseMutex();
				this.mx.Close();
				this.mx = null;
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(string.Format("{0}/{1}/{2}", this.GetType(), System.Reflection.MethodBase.GetCurrentMethod(), e.Message));
			}
		}

		#endregion API
	}
}