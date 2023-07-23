using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace HK.Framework.AudioSystems
{
    public class SoundEffectElement : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;
        
        public AudioSource AudioSource => audioSource;

        public UniTask PlayAsync(AudioClip clip)
        {
            this.audioSource.clip = clip;
            this.audioSource.Play();
            return UniTask.WaitUntil(() => !this.audioSource.isPlaying, cancellationToken:this.GetCancellationTokenOnDestroy());
        }

        public void Play(AudioClip clip)
        {
            this.PlayAsync(clip).Forget();
        }
        
#if UNITY_EDITOR
        public void SetAudioSource(AudioSource audioSource)
        {
            this.audioSource = audioSource;
        }
#endif
    }
}
