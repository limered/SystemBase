using System;
using System.Collections.Generic;
using System.Linq;
using SystemBase;
using SystemBase.StateMachineBase;
using Systems.GameState.States;
using Utils;

namespace Systems
{
    public class Game : GameBase
    {
        public StateContext<Game> GameStateContext;
        private void Awake()
        {
            IoC.RegisterSingleton(this);

            GameStateContext = new StateContext<Game>();
            GameStateContext.Start(new Loading());

            foreach (var systemType in CollectAllSystems())
            {
                RegisterSystem(Activator.CreateInstance(systemType) as IGameSystem);
            }

            Init();

            GameStateContext.GoToState(new StartScreen());
        }

        private static IEnumerable<Type> CollectAllSystems()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(ass => ass.GetTypes(), (ass, type) => new { ass, type })
                .Where(assemblyType => Attribute.IsDefined(assemblyType.type, typeof(GameSystemAttribute)))
                .Select(assemblyType => assemblyType.type);
        }
    }
}