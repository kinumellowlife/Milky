namespace Milky.Algorithm
{
	public class Singleton<T> where T : new()
	{
		#region 構築

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Singleton()
		{
		}

		#endregion 構築

		#region 操作

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

		#endregion 操作

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