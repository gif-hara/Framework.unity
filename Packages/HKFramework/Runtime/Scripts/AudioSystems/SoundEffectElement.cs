using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Unity1Week0619
{
    public class SoundEffectElement : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;

        public UniTask PlayAsync(AudioData data)
        {
            this.audioSource.volume = UnityEngine.Random.Range(data.volumeMin, data.volumeMax);
            this.audioSource.pitch = UnityEngine.Random.Range(data.pitchMin, data.pitchMax);
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
            public float volumeMin = 1.0f;
            public float volumeMax = 1.0f;
            public float pitchMin = 1.0f;
            public float pitchMax = 1.0f;
        }
    }
}
