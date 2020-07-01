using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Milky.Windows.Forms.Controls;

namespace Milky.Windows.Forms.Jornal
{
	public class MilkyLogListItem : MilkyLogItem, INotifyPropertyChanged
	{
		#region fields

		private string uuid = Guid.NewGuid().ToString();
		private readonly ListViewItem listItem = null;
		private ContentAlignment alignmnet = ContentAlignment.TopLeft;
		private Font font = new Font("MS UI Gothic", 9);
		private ColorPair typeColor = new ColorPair(Color.Black, Color.White);
		private ColorPair dateColor = new ColorPair(Color.Black, Color.White);
		private ColorPair messageColor = new ColorPair(Color.Black, Color.White);
		private Color frameColor = Color.Black;
		private ContentAlignment logTypeAlignment = ContentAlignment.MiddleLeft;
		private ContentAlignment logDateAlignment = ContentAlignment.MiddleLeft;
		private ContentAlignment logMessageAlignment = ContentAlignment.MiddleLeft;

		#endregion fields

		#region properties
		public string Id { get { return this.uuid; } }

		public bool Hover { get; set; }

		public ContentAlignment LogTypeTextAlign {
			get
			{
				return this.logTypeAlignment;
			}
			set
			{
				SetProperty(this, ref this.logTypeAlignment, value);
			}
		}

		public ContentAlignment LogDateTextAlign {
			get
			{
				return this.logDateAlignment;
			}
			set
			{
				SetProperty(this, ref this.logDateAlignment, value);
			}
		}

		public ContentAlignment LogMessageTextAlign {
			get
			{
				return this.logMessageAlignment;
			}
			set
			{
				SetProperty(this, ref this.logMessageAlignment, value);
			}
		}

		public ColorPair TypeColor {
			get
			{
				return this.typeColor;
			}
			set
			{
				SetProperty(this, ref this.typeColor, value);
			}
		}
		public ColorPair DateColor {
			get
			{
				return this.dateColor;
			}
			set
			{
				SetProperty(this, ref this.dateColor, value);
			}
		}

		public ColorPair MessageColor {
			get
			{
				return this.messageColor;
			}
			set
			{
				SetProperty(this, ref this.messageColor, value);
			}
		}

		public Font Font {
			get
			{
				return this.font;
			}
			set
			{
				SetProperty(this, ref this.font, value);
			}
		}

		public ContentAlignment TextAlign {
			get
			{
				return this.alignmnet;
			}
			set
			{
				SetProperty(this, ref this.alignmnet, value);
			}
		}
		#endregion

		#region events
		public delegate string ConvertLogTypeDelagte(MilkyLogListItem item);
		public delegate string ConvertLogDateDelegate(MilkyLogListItem item);
		public delegate string ConvertLogMessageDelegate(MilkyLogListItem item);

		public ConvertLogTypeDelagte ConvertLogType;
		public ConvertLogDateDelegate ConvertLogDate;
		public ConvertLogMessageDelegate ConvertLogMessage;
		#endregion

		#region construction

		/// <summary>
		/// 構築
		/// </summary>
		public MilkyLogListItem()
		{
			this.PropertyChanged += MilkyLogListItem_PropertyChanged;

			this.listItem = new ListViewItem
			{
				ImageKey = this.Id,
				//for logtype
				Text = "",
				Tag = this
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
		public static IEnumerable<ColumnHeader> MilkyLogHeaders(string type ="type", string date="date", string message="message")
		{
			yield return new ColumnHeader("type") { Name="type", Text = type, TextAlign = HorizontalAlignment.Center };
			yield return new ColumnHeader("date") { Name="date", Text = date, TextAlign = HorizontalAlignment.Center };
			yield return new ColumnHeader("message") { Name="message", Text = message, TextAlign = HorizontalAlignment.Center };
			yield break;
		}

		#endregion static API

		#region private API

		private void MilkyLogListItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "LogType":
					if (this.ConvertLogType != null)
					{
						this.listItem.Text = this.ConvertLogType(this);
					}
					else {
						this.listItem.Text = this.LogType.ToString();
					}
					break;

				case "DateTime":
					if (this.ConvertLogDate != null)
					{
						this.listItem.SubItems[1].Text = this.ConvertLogDate(this);
					}
					else
					{
						this.listItem.SubItems[1].Text = this.DateTime.ToString("yy/MM/dd HH:mm:ss");
					}
					break;

				case "Message":
					if (this.ConvertLogMessage != null)
					{
						this.listItem.SubItems[2].Text = this.ConvertLogMessage(this);
					}
					else
					{
						this.listItem.SubItems[2].Text = this.Message;
					}
					break;
			}
		}

		#endregion private API
	}
}
