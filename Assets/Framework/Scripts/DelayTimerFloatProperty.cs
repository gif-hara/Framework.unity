using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

namespace HK.Framwork
{
	/// <summary>
	/// FloatPropertyに対応したDelayTimerコンポーネント.
	/// </summary>
	public class DelayTimerFloatProperty : DelayTimerBase
	{
        [SerializeField]
        private FloatProperty.Finder delay;

        [SerializeField]
        private FloatProperty.Finder duration;

        private float _duration;

        void Start()
        {
            this._duration = this.duration.Get;
        }

        void Update()
        {
            var _delay = this.delay.Get;
            this._duration += Time.deltaTime;
            this._duration = this._duration > _delay ? _delay : this._duration;
        }

        public override void Reset()
        {
            this._duration = 0.0f;
        }

        public override bool Complete
        {
            get
            {
                return this.delay.Get <= this._duration;
            }
        }

        public override float Normalize
        {
            get
            {
                return this._duration / this.delay.Get;
            }
        }
    }
}
