using System.Threading;
using Cysharp.Threading.Tasks;

namespace HK.Framework.UISystems
{
    /// <summary>
    /// UIとユーザーを繋ぎこむPresenterのインターフェイス
    /// </summary>
    public interface IUIPresenter : IUIPresenterManagerMediator
    {
        /// <summary>
        /// UIとユーザーとの繋ぎ込みを行う
        /// </summary>
        UniTask PresentationAsync(CancellationToken cancellationToken = default);
    }

    public interface IUIPresenterManagerMediator
    {
        /// <summary>
        /// <see cref="UIManager"/>が呼び出す実際の実行処理
        /// </summary>
        UniTask InvokeAsync(CancellationToken cancellationToken = default);
    }
}
