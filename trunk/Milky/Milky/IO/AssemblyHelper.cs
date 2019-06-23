using System;
using System.Reflection;

namespace Milky.IO
{
	public class AssemblyHelper
	{
		/// <summary>
		/// アセンブリバージョン情報
		/// </summary>
		private System.Version asmVer;

		#region 構築／破棄

		/// <summary>
		/// アセンブリ情報からバージョンを取得し、インスタンスを作成する
		/// </summary>
		public AssemblyHelper()
		{
			Assembly asm = Assembly.GetExecutingAssembly();
			AssemblyName asmName = asm.GetName();

			Version v = asmName.Version;
			int major = v.Major;
			int minor = v.Minor;
			int build = v.Build;
			int rev = v.Revision;
			asmVer = new Version(major, minor, build, rev);
		}

		#endregion 構築／破棄

		#region プロパティ

		/// <summary>
		/// メジャーバージョン番号を返す
		/// </summary>
		public int Major { get { return this.asmVer.Major; } }

		/// <summary>
		/// マイナーバージョン番号を返す
		/// </summary>
		public int Minor { get { return this.asmVer.Minor; } }

		/// <summary>
		/// ローカルバージョン番号を返す
		/// </summary>
		public int Local { get { return this.asmVer.Build; } }

		/// <summary>
		/// アセンブリ情報内”Title”を返す
		/// </summary>
		public string Title { get { return ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(System.Reflection.AssemblyTitleAttribute))).Title; } }

		/// <summary>
		/// アセンブリ情報内”Description”を返す
		/// </summary>
		public string Description { get { return ((AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyDescriptionAttribute))).Description; } }

		/// <summary>
		/// アセンブリ情報内”Company”を返す
		/// </summary>
		public string Company { get { return ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCompanyAttribute))).Company; } }

		/// <summary>
		/// アセンブリ情報内”Product”を返す
		/// </summary>
		public string Product { get { return ((AssemblyProductAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyProductAttribute))).Product; } }

		/// <summary>
		/// アセンブリ情報内”Copyright”を返す
		/// </summary>
		public string Copyright { get { return ((AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCopyrightAttribute))).Copyright; } }

		/// <summary>
		/// アセンブリ情報内”Trademark”を返す
		/// </summary>
		public string Trademark { get { return ((AssemblyTrademarkAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyTrademarkAttribute))).Trademark; } }

		/// <summary>
		/// アセンブリ情報内”Version”を返す
		/// </summary>
		public string Version { get { return ((AssemblyVersionAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyVersionAttribute))).Version; } }

		#endregion プロパティ

		#region メソッド

		/// <summary>
		/// バージョンの大小比較を行う。
		/// </summary>
		/// <param name="from"></param>
		/// <returns>fromの方が古い場合１、同じなら０，新しい場合－１を返す</returns>
		public int CompareTo(AssemblyHelper from)
		{
			return this.asmVer.CompareTo(from.asmVer);
		}

		#endregion メソッド
	}
}