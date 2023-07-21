using System.Collections;
using System.Collections.Generic;
using HK.Framework.AnimationSystems;
using UnityEngine;

public class TestSceneController : MonoBehaviour
{
    [SerializeField]
    private AnimationClip testClip;
    
    [SerializeField]
    private AnimationController animationController;
    
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
    }
}
