using System;
using Cysharp.Threading.Tasks;
using HK.Framework.AnimationSystems;
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

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void InitializeOnSubsystemRegistration()
        {
            AdditionalSetupAsync = null;
            AdditionalSetupContainerBuilderAsync = null;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void InitializeOnBeforeSplashScreen()
        {
            SetupInternalAsync().Forget();
        }

        /// <summary>
        /// セットアップの内部処理
        /// </summary>
        private static async UniTask SetupInternalAsync()
        {
            initializeState = InitializeState.Initializing;
            var setupData = Resources.Load<SetupData>("SetupData");
            // 即時でセットアップ
            {
                SetupAnimationSystems(setupData);
            }

            // 非同期でセットアップ
            {
                await UniTask.WhenAll(
                    CreateUIManagerAsync(setupData),
                    CreateAudioManagerAsync(setupData),
                    RegisterEvents(),
                    AdditionalSetupAsync?.Invoke() ?? UniTask.CompletedTask,
                    UniTask.DelayFrame(1)
                    );
            }
            initializeState = InitializeState.Initialized;
        }

        /// <summary>
        /// アニメーションシステムの設定
        /// </summary>
        private static void SetupAnimationSystems(SetupData setupData)
        {
            AnimationController.SharedController = setupData.RuntimeAnimatorController;
        }

        /// <summary>
        /// <see cref="UIManager"/>を生成する
        /// </summary>
        private static UniTask CreateUIManagerAsync(SetupData setupData)
        {
            Object.Instantiate(setupData.UIManagerPrefab);
            return UniTask.CompletedTask;
        }

        private static UniTask CreateAudioManagerAsync(SetupData setupData)
        {
            Object.Instantiate(setupData.AudioManagerPrefab);
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
                ApplicationQuitObserver.RegisterEvents(builder);
            });

            return UniTask.CompletedTask;
        }
    }
}
