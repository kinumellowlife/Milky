using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Milky.Extensions
{
	/// <summary>
	/// 画像関連の拡張メソッド
	/// </summary>
	public static class ImageExtension
	{
		/// <summary>
		/// １ピクセルを何バイトで表現するか
		/// </summary>
		/// <param name="image">画像情報</param>
		/// <returns>バイト長</returns>
		public static int PixelSize(this Bitmap image)
		{
			return Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;
		}

		/// <summary>
		/// 指定画像に対し、ビット情報としてのインデックス番号からピクセル位置に変換する。
		/// GetBytes()で取得したバイト情報を捜査する際に座標に戻す場面を想定
		/// </summary>
		/// <param name="image">基準画像</param>
		/// <param name="index">ビット位置</param>
		/// <returns>ピクセル座標</returns>
		public static Point ToPixelXY(this Bitmap image, int index)
		{
			return new Point(
				(index / image.PixelSize()) % image.Width,
				(index / image.PixelSize()) / image.Width
				);
		}

		/// <summary>
		/// 指定画像のビット情報をバイト配列として取得
		/// </summary>
		/// <param name="image">元画像</param>
		/// <param name="rect">取得したい範囲</param>
		/// <returns>ビット情報</returns>
		public static byte[] GetBytes(this Bitmap image, Rectangle rect)
		{
			//ロック
			BitmapData bmpData = image.LockBits(rect, ImageLockMode.ReadWrite, image.PixelFormat);

			//ビットマップの先頭アドレス
			IntPtr ptr = bmpData.Scan0;

			//必要なバイト数
			int length = image.Width * image.Height * image.PixelSize();

			//格納先
			byte[] data = new byte[length];

			//コピー
			Marshal.Copy(ptr, data, 0, length);

			//ロック解除
			image.UnlockBits(bmpData);

			return data;
		}

		/// <summary>
		/// 指定画像のビット情報をバイト配列として取得
		/// </summary>
		/// <param name="image">取得する画像</param>
		/// <returns>ビット情報</returns>
		public static byte[] GetBytes(this Bitmap image)
		{
			Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

			return image.GetBytes(rect);
		}
	}
}