using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

namespace HK.Framwork
{
	/// <summary>
	/// LayerMask拡張クラス.
	/// </summary>
	public static class LayerMaskExtensions
	{
		public static bool IsIncluded(this LayerMask self, GameObject gameObject)
		{
			return (self.value & (1 << gameObject.layer)) > 0;
		}
	}
}
