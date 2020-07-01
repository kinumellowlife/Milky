using System.ComponentModel;
using System.Drawing;
using Milky.Algorithm;

namespace Milky.Windows.Forms
{
	public class FontPair : Pair<Font, Font>, INotifyPropertyChanged
	{
		#region プロパティ

		/// <summary>
		/// 前面のフォントの設定と取得
		/// </summary>
		public Font Fore {
			set
			{
				this.Item1 = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Fore"));
			}
			get
			{
				return this.Item1;
			}
		}

		/// <summary>
		/// 背景のフォントの設定と取得
		/// </summary>
		public Font Back {
			set
			{
				this.Item2 = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Back"));
			}
			get
			{
				return this.Item2;
			}
		}

		#endregion プロパティ

		public FontPair(Font fore, Font back) : base(fore, back)
		{
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}