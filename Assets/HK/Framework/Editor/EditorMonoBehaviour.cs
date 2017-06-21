using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

namespace HK.Framework
{
	public abstract class EditorMonoBehaviour<T> : EditorBase where T : MonoBehaviour
	{
		private T _target = null;
		
		protected T Target
		{
			get
			{
				if( _target == null )
				{
					_target = (T)target;
				}
				return _target;
			}
		}
	}
}
