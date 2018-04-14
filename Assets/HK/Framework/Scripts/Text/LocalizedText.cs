using UnityEngine;

namespace HK.Framework.Text
{
    /// <summary>
    /// <see cref="Text"/>にローカライズ済みのテキストを設定する
    /// </summary>
    public sealed class LocalizedText : MonoBehaviour
    {
        [SerializeField]
        private UnityEngine.UI.Text text;

        [SerializeField]
        private StringAsset.Finder finder;
        
        void Start()
        {
            this.Apply();
        }

        void OnValidate()
        {
            if (this.text == null || !this.finder.IsValid)
            {
                return;
            }
            
            this.Apply();
        }

        public void Apply()
        {
            this.text.text = this.finder.Get;
        }
    }
}
