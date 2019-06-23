using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Milky.Extensions;

namespace Milky.Windows.Forms
{
	public class MilkyListViewItem
	{
		public ListViewItem Item { get; set; }

		public delegate ListViewItem CreateItemDelegate(int index);

		public CreateItemDelegate OnCreateItem;
	}

	public class MilkyListView : ListView
	{
		#region fields

		protected string filteringText = "";
		protected List<MilkyListViewItem> allItems = new List<MilkyListViewItem>();
		private List<MilkyListViewItem> filterd = new List<MilkyListViewItem>();
		private bool nowFiltering = false;

		#endregion fields

		#region properties

		public new IEnumerable<MilkyListViewItem> Items {
			get
			{
				foreach (var item in this.allItems)
				{
					yield return item;
				}
			}
		}

		public bool EnableDeleteByKey { get; set; } = false;

		public int Count { get { return this.allItems.Count; } }

		public int FilterdCount { get { return this.filterd.Count; } }

		public int SelectedCount { get { return this.SelectedIndices.Count; } }

		public bool SelectedAny { get { return this.SelectedIndices.Count > 0; } }

		public bool SaveItem { get; set; }

		public MilkyListViewItem SelectedItem {
			get
			{
				if (this.SelectedIndices.Count > 0)
				{
					int index = this.SelectedIndices[0];
					if (this.nowFiltering)
						return this.filterd[index];
					else
						return this.allItems[index];
				}
				return null;
			}
		}

		public new IEnumerable<MilkyListViewItem> SelectedItems {
			get
			{
				for (int i = 0; i < this.SelectedIndices.Count; i++)
				{
					int index = this.SelectedIndices[i];
					if (this.nowFiltering)
						yield return this.filterd[index];
					else
						yield return this.allItems[index];
				}
			}
		}

		//Hide this property.
		private new bool VirtualMode { get { return base.VirtualMode; } set {/* always virtual mode*/ } }

		#endregion properties

		public MilkyListView() : base()
		{
			this.DoubleBuffered = true;
			this.View = View.Details;
			this.GridLines = true;
			this.HideSelection = false;
			this.FullRowSelect = true;
			this.VirtualMode = true;
			this.VirtualListSize = 0;
			this.RetrieveVirtualItem += MilkyListView_RetrieveVirtualItem;
			this.KeyDown += MilkyListView_KeyDown;
		}

		private void MilkyListView_KeyDown(object sender, KeyEventArgs e)
		{
			if (!this.EnableDeleteByKey)
				return;
			switch (e.KeyCode)
			{
				case Keys.Delete:
					Remove(SelectedItems);
					break;
			}
		}

		private void MilkyListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
		{
			if (this.nowFiltering)
			{
				e.Item = this.filterd[e.ItemIndex].Item;
			}
			else
			{
				e.Item = this.allItems[e.ItemIndex].Item;
			}
		}

		public void Remove(IEnumerable<MilkyListViewItem> items)
		{
			if (items == null)
				return;
			items.ForEach(item =>
			{
				var atom = this.allItems.Find(i => i.Equals(item));
				if (atom != null)
				{
					this.allItems.Remove(atom);
					if (!this.VirtualMode)
					{
						base.Items.Remove(atom.Item);
					}
				}
			});
			if (this.VirtualMode)
			{
				this.VirtualListSize = this.allItems.Count;
			}
		}

		public void Remove(MilkyListViewItem item)
		{
			if (item == null)
				return;
			var atom = this.allItems.Find(i => i.Equals(item));
			if (atom != null)
			{
				this.allItems.Remove(atom);
				if (!this.VirtualMode)
				{
					base.Items.Remove(atom.Item);
				}
				else
				{
					this.VirtualListSize = this.allItems.Count;
				}
			}
		}

		public void SelectItems(Predicate<MilkyListViewItem> pred)
		{
			this.allItems.ForEach(item => item.Item.Selected = pred(item));
		}

		public IEnumerable<MilkyListViewItem> FindItems(Predicate<MilkyListViewItem> pred, bool inFiltered = false)
		{
			if (inFiltered)
			{
				foreach (var item in this.filterd)
				{
					if (pred(item))
						yield return item;
				}
			}
			else
			{
				foreach (var item in this.allItems)
				{
					if (pred(item))
						yield return item;
				}
			}
		}

		public virtual void Filter(string text)
		{
			this.filteringText = text;
			if (string.IsNullOrEmpty(text))
			{
				this.nowFiltering = false;
				if (!this.VirtualMode)
				{
					base.Items.Clear();
					this.allItems.ForEach(a => base.Items.Add(a.Item));
				}
				else
				{
					this.VirtualListSize = 0;
					this.VirtualListSize = this.allItems.Count;
				}
			}
			else
			{
				this.filterd.Clear();
				List<string> filterTexts = text.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

				foreach (var item in this.allItems)
				{
					for (int i = 0; i < item.Item.SubItems.Count; i++)
					{
						string check = item.Item.SubItems[i].Text;
						if (!string.IsNullOrEmpty(check))
						{
							CompareInfo ci = CultureInfo.CurrentCulture.CompareInfo;
							bool find = true;
							filterTexts.ForEach(t =>
							{
								if (ci.IndexOf(check, t, CompareOptions.IgnoreWidth | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreCase) == -1)
								{
									find = false;
								}
							});
							if (find)
							{
								this.filterd.Add(item);
								break;
							}
						}
					}
				}
				this.nowFiltering = true;
				if (!this.VirtualMode)
				{
					base.Items.Clear();
					this.filterd.ForEach(f => base.Items.Add(f.Item));
				}
				else
				{
					this.VirtualListSize = 0;
					this.VirtualListSize = this.filterd.Count;
				}
			}
			AutoFit();
		}

		public void AutoFit()
		{
			foreach (ColumnHeader header in this.Columns)
			{
				header.Width = -2;
			}
		}

		public void ClearAllItem()
		{
			this.allItems.Clear();
			this.filterd.Clear();
			this.nowFiltering = false;
			this.VirtualListSize = 0;
		}
	}
}