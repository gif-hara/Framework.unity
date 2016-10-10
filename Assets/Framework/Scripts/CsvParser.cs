using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using System.Linq;

namespace HK.Framework
{
	/// <summary>
	/// .
	/// </summary>
	public class CsvParser<T> where T : class
	{
		public static List<T> Parse(TextAsset textAsset, System.Func<List<string>, T> parser)
		{
			return textAsset.text.Trim().Replace("\r", "").Split('\n').Where((t, i) => i != 0).Select(t => parser(t.Split(',').ToList())).ToList();
		}
	}
}