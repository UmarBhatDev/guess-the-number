using GTN.Features.GameScreen;
using UnityEngine.Scripting;
using Zenject;

namespace GTN.Features.PlayerBehaviour
{
    [Preserve]
    public class AIPlayerBehaviourProvider : IFactory<AvatarIconView, IAIPlayerBehaviour>
    {
        private readonly PlayersInputModel _playersInputModel;

        public AIPlayerBehaviourProvider(PlayersInputModel playersInputModel)
        {
            _playersInputModel = playersInputModel;
        }

        public IAIPlayerBehaviour Create(AvatarIconView avatarIconView)
        {
            return new AIPlayerBehaviour(avatarIconView, _playersInputModel);
        }
    }
}