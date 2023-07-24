using System;
using System.Collections.Generic;
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
        private AudioSource seSource;

        private readonly Dictionary<AudioClip, SortedList<int, SoundEffectData>> soundEffectData = new();

        private IDisposable fadeStream;

        public static AudioManager Instance { get; private set; }
        
        public static AudioSource SeSource => Instance.seSource;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            Instance = null;
        }
        
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
        
        public static void PlayOneShot(AudioClip clip, float volumeScale = 1.0f)
        {
            if (Instance.soundEffectData.TryGetValue(clip, out var sortedList))
            {
                PlayOneShotAsync(clip, sortedList).Forget();
            }
            else
            {
                Instance.seSource.PlayOneShot(clip, volumeScale);
            }
        }

        private static async UniTask PlayOneShotAsync(AudioClip clip, SortedList<int, SoundEffectData> list)
        {
            if (list.Count <= 0)
            {
                return;
            }
            
            var data = list.Values[0];
            list.RemoveAt(0);
            
            Instance.seSource.PlayOneShot(clip, data.volumeScale);
            await UniTask.Delay(TimeSpan.FromSeconds(clip.length), cancellationToken: Instance.GetCancellationTokenOnDestroy());
            list.Add(data.index, data);
        }
        
        public static void RegisterSoundEffectData(AudioClip clip, int totalPlayCount, float initialVolume = 1.0f)
        {
            Assert.IsFalse(Instance.soundEffectData.ContainsKey(clip), $"{clip.name}は既に登録されています");

            var sortedList = new SortedList<int, SoundEffectData>();
            for (var i = 0; i < totalPlayCount; i++)
            {
                var attenuate = Newton(
                    p => (1.0f - Mathf.Pow(p, totalPlayCount)) / (1.0f - p) - 1.0f / initialVolume,
                    p =>
                    {
                        var ip = 1.0f - p;
                        var t0 = -totalPlayCount * Mathf.Pow(p, totalPlayCount - 1.0f) / ip;
                        var t1 = (1.0f - Mathf.Pow(p, totalPlayCount)) / ip / ip;
                        return t0 + t1;
                    },
                    0.9f,
                    1
                    );
                var data = new SoundEffectData()
                {
                    index = i,
                    volumeScale = initialVolume * Mathf.Pow(attenuate, i),
                };
                sortedList.Add(i, data);
            }
            Instance.soundEffectData.Add(clip, sortedList);
        }

        [Serializable]
        public class SoundEffectData
        {
            public int index;
            
            public float volumeScale;
        }

        private static float Newton(Func<float, float> func, Func<float, float> derive, float initialX, int maxLoop)
        {
            var x = initialX;
            for (var i = 0; i < maxLoop; i++)
            {
                var curY = func(x);
                if(curY < 0.00001f && curY > -0.00001f)
                {
                    break;
                }
                
                x = x - curY / derive(x);
            }
            
            return x;
        }
        
#if UNITY_EDITOR
        public void SetBGMSource(AudioSource source)
        {
            this.bgmSource = source;
        }
        
        public void SetSESource(AudioSource source)
        {
            this.seSource = source;
        }
#endif
    }
}
