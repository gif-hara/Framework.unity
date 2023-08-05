using System.Threading;
using Cysharp.Threading.Tasks;

namespace HK.Framework.UISystems
{
    /// <summary>
    /// <see cref="IUIPresenter"/>の抽象クラス
    /// </summary>
    public abstract class UIPresenter : IUIPresenter
    {
        public async UniTask PresentationAsync(CancellationToken cancellationToken = default)
        {
            await UIManager.RegisterPresenter(this, cancellationToken);
        }
        
        /// <summary>
        /// プレゼンテーションを実行する
        /// </summary>
        /// <remarks>
        /// このクラスを継承したクラスで実装してください
        /// </remarks>
        protected virtual UniTask PresentationInternalAsync(CancellationToken cancellationToken = default)
        {
            return UniTask.CompletedTask;
        }

        UniTask IUIPresenterManagerMediator.InvokeAsync(CancellationToken cancellationToken)
        {
            return this.PresentationInternalAsync(cancellationToken);
        }
    }
}
