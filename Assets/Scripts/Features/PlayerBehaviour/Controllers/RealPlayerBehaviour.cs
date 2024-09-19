using System.Threading;
using Cysharp.Threading.Tasks;
using GTN.Features.GameScreen;
using GTN.Features.InGameKeyboard;
using UnityEngine.Scripting;

namespace GTN.Features.PlayerBehaviour
{
    [Preserve]
    public class RealPlayerBehaviour : IRealPlayerBehaviour
    {
        private readonly AvatarIconView _avatarIconView;
        private readonly IInGameKeyboardService _inGameKeyboardService;

        public RealPlayerBehaviour(AvatarIconView avatarIconView, IInGameKeyboardService inGameKeyboardService)
        {
            _avatarIconView = avatarIconView;
            _inGameKeyboardService = inGameKeyboardService;
        }

        public async UniTask<int> AskMovementResult((int from, int to) guessRange, CancellationToken cancellationToken)
        {
            await UniTask.WaitForSeconds(1, cancellationToken: cancellationToken);
            return await _inGameKeyboardService.GetNumberFromKeyboard(cancellationToken);
        }

        public async UniTask RaiseHighlight(CancellationToken cancellationToken)
        {
            await _avatarIconView.Raise(cancellationToken);
        }
        
        public async UniTask HideHighlight(CancellationToken cancellationToken)
        {
            await _avatarIconView.Hide(cancellationToken);
        }

        public async UniTask Celebrate(CancellationToken cancellationToken)
        {
            await RaiseHighlight(cancellationToken);
        }
    }
}