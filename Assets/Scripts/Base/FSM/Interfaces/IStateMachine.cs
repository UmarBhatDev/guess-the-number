namespace GTN.Base.FSM
{
    public interface IStateMachine
    {
        void EnterState<TState>()
            where TState : class, IGameState;

        void EnterState<TState, TPayload>(TPayload payload)
            where TState : class, IGameState<TPayload>
            where TPayload : struct;
    }
}