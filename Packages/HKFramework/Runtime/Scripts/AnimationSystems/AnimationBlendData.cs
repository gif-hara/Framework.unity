using System;
using UnityEngine;

namespace HK.Framework.AnimationSystems
{
    /// <summary>
    /// アニメーションブレンドに必要なデータ
    /// </summary>
    [Serializable]
    public sealed class AnimationBlendData
    {
        /// <summary>
        /// 再生するアニメーション
        /// </summary>
        public AnimationClip animationClip;

        /// <summary>
        /// ブレンドする時間（秒）
        /// </summary>
        public float blendSeconds;
    }
}
