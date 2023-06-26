using Cysharp.Threading.Tasks;
using UnityEngine;

public static class BootSystem
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeOnBeforeSplashScreen()
    {
        HK.Framework.BootSystems.BootSystem.AdditionalSetupAsync += () =>
        {
            Debug.Log("BootSystem.AdditionalSetupAsync");
            return UniTask.CompletedTask;
        };
    }
}
