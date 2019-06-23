namespace Milky.Mathmatics
{
	static public class Bcc
	{
		public const byte STX = 0x02;
		public const byte ETX = 0x03;

		/// <summary>
		/// BCC1,BCC2を計算する
		/// </summary>
		/// <param name="data">計算対象のバッファ</param>
		/// <param name="bcc1">BCC1計算結果格納先</param>
		/// <param name="bcc2">BCC2計算結果格納先</param>
		static private bool CalcBcc12(byte[] data, out byte bcc1, out byte bcc2)
		{
			bcc1 = 0x00;
			bcc2 = 0x00;

			if (data == null)
				return false;
			if (data.Length < 5)
				return false;

			//TEXT1 ～ ETXまでループ
			//先頭はSTX
			for (int at = 1; at <= data.Length - 3; at++)
			{
				if ((at % 2) != 0)
				{
					//奇数・・・ＢＣＣ１
					bcc1 = (byte)(bcc1 ^ data[at]);
				}
				else
				{
					//偶数・・・ＢＣＣ２
					bcc2 = (byte)(bcc2 ^ data[at]);
				}
			}

			return true;
		}

		/// <summary>
		/// BCCチェック
		/// </summary>
		/// <param name="data">受信データ全部（STX～BCC2）</param>
		/// <returns>BCC1,BCC2どちらも正常ならTrue</returns>
		static public bool CheckBcc12(byte[] data)
		{
			if (data.Length < 5)
				return false;

			if (CalcBcc12(data, out byte bcc1, out byte bcc2))
			{
				//BCC1チェック
				if (data[data.Length - 2] != bcc1)
				{
					return false;
				}

				//BCC2設定
				if (data[data.Length - 1] != bcc2)
				{
					return false;
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// BCC1,2とETXのチェックを行う。
		/// </summary>
		/// <param name="data">データ</param>
		/// <returns>正常ならTrue</returns>
		static public bool CheckBcc12Etx(byte[] data)
		{
			if (data.Length < 5)
			{
				return false;
			}
			if (CheckBcc12(data) && (data[data.Length - 3] == ETX))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// ETX、BCC1、BCC2を計算してセットする
		/// </summary>
		/// <param name="data">計算対象のバッファ。計算結果のセット先もここ。</param>
		static public void CalcBcc12Etx(byte[] data)
		{
			if (data.Length < 5)
				return;

			data[data.Length - 3] = ETX;

			CalcBcc12(data, out byte bcc1, out byte bcc2);

			data[data.Length - 2] = bcc1;
			data[data.Length - 1] = bcc2;
		}

		/// <summary>
		/// STXとETX,BCC1,BCC2をセットする
		/// </summary>
		/// <param name="data">計算対象のバッファ。計算結果のセット先もここ。</param>
		static public void CalcStxBcc112Etx(byte[] data)
		{
			if (data.Length < 1)
				return;

			data[0] = STX;

			CalcBcc12Etx(data);
		}
	}
}