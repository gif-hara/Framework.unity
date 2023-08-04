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

        public static UIManager Instance { get; private set; }

        public static Camera UICamera => Instance.uiCamera;

        private void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        
        public static T Register<T>(T uiViewPrefab) where T : UIView<T>
        {
            var uiView = Instantiate(uiViewPrefab, Instance.uiParent);
            uiView.HideImmediate();

            return uiView;
        }
        
        public static void Unregister<T>(UIView<T> uiView) where T : UIView<T>
        {
            if(ApplicationQuitObserver.IsQuit)
            {
                return;
            }
            
            Destroy(uiView.gameObject);
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
