using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;
using Milky.Drawing;
using Milky.Extensions;

namespace Milky.Windows.Forms.Controls
{
	public class MilkyLabel : Label, INotifyPropertyChanged
	{
		#region public enums

		/// <summary>
		/// 影の方向
		/// </summary>
		public enum ShadowDirection
		{
			/// <summary>左上</summary>
			TopLeft,

			/// <summary>右上</summary>
			TopRight,

			/// <summary>左下</summary>
			BottomLeft,

			/// <summary>右下</summary>
			BottomRight
		}

		#endregion public enums

		#region fields

		private Bitmap surface;
		private Color shadowColor = Color.Gray;
		private int shadowSize = 2;
		private ShadowDirection shadowDir = ShadowDirection.TopRight;
		private bool useTranspanret = false;
		private Color orgBackColor = SystemColors.Control;

		#endregion fields

		#region properties

		/// <summary>
		/// 透過色を使うかどうか
		/// </summary>
		[Category("設定")]
		[DisplayName("透過色使用")]
		[Description("透過色を使うかどうか")]
		public bool UseTransparent {
			get
			{
				return this.useTranspanret;
			}
			set
			{
				this.useTranspanret = value;
				this.SetStyle(ControlStyles.SupportsTransparentBackColor, value);
				if (value)
				{
					//					this.orgBackColor = this.BackColor;
					this.BackColor = Color.Transparent;
				}
				//				else {
				//					this.BackColor = this.orgBackColor;
				//				}
			}
		}

		/// <summary>
		/// 影の方向
		/// </summary>
		[Category("設定")]
		[DisplayName("影の方向")]
		[Description("影の方向を決める")]
		public ShadowDirection ShadowDir {
			get
			{
				return this.shadowDir;
			}
			set
			{
				this.shadowDir = value;
				this.Invalidate();
			}
		}

		/// <summary>
		/// シャドウサイズ
		/// </summary>
		[Category("設定")]
		[DisplayName("影の大きさ")]
		[Description("影の大きさを決める")]
		public int ShadowSize {
			get
			{
				return this.shadowSize;
			}
			set
			{
				this.shadowSize = value;
				this.Invalidate();
			}
		}

		/// <summary>
		/// シャドウの色
		/// </summary>
		[Category("設定")]
		[DisplayName("影の色")]
		[Description("影の色")]
		public Color ShadowColor {
			get
			{
				return this.shadowColor;
			}
			set
			{
				this.shadowColor = value;
				this.Invalidate();
			}
		}

		/// <summary>
		/// 親となるコントロール
		/// </summary>
		[Category("設定")]
		[DisplayName("親コントロール")]
		[Description("透過色を使う場合、親の色が反映されるので指定すること。")]
		public Control ParentContainer {
			get
			{
				return this.Parent;
			}
			set
			{
				this.Parent = value;
			}
		}

		/// <summary>
		/// 表示テキスト
		/// </summary>
		public new string Text {
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;

				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Text"));
			}
		}

		#endregion properties

		#region events

		/// <summary>
		/// プロパティ変更通知用イベント
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion events

		#region construct

		/// <summary>
		/// 構築
		/// </summary>
		public MilkyLabel() : base()
		{
			this.Paint += MilkyLabel_Paint;
			this.Resize += MilkyLabel_Resize;
		}

		#endregion construct

		private void MilkyLabel_Resize(object sender, EventArgs e)
		{
			CreateSurface();
		}

		private void MilkyLabel_Paint(object sender, PaintEventArgs e)
		{
			if (this.surface == null)
			{
				CreateSurface();
			}
			PointF shadowPos = new PointF(0, 0);
			PointF textPos = new PointF(0, 0);
			SizeF allSize = this.Text.Measure(this.Font);
			using (Graphics g = Graphics.FromImage(this.surface))
			{
				float height = 0;
				this.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None).ForAll((index, line) =>
				{
					SizeF textSize = line.Measure(this.Font);

					int w = this.Width;
					int h = this.Height;
					w -= (this.Padding.Left + this.Padding.Right);
					h -= (this.Padding.Top + this.Padding.Bottom);

					//普通に描く場合のテキストの位置を計算する
					switch (this.TextAlign)
					{
						case ContentAlignment.TopLeft:
						case ContentAlignment.MiddleLeft:
						case ContentAlignment.BottomLeft:
							textPos.X = 0;
							break;

						case ContentAlignment.TopCenter:
						case ContentAlignment.MiddleCenter:
						case ContentAlignment.BottomCenter:
							textPos.X = (w - textSize.Width) / 2;
							break;

						case ContentAlignment.TopRight:
						case ContentAlignment.MiddleRight:
						case ContentAlignment.BottomRight:
							textPos.X = w - textSize.Width;
							break;
					}
					switch (this.TextAlign)
					{
						case ContentAlignment.TopLeft:
						case ContentAlignment.TopCenter:
						case ContentAlignment.TopRight:
							textPos.Y = height;
							break;

						case ContentAlignment.MiddleLeft:
						case ContentAlignment.MiddleCenter:
						case ContentAlignment.MiddleRight:
							textPos.Y = (h - allSize.Height) / 2 + height;
							break;

						case ContentAlignment.BottomLeft:
						case ContentAlignment.BottomCenter:
						case ContentAlignment.BottomRight:
							textPos.Y = h - allSize.Height + height;
							break;
					}

					//影の位置を計算する
					switch (this.shadowDir)
					{
						case ShadowDirection.TopLeft:
							shadowPos.X = textPos.X - shadowSize;
							shadowPos.Y = textPos.Y - shadowSize;
							break;

						case ShadowDirection.TopRight:
							shadowPos.X = textPos.X + shadowSize;
							shadowPos.Y = textPos.Y - shadowSize;
							break;

						case ShadowDirection.BottomLeft:
							shadowPos.X = textPos.X - shadowSize;
							shadowPos.Y = textPos.Y + shadowSize;
							break;

						case ShadowDirection.BottomRight:
							shadowPos.X = textPos.X + shadowSize;
							shadowPos.Y = textPos.Y + shadowSize;
							break;
					}

					textPos.X += this.Padding.Left;
					textPos.Y += this.Padding.Top;
					shadowPos.X += this.Padding.Left;
					shadowPos.Y += this.Padding.Top;

					height += textSize.Height;

					if (index == 0)
					{
						g.FillRectangle(new SolidBrush(this.BackColor), 0, 0, this.Width, this.Height);
					}
					if (shadowSize > 0)
					{
						g.SmoothingMode = SmoothingMode.HighQuality;
						g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
						g.DrawString(line, this.Font, new SolidBrush(this.shadowColor), shadowPos);
					}

					if (shadowSize > 0)
					{
						g.SmoothingMode = SmoothingMode.HighQuality;
						g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
					}
					g.DrawString(line, this.Font, new SolidBrush(this.ForeColor), textPos);
				});
			}
			if (shadowSize > 0)
			{
				if (!ImgUtils.GaussianBlur(this.surface, this.shadowSize)) throw new Exception();
			}
			using (Graphics g = Graphics.FromImage(this.surface))
			{
				float height = 0;
				this.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None).ForAll((index, line) =>
				{
					SizeF textSize = line.Measure(this.Font);

					int w = this.Width;
					int h = this.Height;
					w -= (this.Padding.Left + this.Padding.Right);
					h -= (this.Padding.Top + this.Padding.Bottom);

					//普通に描く場合のテキストの位置を計算する
					switch (this.TextAlign)
					{
						case ContentAlignment.TopLeft:
						case ContentAlignment.MiddleLeft:
						case ContentAlignment.BottomLeft:
							textPos.X = 0;
							break;

						case ContentAlignment.TopCenter:
						case ContentAlignment.MiddleCenter:
						case ContentAlignment.BottomCenter:
							textPos.X = (w - textSize.Width) / 2;
							break;

						case ContentAlignment.TopRight:
						case ContentAlignment.MiddleRight:
						case ContentAlignment.BottomRight:
							textPos.X = w - textSize.Width;
							break;
					}
					switch (this.TextAlign)
					{
						case ContentAlignment.TopLeft:
						case ContentAlignment.TopCenter:
						case ContentAlignment.TopRight:
							textPos.Y = height;
							break;

						case ContentAlignment.MiddleLeft:
						case ContentAlignment.MiddleCenter:
						case ContentAlignment.MiddleRight:
							textPos.Y = (h - allSize.Height) / 2 + height;
							break;

						case ContentAlignment.BottomLeft:
						case ContentAlignment.BottomCenter:
						case ContentAlignment.BottomRight:
							textPos.Y = h - allSize.Height + height;
							break;
					}

					textPos.X += this.Padding.Left;
					textPos.Y += this.Padding.Top;

					height += textSize.Height;

					g.SmoothingMode = SmoothingMode.HighQuality;
					g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
					g.DrawString(line, this.Font, new SolidBrush(this.ForeColor), textPos);
				});
			}

			e.Graphics.DrawImage(this.surface, 0, 0);
		}

		#region bind

		private readonly ControlBinder binder = new ControlBinder();

		/// <summary>
		/// bind to property
		/// </summary>
		/// <param name="propertyName">bind property name</param>
		/// <param name="attachObject">bind object(from)</param>
		/// <param name="attachPropertyName">bind property name(from)</param>
		public void Bind(string propertyName, object attachObject, string attachPropertyName, Func<object, object> callback = null)
			=> this.binder.Bind(this, propertyName, attachObject, attachPropertyName, callback);

		/// <summary>
		/// un-bind property
		/// </summary>
		/// <param name="propertyName">bind property name</param>
		/// <param name="attachObject">bind object(from)</param>
		/// <param name="attachPropertyName">bind property name(from)</param>
		public void UnBind(string propertyName, object attachObject, string attachPropertyName, Func<object, object> callback = null)
			=> this.binder.UnBind(propertyName, attachObject, attachPropertyName, callback);

		#endregion bind

		#region private API

		private void CreateSurface()
		{
			if (surface != null)
			{
				surface.Dispose();
				surface = null;
			}
			int w = Math.Max(this.Width, 1);
			int h = Math.Max(this.Height, 1);
			surface = new Bitmap(w, h, PixelFormat.Format24bppRgb);
		}

		#endregion private API
	}
}