using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
using HK.Framework.AnimationSystems;
using HK.Framework.AudioSystems;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestSceneController : MonoBehaviour
{
    [SerializeField]
    private AnimationClip testClip;
    
    [SerializeField]
    private AnimationController animationController;
    
    [SerializeField]
    private AudioClip soundEffectClip;
    
    private async void Start()
    {
        Debug.Log("TestSceneController.Start");
        await HK.Framework.BootSystems.BootSystem.IsReady;
        Debug.Log("TestSceneController.Start: BootSystem.IsReady 1");
        await HK.Framework.BootSystems.BootSystem.IsReady;
        Debug.Log("TestSceneController.Start: BootSystem.IsReady 2");
        
        var animationBlendData = new AnimationBlendData()
        {
            animationClip = testClip,
            blendSeconds = 0.0f,
        };
        animationController.Play(animationBlendData);

        this.GetAsyncUpdateTrigger()
            .Subscribe(_ =>
            {
                if (Keyboard.current.qKey.isPressed)
                {
                    AudioManager.PlayOneShot(this.soundEffectClip);
                }
            });
    }
}
