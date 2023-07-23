using HK.Framework.AudioSystems;
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

        [SerializeField]
        private AudioManager audioManagerPrefab;
        
        public AudioManager AudioManagerPrefab => audioManagerPrefab;

#if UNITY_EDITOR
        public void SetUIManagerPrefabEditor(UIManager uiManager)
        {
            uiManagerPrefab = uiManager;
        }
        
        public void SetAnimatorControllerEditor(RuntimeAnimatorController runtimeAnimatorController)
        {
            this.runtimeAnimatorController = runtimeAnimatorController;
        }
        
        public void SetAudioManagerPrefabEditor(AudioManager audioManager)
        {
            audioManagerPrefab = audioManager;
        }
#endif
    }
}
