using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace HK.Framwork
{
	/// <summary>
	/// Destroy関数が呼ばれる直前にイベントをフックできるインターフェイス.
	/// </summary>
	public interface IReceivePreDestroyEvent : IEventSystemHandler
	{
		void OnPreDestroy();
	}
}
