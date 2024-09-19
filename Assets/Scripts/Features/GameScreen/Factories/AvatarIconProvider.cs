using GTN.Base.Data;
using GTN.Features.PlayerBehaviour;
using UnityEngine;
using UnityEngine.Scripting;
using Zenject;

namespace GTN.Features.GameScreen
{
    [Preserve]
    public class AvatarIconProvider : IFactory<PlayerType, Transform, AvatarIconView>
    {
        private readonly DiContainer _diContainer;
        private readonly ViewRegistry _viewRegistry;
        private readonly PlayerAvatarIconRegistry _playerAvatarIconRegistry;

        public AvatarIconProvider(ViewRegistry viewRegistry, DiContainer container, 
            PlayerAvatarIconRegistry playerAvatarIconRegistry)
        {
            _diContainer = container;
            _viewRegistry = viewRegistry;
            _playerAvatarIconRegistry = playerAvatarIconRegistry;
        }

        public AvatarIconView Create(PlayerType playerType, Transform parent)
        {
            var view = _diContainer.InstantiatePrefabForComponent<AvatarIconView>(_viewRegistry.AvatarIconView, parent);

            var sprite = _playerAvatarIconRegistry.GetRandomIcon(playerType);
            
            view.SetSprite(sprite);

            return view;
        }
    }
}