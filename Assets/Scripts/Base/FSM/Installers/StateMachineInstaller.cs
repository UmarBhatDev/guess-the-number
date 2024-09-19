using GTN.Utilities;
using UnityEngine.Scripting;
using Zenject;

namespace GTN.Base.FSM
{
    [Preserve]
    public class StateMachineInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallService<StateMachine>();

            Container
                .Bind<IStateBase>()
                .To(x => x.AllClasses().DerivingFrom<IStateBase>()).AsSingle();
        }
    }
}