using System;
using System.Collections.ObjectModel;
using UniRx;

namespace SystemBase.StateMachineBase
{
    public abstract class BaseState<T> : IState<T>, IDisposable
    {
        public readonly CompositeDisposable StateDisposables = new CompositeDisposable();

        protected BaseState()
        {
            var definedAttribute = Attribute.GetCustomAttribute(GetType(), typeof(NextValidStatesAttribute)) as NextValidStatesAttribute;
            if (definedAttribute != null)
            {
                ValidNextStates = new ReadOnlyCollection<Type>(definedAttribute.ValidStateChanges);
            }
        }

        public ReadOnlyCollection<Type> ValidNextStates { get; private set; }

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