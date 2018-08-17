using System;
using System.Collections.ObjectModel;

namespace SystemBase.StateMachineBase
{
    public interface IState
    {
        ReadOnlyCollection<Type> ValidNextStates { get; }
        bool Enter<TState>(IStateContext<TState> context) where TState : IState;
        void Exit();
    }
}
