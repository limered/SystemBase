using UniRx;

namespace SystemBase.StateMachineBase
{
    public interface IStateContext<TState> where TState : IState
    {
        ReactiveProperty<TState> CurrentState { get; }
        
        bool GoToState(TState state);
    }
}