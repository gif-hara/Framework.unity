using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace HK.Framework
{
	/// <summary>
	/// UnityEngine.Objecrの拡張クラス.
	/// </summary>
	public static class ObjectExtensions
	{
        public static void Destroy( this Object self, Object obj, GameObject preDestroyEventReceiver )
        {
            ExecuteEvents.Execute<IReceivePreDestroyEvent>( preDestroyEventReceiver, null, ( handler, eventData ) => handler.OnPreDestroy() );
            Object.Destroy( obj );
        }
        public static void Destroy( this Object self, Object obj, float delay, GameObject preDestroyEventReceiver )
        {
            ExecuteEvents.Execute<IReceivePreDestroyEvent>( preDestroyEventReceiver, null, ( handler, eventData ) => handler.OnPreDestroy() );
            Object.Destroy( obj, delay );
        }
    }
}
