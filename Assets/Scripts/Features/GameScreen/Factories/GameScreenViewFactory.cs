using GTN.Base.Data;
using UnityEngine.Scripting;
using Zenject;

namespace GTN.Features.GameScreen
{
    [Preserve]
    public class GameScreenViewFactory : IFactory<GameScreenView>
    {
        private readonly CanvasData _canvasData;
        private readonly DiContainer _diContainer;
        private readonly ViewRegistry _viewRegistry;

        public GameScreenViewFactory(ViewRegistry viewRegistry, DiContainer container, CanvasData canvasData)
        {
            _canvasData = canvasData;
            _diContainer = container;
            _viewRegistry = viewRegistry;
        }

        public GameScreenView Create()
        {
            return _diContainer.InstantiatePrefabForComponent<GameScreenView>(_viewRegistry.GameScreenView, _canvasData.Canvas.transform);
        }
    }
}