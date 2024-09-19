using System;
using Cysharp.Threading.Tasks;
using GTN.Base.FSM;
using GTN.Features.SceneTransitions;
using UnityEngine.Scripting;

namespace GTN.Features.MainScreen
{
    [Preserve]
    public class MainScreenState : IGameState<MainScreenState.Payload>
    {
        private const string SCENE_NAME = "MainScreenScene";

        private readonly CurtainViewFactory _curtainViewFactory;
        private readonly MainScreenControllerFactory _mainScreenControllerFactory;

        private IDisposable _mainScreenDisposable;

        public MainScreenState(MainScreenControllerFactory mainScreenControllerFactory, 
            CurtainViewFactory curtainViewFactory)
        {
            _mainScreenControllerFactory = mainScreenControllerFactory;
            _curtainViewFactory = curtainViewFactory;
        }

        public async UniTaskVoid Enter(Payload payload)
        {
            var curtainType = payload.CurtainOverride ?? CurtainType.DefaultCurtain;

            if (payload.CurtainOverride != null)
                await Transition.ToScene(SCENE_NAME, _curtainViewFactory, curtainType);

            var mainMenuController = _mainScreenControllerFactory.Create();
            
            _mainScreenDisposable = mainMenuController;

            mainMenuController.Initialize();
        }
        
        public void Exit()
        {
            _mainScreenDisposable?.Dispose();
        }
        
        public struct Payload
        {
            public CurtainType? CurtainOverride { get; set; }
        }
    }
}