using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milky.IO
{
	public class FilterParser
	{
		public List<Tuple<string, string>> namedFilters = new List<Tuple<string, string>>();

		public bool Filtered { get { return this.namedFilters.Count != 0; } }
		public string Text { get; set; } = "";

		public IEnumerable<string> this[string key] {
			get
			{
				bool hasNamedKey = this.namedFilters.Any(f => f.Item1.Length > 0);

				if (hasNamedKey)
				{
					if (string.IsNullOrEmpty(key))
					{
						return namedFilters.Where(f => f.Item1.Equals("")).Select(f => f.Item2);
					}
					else
					{
						return namedFilters.Where(f => f.Item1.Equals(key)).Select(f => f.Item2);
					}
				}
				else
				{
					return namedFilters.Select(f => f.Item2);
				}
			}
		}

		public bool Check(Func<string, string, bool> pred)
		{
			bool ret = true;
			foreach (var t in this.namedFilters)
			{
				if (!pred(t.Item1, t.Item2))
				{
					ret = false;
				}
			}
			return ret;
		}

		public bool Has(string filter)
		{
			return this.namedFilters.Any(f => f.Item1.Equals(filter));
		}

		public void Set(string text)
		{
			this.namedFilters.Clear();
			string[] texts = text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
			foreach (var t in texts)
			{
				if (t.Contains(":"))
				{
					int pos = t.IndexOf(':');
					string name = t.Substring(0, pos);
					string value = t.Substring(pos + 1, t.Length - pos - 1);
					if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
					{
						namedFilters.Add(new Tuple<string, string>(name, value));
					}
				}
				else
				{
					namedFilters.Add(new Tuple<string, string>("", t));
				}
			}
		}
	}
}
