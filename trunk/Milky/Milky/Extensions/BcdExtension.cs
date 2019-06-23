namespace Milky.Extensions
{
	static public class BcdExtension
	{
		/// <summary>
		/// バイナリへ変換する
		/// </summary>
		/// <param name="bcd">BCDデータ</param>
		/// <returns>バイナリデータ</returns>
		static public byte ToBin(this byte bcd)
		{
			return (byte)(((bcd / 10) << 4) | (bcd % 10));
		}

		/// <summary>
		/// バイナリへ変換する
		/// </summary>
		/// <param name="bcd">BCDデータ</param>
		/// <returns>バイナリデータ</returns>
		static public short ToBin(this short bcd)
		{
			short shift = 0xFF;
			short result = 0;
			for (int count = 0; count < 4; count++)
			{
				var value = ToBin((byte)((bcd & shift << (8 * count)) >> (8 * count)));
				result *= 100;
				result += value;
			}
			return result;
		}

		/// <summary>
		/// バイナリへ変換する
		/// </summary>
		/// <param name="bcd">BCDデータ</param>
		/// <returns>バイナリデータ</returns>
		static public int ToBin(this int bcd)
		{
			int shift = 0xFF;
			int result = 0;
			for (int count = 0; count < 4; count++)
			{
				var value = ToBin((byte)((bcd & shift << (8 * count)) >> (8 * count)));
				result *= 100;
				result += value;
			}
			return result;
		}

		/// <summary>
		/// バイナリへ変換する
		/// </summary>
		/// <param name="bcd">BCDデータ</param>
		/// <returns>バイナリデータ</returns>
		static public long ToBin(this long bcd)
		{
			long shift = 0xFF;
			long result = 0;
			for (int count = 0; count < 8; count++)
			{
				var value = ToBin((byte)((bcd & shift << (8 * count)) >> (8 * count)));
				result *= 100;
				result += value;
			}
			return result;
		}

		/// <summary>
		/// バイナリへ変換する
		/// </summary>
		/// <param name="bcd">BCDデータ</param>
		/// <returns>バイナリデータ</returns>
		static public ushort ToBin(this ushort bcd)
		{
			ushort shift = 0xFF;
			ushort result = 0;
			for (int count = 0; count < 4; count++)
			{
				var value = ToBin((byte)((bcd & shift << (8 * count)) >> (8 * count)));
				result *= 100;
				result += value;
			}
			return result;
		}

		/// <summary>
		/// バイナリへ変換する
		/// </summary>
		/// <param name="bcd">BCDデータ</param>
		/// <returns>バイナリデータ</returns>
		static public uint ToBin(this uint bcd)
		{
			uint shift = 0xFF;
			uint result = 0;
			for (int count = 0; count < 4; count++)
			{
				var value = ToBin((byte)((bcd & shift << (8 * count)) >> (8 * count)));
				result *= 100;
				result += value;
			}
			return result;
		}

		/// <summary>
		/// バイナリへ変換する
		/// </summary>
		/// <param name="bcd">BCDデータ</param>
		/// <returns>バイナリデータ</returns>
		static public ulong ToBin(this ulong bcd)
		{
			ulong shift = 0xFF;
			ulong result = 0;
			for (int count = 0; count < 8; count++)
			{
				var value = ToBin((byte)((bcd & shift << (8 * count)) >> (8 * count)));
				result *= 100;
				result += value;
			}
			return result;
		}

		/// <summary>
		/// BCD変換
		/// </summary>
		/// <param name="data">変換前</param>
		/// <returns>BCD</returns>
		static public byte ToBcd(this byte data)
		{
			byte n = data;
			byte bcd = 0;
			byte shiftNum = 0;

			while (n > 0)
			{
				bcd |= (byte)((n % 10) << shiftNum);
				shiftNum += 4;
				n /= 10;
			}

			return bcd;
		}

		/// <summary>
		/// BCD変換
		/// </summary>
		/// <param name="data">変換前</param>
		/// <returns>BCD</returns>
		static public int ToBcd(this int data)
		{
			int n = data;
			int bcd = 0;
			int shiftNum = 0;

			while (n > 0)
			{
				bcd |= (int)((n % 10) << shiftNum);
				shiftNum += 4;
				n /= 10;
			}

			return bcd;
		}

		/// <summary>
		/// BCD変換
		/// </summary>
		/// <param name="data">変換前</param>
		/// <returns>BCD</returns>
		static public short ToBcd(this short data)
		{
			short n = data;
			short bcd = 0;
			int shiftNum = 0;

			while (n > 0)
			{
				bcd |= (short)((n % 10) << shiftNum);
				shiftNum += 4;
				n /= 10;
			}

			return bcd;
		}

		/// <summary>
		/// BCD変換
		/// </summary>
		/// <param name="data">変換前</param>
		/// <returns>BCD</returns>
		static public long ToBcd(this long data)
		{
			long n = data;
			long bcd = 0;
			int shiftNum = 0;

			while (n > 0)
			{
				bcd |= (long)((n % 10) << shiftNum);
				shiftNum += 4;
				n /= 10;
			}

			return bcd;
		}

		/// <summary>
		/// BCD変換
		/// </summary>
		/// <param name="data">変換前</param>
		/// <returns>BCD</returns>
		static public uint ToBcd(this uint data)
		{
			uint n = data;
			uint bcd = 0;
			int shiftNum = 0;

			while (n > 0)
			{
				bcd |= (uint)((n % 10) << shiftNum);
				shiftNum += 4;
				n /= 10;
			}

			return bcd;
		}

		/// <summary>
		/// BCD変換
		/// </summary>
		/// <param name="data">変換前</param>
		/// <returns>BCD</returns>
		static public ushort ToBcd(this ushort data)
		{
			ushort n = data;
			ushort bcd = 0;
			int shiftNum = 0;

			while (n > 0)
			{
				bcd |= (ushort)((n % 10) << shiftNum);
				shiftNum += 4;
				n /= 10;
			}

			return bcd;
		}

		/// <summary>
		/// BCD変換
		/// </summary>
		/// <param name="data">変換前</param>
		/// <returns>BCD</returns>
		static public ulong ToBcd(this ulong data)
		{
			ulong n = data;
			ulong bcd = 0;
			int shiftNum = 0;

			while (n > 0)
			{
				bcd |= (ulong)((n % 10) << shiftNum);
				shiftNum += 4;
				n /= 10;
			}

			return bcd;
		}
	}
}