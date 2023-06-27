using HK.Framework.UISystems;
using UnityEngine;

namespace HK.Framework.BootSystems
{
    public class SetupData : ScriptableObject
    {
        [SerializeField]
        private UIManager uiManagerPrefab;

        public UIManager UIManagerPrefab => uiManagerPrefab;

#if UNITY_EDITOR
        public void SetUIManagerPrefabEditor(UIManager uiManager)
        {
            uiManagerPrefab = uiManager;
        }
#endif
    }
}
