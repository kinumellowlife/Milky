using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Milky.Extensions;

namespace Milky.Windows.Forms
{
	public class MilkyRatioBar : PictureBox
	{
		#region public enums

		/// <summary>
		/// 要素を示す１要素のインタフェース
		/// </summary>
		public interface IRatioItem
		{
			/// <summary>
			/// 要素名の取得と設定
			/// </summary>
			string Name { get; set; }

			/// <summary>
			/// 値の取得と設定
			/// </summary>
			double Value { get; set; }

			/// <summary>
			/// 塗りつぶす色の取得と設定
			/// </summary>
			Color InnerColor { get; set; }

			/// <summary>
			/// 枠の色の取得と設定
			/// </summary>
			Color LineColor { get; set; }

			/// <summary>
			/// 文字色の取得と設定
			/// </summary>
			Color ForeColor { get; set; }

			/// <summary>
			/// 枠の線の太さの取得と設定
			/// </summary>
			float LineWidth { get; set; }

			/// <summary>
			/// 割合の取得と設定
			/// </summary>
			/// <remarks>
			/// 自動計算されるのでユーザは書き込まないこと。
			/// （書き込んでも意味がない）
			/// </remarks>
			double Ratio { get; set; }
		}

		/// <summary>
		/// 描画方向
		/// </summary>
		public enum BarDirection
		{
			/// <summary>
			/// 水平方向
			/// </summary>
			Horisonal,

			/// <summary>
			/// 垂直方向
			/// </summary>
			Vertical,
		}

		#endregion public enums

		#region public class

		/// <summary>
		/// バーグラフの１要素
		/// </summary>
		public class RatioItem : IRatioItem
		{
			#region properties

			/// <summary>
			/// 要素名の取得と設定
			/// </summary>
			public string Name { get; set; }

			/// <summary>
			/// 値の取得と設定
			/// </summary>
			public double Value { get; set; }

			/// <summary>
			/// 塗りつぶす色の取得と設定
			/// </summary>
			public Color InnerColor { get; set; } = Color.Blue;

			/// <summary>
			/// 枠の色の取得と設定
			/// </summary>
			public Color LineColor { get; set; } = Color.White;

			/// <summary>
			/// 文字色の取得と設定
			/// </summary>
			public Color ForeColor { get; set; } = Color.Black;

			/// <summary>
			/// 枠の線の太さの取得と設定
			/// </summary>
			public float LineWidth { get; set; } = 1.0F;

			/// <summary>
			/// 割合の取得と設定
			/// </summary>
			/// <remarks>
			/// 自動計算されるのでユーザは書き込まないこと。
			/// （書き込んでも意味がない）
			/// </remarks>
			public double Ratio { get; set; }

			#endregion properties

			#region construct

			/// <summary>
			/// 構築
			/// </summary>
			/// <param name="name"></param>
			public RatioItem(string name)
			{
				this.Name = name;
			}

			/// <summary>
			/// 構築
			/// </summary>
			/// <param name="name"></param>
			/// <param name="value"></param>
			/// <param name="innerColor"></param>
			public RatioItem(string name, double value, Color innerColor)
				: this(name)
			{
				this.Value = value;
				this.InnerColor = innerColor;
			}

			#endregion construct
		}

		#endregion public class

		#region events

		/// <summary>
		///
		/// </summary>
		public delegate void RatioUpdateDelegate();

		/// <summary>
		///
		/// </summary>
		public RatioUpdateDelegate OnRatioUpdate;

		#endregion events

		#region fields

		private List<IRatioItem> items = new List<IRatioItem>();
		private Bitmap surface;

		#endregion fields

		#region property

		/// <summary>
		/// 要素の個数を取得する
		/// </summary>
		[Browsable(false)]
		public int Count {
			get
			{
				return this.items.Count;
			}
		}

		/// <summary>
		/// 最大値の設定
		/// </summary>
		[Category("設定")]
		[DisplayName("最大値使用")]
		[Description("最大値の設定を使うかどうか")]
		public bool UseMax { get; set; }

		/// <summary>
		/// 最大値の設定を有効にするかどうか。
		/// 無効の場合は要素のValueの総和が１００％となる。
		/// </summary>
		[Category("設定")]
		[DisplayName("最大値")]
		[Description("最大値")]
		public double MaxValue { get; set; }

		/// <summary>
		/// 要素名を表示するかどうか
		/// </summary>
		[Category("設定")]
		[DisplayName("項目名表示")]
		[Description("項目名を表示するかどうか")]
		public bool ShowName { get; set; } = true;

		/// <summary>
		/// 塗りつぶす方向の取得と設定
		/// </summary>
		[Category("設定")]
		[DisplayName("バーの方向")]
		[Description("バーの方向")]
		public BarDirection Direction { get; set; } = BarDirection.Horisonal;

		/// <summary>
		/// 表示フォントの取得と設定
		/// </summary>
		[Category("設定")]
		[DisplayName("フォント")]
		[Description("項目名表示用フォント")]
		public override Font Font { get; set; } = new Font("ＭＳ ゴシック", 9);

		#endregion property

		#region construct

		/// <summary>
		/// 構築
		/// </summary>
		public MilkyRatioBar()
		{
			this.Paint += new PaintEventHandler(MilkyRatioBar_Paint);
			this.Resize += new EventHandler(MilkyRatioBar_Resize);
		}

		#endregion construct

		#region private API

		/// <summary>
		/// 割合の再計算
		/// </summary>
		private void CalcRatio()
		{
			double sum = 0.0;
			bool changed = false;
			items.ForEach(delegate (IRatioItem item) { sum += item.Value; });
			if (UseMax)
			{
				if (sum < MaxValue)
				{
					sum = MaxValue;
				}
			}
			items.ForEach(delegate (IRatioItem item)
			{
				double before = item.Ratio;
				item.Ratio = item.Value / sum;
				if (before != item.Ratio)
					changed = true;
			});

			if (changed)
			{
				this.OnRatioUpdate?.Invoke();
			}
		}

		/// <summary>
		/// 再描画
		/// </summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント情報</param>
		private void MilkyRatioBar_Paint(object sender, PaintEventArgs e)
		{
			if (this.Width <= 0)
				return;
			if (this.Height <= 0)
				return;
			if (this.surface == null)
			{
				this.surface = new Bitmap(this.Width, this.Height);
			}
			CalcRatio();

			using (Graphics g = Graphics.FromImage(this.surface))
			{
				g.FillRectangle(new SolidBrush(this.BackColor), this.Bounds);

				int renderd = 0;
				foreach (var i in items.Select((item, index) => new { item, index }))
				{
					int w = (this.Direction == BarDirection.Horisonal) ? (int)(this.Width * i.item.Ratio) : this.Width;
					int h = (this.Direction == BarDirection.Vertical) ? (int)(this.Height * i.item.Ratio) : this.Height;
					if ((i.index == (items.Count - 1)) && (UseMax == false))
					{
						//最後は割合計算したままだと少し残るかもしれないので端っこまで。
						w = (this.Direction == BarDirection.Horisonal) ? this.Width - renderd : this.Width;
						h = (this.Direction == BarDirection.Vertical) ? this.Height - renderd : this.Height;
					}

					Rectangle rect;
					if (this.Direction == BarDirection.Horisonal)
					{
						rect = new Rectangle(renderd, 0, w, h);
					}
					else
					{
						rect = new Rectangle(0, renderd, w, h);
					}

					g.FillRectangle(new SolidBrush(i.item.InnerColor), rect);
					if (!i.item.LineWidth.Equals(0))
					{
						g.DrawRectangle(new Pen(i.item.LineColor, i.item.LineWidth), rect);
					}
					if (ShowName)
					{
						SizeF textSize = i.item.Name.Measure(this.Font);
						PointF center = new PointF(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
						PointF drawingPoint = new PointF(center.X - textSize.Width / 2, center.Y - textSize.Height / 2);

						g.DrawString(i.item.Name, this.Font, new SolidBrush(i.item.ForeColor), drawingPoint);
					}
					if (this.Direction == BarDirection.Horisonal)
						renderd += w;
					else
						renderd += h;
				}
			}
			if (this.surface == null)
				return;
			e.Graphics.DrawImage(this.surface, 0, 0);
		}

		/// <summary>
		/// サイズ変更
		/// </summary>
		/// <param name="sender">イベント送信元</param>
		/// <param name="e">イベント情報</param>
		private void MilkyRatioBar_Resize(object sender, EventArgs e)
		{
			if (this.surface != null)
			{
				this.surface.Dispose();
				this.surface = null;
			}
			this.surface = new Bitmap(this.Width, this.Height);
		}

		#endregion private API

		#region API

		/// <summary>
		/// 要素の追加
		/// </summary>
		/// <param name="item"></param>
		public void AddItem(IRatioItem item)
		{
			this.items.Add(item);
			CalcRatio();
		}

		/// <summary>
		/// 要素の削除
		/// </summary>
		/// <param name="item"></param>
		public void RemoveItem(IRatioItem item)
		{
			this.items.Remove(item);
			CalcRatio();
		}

		/// <summary>
		/// 要素全てを削除
		/// </summary>
		public void Clear()
		{
			this.items.Clear();
			CalcRatio();
		}

		/// <summary>
		/// 昇順でソート
		/// </summary>
		public void Sort()
		{
			this.items.Sort(delegate (IRatioItem lhs, IRatioItem rhs)
			{
				if (lhs.Value < rhs.Value)
					return -1;
				else if (lhs.Value > rhs.Value)
					return 1;
				else
					return 0;
			});
			CalcRatio();
		}

		/// <summary>
		/// ソートアルゴリズムを指定してソート
		/// </summary>
		/// <param name="comparison"></param>
		public void Sort(Comparison<IRatioItem> comparison)
		{
			this.items.Sort(comparison);
			CalcRatio();
		}

		#endregion API
	}
}