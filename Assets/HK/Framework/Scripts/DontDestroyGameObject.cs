using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

namespace HK.Framework
{
	/// <summary>
	/// gameObjectをDontDestroyするコンポーネント.
	/// </summary>
	public class DontDestroyGameObject : MonoBehaviour
	{
		void Awake()
	    {
	        DontDestroyOnLoad( this.gameObject );
	    }
	}
}
