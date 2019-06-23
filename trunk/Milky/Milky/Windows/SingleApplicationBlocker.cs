using System;
using System.Reflection;
using System.Threading;

namespace Milky.Windows
{
	/// <summary>
	/// アプリケーションの２重起動を抑制する
	/// </summary>
	public class SingleApplicationBlocker : IDisposable
	{
		/// <summary>２重起動防止用に作成するミューテックスオブジェクト</summary>
		private Mutex appMutex;

		private string keyName = "";

		#region constructor
		/// <summary>
		/// construction
		/// </summary>
		/// <param name="key">block key name.If key is null or empty, this key is created from assembly name.</param>
		public SingleApplicationBlocker(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				Assembly asm = Assembly.GetExecutingAssembly();
				this.keyName = asm.GetName().FullName;
			}
			else
			{
				this.keyName = key;
			}
		}
		#endregion

		/// <summary>
		/// ２重起動チェック関数
		/// </summary>
		/// <param name="name">ミューテックス名</param>
		/// <returns>同名のミューテックスが存在していなければ true, 存在していれば false が返る</returns>
		public bool IsRunOK()
		{
			appMutex = appMutex ?? new Mutex(false, this.keyName);
			if (appMutex.WaitOne(0, false))
			{
				return true;
			}
			return false;
		}

		#region IDisposable メンバ

		/// <summary>
		/// 破棄
		/// </summary>
		public void Dispose()
		{
			// GC.KeepAlive メソッドが呼び出されるまで、ガベージコレクション対象から除外される
			GC.KeepAlive(appMutex);
			try
			{
				appMutex.ReleaseMutex();
			}
			catch
			{
			}
			appMutex.Close();
		}

		#endregion IDisposable メンバ
	}
}