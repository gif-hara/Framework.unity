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
        /// <summary>
        /// ブートシステムが初期化完了したか返す
        /// </summary>
        public static UniTask IsReady
        {
            get
            {
                return UniTask.WaitUntil(() => initializeState == InitializeState.Initialized);
            }
        }
        
        /// <summary>
        /// 初期化の状態
        /// </summary>
        private enum InitializeState
        {
            None,
            Initializing,
            Initialized,
        }
        
        private static InitializeState initializeState = InitializeState.None;
        
        /// <summary>
        /// 追加の初期化処理
        /// </summary>
        public static event Func<UniTask> AdditionalSetupAsync;
        
        /// <summary>
        /// 追加のイベント登録処理
        /// </summary>
        public static event Action<BuiltinContainerBuilder> AdditionalSetupContainerBuilderAsync;

        /// <summary>
        /// 初期化
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void InitializeOnBeforeSplashScreen()
        {
            SetupInternal().Forget();
        }

        /// <summary>
        /// セットアップの内部処理
        /// </summary>
        private static async UniTask SetupInternal()
        {
            initializeState = InitializeState.Initializing;
            var setupData = Resources.Load<SetupData>("SetupData");
            await UniTask.WhenAll(
                CreateUIManagerAsync(setupData),
                RegisterEvents(),
                AdditionalSetupAsync?.Invoke() ?? UniTask.CompletedTask,
                UniTask.DelayFrame(1)
                );
            initializeState = InitializeState.Initialized;
        }
        
        /// <summary>
        /// <see cref="UIManager"/>を生成する
        /// </summary>
        private static UniTask CreateUIManagerAsync(SetupData setupData)
        {
            Object.Instantiate(setupData.UIManagerPrefab);
            return UniTask.CompletedTask;
        }
        
        /// <summary>
        /// イベントの登録を行う
        /// </summary>
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
