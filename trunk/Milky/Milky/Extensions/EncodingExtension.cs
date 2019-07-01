using System.Text;

namespace Milky.Extensions
{
	public static class EncodingExtension
	{
		/// <summary>
		/// 指定バイト列のエンコーディング情報を調べる
		/// </summary>
		/// <param name="byts">文字列の入ったバイト配列</param>
		/// <returns>エンコーディング情報</returns>
		public static Encoding GetEncoding(this byte[] byts)
		{
			const byte bESC = 0x1B;
			const byte bAT = 0x40;
			const byte bDollar = 0x24;
			const byte bAnd = 0x26;
			const byte bOP = 0x28;
			const byte bB = 0x42;
			const byte bD = 0x44;
			const byte bJ = 0x4A;
			const byte bI = 0x49;

			int len = byts.Length;
			int binary = 0;
			int ucs2 = 0;
			int sjis = 0;
			int euc = 0;
			int utf8 = 0;
			byte b1, b2;

			if (byts == null)
			{
				return Encoding.Default;
			}

			for (int _ucs2 = 0; _ucs2 < len; _ucs2++)
			{
				if (byts[_ucs2] <= 0x06 || byts[_ucs2] == 0x7F || byts[_ucs2] == 0xFF)
				{
					//'binary'
					binary++;
					if ((len - 1 > _ucs2) &&
						(byts[_ucs2] == 0x00) &&
						(_ucs2 > 0) &&
						(byts[_ucs2 - 1] <= 0x7F))
					{
						//smells like raw unicode
						ucs2++;
					}
				}
			}

			if (binary > 0)
			{
				if (ucs2 > 0)
					//JIS
					//ucs2(Unicode)
					return Encoding.Unicode;
				else
					//binary
					return null;
			}

			for (int _pos = 0; _pos < len - 1; _pos++)
			{
				b1 = byts[_pos];
				b2 = byts[_pos + 1];

				if (b1 == bESC)
				{
					if (b2 >= 0x80)
						//not Japanese
						//ASCII
						return Encoding.ASCII;
					else if (len - 2 > _pos &&
				b2 == bDollar && byts[_pos + 2] == bAT)
						//JIS_0208 1978
						//JIS
						return Encoding.GetEncoding(50220);
					else if (len - 2 > _pos &&
				b2 == bDollar && byts[_pos + 2] == bB)
						//JIS_0208 1983
						//JIS
						return Encoding.GetEncoding(50220);
					else if (len - 5 > _pos &&
				b2 == bAnd && byts[_pos + 2] == bAT && byts[_pos + 3] == bESC &&
				byts[_pos + 4] == bDollar && byts[_pos + 5] == bB)
						//JIS_0208 1990
						//JIS
						return Encoding.GetEncoding(50220);
					else if (len - 3 > _pos &&
				b2 == bDollar && byts[_pos + 2] == bOP && byts[_pos + 3] == bD)
						//JIS_0212
						//JIS
						return Encoding.GetEncoding(50220);
					else if (len - 2 > _pos &&
				b2 == bOP && (byts[_pos + 2] == bB || byts[_pos + 2] == bJ))
						//JIS_ASC
						//JIS
						return Encoding.GetEncoding(50220);
					else if (len - 2 > _pos &&
				b2 == bOP && byts[_pos + 2] == bI)
						//JIS_KANA
						//JIS
						return Encoding.GetEncoding(50220);
				}
			}

			for (int _sjis = 0; _sjis < len - 1; _sjis++)
			{
				b1 = byts[_sjis];
				b2 = byts[_sjis + 1];
				if (((b1 >= 0x81 && b1 <= 0x9F) || (b1 >= 0xE0 && b1 <= 0xFC)) &&
			((b2 >= 0x40 && b2 <= 0x7E) || (b2 >= 0x80 && b2 <= 0xFC)))
				{
					sjis += 2;
					_sjis++;
				}
			}
			for (int _euc = 0; _euc < len - 1; _euc++)
			{
				b1 = byts[_euc];
				b2 = byts[_euc + 1];
				if (((b1 >= 0xA1 && b1 <= 0xFE) && (b2 >= 0xA1 && b2 <= 0xFE)) ||
			(b1 == 0x8E && (b2 >= 0xA1 && b2 <= 0xDF)))
				{
					euc += 2;
					_euc++;
				}
				else if (len - 2 > _euc &&
			b1 == 0x8E && (b2 >= 0xA1 && b2 <= 0xFE) &&
			(byts[_euc + 2] >= 0xA1 && byts[_euc + 2] <= 0xFE))
				{
					euc += 3;
					_euc += 2;
				}
			}
			for (int _utf8 = 0; _utf8 < len - 1; _utf8++)
			{
				b1 = byts[_utf8];
				b2 = byts[_utf8 + 1];
				if ((b1 >= 0xC0 && b1 <= 0xDF) && (b2 >= 0x80 && b2 <= 0xBF))
				{
					utf8 += 2;
					_utf8++;
				}
				else if (len - 2 > _utf8 &&
			(b1 >= 0xE0 && b1 <= 0xEF) && (b2 >= 0x80 && b2 <= 0xBF) &&
			(byts[_utf8 + 2] >= 0x80 && byts[_utf8 + 2] <= 0xBF))
				{
					utf8 += 3;
					_utf8 += 2;
				}
			}

			if (euc > sjis && euc > utf8)
				//EUC
				return Encoding.GetEncoding(51932);
			else if (sjis > euc && sjis > utf8)
				//SJIS
				return Encoding.GetEncoding(932);
			else if (utf8 > euc && utf8 > sjis)
				//UTF8
				return Encoding.UTF8;

			return Encoding.Default;
		}
	}
}