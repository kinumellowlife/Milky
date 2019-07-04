using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

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
				this._logType = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LogType"));
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
				this._lastTime = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DateTime"));
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
				this._message = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Message"));
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
				this._tag = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tag"));
			}
		}

		#endregion properties

		#region events

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion events
	}

	public class MilkyLogListItem : MilkyLogItem
	{
		#region fields

		private readonly ListViewItem listItem = null;

		#endregion fields

		#region construction

		/// <summary>
		/// 構築
		/// </summary>
		public MilkyLogListItem()
		{
			this.PropertyChanged += MilkyLogListItem_PropertyChanged;

			this.listItem = new ListViewItem
			{
				//for logtype
				Text = ""
			};
			//for datime
			this.listItem.SubItems.Add("");
			//for message
			this.listItem.SubItems.Add("");
		}

		#endregion construction

		#region static API

		/// <summary>
		/// ListViewItemに変換
		/// </summary>
		public static explicit operator ListViewItem(MilkyLogListItem mi)
		{
			return mi.listItem;
		}

		/// <summary>
		/// カラムヘッダーのテキストを返す
		/// </summary>
		public static IEnumerable<ColumnHeader> MilkyLogHeaders()
		{
			yield return new ColumnHeader("type") { Text = "type" };
			yield return new ColumnHeader("date") { Text = "date" };
			yield return new ColumnHeader("message") { Text = "message" };
			yield break;
		}

		#endregion static API

		#region private API

		private void MilkyLogListItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "LogType":
					this.listItem.Text = this.LogType.ToString();
					break;

				case "DateTime":
					this.listItem.SubItems[1].Text = this.DateTime.ToString("yy/MM/dd HH:mm:ss");
					break;

				case "Message":
					this.listItem.SubItems[2].Text = this.Message;
					break;
			}
		}

		#endregion private API
	}
}