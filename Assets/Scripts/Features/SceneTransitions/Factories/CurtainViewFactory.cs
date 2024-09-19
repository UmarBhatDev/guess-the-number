using GTN.Base.Data;
using UnityEngine.Scripting;
using Zenject;

namespace GTN.Features.SceneTransitions
{
    [Preserve]
    public class CurtainViewFactory : IFactory<CurtainType, CurtainViewBase>
    {
        private readonly DiContainer _container;
        private readonly CanvasData _canvasData;
        private readonly CurtainRegistry _curtainRegistry;

        public CurtainViewFactory(DiContainer container, CurtainRegistry curtainRegistry, CanvasData canvasData)
        {
            _container = container;
            _canvasData = canvasData;
            _curtainRegistry = curtainRegistry;
        }

        public CurtainViewBase Create(CurtainType curtainType)     
        {
            return _container.InstantiatePrefabForComponent<CurtainViewBase>(_curtainRegistry.GetCurtainByType(curtainType));
        }
    }
}