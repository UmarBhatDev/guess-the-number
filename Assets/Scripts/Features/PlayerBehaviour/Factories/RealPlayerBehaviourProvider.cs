using GTN.Features.GameScreen;
using GTN.Features.InGameKeyboard;
using UnityEngine.Scripting;
using Zenject;

namespace GTN.Features.PlayerBehaviour
{
    [Preserve]
    public class RealPlayerBehaviourProvider : IFactory<AvatarIconView, IRealPlayerBehaviour>
    {
        private readonly IInGameKeyboardService _inGameKeyboardService;

        public RealPlayerBehaviourProvider(IInGameKeyboardService inGameKeyboardService)
        {
            _inGameKeyboardService = inGameKeyboardService;
        }

        public IRealPlayerBehaviour Create(AvatarIconView avatarIconView)
        {
            return new RealPlayerBehaviour(avatarIconView, _inGameKeyboardService);
        }
    }
}