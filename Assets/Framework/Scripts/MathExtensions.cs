using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace HK.Framework
{
	/// <summary>
	/// 計算系処理拡張クラス.
	/// </summary>
	public static class MathExtension
	{
		public static bool IsEqual(float a, float b, float threshold = 0.0001f)
		{
			a = a - b;
			a = a < 0 ? -a : a;
			return a <= threshold;
		}
	}
}