using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Assets.SystemBase
{
    public abstract class GameSystem<TComponent1, TComponent2, TComponent3, TComponent4>
        : GameSystem<TComponent1, TComponent2, TComponent3>
        where TComponent1 : IGameComponent
        where TComponent2 : IGameComponent
        where TComponent3 : IGameComponent
        where TComponent4 : IGameComponent
    {
        public override Type[] ComponentsToRegister { get { return new[] { typeof(TComponent1), typeof(TComponent2), typeof(TComponent3), typeof(TComponent4) }; } }

        public abstract void Register(TComponent4 component);
    }

    public abstract class GameSystem<TComponent1, TComponent2, TComponent3>
        : GameSystem<TComponent1, TComponent2>
        where TComponent1 : IGameComponent
        where TComponent2 : IGameComponent
        where TComponent3 : IGameComponent
    {
        public override Type[] ComponentsToRegister { get { return new[] { typeof(TComponent1), typeof(TComponent2), typeof(TComponent3) }; } }

        public abstract void Register(TComponent3 component);
    }

    public abstract class GameSystem<TComponent1, TComponent2>
        : GameSystem<TComponent1>
        where TComponent1 : IGameComponent
        where TComponent2 : IGameComponent
    {
        public override Type[] ComponentsToRegister { get { return new[] { typeof(TComponent1), typeof(TComponent2) }; } }

        public abstract void Register(TComponent2 component);
    }

    public abstract class GameSystem<TComponent> : IGameSystem where TComponent : IGameComponent
    {
        private Dictionary<Type, Action<IGameComponent>> _registerMethods;
        public abstract int Priority { get; }

        public virtual Type[] ComponentsToRegister { get { return new[] { typeof(TComponent) }; } }
        public virtual void Init() { }

        public void RegisterComponent(IGameComponent component)
        {
            if (_registerMethods == null)
            {
                _registerMethods = new Dictionary<Type, Action<IGameComponent>>();
                MethodInfo[] methods = GetType().GetMethods();
                foreach (var m in methods)
                {
                    if (m.Name != "Register" || m.GetParameters().Length != 1) continue;

                    //Debug.Log(GetType().Name + ": found Register(" + m.GetParameters()[0].ParameterType.Name + ")");
                    // ReSharper disable once AccessToForEachVariableInClosure
                    _registerMethods.Add(m.GetParameters()[0].ParameterType, c => m.Invoke(this, new object[] { c }));
                }
            }

            if (_registerMethods.ContainsKey(component.GetType())) _registerMethods[component.GetType()](component);
            else Debug.LogError(GetType().Name + ": No Register-Method found for '" + component.GetType().Name + "'");
        }

        public abstract void Register(TComponent component);
    }
}