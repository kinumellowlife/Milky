using System.Collections.Generic;

namespace Milky.IO
{
	/// <summary>
	/// 引数を分割するクラス
	/// </summary>
	/// <remarks>
	/// ルール定義方法
	///		a+bc+/foo+//debug/
	///	-a は何らからのオプションを０個以上持つ
	///	func -a [-b] -c 0 1 --foo --debug
	/// </remarks>
	public class ArgmentParser
	{
		#region public class

		/// <summary>
		/// オプションコマンドパラメータ
		/// </summary>
		public class CmdParam
		{
			#region property

			/// <summary>
			/// キーの取得と設定
			/// </summary>
			public string Key { get; set; }

			/// <summary>
			/// 値の取得と設定
			/// </summary>
			public string Value { get; set; }

			#endregion property

			#region construct

			/// <summary>
			/// 構築
			/// </summary>
			public CmdParam() { }

			/// <summary>
			/// 構築
			/// </summary>
			/// <param name="key">オプションのキー</param>
			/// <param name="value">値</param>
			public CmdParam(string key, string value) { this.Key = key; this.Value = value; }

			/// <summary>
			/// 構築
			/// </summary>
			/// <param name="key">オプションのキー</param>
			public CmdParam(string key) { this.Key = key; }

			#endregion construct
		}

		/// <summary>
		/// ルール
		/// </summary>
		public class ArgRule : CmdParam
		{
			#region property

			/// <summary>
			/// パラメータを受け取るかどうか
			/// </summary>
			public bool HasParam { get; set; }

			#endregion property

			#region construct

			/// <summary>
			/// 構築
			/// </summary>
			public ArgRule() { }

			/// <summary>
			/// 構築
			/// </summary>
			/// <param name="key">オプションのキー</param>
			/// <param name="value">値</param>
			public ArgRule(string key, string value) : base(key, value) { }

			/// <summary>
			/// 構築
			/// </summary>
			/// <param name="key">オプションのキー</param>
			public ArgRule(string key) : base(key) { }

			#endregion construct
		}

		#endregion public class

		#region fields

		private Dictionary<string, ArgRule> cmds = new Dictionary<string, ArgRule>();
		private List<CmdParam> cmdParams = new List<CmdParam>();

		#endregion fields

		#region property

		/// <summary>
		/// ルール一覧の取得
		/// </summary>
		public IEnumerable<ArgRule> Rules {
			get
			{
				foreach (var a in cmds)
				{
					yield return a.Value;
				}
			}
		}

		/// <summary>
		/// コマンドパラメータ一覧
		/// </summary>
		public IEnumerable<CmdParam> CmdParams {
			get
			{
				foreach (var c in cmdParams)
				{
					yield return c;
				}
			}
		}

		#endregion property

		#region construct

		/// <summary>
		/// 構築
		/// </summary>
		/// <param name="args">アプリケーション起動時の引数</param>
		/// <param name="rule">オプションルール</param>
		public ArgmentParser(string[] args, string rule)
		{
			//ルールをパース
			ParseRule(rule);
			//オプションのパース
			ParseCommandLine(args);
		}

		#endregion construct

		#region Inner API

		/// <summary>
		///
		/// </summary>
		private enum ParseState
		{
			Start,
			SimpleArg,
			StringArg,
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="rule"></param>
		private void ParseRule(string rule)
		{
			ParseState nowState = ParseState.Start;
			ArgRule argRule = null;
			string stack = "";
			foreach (char c in rule)
			{
				switch (nowState)
				{
					case ParseState.Start:
						switch (c)
						{
							case '/':
								nowState = ParseState.StringArg;
								stack = "";
								break;

							case '+':
								if (argRule != null)
								{
									argRule.HasParam = true;
								}
								break;

							default:
								argRule = new ArgRule("" + c);
								cmds.Add(argRule.Key, argRule);
								break;
						}
						break;

					case ParseState.StringArg:
						switch (c)
						{
							case '/':
								if (argRule != null)
								{
									argRule = new ArgRule(stack);
									cmds.Add(argRule.Key, argRule);
								}
								nowState = ParseState.Start;
								break;

							case '+':
								argRule = new ArgRule(stack);
								cmds.Add(argRule.Key, argRule);
								break;

							default:
								stack += c;
								break;
						}
						break;
				}
			}
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="args"></param>
		private void ParseCommandLine(string[] args)
		{
			ParseState state = ParseState.Start;
			CmdParam cp = null;
			foreach (var a in args)
			{
				string cmd = a;
				while ((cmd.Length > 0) && (cmd[0] == '-'))
				{
					cmd = cmd.Remove(0, 1);
				}

				switch (state)
				{
					case ParseState.Start:
						if (cmds.ContainsKey(cmd))
						{
							ArgRule rule = cmds[cmd];
							cp = new CmdParam(rule.Key);
							cmdParams.Add(cp);
							if (rule.HasParam)
							{
								state = ParseState.StringArg;
							}
						}
						else
						{
							cp = new CmdParam("", a);
							cmdParams.Add(cp);
						}
						break;

					case ParseState.StringArg:
						cp.Value = a;
						state = ParseState.Start;
						break;
				}
			}
		}

		#endregion Inner API
	}
}