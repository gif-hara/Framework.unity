using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneController : MonoBehaviour
{
    private async void Start()
    {
        Debug.Log("TestSceneController.Start");
        await HK.Framework.BootSystems.BootSystem.IsReady;
        Debug.Log("TestSceneController.Start: BootSystem.IsReady 1");
        await HK.Framework.BootSystems.BootSystem.IsReady;
        Debug.Log("TestSceneController.Start: BootSystem.IsReady 2");
    }
}
