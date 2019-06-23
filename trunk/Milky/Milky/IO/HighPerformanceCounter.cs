using System.Runtime.InteropServices;
using System.Threading;

namespace Milky.IO
{
	/// <summary>
	/// QueryPerformanceCounterを利用した高解像度カウンタ
	/// </summary>
	public class HightPerformanceCounter
	{
		#region dlls

		[DllImport("Kernel32.dll")]
		private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

		[DllImport("Kernel32.dll")]
		private static extern bool QueryPerformanceFrequency(out long lpFrequency);

		#endregion dlls

		#region fields

		private long startTime;
		private long stopTime;
		private long freq;

		#endregion fields

		#region 構築

		/// <summary>
		/// 構築
		/// </summary>
		public HightPerformanceCounter()
		{
			QueryPerformanceFrequency(out freq);
			startTime = 0;
			stopTime = 0;
		}

		#endregion 構築

		#region プロパティ

		/// <summary>
		/// QueryPerformanceCounterが使えるOSかどうかの取得
		/// </summary>
		public bool IsSupported {
			get
			{
				if (QueryPerformanceFrequency(out freq) == false)
				{
					return false;
				}
				else
				{
					return false;
				}
			}
		}

		/// <summary>
		/// 経過時間（秒）を取得
		/// </summary>
		public double ElapsedSeconds {
			get
			{
				return (double)(stopTime - startTime) / (double)freq;
			}
		}

		#endregion プロパティ

		#region 操作

		/// <summary>
		/// 計測開始
		/// </summary>
		public void Start()
		{
			Thread.Sleep(0);

			QueryPerformanceCounter(out startTime);

			stopTime = startTime;
		}

		/// <summary>
		/// 計測終了
		/// </summary>
		public void Stop()
		{
			QueryPerformanceCounter(out stopTime);
		}

		#endregion 操作
	}
}