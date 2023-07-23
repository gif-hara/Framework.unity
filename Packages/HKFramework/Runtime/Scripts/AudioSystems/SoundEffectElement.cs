using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace HK.Framework.AudioSystems
{
    public class SoundEffectElement : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;

        public UniTask PlayAsync(AudioData data)
        {
            this.audioSource.PlayOneShot(data.clip);
            return UniTask.WaitUntil(() => !this.audioSource.isPlaying, cancellationToken:this.GetCancellationTokenOnDestroy());
        }

        public void Play(AudioData data)
        {
            this.PlayAsync(data).Forget();
        }

        [Serializable]
        public class AudioData
        {
            public AudioClip clip;
        }
        
#if UNITY_EDITOR
        public void SetAudioSource(AudioSource audioSource)
        {
            this.audioSource = audioSource;
        }
#endif
    }
}
