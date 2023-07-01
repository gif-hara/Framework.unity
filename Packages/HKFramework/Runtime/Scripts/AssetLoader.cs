using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace HK.Framework
{
    /// <summary>
    /// Addressableでアセットをロードするクラス
    /// </summary>
    public static class AssetLoader
    {
        public static async UniTask<T> LoadAsync<T>(string path)
        {
            var handle = Addressables.LoadAssetAsync<T>(path);
            await handle.ToUniTask();
            return handle.Result;
        }
    }
}
