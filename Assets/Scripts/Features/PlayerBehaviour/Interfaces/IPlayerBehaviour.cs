using System.Threading;
using Cysharp.Threading.Tasks;

namespace GTN.Features.PlayerBehaviour
{
    public interface IPlayerBehaviour
    {
        UniTask<int> AskMovementResult((int from, int to) guessRange, CancellationToken cancellationToken);
        UniTask RaiseHighlight(CancellationToken cancellationToken);
        UniTask HideHighlight(CancellationToken cancellationToken);
        UniTask Celebrate(CancellationToken cancellationToken);
    }
}