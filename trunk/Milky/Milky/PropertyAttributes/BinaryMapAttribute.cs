using System;

namespace Milky.PropertyAttributes
{
	/// <summary>
	/// バイナリデータ種別
	/// </summary>
	public enum BinaryDataType
	{
		/// <summary>
		/// バイナリ
		/// </summary>
		Bin,

		/// <summary>
		/// BCD
		/// </summary>
		BCD,
	}

	/// <summary>
	/// バイナリデータ定義のグローバル設定
	/// </summary>
	public class BinaryContractAttribute : Attribute
	{
		/// <summary>
		/// 先頭のオフセット
		/// ファイルの途中からクラス定義を始めるときに使う。
		/// </summary>
		public int StartByte { get; set; } = 0;

		/// <summary>
		/// バイナリデータの総サイズ。
		/// たとえ指定してもクラス内部でそれ以上のエリアを指定するとサイズは自動的に大きくなる。
		///
		/// なお、メンバクラスに指定しても意味がない。
		/// </summary>
		public int TotalSize { get; set; } = 0;
	}

	/// <summary>
	/// フォーマットが決められたバイナリデータとのシリアライズ・デシリアラズを可能にする属性
	/// </summary>
	public class BinaryMapAttribute : Attribute
	{
		/// <summary>
		/// バイナリデータのタイプ
		/// </summary>
		public BinaryDataType DataType { get; set; } = BinaryDataType.Bin;

		/// <summary>
		/// エンディアン変換（反転）が必要かどうか
		/// </summary>
		public bool NeedReverse { get; set; } = false;

		/// <summary>
		/// 開始バイト位置
		/// </summary>
		public int StartByte { get; set; } = 0;

		/// <summary>
		/// 値が示すバイト範囲
		/// </summary>
		public int ByteLength { get; set; } = 1;

		/// <summary>
		/// 開始ビット
		/// -1指定で無視される。
		/// １バイト内のビット位置指定のみ想定しており、
		/// ビット位置指定のときは、取り込む型はbooleanかbyte型のみ可能。
		/// それ以外の型で指定しても無視される。
		/// </summary>
		public int StartBit { get; set; } = -1;

		/// <summary>
		/// ビット長
		/// </summary>
		public int BitLength { get; set; } = 1;

		/// <summary>
		/// 配列の場合、要素数の数
		/// </summary>
		public int ArrayLength { get; set; } = -1;

		/// <summary>
		/// 文字列の場合指定したエンコードでバイナリデータをエンコーディングする
		/// </summary>
		public string Encoding { get; set; } = "utf8";

		/// <summary>
		/// 読み込みだけ行うかどうか。
		/// Trueにしておくと、Write()メソッドでバイナリ出力されなくなる
		/// </summary>
		public bool ReadOnly { get; set; } = false;

		/// <summary>
		/// 配列要素１つのサイズ
		/// </summary>
		public int ArraySize { get; set; } = -1;
	}
}