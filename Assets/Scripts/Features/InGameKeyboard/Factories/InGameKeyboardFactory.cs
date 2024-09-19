using GTN.Base.Data;
using UnityEngine.Scripting;
using Zenject;

namespace GTN.Features.InGameKeyboard
{
    [Preserve]
    public class InGameKeyboardFactory : IFactory<InGameKeyboardView>
    {
        private readonly CanvasData _canvasData;
        private readonly DiContainer _diContainer;
        private readonly ViewRegistry _viewRegistry;

        public InGameKeyboardFactory(CanvasData canvasData, DiContainer diContainer, ViewRegistry viewRegistry)
        {
            _canvasData = canvasData;
            _diContainer = diContainer;
            _viewRegistry = viewRegistry;
        }

        public InGameKeyboardView Create()
        {
            return _diContainer.InstantiatePrefabForComponent<InGameKeyboardView>(_viewRegistry.InGameKeyboardView,
                _canvasData.Canvas.transform);

        }
    }
}