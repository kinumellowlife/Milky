using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Milky.Windows.Forms.Jornal
{
	/// <summary>
	/// ログ１件分の情報
	/// </summary>
	public class MilkyLogItem : INotifyPropertyChanged
	{
		#region enums

		public enum MilkyLogTypeFlags : uint
		{
			Debug = 0x00000100,
			Alert = 0x80000000,
			Message = 0x00000002,
			Information = 0x00000004,
		}

		#endregion enums

		#region fields

		private DateTime _lastTime;
		private string _message;
		private object _tag;
		private MilkyLogTypeFlags _logType = MilkyLogTypeFlags.Message;

		#endregion fields

		#region properties
		/// <summary>
		/// ログ種類
		/// </summary>
		public MilkyLogTypeFlags LogType {
			get
			{
				return this._logType;
			}
			set
			{
				SetProperty(this, ref this._logType, value);
			}
		}

		/// <summary>
		/// 生成日
		/// </summary>
		public DateTime DateTime {
			get
			{
				return this._lastTime;
			}
			set
			{
				SetProperty(this, ref this._lastTime, value);
			}
		}

		/// <summary>
		/// メッセージ
		/// </summary>
		public string Message {
			get
			{
				return this._message;
			}
			set
			{
				SetProperty(this, ref this._message, value);
			}
		}

		/// <summary>
		/// 付加情報
		/// </summary>
		public object Tag {
			get
			{
				return this._tag;
			}
			set
			{
				SetProperty(this, ref this._tag, value);
			}
		}

		#endregion properties

		#region events

		public event PropertyChangedEventHandler PropertyChanged;

		protected void SetProperty<T>(object o, ref T field, T value, [CallerMemberName] string propertyName = null)
		{
			field = value;
			PropertyChanged?.Invoke(o, new PropertyChangedEventArgs(propertyName));
		}

		#endregion events
	}
}