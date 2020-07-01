using System.Drawing;
using Milky.Algorithm;

namespace Milky.Windows.Forms
{
	public class ColorPair : Pair<Color, Color>
	{
		#region プロパティ

		/// <summary>
		/// 前面色の設定と取得
		/// </summary>
		public Color Fore {
			set
			{
				this.Item1 = value;
			}
			get
			{
				return this.Item1;
			}
		}

		/// <summary>
		/// 背景色の設定と取得
		/// </summary>
		public Color Back {
			set
			{
				this.Item2 = value;
			}
			get
			{
				return this.Item2;
			}
		}

		#endregion プロパティ

		#region 構築

		/// <summary>
		/// 構築
		/// </summary>
		/// <param name="fore">全面色</param>
		/// <param name="back">背景色</param>
		public ColorPair(Color fore, Color back)
			: base(fore, back)
		{
		}

		#endregion 構築
	}
}