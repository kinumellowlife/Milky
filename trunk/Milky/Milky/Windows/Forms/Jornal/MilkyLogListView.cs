using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Milky.Extensions;
using Milky.Windows.Forms.Controls;

namespace Milky.Windows.Forms.Jornal
{
	public class MilkyLogListView : ListView, INotifyPropertyChanged
	{
		#region fields

		private ImageList imageList = new ImageList();

		/// <summary>
		/// アイテム全部
		/// </summary>
		private List<MilkyLogListItem> allItems = new List<MilkyLogListItem>();

		/// <summary>
		/// フィルタして表示するアイテムのallItemsに対する要素番号
		/// </summary>
		private List<int> filtered = new List<int>();

		private Font headerFont = new Font("MS UI Gothic", 9);

		/// <summary>
		/// 表示フラグ
		/// </summary>
		private MilkyLogItem.MilkyLogTypeFlags viewFlag = MilkyLogItem.MilkyLogTypeFlags.Alert | MilkyLogItem.MilkyLogTypeFlags.Debug | MilkyLogItem.MilkyLogTypeFlags.Information | MilkyLogItem.MilkyLogTypeFlags.Message;

		#region events

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion events

		#endregion fields

		#region 使用できないようにするプロパティ

		protected new ListViewItemCollection Items { get; }

		protected new bool VirtualMode {
			get
			{
				return base.VirtualMode;
			}
			set
			{
				base.VirtualMode = value;
			}
		}

		protected new View View {
			get
			{
				return base.View;
			}
			set
			{
				base.View = value;
			}
		}

		#endregion 使用できないようにするプロパティ

		#region propperties

		/// <summary>
		/// ヘッダのフォント
		/// </summary>
		public Font HeaderFont {
			get
			{
				return this.headerFont;
			}
			set
			{
				this.headerFont = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HeaderFont"));
				this.Invalidate();
			}
		}

		/// <summary>
		/// 表示モード
		/// </summary>
		public MilkyLogItem.MilkyLogTypeFlags ViewFlag {
			get
			{
				return this.viewFlag;
			}
			set
			{
				this.viewFlag = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ViewFlag"));
			}
		}

		#endregion propperties

		#region construction

		/// <summary>
		/// 構築
		/// </summary>
		public MilkyLogListView()
		{
			if (!FormUtils.IsDesinMode)
			{
				//実行時、カラム生成
				this.Columns.Clear();
				MilkyLogListItem.MilkyLogHeaders().ForEach(h => this.Columns.Add(h));
			}
			this.SmallImageList = this.imageList;
			this.LargeImageList = this.imageList;
			this.DoubleBuffered = true;
			this.HideSelection = false;
			this.FullRowSelect = true;
			this.GridLines = true;
			this.VirtualMode = true;
			this.MultiSelect = false;
			this.View = View.Details;
			this.RetrieveVirtualItem += MilkyLogListView_RetrieveVirtualItem;
			this.PropertyChanged += MilkyLogListView_PropertyChanged;
			this.DrawColumnHeader += MilkyLogListView_DrawColumnHeader;
			this.DrawItem += MilkyLogListView_DrawItem;
			this.DrawSubItem += MilkyLogListView_DrawSubItem;
			this.ItemMouseHover += MilkyLogListView_ItemMouseHover;
			this.OwnerDraw = true;
		}

		private void MilkyLogListView_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
		{
			var item = e.Item.Tag as MilkyLogListItem;
			this.allItems.ForEach(i => i.Hover = false);
			item.Hover = true;
		}

		//private void SetColumnWidth(MilkyLogListItem item)
		//{
		//	if ((!this.imageList.Images.ContainsKey(item.Id)) ||
		//		(height!= this.imageList.Images[item.Id].Height))
		//	{
		//		var image = new Bitmap(1, (int)height * 2);
		//		if (!this.imageList.Images.ContainsKey(item.Id))
		//		{
		//			this.imageList.Images.Add(item.Id, image);
		//		}
		//		else
		//		{
		//			this.imageList.Images.RemoveByKey(item.Id);
		//			this.imageList.Images.Add(item.Id, image);
		//			this.imageList.ImageSize = new Size(1, (int)height);
		//		}
		//	}	
		//}	

		private void MilkyLogListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
		{
			var item = e.Item.Tag as MilkyLogListItem;
			ContentAlignment alignment = ContentAlignment.MiddleLeft;
			ColorPair color = new ColorPair(Color.Black, Color.White);
			
			switch(e.ColumnIndex)
			{
				case 0:
					alignment = item.LogTypeTextAlign;
					color = item.TypeColor;
					break;
				case 1:
					alignment = item.LogDateTextAlign;
					color = item.DateColor;
					break;
				case 2:
					alignment = item.LogMessageTextAlign;
					color = item.MessageColor;
					break;
			}

			using (StringFormat sf = new StringFormat())
			{
				switch( alignment )
				{
					case ContentAlignment.BottomCenter:
					case ContentAlignment.MiddleCenter:
					case ContentAlignment.TopCenter:
						sf.Alignment = StringAlignment.Center;
						break;
					case ContentAlignment.BottomLeft:
					case ContentAlignment.MiddleLeft:
					case ContentAlignment.TopLeft:
						sf.Alignment = StringAlignment.Near;
						break;
					case ContentAlignment.BottomRight:
					case ContentAlignment.MiddleRight:
					case ContentAlignment.TopRight:
						sf.Alignment = StringAlignment.Far;
						break;
				}

				switch( alignment )
				{
					case ContentAlignment.BottomCenter:
					case ContentAlignment.BottomLeft:
					case ContentAlignment.BottomRight:
						sf.LineAlignment = StringAlignment.Near;
						break;
					case ContentAlignment.MiddleCenter:
					case ContentAlignment.MiddleLeft:
					case ContentAlignment.MiddleRight:
						sf.LineAlignment = StringAlignment.Center;
						break;
					case ContentAlignment.TopCenter:
					case ContentAlignment.TopLeft:
					case ContentAlignment.TopRight:
						sf.LineAlignment = StringAlignment.Far;
						break;
				}

				if( e.ColumnIndex > 0)
				{
					if ((e.ItemState & ListViewItemStates.Selected) == 0)
					{
						e.DrawBackground();
					}

					// Draw the subitem text in red to highlight it. 
					e.Graphics.DrawString(e.SubItem.Text, this.Font, new SolidBrush(color.Fore), e.Bounds, sf);
				}
				else
				{
					e.DrawText();
				}
			}
		}

		private void MilkyLogListView_DrawItem(object sender, DrawListViewItemEventArgs e)
		{
			if ((e.State & ListViewItemStates.Selected) != 0)
			{
				// Draw the background and focus rectangle for a selected item.
				e.Graphics.FillRectangle(new SolidBrush(this.BackColor), e.Bounds);
				e.DrawFocusRectangle();
			}
			else
			{
				// Draw the background for an unselected item.
				using (LinearGradientBrush brush = new LinearGradientBrush(e.Bounds, this.ForeColor,this.ForeColor, LinearGradientMode.Horizontal))
				{
					e.Graphics.FillRectangle(brush, e.Bounds);
				}
			}

			// Draw the item text for views other than the Details view.
			if (this.View != View.Details)
			{
				e.DrawText();
			}
		}	

		#endregion construction

		#region private API

		private void MilkyLogListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
		{
			using (StringFormat sf = new StringFormat())
			{
				switch (e.Header.TextAlign)
				{
					case HorizontalAlignment.Center:
						sf.Alignment = StringAlignment.Center;
						break;
					case HorizontalAlignment.Right:
						sf.Alignment = StringAlignment.Far;
						break;
				}

				// Draw the standard header background.
				e.DrawBackground();

				// Draw the header text.
				e.Graphics.DrawString(e.Header.Text, this.headerFont, Brushes.Black, e.Bounds, sf);
			}
		}

		/// <summary>
		/// プロパティ変更通知
		/// </summary>
		private void MilkyLogListView_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			UpdateView();
		}

		/// <summary>
		/// フィルタ条件に基づいて表示項目を決める
		/// </summary>
		private void MakeFilter()
		{
			this.filtered.Clear();
			this.allItems.ForAll((index, item) =>
			{
				if ((item.LogType & this.viewFlag) == item.LogType)
					this.filtered.Add(index);
			});
		}

		/// <summary>
		/// フィルタ条件に基づいて表示項目を決め、描画更新する
		/// </summary>
		private void UpdateView()
		{
			MakeFilter();
			if (this.VirtualListSize != this.filtered.Count)
			{
				this.VirtualListSize = this.filtered.Count;
			}
			else
			{
				this.Invalidate();
			}
		}

		#endregion private API

		#region public API

		/// <summary>
		/// 指定アイテムを検索し、最初に見つかったものを返す
		/// </summary>
		/// <param name="match">条件式</param>
		public MilkyLogListItem FindItem(Func<MilkyLogListItem, bool> match)
		{
			foreach (var item in this.allItems)
			{
				if (match(item))
					return item;
			}
			return null;
		}

		/// <summary>
		/// 指定タグのアイテムを検索し、最初に見つかったものを返す
		/// </summary>
		/// <param name="value">検索値</param>
		public MilkyLogListItem FindByTag(object value)
		{
			foreach (var item in this.allItems)
			{
				if (item.Tag.Equals(value))
					return item;
			}
			return null;
		}

		/// <summary>
		/// ログアイテムを追加する
		/// </summary>
		public void AddItem(MilkyLogListItem item)
		{
			this.allItems.Add(item);
			UpdateView();
		}

		/// <summary>
		/// ログアイテムを追加する
		/// </summary>
		public void AddItem(IEnumerable<MilkyLogListItem> items)
		{
			items.ForEach(item => AddItem(item));
			UpdateView();
		}

		/// <summary>
		/// ログアイテムを削除する
		/// </summary>
		public void Remove(MilkyLogListItem item)
		{
			this.allItems.Remove(item);
			UpdateView();
		}

		/// <summary>
		/// ログアイテムを削除する
		/// </summary>
		public void Remove(int index)
		{
			if (this.allItems.Count > index)
				this.allItems.RemoveAt(index);
			UpdateView();
		}

		/// <summary>
		/// 指定条件に合致する要素を消す
		/// </summary>
		public void Remove(Predicate<MilkyLogListItem> match)
		{
			List<int> removes = new List<int>();
			this.allItems.ForAll((index, item) =>
			{
				if (match(item))
					removes.Add(index);
			});
			//後ろから消す
			removes.Reverse();
			foreach (var index in removes)
			{
				this.allItems.RemoveAt(index);
			}

			UpdateView();
		}

		/// <summary>
		/// 全部消す
		/// </summary>
		public void RemoveAll()
		{
			this.allItems.Clear();
			UpdateView();
		}

		/// <summary>
		/// アイテムの描画更新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MilkyLogListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
		{
			e.Item = (ListViewItem)this.allItems[this.filtered[e.ItemIndex]];
			this.Invalidate();
		}

		#endregion public API
	}
}