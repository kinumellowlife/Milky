namespace Milky.Algorithm
{
	/// <summary>
	/// シングルトンクラス生成用クラス
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Singleton<T> where T : new()
	{
		#region construction

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Singleton()
		{
		}

		#endregion construction

		#region public API

		/// <summary>
		/// シングルトンインスタンスの取得
		/// </summary>
		/// <remarks>
		/// シングルトンインスタンスの取得を行う。
		/// もちろんインスタンスは取得のみ可能。
		/// </remarks>
		public static T Instance {
			get
			{
				return SingletonCreator.instance;
			}
		}

		#endregion public API

		/// <summary>
		/// シングルトンのクリエイター
		/// </summary>
		/// <remarks>
		/// 唯一のインスタンスを保持するクラス
		/// </remarks>
		private class SingletonCreator
		{
			#region 構築

			/// <summary>
			/// コンストラクタ
			/// </summary>
			static SingletonCreator()
			{
			}

			#endregion 構築

			#region フィールド

			/// <summary>唯一のインスタンス</summary>
			internal static readonly T instance = new T();

			#endregion フィールド
		}
	}
}