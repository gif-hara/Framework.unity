using System;
using Cysharp.Threading.Tasks;
using HK.Framework.MessageSystems;
using HK.Framework.TimeSystems;
using HK.Framework.UISystems;
using MessagePipe;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HK.Framework.BootSystems
{
    /// <summary>
    /// ブートシステム
    /// </summary>
    public static class BootSystem
    {
        public static UniTask IsReady { get; private set; }
        
        public static event Func<UniTask> AdditionalSetupAsync;
        
        public static event Action<BuiltinContainerBuilder> AdditionalSetupContainerBuilderAsync;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void InitializeOnBeforeSplashScreen()
        {
            IsReady = SetupInternal();
        }

        private static async UniTask SetupInternal()
        {
            var setupData = Resources.Load<SetupData>("SetupData");
            await UniTask.WhenAll(
                CreateUIManagerAsync(setupData),
                RegisterEvents(),
                AdditionalSetupAsync?.Invoke() ?? UniTask.CompletedTask,
                UniTask.DelayFrame(1)
                );

            IsReady = UniTask.CompletedTask;
        }
        
        private static UniTask CreateUIManagerAsync(SetupData setupData)
        {
            Object.Instantiate(setupData.UIManagerPrefab);
            return UniTask.CompletedTask;
        }
        
        private static UniTask RegisterEvents()
        {
            MessageBroker.Setup(builder =>
            {
                AdditionalSetupContainerBuilderAsync?.Invoke(builder);
                TimeEvents.RegisterEvents(builder);
            });
            
            return UniTask.CompletedTask;
        }
    }
}
