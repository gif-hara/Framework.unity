using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

namespace HK.Framwork
{
	/// <summary>
	/// ExecuteEventsの拡張クラス.
	/// </summary>
	public static class ExecuteEventsExtensions
	{
        public static void Execute<T>(List<GameObject> targets, BaseEventData eventData, ExecuteEvents.EventFunction<T> functor) where T : IEventSystemHandler
        {
            targets.ForEach( g =>
            {
                ExecuteEvents.Execute<T>( g, eventData, functor );
            } );
        }
		public static void Broadcast<T>(GameObject target, BaseEventData eventData, ExecuteEvents.EventFunction<T> functor) where T : IEventSystemHandler
		{
			Broadcast<T>(target.transform, eventData, functor);
		}

		public static void Broadcast<T>(Transform target, BaseEventData eventData, ExecuteEvents.EventFunction<T> functor) where T : IEventSystemHandler
		{
			ExecuteEvents.Execute<T>(target.gameObject, eventData, functor);
			for(int i=0; i<target.childCount; i++)
			{
				Broadcast<T>(target.GetChild(i), eventData, functor);
			}
		}
	}
}
