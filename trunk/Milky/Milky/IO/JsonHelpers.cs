using System.ComponentModel;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Milky.IO
{
	/// <summary>
	/// JSON読み書き用パディング設定
	/// </summary>
	[DisplayName("パティング設定")]
	[Category("調整")]
	[DataContract]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class JsonPadding
	{
		#region fields

		private Padding pad = new Padding(0, 0, 0, 0);

		#endregion fields

		#region properties

		/// <summary>
		/// 上部パディング幅の取得と設定
		/// </summary>
		[DataMember(Order = 0)]
		[Browsable(false)]
		public int Top { get { return this.pad.Top; } set { this.pad.Top = value; } }

		/// <summary>
		/// 左部パディング幅の取得と設定
		/// </summary>
		[DataMember(Order = 1)]
		[Browsable(false)]
		public int Left { get { return this.pad.Left; } set { this.pad.Left = value; } }

		/// <summary>
		/// 右部パディング幅の取得と設定
		/// </summary>
		[DataMember(Order = 2)]
		[Browsable(false)]
		public int Right { get { return this.pad.Right; } set { this.pad.Right = value; } }

		/// <summary>
		/// 下部パディング幅の取得と設定
		/// </summary>
		[DataMember(Order = 3)]
		[Browsable(false)]
		public int Bottom { get { return this.pad.Bottom; } set { this.pad.Bottom = value; } }

		/// <summary>
		/// パディングの取得と設定
		/// </summary>
		[DisplayName("パディング")]
		[Category("調整")]
		public Padding Padding {
			get
			{
				return this.pad;
			}
			set
			{
				this.pad = value;
			}
		}

		#endregion properties

		#region API

		/// <summary>
		/// クラスを表現する文字列の取得
		/// </summary>
		/// <returns>クラスを表すテキスト</returns>
		public override string ToString()
		{
			return "パディング設定";
		}

		#endregion API
	}

	/// <summary>
	/// JSON読み書き用フォント設定
	/// </summary>
	[DisplayName("フォント設定")]
	[Category("表示")]
	[DataContract]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class JsonFont
	{
		#region fields

		private Font font;

		//private string fontFace = "ＭＳ 明朝";
		private string fontFace = "";

		//private float fontSize = 10.0F;
		private float fontSize = 0.0F;

		private bool italic = false;
		private bool regular = true;
		private bool bold = false;
		private bool strikeout = false;
		private bool underline = false;

		#endregion fields

		#region properties

		/// <summary>
		/// フォント名の取得と設定
		/// </summary>
		[DataMember(Order = 0)]
		[Browsable(false)]
		public string FontFace { get { return this.fontFace; } set { this.fontFace = value; CreateFont(); } }

		/// <summary>
		/// フォントサイズの取得と設定
		/// </summary>
		[DataMember(Order = 1)]
		[Browsable(false)]
		public float FontSize { get { return this.fontSize; } set { this.fontSize = value; CreateFont(); } }

		/// <summary>
		/// イタリック表示するかどうか
		/// </summary>
		[DataMember(Order = 2)]
		[Browsable(false)]
		public bool FontItalic { get { return this.italic; } set { this.italic = value; CreateFont(); } }

		/// <summary>
		/// ボールド表示するかどうか
		/// </summary>
		[DataMember(Order = 3)]
		[Browsable(false)]
		public bool FontBold { get { return this.bold; } set { this.bold = value; CreateFont(); } }

		/// <summary>
		/// レギュラー表示するかどうか
		/// </summary>
		[DataMember(Order = 4)]
		[Browsable(false)]
		public bool FontRegular { get { return this.regular; } set { this.regular = value; CreateFont(); } }

		/// <summary>
		/// アンダーラインを引くかどうか
		/// </summary>
		[DataMember(Order = 5)]
		[Browsable(false)]
		public bool FontUnderline { get { return this.underline; } set { this.underline = value; CreateFont(); } }

		/// <summary>
		/// 取消表示をするかどうか
		/// </summary>
		[DataMember(Order = 6)]
		[Browsable(false)]
		public bool FontStrikeout { get { return this.strikeout; } set { this.strikeout = value; CreateFont(); } }

		/// <summary>
		/// フォントの取得と設定
		/// </summary>
		[DisplayName("フォント")]
		[Category("表示")]
		public Font Font {
			get
			{
				return this.font;
			}
			set
			{
				this.font = value;
				this.FontFace = this.font.Name;
				this.FontSize = this.font.Size;
				this.FontItalic = (this.font.Style & FontStyle.Italic) == FontStyle.Italic;
				this.FontBold = (this.font.Style & FontStyle.Bold) == FontStyle.Bold;
				this.FontRegular = (this.font.Style & FontStyle.Regular) == FontStyle.Regular;
				this.FontUnderline = (this.font.Style & FontStyle.Underline) == FontStyle.Underline;
				this.FontStrikeout = (this.font.Style & FontStyle.Strikeout) == FontStyle.Strikeout;
			}
		}

		#endregion properties

		#region API

		private void CreateFont()
		{
			if (string.IsNullOrEmpty(this.fontFace))
				return;
			if (this.fontSize <= 0F)
				return;

			FontStyle style = FontStyle.Regular;
			if (this.FontItalic)
				style |= FontStyle.Italic;
			if (this.FontRegular)
				style |= FontStyle.Regular;
			if (this.FontUnderline)
				style |= FontStyle.Underline;
			if (this.FontBold)
				style |= FontStyle.Bold;
			if (this.FontStrikeout)
				style |= FontStyle.Strikeout;

			this.font = new Font(this.fontFace, this.fontSize, style);
		}

		/// <summary>
		/// クラスを表現する文字列の取得
		/// </summary>
		/// <returns>クラスを表すテキスト</returns>
		public override string ToString()
		{
			return "フォント設定";
		}

		#endregion API
	}

	/// <summary>
	/// JSON読み書き用テキスト表示位置設定
	/// </summary>
	[DisplayName("テキスト表示位置設定")]
	[Category("調整")]
	[DataContract]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class JsonTextAlignment
	{
		#region fields

		private string align = "middleCenter";

		#endregion fields

		#region properties

		/// <summary>
		/// テキスト表示位置を示す文字列の取得と設定
		/// </summary>
		[DataMember]
		[Browsable(false)]
		public string Align { get { return this.align; } set { this.align = value; } }

		/// <summary>
		/// テキスト表示位置の取得と設定
		/// </summary>
		[DisplayName("テキスト表示位置")]
		[Category("調整")]
		public ContentAlignment Alignment {
			get
			{
				switch (this.align.ToLower())
				{
					case "topleft": return ContentAlignment.TopLeft;
					case "topcenter": return ContentAlignment.TopCenter;
					case "topright": return ContentAlignment.TopRight;
					case "middleleft": return ContentAlignment.MiddleLeft;
					case "middlecenter": return ContentAlignment.MiddleCenter;
					case "middleright": return ContentAlignment.MiddleRight;
					case "bottomleft": return ContentAlignment.BottomLeft;
					case "bottomcenter": return ContentAlignment.BottomCenter;
					case "bottomright": return ContentAlignment.BottomRight;
				}
				return ContentAlignment.MiddleCenter;
			}
			set
			{
				this.align = value.ToString();
			}
		}

		#endregion properties

		#region API

		/// <summary>
		/// クラスを表現する文字列の取得
		/// </summary>
		/// <returns>クラスを表すテキスト</returns>
		public override string ToString()
		{
			return "テキスト表示位置設定";
		}

		#endregion API
	}

	/// <summary>
	/// JSON出力可能な色
	/// </summary>
	[DisplayName("色設定")]
	[Category("表示")]
	[DataContract]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class JsonColor
	{
		#region fields

		private int red = 0;
		private int green = 0;
		private int blue = 0;
		private int alpha = 0;

		#endregion fields

		#region properties

		/// <summary>
		/// アルファ値の取得と設定
		/// </summary>
		[DataMember(Order = 0)]
		[Browsable(false)]
		public int Alpha { get { return this.alpha; } set { this.alpha = value; } }

		/// <summary>
		/// 赤レベルの取得と設定
		/// </summary>
		[DataMember(Order = 1)]
		[Browsable(false)]
		public int Red { get { return this.red; } set { this.red = value; } }

		/// <summary>
		/// 緑レベルの取得と設定
		/// </summary>
		[DataMember(Order = 2)]
		[Browsable(false)]
		public int Green { get { return this.green; } set { this.green = value; } }

		/// <summary>
		/// 青レベルの取得と設定
		/// </summary>
		[DataMember(Order = 3)]
		[Browsable(false)]
		public int Blue { get { return this.blue; } set { this.blue = value; } }

		/// <summary>
		/// 色の取得と設定
		/// </summary>
		[DisplayName("色")]
		[Category("表示")]
		public Color Color {
			get
			{
				return Color.FromArgb(this.alpha, this.red, this.green, this.blue);
			}
			set
			{
				this.alpha = value.A;
				this.red = value.R;
				this.green = value.G;
				this.blue = value.B;
			}
		}

		#endregion properties

		#region API

		/// <summary>
		/// クラスを表現する文字列の取得
		/// </summary>
		/// <returns>クラスを表すテキスト</returns>
		public override string ToString()
		{
			return "色設定";
		}

		#endregion API
	}

	/// <summary>
	/// JSON出力可能なサイズ
	/// </summary>
	[DisplayName("サイズ設定")]
	[Category("表示")]
	[DataContract]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class JsonSize
	{
		#region fields

		private int width = 0;
		private int height = 0;

		#endregion fields

		#region properties

		/// <summary>
		/// 幅の取得と設定
		/// </summary>
		[DataMember(Order = 0)]
		[Browsable(false)]
		public int Width { get { return this.width; } set { this.width = value; } }

		/// <summary>
		/// 高さの取得と設定
		/// </summary>
		[DataMember(Order = 1)]
		[Browsable(false)]
		public int Height { get { return this.height; } set { this.height = value; } }

		/// <summary>
		/// 色の取得と設定
		/// </summary>
		[DisplayName("色")]
		[Category("表示")]
		public Size Size {
			get
			{
				return new Size(this.width, this.height);
			}
			set
			{
				this.width = value.Width;
				this.height = value.Height;
			}
		}

		#endregion properties

		#region API

		/// <summary>
		/// クラスを表現する文字列の取得
		/// </summary>
		/// <returns>クラスを表すテキスト</returns>
		public override string ToString()
		{
			return "サイズ設定";
		}

		#endregion API
	}
}