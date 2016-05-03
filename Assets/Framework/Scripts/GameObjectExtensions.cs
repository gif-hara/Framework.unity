using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

namespace HK.Framwork
{
	/// <summary>
	/// GameObjectの拡張クラス.
	/// </summary>
	public static class GameObjectExtensions
	{
		public static GameObject FindGameObjectWithTag(this GameObject self, string tag, float radius)
		{
			var findObjects = GameObject.FindGameObjectsWithTag(tag);
			var origin = self.transform.position;
			GameObject result = null;
			float min = radius;
			for(int i=0, imax=findObjects.Length; i<imax; i++)
			{
				var obj = findObjects[i];
				var distance = Vector3.Distance(origin, obj.transform.position);
				if(min > distance)
				{
					min = distance;
					result = obj;
				}
			}

			return result;
		}
	}
}
