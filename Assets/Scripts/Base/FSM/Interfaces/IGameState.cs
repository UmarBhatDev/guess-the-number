using Cysharp.Threading.Tasks;

namespace GTN.Base.FSM
{
    public interface IGameState : IStateBase
    {
        void Enter();
    }

    public interface IGameState<in TPayload> : IStateBase
        where TPayload : struct
    {
        UniTaskVoid Enter(TPayload payload);
    }
}