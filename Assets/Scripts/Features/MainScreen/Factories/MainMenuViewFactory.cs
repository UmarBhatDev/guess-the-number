using GTN.Base.Data;
using UnityEngine.Scripting;
using Zenject;

namespace GTN.Features.MainScreen
{
    [Preserve]
    public class MainMenuViewFactory : IFactory<MainMenuView>
    {
        private readonly CanvasData _canvasData;
        private readonly DiContainer _diContainer;
        private readonly ViewRegistry _viewRegistry;

        public MainMenuViewFactory(ViewRegistry viewRegistry, DiContainer container, CanvasData canvasData)
        {
            _canvasData = canvasData;
            _diContainer = container;
            _viewRegistry = viewRegistry;
        }

        public MainMenuView Create()
        {
            return _diContainer.InstantiatePrefabForComponent<MainMenuView>(_viewRegistry.MainMenuPanel, _canvasData.Canvas.transform);
        }
    }
}