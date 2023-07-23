using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
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

        private void OnDestroy()
        {
            this.fadeStream?.Dispose();
        }

        public static async UniTask SetupAsync()
        {
            Assert.IsNull(Instance);

            var prefab = await Resources.LoadAsync<AudioManager>("AudioManager");
            Instance = Instantiate((AudioManager)prefab);
            DontDestroyOnLoad(Instance);
        }

        public static void PlayBGM(AudioClip clip)
        {
            Instance.fadeStream?.Dispose();
            Instance.bgmSource.clip = clip;
            Instance.bgmSource.loop = true;
            Instance.bgmSource.volume = 0.15f;
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
                    time += Time.deltaTime;
                    Instance.bgmSource.volume = Mathf.Lerp(original, to, time / duration);
                    if (time >= duration)
                    {
                        Instance.bgmSource.volume = to;
                        Instance.fadeStream.Dispose();
                    }
                });
        }

        public static async UniTask PlaySEAsync(SoundEffectElement.AudioData data)
        {
            var element = Instantiate(Instance.soundEffectElementPrefab, Instance.transform);
            await element.PlayAsync(data);
            Destroy(element.gameObject);
        }

        public static void PlaySE(SoundEffectElement.AudioData data)
        {
            PlaySEAsync(data).Forget();
        }
    }
}
