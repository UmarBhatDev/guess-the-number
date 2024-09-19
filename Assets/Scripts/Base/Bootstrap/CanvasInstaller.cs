using GTN.Base.Data;
using UnityEngine;
using Zenject;

namespace GTN.Base.Bootstrap
{
    public class CanvasInstaller : Installer
    {
        private readonly ViewRegistry _viewRegistry;

        public CanvasInstaller(ViewRegistry viewRegistry)
        {
            _viewRegistry = viewRegistry;
        }

        public override void InstallBindings()
        {
            var canvas = Container.InstantiatePrefab(_viewRegistry.GameplayCanvas);
            var canvasData = new CanvasData(canvas.GetComponent<Canvas>());
            
            Container.Bind<CanvasData>().FromInstance(canvasData).AsSingle();
        }
    }
}