using HK.Framework.UISystems;
using UnityEngine;

namespace HK.Framework.BootSystems
{
    public class SetupData : ScriptableObject
    {
        [SerializeField]
        private UIManager uiManagerPrefab;

        public UIManager UIManagerPrefab => uiManagerPrefab;
        
        [SerializeField]
        private RuntimeAnimatorController runtimeAnimatorController;
        
        public RuntimeAnimatorController RuntimeAnimatorController => runtimeAnimatorController;

#if UNITY_EDITOR
        public void SetUIManagerPrefabEditor(UIManager uiManager)
        {
            uiManagerPrefab = uiManager;
        }
        
        public void SetAnimatorControllerEditor(RuntimeAnimatorController runtimeAnimatorController)
        {
            this.runtimeAnimatorController = runtimeAnimatorController;
        }
#endif
    }
}
