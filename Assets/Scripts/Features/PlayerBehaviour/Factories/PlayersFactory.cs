using System.Collections.Generic;
using GTN.Features.GameScreen;
using UnityEngine;
using UnityEngine.Scripting;
using Zenject;

namespace GTN.Features.PlayerBehaviour
{
    [Preserve]
    public class PlayersFactory
    {
        private readonly IFactory<PlayerType, Transform, AvatarIconView> _avatarIconProvider;
        private readonly IFactory<AvatarIconView, IAIPlayerBehaviour> _aiPlayerBehaviourProvider;
        private readonly IFactory<AvatarIconView, IRealPlayerBehaviour> _realPlayerBehaviourProvider;

        public PlayersFactory(IFactory<PlayerType, Transform, AvatarIconView> avatarIconProvider,
            IFactory<AvatarIconView, IAIPlayerBehaviour> aiPlayerBehaviourProvider, 
            IFactory<AvatarIconView, IRealPlayerBehaviour> realPlayerBehaviourProvider)
        {
            _avatarIconProvider = avatarIconProvider;
            _aiPlayerBehaviourProvider = aiPlayerBehaviourProvider;
            _realPlayerBehaviourProvider = realPlayerBehaviourProvider;
        }
        
        public Queue<IPlayerBehaviour> CreatePlayerBehaviours((int ai, int real) playersCount, Transform avatarParent)
        {
            var players = new Queue<IPlayerBehaviour>(playersCount.real + playersCount.ai);

            for (var index = 0; index < playersCount.real; index++)
            {
                var playerIcon = _avatarIconProvider.Create(PlayerType.Real, avatarParent);
                
                players.Enqueue(_realPlayerBehaviourProvider.Create(playerIcon));
            }
            
            for (var index = 0; index < playersCount.ai; index++)
            {
                var playerIcon = _avatarIconProvider.Create(PlayerType.AI, avatarParent);

                players.Enqueue(_aiPlayerBehaviourProvider.Create(playerIcon));
            }

            return players;
        }

    }
}