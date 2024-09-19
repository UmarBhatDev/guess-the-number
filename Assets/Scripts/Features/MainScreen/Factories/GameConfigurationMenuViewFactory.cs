using GTN.Base.Data;
using UnityEngine.Scripting;
using Zenject;

namespace GTN.Features.MainScreen
{
    [Preserve]
    public class GameConfigurationMenuViewFactory : IFactory<GameConfigurationMenuView>
    {
        private readonly CanvasData _canvasData;
        private readonly DiContainer _diContainer;
        private readonly ViewRegistry _viewRegistry;

        public GameConfigurationMenuViewFactory(ViewRegistry viewRegistry, DiContainer container, CanvasData canvasData)
        {
            _canvasData = canvasData;
            _diContainer = container;
            _viewRegistry = viewRegistry;
        }

        public GameConfigurationMenuView Create()
        {
            return _diContainer.InstantiatePrefabForComponent<GameConfigurationMenuView>(_viewRegistry.GameConfigurationMenuPanel, _canvasData.Canvas.transform);
        }
    }
}