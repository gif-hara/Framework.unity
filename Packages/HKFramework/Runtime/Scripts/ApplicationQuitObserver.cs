using UnityEngine;

namespace HK.Framework
{
    /// <summary>
    /// ApplicationQuitを監視するクラス
    /// </summary>
    /// <remarks>
    /// よくあるシーン再生終了したら後処理でエラーになる問題を解決するために作成
    /// </remarks>
    public static class ApplicationQuitObserver
    {
        public static bool IsQuit { get; private set; }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            Application.quitting -= OnApplicationQuit;
            Application.quitting += OnApplicationQuit;
            IsQuit = false;
        }
        
        private static void OnApplicationQuit()
        {
            IsQuit = true;
        }
    }
}
