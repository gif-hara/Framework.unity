using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

namespace HK.Framwork
{
	/// <summary>
	/// RectTransformUtiltiy拡張クラス.
	/// </summary>
	public static class RectTransformUtilityExtensions
	{
		public static Vector2 ScreenPoint(Camera mainCamera, Vector3 worldPosition, RectTransform parent, Canvas canvas)
		{
			var screenPosition = mainCamera.WorldToScreenPoint(worldPosition);
			var localPosition = Vector2.zero;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, screenPosition, canvas.worldCamera, out localPosition);
			return localPosition;
		}
	}
}