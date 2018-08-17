using System;
using System.Collections.ObjectModel;
using UniRx;

namespace SystemBase.StateMachineBase
{
    public abstract class BaseState<T> : IState<T>, IDisposable
    {
        public readonly CompositeDisposable StateDisposables = new CompositeDisposable();

        public abstract ReadOnlyCollection<Type> ValidNextStates { get; }

        public void Dispose()
        {
            if (StateDisposables.IsDisposed) return;

            StateDisposables.Dispose();
        }

        public abstract bool Enter(StateContext<T> context);

        public bool Enter(IStateContext<IState<T>, T> context)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            return Enter(context as StateContext<T>);
        }

        public virtual void Exit()
        {
            Dispose();
        }
    }
}