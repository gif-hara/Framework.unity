using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
using HK.Framework.TimeSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Framework.AudioSystems
{
    /// <summary>
    /// オーディオを管理するクラス
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource bgmSource;

        [SerializeField]
        private SoundEffectElement soundEffectElementPrefab;

        private IDisposable fadeStream;

        public static AudioManager Instance { get; private set; }
        
        public static AudioSource BGMSource => Instance.bgmSource;

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
            Instance.bgmSource.clip = clip;
            Instance.bgmSource.loop = true;
            Instance.bgmSource.Play();
        }

        public static void FadeBGM(float duration, float to)
        {
            Instance.fadeStream?.Dispose();
            var original = Instance.bgmSource.volume;
            var time = 0.0f;
            Instance.fadeStream = Instance.GetAsyncUpdateTrigger()
                .Subscribe(_ =>
                {
                    time += TimeManager.Game.deltaTime;
                    Instance.bgmSource.volume = Mathf.Lerp(original, to, time / duration);
                    if (time >= duration)
                    {
                        Instance.bgmSource.volume = to;
                        Instance.fadeStream.Dispose();
                    }
                });
        }

        public static async UniTask PlaySEAsync(AudioClip clip, Action<SoundEffectElement> onCreatedAction = null)
        {
            var element = Instantiate(Instance.soundEffectElementPrefab, Instance.transform);
            onCreatedAction?.Invoke(element);
            await element.PlayAsync(clip);
            Destroy(element.gameObject);
        }

        public static void PlaySE(AudioClip clip, Action<SoundEffectElement> onCreated = null)
        {
            PlaySEAsync(clip, onCreated).Forget();
        }
        
#if UNITY_EDITOR
        public void SetBGMSource(AudioSource source)
        {
            this.bgmSource = source;
        }
        
        public void SetSoundEffectElementPrefab(SoundEffectElement prefab)
        {
            this.soundEffectElementPrefab = prefab;
        }
#endif
    }
}
