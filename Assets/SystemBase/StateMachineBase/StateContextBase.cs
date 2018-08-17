using System.Linq;
using UniRx;

namespace SystemBase.StateMachineBase
{
    public class StateContextBase<TState> : IStateContext<TState> where TState : IState
    {
        public StateContextBase(TState initialState)
        {
            CurrentState = new ReactiveProperty<TState>(initialState);
        }

        public ReactiveProperty<TState> CurrentState { get; private set; }
        public bool GoToState(TState state)
        {
            try
            {
                if (CurrentState.Value.ValidNextStates.All(st => st != state.GetType())) return false;

                BeforeStateChange(state);
                CurrentState.Value.Exit();
                CurrentState.Value = state;
                return CurrentState.Value.Enter(this);
            }
            finally
            {
                AfterStateChange(state);
            }
        }

        protected virtual void BeforeStateChange(TState state) { }
        protected virtual void AfterStateChange(TState state) { }
    }
}