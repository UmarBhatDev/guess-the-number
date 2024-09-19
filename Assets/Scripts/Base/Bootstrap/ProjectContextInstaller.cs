using GTN.Base.FSM;
using GTN.Features.GameScreen;
using GTN.Features.InGameKeyboard;
using GTN.Features.MainScreen;
using GTN.Features.PlayerBehaviour;
using GTN.Features.SceneTransitions;
using Zenject;

namespace GTN.Base.Bootstrap
{
    public class ProjectContextInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Install<PlayerBehaviourInstaller>();
            Container.Install<InGameKeyboardInstaller>();
            Container.Install<StateMachineInstaller>();
            Container.Install<GameScreenInstaller>();
            Container.Install<MainScreenInstaller>();
            Container.Install<CurtainInstaller>();
            Container.Install<CanvasInstaller>();
        }
    }
}