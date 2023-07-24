using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
using HK.Framework.TimeSystems;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace HK.Framework.AudioSystems
{
    /// <summary>
    /// オーディオを管理するクラス
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;

        [SerializeField]
        private SoundEffectElement soundEffectElementPrefab;

        private IDisposable fadeStream;

        public static AudioManager Instance { get; private set; }
        
        public static AudioSource AudioSource => Instance.audioSource;

        private void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        
        private void OnDestroy()
        {
            this.fadeStream?.Dispose();
        }
        
        public static void PlayBGM(AudioClip clip)
        {
            Instance.fadeStream?.Dispose();
            Instance.audioSource.clip = clip;
            Instance.audioSource.loop = true;
            Instance.audioSource.Play();
        }

        public static void FadeBGM(float duration, float to)
        {
            Instance.fadeStream?.Dispose();
            var original = Instance.audioSource.volume;
            var time = 0.0f;
            Instance.fadeStream = Instance.GetAsyncUpdateTrigger()
                .Subscribe(_ =>
                {
                    time += TimeManager.Game.deltaTime;
                    Instance.audioSource.volume = Mathf.Lerp(original, to, time / duration);
                    if (time >= duration)
                    {
                        Instance.audioSource.volume = to;
                        Instance.fadeStream.Dispose();
                    }
                });
        }
        
        public static void PlayOneShot(AudioClip clip, float volumeScale = 1.0f)
        {
            Instance.audioSource.PlayOneShot(clip, volumeScale);
        }
        
#if UNITY_EDITOR
        public void SetAudioSource(AudioSource source)
        {
            this.audioSource = source;
        }
        
        public void SetSoundEffectElementPrefab(SoundEffectElement prefab)
        {
            this.soundEffectElementPrefab = prefab;
        }
#endif
    }
}
