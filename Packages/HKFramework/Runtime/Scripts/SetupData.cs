using HK.Framework.UISystems;
using UnityEditor.Animations;
using UnityEngine;

namespace HK.Framework.BootSystems
{
    public class SetupData : ScriptableObject
    {
        [SerializeField]
        private UIManager uiManagerPrefab;

        public UIManager UIManagerPrefab => uiManagerPrefab;
        
        [SerializeField]
        private AnimatorController animatorController;
        
        public AnimatorController AnimatorController => animatorController;

#if UNITY_EDITOR
        public void SetUIManagerPrefabEditor(UIManager uiManager)
        {
            uiManagerPrefab = uiManager;
        }
        
        public void SetAnimatorControllerEditor(AnimatorController animatorController)
        {
            this.animatorController = animatorController;
        }
#endif
    }
}
