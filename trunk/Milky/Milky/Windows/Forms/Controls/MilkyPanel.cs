using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Milky.Algorithm;

namespace Milky.Windows.Forms.Controls
{
	public class MilkyPanel : Panel, IControlBinder
	{
		#region fields

		private Bitmap surface;
		private Pair<bool, Color, float, Pen> frame = new Pair<bool, Color, float, Pen>(true, Color.White, 1.0F, null);

		/// <summary>
		/// 枠の色と塗りつぶす色
		/// </summary>
		private Pair<Color, Color, float, float> fills = new Pair<Color, Color, float, float>(Color.Black, Color.White, 1.0F, 1.0F);

		/// <summary>
		/// ハッチスタイルを使うかどうかとハッチスタイル
		/// </summary>
		private Pair<bool, HatchStyle, HatchBrush> hatch = new Pair<bool, HatchStyle, HatchBrush>(false, HatchStyle.Horizontal, null);

		private Pair<bool, Image, string, Bitmap> texture = new Pair<bool, Image, string, Bitmap>(false, null, null, null);
		private Pair<bool, bool, LinearGradientBrush, float, LinearGradientMode> gradient = new Pair<bool, bool, LinearGradientBrush, float, LinearGradientMode>(true, false, null, 0F, LinearGradientMode.Horizontal);

		#endregion fields

		#region property

		/// <summary>
		/// 色１用透過率の設定と取得
		/// </summary>
		[Category("設定")]
		[DisplayName("色１透過率")]
		[Description("透過率")]
		public float AlphaInnerColor1 {
			get
			{
				return fills.Item3;
			}
			set
			{
				if ((0.0F <= value) && (value <= 1.0F))
				{
					this.fills.Item3 = value;
					this.fills.Item1 = Color.FromArgb((int)(fills.Item3 * 255), fills.Item1.R, fills.Item1.G, fills.Item1.B);
				}
				CreateFramePen();
				Invalidate();
			}
		}

		/// <summary>
		/// 色２用透過率の設定と取得
		/// </summary>
		[Category("設定")]
		[DisplayName("色２透過率")]
		[Description("透過率")]
		public float AlphaInnerColor2 {
			get
			{
				return fills.Item4;
			}
			set
			{
				if ((0.0F <= value) && (value <= 1.0F))
				{
					this.fills.Item4 = value;
					this.fills.Item2 = Color.FromArgb((int)(fills.Item4 * 255), fills.Item2.R, fills.Item2.G, fills.Item2.B);
				}
				CreateFramePen();
				Invalidate();
			}
		}

		/// <summary>
		/// 枠の表示をするかどうか
		/// </summary>
		[Category("設定")]
		[DisplayName("フレーム表示")]
		[Description("フレーム表示するかどうか")]
		public bool UseFrame {
			get
			{
				return this.frame.Item1;
			}
			set
			{
				this.frame.Item1 = value;
				CreateFramePen();
				Invalidate();
			}
		}

		/// <summary>
		/// 線の幅の取得と設定
		/// </summary>
		[Category("設定")]
		[DisplayName("フレームの太さ")]
		[Description("フレームの太さ")]
		public float LineWidth {
			get
			{
				return this.frame.Item3;
			}
			set
			{
				if (!this.frame.Item3.Equals(value))
				{
					if (value > 0.0F)
					{
						this.frame.Item3 = value;
					}
					CreateFramePen();
					Invalidate();
				}
			}
		}

		/// <summary>
		/// 線の色の取得と設定
		/// </summary>
		[Category("設定")]
		[DisplayName("フレーム色")]
		[Description("フレームの色")]
		public Color LineColor {
			get
			{
				return this.frame.Item2;
			}
			set
			{
				this.frame.Item2 = value;
				CreateFramePen();
				Invalidate();
			}
		}

		/// <summary>
		/// 塗りつぶし色１の取得と設定
		/// </summary>
		[Category("設定")]
		[DisplayName("色１")]
		[Description("色１")]
		public Color InnerColor1 {
			get
			{
				//				return this.fills.Item1;
				return Color.FromArgb(255, fills.Item1);
			}
			set
			{
				//this.fills.Item1 = value;
				this.fills.Item1 = Color.FromArgb((int)(fills.Item3 * 255), value);
				CreateGradientBrush();
				Invalidate();
			}
		}

		/// <summary>
		/// 塗りつぶし色２の取得と設定
		/// </summary>
		[Category("設定")]
		[DisplayName("色２")]
		[Description("色２")]
		public Color InnerColor2 {
			get
			{
				//				return this.fills.Item2;
				return Color.FromArgb(255, fills.Item2);
			}
			set
			{
				//this.fills.Item2 = value;
				this.fills.Item2 = Color.FromArgb((int)(fills.Item4 * 255), value);
				CreateGradientBrush();
				Invalidate();
			}
		}

		/// <summary>
		/// ハッチ表示するかどうか
		/// </summary>
		[Category("設定")]
		[DisplayName("ハッチ表示")]
		[Description("ハッチ表示するかどうか")]
		public bool UseHatchStyle {
			get
			{
				return this.hatch.Item1;
			}
			set
			{
				this.hatch.Item1 = value;
				CreateHatchBrush();
				Invalidate();
			}
		}

		/// <summary>
		/// ハッチスタイルの取得と設定
		/// </summary>
		[Category("設定")]
		[DisplayName("ハッチスタイル")]
		[Description("ハッチスタイル")]
		public HatchStyle HatchStyle {
			get
			{
				return this.hatch.Item2;
			}
			set
			{
				this.hatch.Item2 = value;
				CreateHatchBrush();
				Invalidate();
			}
		}

		/// <summary>
		/// テクスチャで敷き詰めるかどうか
		/// </summary>
		[Category("設定")]
		[DisplayName("テクスチャ使用")]
		[Description("テクスチャを使うかどうか")]
		public bool UseTexture {
			get
			{
				return this.texture.Item1;
			}
			set
			{
				this.texture.Item1 = value;
				CreateTextureBrush();
				Invalidate();
			}
		}

		/// <summary>
		/// 敷き詰める画像の取得と設定
		/// </summary>
		[Category("設定")]
		[DisplayName("テクスチャ")]
		[Description("敷き詰めるテクスチャ")]
		public override Image BackgroundImage {
			get
			{
				return this.texture.Item2;
			}
			set
			{
				this.texture.Item2 = value;
				CreateTextureBrush();
				Invalidate();
			}
		}

		/// <summary>
		/// ガンマ補正を使うかどうか
		/// </summary>
		[Category("設定")]
		[DisplayName("ガンマ補正")]
		[Description("ガンマ補正するかどうか")]
		public bool UseGammaCorrection {
			get
			{
				return this.gradient.Item2;
			}
			set
			{
				if (this.gradient.Item2 != value)
				{
					this.gradient.Item2 = value;
					CreateGradientBrush();
					Invalidate();
				}
			}
		}

		/// <summary>
		/// グラデーション開始アングルの取得と設定
		/// </summary>
		[Category("設定")]
		[DisplayName("グラデーション開始アングル")]
		[Description("グラデーション開始アングル")]
		public float GradientAngle {
			get
			{
				return this.gradient.Item4;
			}
			set
			{
				this.gradient.Item4 = value;
				CreateGradientBrush();
				Invalidate();
			}
		}

		/// <summary>
		/// グラデーションモードの取得と設定
		/// </summary>
		[Category("設定")]
		[DisplayName("グラデーションモード")]
		[Description("グラデーションモード")]
		public LinearGradientMode GradientMode {
			get
			{
				return this.gradient.Item5;
			}
			set
			{
				this.gradient.Item5 = value;
				CreateGradientBrush();
				Invalidate();
			}
		}

		#endregion property

		#region construct

		public MilkyPanel() : base()
		{
			CreateSurface();
			CreateGradientBrush();
			CreateTextureBrush();
			CreateFramePen();

			this.Resize += (sender, e) =>
			{
				CreateSurface();
				CreateGradientBrush();
				CreateTextureBrush();
				CreateFramePen();
				Invalidate();
			};

			this.Paint += MilkyPanel_Paint;
		}

		#endregion construct

		private void MilkyPanel_Paint(object sender, PaintEventArgs e)
		{
			if (this.surface != null)
			{
				using (Graphics g = Graphics.FromImage(this.surface))
				{
					if (this.texture.Item1)
					{
						g.DrawImage(this.texture.Item4, 0, 0);
					}
					else if (this.hatch.Item1)
					{
						g.FillRectangle(this.hatch.Item3, 0, 0, this.Width, this.Height);
					}
					else
					{
						g.FillRectangle(this.gradient.Item3, 0, 0, this.Width, this.Height);
					}
					if (this.frame.Item1)
					{
						g.DrawRectangle(this.frame.Item4, 0, 0, this.Width - this.frame.Item3, this.Height - this.frame.Item3);
					}
				}
				e.Graphics.DrawImage(this.surface, 0, 0);
			}
		}

		private void CreateTextureBrush()
		{
			if ((this.Width > 0) && (this.Height > 0))
			{
				if (this.texture.Item4 != null)
				{
					this.texture.Item4.Dispose();
					this.texture.Item4 = null;
				}
				if (this.texture.Item4 == null)
				{
					this.texture.Item4 = new Bitmap(this.Width, this.Height);
					using (Graphics g = Graphics.FromImage(this.texture.Item4))
					{
						g.FillRectangle(new SolidBrush(fills.Item1), 0, 0, this.Width, this.Height);
					}
				}

				//外部ファイル指定
				if (!string.IsNullOrEmpty(this.texture.Item3))
				{
					Image temp = Image.FromFile(this.texture.Item3);
					using (Graphics g = Graphics.FromImage(this.texture.Item4))
					{
						TextureBrush tb = new TextureBrush(temp);
						g.FillRectangle(tb, g.VisibleClipBounds);
					}
				}
				//イメージ直指定
				else if (this.texture.Item2 != null)
				{
					using (Graphics g = Graphics.FromImage(this.texture.Item4))
					{
						TextureBrush tb = new TextureBrush(this.texture.Item2);
						g.FillRectangle(tb, g.VisibleClipBounds);
					}
				}
			}
		}

		private void CreateSurface()
		{
			if ((this.Width > 0) && (this.Height > 0))
			{
				this.surface = new Bitmap(this.Width, this.Height);
				using (Graphics g = Graphics.FromImage(surface))
				{
					g.FillRectangle(new SolidBrush(this.fills.Item1), 0, 0, this.surface.Width, this.surface.Height);
				}
			}
		}

		private void CreateHatchBrush()
		{
			this.hatch.Item3 = new HatchBrush(this.hatch.Item2, fills.Item1, fills.Item2);
		}

		private void CreateGradientBrush()
		{
			if ((this.Width > 0) && (this.Height > 0))
			{
				if (this.gradient.Item4 == 0F)
				{
					this.gradient.Item3 = new LinearGradientBrush(new Rectangle(0, 0, this.Width, this.Height), this.fills.Item1, this.fills.Item2, this.gradient.Item5);
				}
				else
				{
					this.gradient.Item3 = new LinearGradientBrush(new Rectangle(0, 0, this.Width, this.Height), this.fills.Item1, this.fills.Item2, this.gradient.Item4);
				}
				this.gradient.Item3.GammaCorrection = this.gradient.Item2;
			}
		}

		private void CreateFramePen()
		{
			this.frame.Item4 = new Pen(this.frame.Item2, this.frame.Item3);
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
	}
}