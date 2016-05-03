using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

namespace HK.Framwork
{
	/// <summary>
	/// 遅延タイマーコンポーネント.
	/// </summary>
	public class DelayTimer : DelayTimerBase
	{
		[SerializeField]
		private float delay;

        [SerializeField]
		private float duration = 0.0f;

		void Update()
		{
            this.duration += Time.deltaTime;
            this.duration = this.duration > this.delay ? this.delay : this.duration;
		}

		public override void Reset()
		{
			this.duration = 0.0f;
		}

        public void SetDelay(FloatProperty.Finder finder)
        {
            this.delay = finder.Get;
        }

		public override bool Complete
		{
			get
			{
				return this.delay <= this.duration;
			}
		}

		public override float Normalize
		{
			get
			{
				return this.duration / this.delay;
			}
		}

	}
}
