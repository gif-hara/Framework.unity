using Cysharp.Threading.Tasks;
using UnityEngine;

namespace HK.Framework.UISystems
{
    /// <summary>
    /// UIを表すクラス
    /// </summary>
    public abstract class UIView<T> : MonoBehaviour where T : UIView<T>
    {
        public T Register()
        {
            return UIManager.Register(this as T);
        }
        
        public void Unregister()
        {
            UIManager.Unregister(this);
        }
        
        public virtual UniTask ShowAsync()
        {
            this.gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public virtual UniTask HideAsync()
        {
            this.gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }
        
        public virtual void ShowImmediate()
        {
            this.gameObject.SetActive(true);
        }
        
        public virtual void HideImmediate()
        {
            this.gameObject.SetActive(false);
        }
    }
}
