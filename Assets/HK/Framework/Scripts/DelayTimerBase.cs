using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

namespace HK.Framework
{
	/// <summary>
	/// DelayTimer抽象クラス.
	/// </summary>
	public abstract class DelayTimerBase : MonoBehaviour
	{
        public abstract void Reset();

        public abstract bool Complete { get; }

        public abstract float Normalize { get; }
    }
}
