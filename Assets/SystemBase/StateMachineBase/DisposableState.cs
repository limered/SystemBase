
using System;
using System.Collections.ObjectModel;
using UniRx;

namespace SystemBase.StateMachineBase
{
    public abstract class DisposableState : IState, IDisposable
    {
        public readonly CompositeDisposable Life = new CompositeDisposable();

        public abstract ReadOnlyCollection<Type> ValidNextStates { get; }
        public abstract bool Enter<TState>(IStateContext<TState> context) where TState : IState;
        public virtual void Exit()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (Life.IsDisposed) return;
            
            Life.Dispose();
        }
    }

    public static class DisposableStateExtensions 
    {
        public static T AddTo<T>(this T dis, DisposableState state) where T : IDisposable
        {
            state.Life.Add(dis);
            return dis;
        }
    }
}
