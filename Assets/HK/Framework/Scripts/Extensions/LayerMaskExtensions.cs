using UnityEngine;

namespace HK.Framework
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
