using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Milky.Collections;
using Milky.Extensions;

namespace Milky.Windows.Forms.Controls
{
	/// <summary>
	/// コンボボックスにキー・バリュー形式での操作を追加するアダプタ
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	public class ComboBoxAdapter<TKey, TValue>
	{
		#region inner class

		/// <summary>
		/// コンボボックスに表示されるテキストを管理するクラス
		/// </summary>
		public class ComboTextHolder
		{
			#region fields

			private TKey key;
			private TValue value;

			#endregion fields

			#region events

			/// <summary>
			/// 表示テキスト変換イベント
			/// </summary>
			public GetViewTextDelegate OnGetViewText;

			#endregion events

			#region construct

			/// <summary>
			/// 構築
			/// </summary>
			/// <param name="key">キー</param>
			/// <param name="value">値</param>
			public ComboTextHolder(TKey key, TValue value)
			{
				this.key = key;
				this.value = value;
			}

			#endregion construct

			#region API

			/// <summary>
			/// 表示テキストへの変換
			/// </summary>
			/// <returns>表示テキスト</returns>
			public override string ToString()
			{
				if (OnGetViewText == null)
				{
					return value.ToString();
				}
				else
				{
					return OnGetViewText(this.value);
				}
			}

			#endregion API
		}

		#endregion inner class

		#region public delegate

		/// <summary>
		/// Valueをテキストに変換するためのイベント
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public delegate string GetViewTextDelegate(TValue value);

		/// <summary>
		/// テキスト変換イベント
		/// </summary>
		public GetViewTextDelegate OnGetViewText;

		#endregion public delegate

		#region fields

		/// <summary>
		/// キー・バリューのコンテナ
		/// </summary>
		private MapList<TKey, TValue> container = new MapList<TKey, TValue>();

		/// <summary>
		/// アタッチするコンボボックス
		/// </summary>
		private ComboBox comboBox;

		#endregion fields

		#region property

		/// <summary>
		/// キーに基づく要素の取得
		/// </summary>
		/// <param name="key"></param>
		public TValue this[TKey key] {
			get
			{
				if (this.container.ContainsKey(key))
				{
					return this.container[key];
				}
				else
				{
					return default(TValue);
				}
			}
		}

		/// <summary>
		/// 先頭からの要素番号を指定して値を取得
		/// </summary>
		/// <param name="index"></param>
		public TValue this[int index] {
			get
			{
				if (this.container.Count <= index)
				{
					return default(TValue);
				}
				else
				{
					return this.container[index];
				}
			}
		}

		/// <summary>
		/// 選択中の値を取得する
		/// </summary>
		public TValue SelectedItem {
			get
			{
				return this[this.comboBox.SelectedIndex];
			}
		}

		/// <summary>
		/// 選択中の値のテキストを取得する
		/// </summary>
		public string SelectedItemText {
			get
			{
				string viewText = "";
				if (OnGetViewText != null)
				{
					viewText = OnGetViewText(SelectedItem);
				}
				else
				{
					viewText = SelectedItem.ToString();
				}
				return viewText;
			}
		}

		/// <summary>
		/// 値の取得
		/// </summary>
		public IEnumerable<TValue> Values {
			get
			{
				foreach (var v in container.Values)
				{
					yield return v;
				}
			}
		}

		#endregion property

		#region construct

		/// <summary>
		/// 構築
		/// </summary>
		public ComboBoxAdapter(ComboBox target)
		{
			this.comboBox = target;
		}

		#endregion construct

		#region API

		/// <summary>
		/// キーバリュー形式のアイテムを追加する
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public void AddItem(TKey key, TValue value, bool autoCalcWidth = true)
		{
			if (!this.container.ContainsKey(key))
			{
				var item = new ComboTextHolder(key, value);
				if (OnGetViewText != null)
				{
					item.OnGetViewText += this.OnGetViewText;
				}
				this.comboBox.Items.Add(item);
				this.container.Add(key, value);

				if (autoCalcWidth)
				{
					RecalcComboDropdownWidth();
				}
			}
		}

		/// <summary>
		/// アイテムを選択する
		/// </summary>
		/// <param name="index"></param>
		public void SelectIndex(int index)
		{
			if (this.comboBox == null)
				return;
			if (this.comboBox.Items.Count > index)
			{
				this.comboBox.SelectedIndex = index;
			}
		}

		/// <summary>
		/// 指定したキーに関連付けられているアイテムを選択する
		/// </summary>
		/// <param name="key">選択アイテムのキー</param>
		public void SelectItemByKey(TKey key)
		{
			if (this.container.ContainsKey(key))
			{
				int index = 0;
				foreach (var expect in this.container.Keys)
				{
					if (expect.Equals(key))
						break;
					index++;
				}
				SelectIndex(index);
			}
		}

		/// <summary>
		/// 指定したキーに基づくアイテムを削除する
		/// </summary>
		/// <param name="key"></param>
		public void RemoveItem(TKey key)
		{
			if (this.container.ContainsKey(key))
			{
				TValue value = this.container[key];
				this.comboBox.Items.Remove(key);
				this.container.RemoveAt(key);
				RecalcComboDropdownWidth();
			}
		}

		/// <summary>
		/// クリア
		/// </summary>
		public void Clear()
		{
			this.container.Clear();
			this.comboBox.Items.Clear();
			RecalcComboDropdownWidth();
		}

		/// <summary>
		/// コンボボックスの内容をリフレッシュする
		/// </summary>
		public void Refresh()
		{
			//コンボボックスのテキストは動的に変化しない・・・.NETのバグらしい・・・

			//今選択されているインデックス
			int index = this.comboBox.SelectedIndex;
			this.comboBox.Items.Clear();

			foreach (var key in this.container.Keys)
			{
				var item = new ComboTextHolder(key, this.container[key]);
				if (OnGetViewText != null)
				{
					item.OnGetViewText += this.OnGetViewText;
				}
				this.comboBox.Items.Add(item);
			}
			RecalcComboDropdownWidth();
			this.SelectIndex(index);
		}

		/// <summary>
		/// プルダウンサイズをアイテムの文字列幅にする。
		/// </summary>
		public void RecalcComboDropdownWidth()
		{
			int textSize = this.comboBox.Width;
			foreach (var o in this.comboBox.Items)
			{
				string text = o.ToString();
				SizeF size = text.Measure(this.comboBox.Font);
				textSize = Math.Max(textSize, (int)size.Width);
			}
			this.comboBox.DropDownWidth = textSize;
		}

		#endregion API
	}
}