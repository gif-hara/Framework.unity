using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using System.Linq;

namespace HK.Framework
{
	/// <summary>
	/// .
	/// </summary>
	public class CsvParser
	{
		public static List<T> Parse<T>(TextAsset textAsset, System.Func<List<string>, T> parser) where T : class
		{
			return textAsset.text.Trim().Replace("\r", "").Split('\n').Where((t, i) => i != 0).Select(t => parser(t.Split(',').ToList())).ToList();
		}

		public static List<string[]> Split(TextAsset textAsset)
		{
			return textAsset.text.Trim().Replace("\r", "").Split('\n').Where((t, i) => i != 0).Select(t => t.Split(',')).ToList();
		}
	}
}