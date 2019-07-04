using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Milky.Extensions;

namespace Milky.Windows.Forms.Jornal
{
	public class MilkyLogListView : ListView, INotifyPropertyChanged
	{
		#region fields

		/// <summary>
		/// アイテム全部
		/// </summary>
		private List<MilkyLogListItem> allItems = new List<MilkyLogListItem>();

		/// <summary>
		/// フィルタして表示するアイテムのallItemsに対する要素番号
		/// </summary>
		private List<int> filtered = new List<int>();

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
			this.DoubleBuffered = true;
			this.HideSelection = false;
			this.FullRowSelect = true;
			this.GridLines = true;
			this.VirtualMode = true;
			this.MultiSelect = false;
			this.View = View.Details;
			this.RetrieveVirtualItem += MilkyLogListView_RetrieveVirtualItem;
			this.PropertyChanged += MilkyLogListView_PropertyChanged;
		}

		#endregion construction

		#region private API

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