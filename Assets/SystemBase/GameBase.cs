using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace SystemBase
{
    public class GameBase : MonoBehaviour, IGameSystem
    {
        public StringReactiveProperty DebugMainFrameCallback = new StringReactiveProperty();
        private readonly Dictionary<Type, IGameSystem> _gameSystemDict = new Dictionary<Type, IGameSystem>();
        private readonly List<IGameSystem> _gameSystems = new List<IGameSystem>();
        private readonly Dictionary<Type, List<IGameSystem>> _systemToComponentMapper = new Dictionary<Type, List<IGameSystem>>();

        public int Priority { get { return -1; } }
        public Type[] ComponentsToRegister { get { return new Type[0]; } }

        public virtual void Init()
        {
            MapAllSystemsComponents();

            DebugMainFrameCallback.ObserveOnMainThread().Subscribe(OnDebugCallbackCalled);
        }

        public void RegisterComponent(IGameComponent component)
        {
            List<IGameSystem> systemsToRegisterTo;
            if (!_systemToComponentMapper.TryGetValue(component.GetType(), out systemsToRegisterTo)) return;

            foreach (var system in systemsToRegisterTo)
            {
                system.RegisterComponent(component);
            }
        }

        public T System<T>() where T : IGameSystem
        {
            IGameSystem system;
            if (_gameSystemDict.TryGetValue(typeof(T), out system))
                return (T)system;
            throw new ArgumentException("System: " + typeof(T) + " not registered!");
        }

        protected virtual void OnDebugCallbackCalled(string s)
        {
            print(s);
        }

        protected void RegisterSystem<T>(T system) where T : IGameSystem
        {
            _gameSystems.Add(system);
            _gameSystemDict.Add(typeof(T), system);
        }

        private void MapAllSystemsComponents()
        {
            IOrderedEnumerable<IGameSystem> orderedSystems = _gameSystems.OrderBy(system => system.Priority);

            foreach (var system in orderedSystems)
            {
                foreach (var componentType in system.ComponentsToRegister)
                {
                    MapSystemToComponent(system, componentType);
                }

                system.Init();
            }
        }

        private void MapSystemToComponent(IGameSystem system, Type componentType)
        {
            if (!_systemToComponentMapper.ContainsKey(componentType))
            {
                _systemToComponentMapper.Add(componentType, new List<IGameSystem>());
            }
            _systemToComponentMapper[componentType].Add(system);
        }
    }
}
