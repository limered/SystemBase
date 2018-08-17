using System.Linq;
using UniRx;

namespace SystemBase.StateMachineBase
{
    public class StateContext<T> : IStateContext<BaseState<T>, T>
    {
        private readonly ReactiveCommand<BaseState<T>> _afterStateChange;
        private readonly ReactiveCommand<BaseState<T>> _bevoreStateChange;

        public StateContext()
        {
            _bevoreStateChange = new ReactiveCommand<BaseState<T>>();
            _afterStateChange = new ReactiveCommand<BaseState<T>>();
        }

        public ReactiveCommand<BaseState<T>> AfterStateChange
        {
            get { return _afterStateChange; }
        }

        public ReactiveCommand<BaseState<T>> BevoreStateChange
        {
            get { return _bevoreStateChange; }
        }

        public ReactiveProperty<BaseState<T>> CurrentState { get; private set; }

        public bool GoToState(BaseState<T> state)
        {
            try
            {
                if (CurrentState.Value.ValidNextStates.All(st => st != state.GetType())) return false;

                _bevoreStateChange.Execute(state);
                CurrentState.Value.Exit();
                CurrentState.Value = state;
                return CurrentState.Value.Enter(this);
            }
            finally
            {
                _afterStateChange.Execute(state);
            }
        }

        public void Start(BaseState<T> initialState)
        {
            CurrentState = new ReactiveProperty<BaseState<T>>(initialState);
            CurrentState.Value.Enter(this);
        }
    }
}