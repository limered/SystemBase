using System;
using System.Collections.Generic;
using System.Linq;
using SystemBase;
using Systems.GameState.States;
using Utils;

namespace Systems
{
    public class Game : GameBase
    {
        private void Awake()
        {
            IoC.RegisterSingleton(this);

            foreach (var systemType in CollectAllSystems())
            {
                RegisterSystem(Activator.CreateInstance(systemType) as IGameSystem);
            }

            Init();
        }

        private static IEnumerable<Type> CollectAllSystems()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(ass => ass.GetTypes(), (ass, type) => new {ass, type})
                .Where(assemblyType => Attribute.IsDefined(assemblyType.type, typeof(GameSystemAttribute)))
                .Select(assemblyType => assemblyType.type);
        }
    }
}