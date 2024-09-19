using GTN.Utilities;
using UnityEngine.Scripting;

namespace GTN.Base.FSM
{
    [Preserve]
    public class BootstrapState : IGameState
    {
        private readonly IStateMachine _stateMachine;

        public BootstrapState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _stateMachine.GoMainMenu();
        }

        public void Exit()
        {
        }
    }
}