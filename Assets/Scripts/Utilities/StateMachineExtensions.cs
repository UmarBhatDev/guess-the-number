using GTN.Base.FSM;
using GTN.Features.GameScreen;
using GTN.Features.MainScreen;
using GTN.Features.SceneTransitions;

namespace GTN.Utilities
{
    public static class StateMachineExtensions
    {
        public static void GoBootstrap(this IStateMachine stateMachine)
            => stateMachine.EnterState<BootstrapState>();

        public static void GoMainMenu(this IStateMachine stateMachine, CurtainType? curtainType = null)
            => stateMachine.EnterState<MainScreenState, MainScreenState.Payload>(new MainScreenState.Payload
            {
                CurtainOverride = curtainType,
            });

        public static void GoGame(this IStateMachine stateMachine, (int from, int to) numbersRange,
            (int ai, int real) playersCount, CurtainType? curtainType = null)
            => stateMachine.EnterState<GameState, GameState.Payload>(new GameState.Payload
            {
                CurtainOverride = curtainType,
                NumbersRange = numbersRange,
                PlayersCount = playersCount,
            });
    }
}