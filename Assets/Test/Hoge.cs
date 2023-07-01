using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoge : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        await HK.Framework.BootSystems.BootSystem.IsReady;
        Debug.Log("Hoge.Start: BootSystem.IsReady 1");
    }
}
