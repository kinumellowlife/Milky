using Milky.Extensions;

namespace Milky.Mathmatics
{
	public class Bcd
	{
		/// <summary>
		/// BCDへからバイナリへ変換する
		/// </summary>
		/// <param name="data">変換するデータ</param>
		static public void ToBin(byte[] data)
		{
			for (int index = 0; index < data.Length; index++)
			{
				data[index] = data[index].ToBin();
			}
		}

		/// <summary>
		/// BCDへ変換する
		/// </summary>
		/// <param name="data">変換するデータ</param>
		static public void ToBcd(byte[] data)
		{
			for (int index = 0; index < data.Length; index++)
			{
				data[index] = data[index].ToBcd();
			}
		}
	}
}