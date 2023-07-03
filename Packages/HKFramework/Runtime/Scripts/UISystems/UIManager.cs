using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.Framework.UISystems
{
    /// <summary>
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

        public static T Open<T>(T uiViewPrefab) where T : UIView
        {
            return Instantiate(uiViewPrefab, Instance.uiParent);
        }

        public static void Close(UIView uiView)
        {
            Destroy(uiView.gameObject);
        }

        public static void Show(UIView uiView)
        {
            uiView.gameObject.SetActive(true);
        }

        public static void Hidden(UIView uiView)
        {
            uiView.gameObject.SetActive(false);
        }

        public static void SetAsLastSibling(UIView uiView)
        {
            uiView.transform.SetAsLastSibling();
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
