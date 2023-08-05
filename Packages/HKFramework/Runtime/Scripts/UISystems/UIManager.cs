using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Framework.UISystems
{
    /// <summary>
    /// UIを管理するクラス
    /// </summary>
    public sealed class UIManager : MonoBehaviour
    {
        [SerializeField]
        private RectTransform uiParent;

        [SerializeField]
        private Camera uiCamera;
        
        private readonly List<IUIPresenter> presenters = new();

        public static UIManager Instance { get; private set; }

        public static Camera UICamera => Instance.uiCamera;

        private void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        
        public static T RegisterView<T>(T uiViewPrefab) where T : UIView<T>
        {
            var uiView = Instantiate(uiViewPrefab, Instance.uiParent);
            uiView.HideImmediate();

            return uiView;
        }
        
        public static void UnregisterView<T>(UIView<T> uiView) where T : UIView<T>
        {
            if(ApplicationQuitObserver.IsQuit)
            {
                return;
            }
            
            Destroy(uiView.gameObject);
        }
        
        public static async UniTask RegisterPresenter<T>(T uiPresenter, CancellationToken cancellationToken) where T : IUIPresenter
        {
            Instance.presenters.Add(uiPresenter);
            await uiPresenter.InvokeAsync(cancellationToken);
            Instance.presenters.Remove(uiPresenter);
        }
        
        public static Vector2 WorldToScreenPoint(Vector3 worldPosition, Camera worldCamera)
        {
            var screenPosition = RectTransformUtility.WorldToScreenPoint(worldCamera, worldPosition);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(Instance.uiParent, screenPosition, Instance.uiCamera, out var result);
            return result;
        }

#if UNITY_EDITOR
        public void SetUIParentEditor(RectTransform uiParent)
        {
            this.uiParent = uiParent;
        }

        public void SetUICameraEditor(Camera uiCamera)
        {
            this.uiCamera = uiCamera;
        }
#endif
    }
}
