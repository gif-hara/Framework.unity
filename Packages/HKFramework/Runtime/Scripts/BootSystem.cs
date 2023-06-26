using Cysharp.Threading.Tasks;
using HK.Framework.UISystems;
using UnityEngine;

namespace HK.Framework.BootSystems
{
    /// <summary>
    /// ブートシステム
    /// </summary>
    public static class BootSystem
    {
        public static UniTask IsReady { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void InitializeOnBeforeSplashScreen()
        {
            IsReady = SetupInternal();
        }

        private static async UniTask SetupInternal()
        {
            await UniTask.WhenAll(
                UniTask.DelayFrame(1)
                );

            IsReady = UniTask.CompletedTask;
        }
        
        // UIManagerを生成する
        private static UIManager CreateUIManager(SetupData setupData)
        {
            var uiManager = Object.Instantiate(setupData.UIManagerPrefab);
            uiManager.transform.SetParent(null);
            uiManager.gameObject.SetActive(true);
            return uiManager;
        }
    }
}