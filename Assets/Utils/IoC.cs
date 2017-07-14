using System;
using System.Collections.Generic;
using Assets.SystemBase;

namespace Assets.Utils
{
    public static class IoC
    {
        private static readonly Dictionary<Type, object> Singletons = new Dictionary<Type, object>();
        static IoC()
        {
        }

        public static Game Game { get { return Resolve<Game>(); } }
        public static TSingleton Resolve<TSingleton>()
        {
            var yes = false;

            if (Singletons.ContainsKey(typeof(TSingleton)) && Singletons[typeof(TSingleton)] is Func<TSingleton>)
            {
                Singletons[typeof(TSingleton)] = ((Func<TSingleton>)Singletons[typeof(TSingleton)])();
                yes = true;
            }
            if (yes || Singletons.ContainsKey(typeof(TSingleton)))
                return (TSingleton)Singletons[typeof(TSingleton)];
            throw new KeyNotFoundException("unknown interface: " + typeof(TSingleton).FullName);
        }

        public static TSingleton ResolveOrDefault<TSingleton>()
        {
            var yes = false;

            if (Singletons.ContainsKey(typeof(TSingleton)) && Singletons[typeof(TSingleton)] is Func<TSingleton>)
            {
                Singletons[typeof(TSingleton)] = ((Func<TSingleton>)Singletons[typeof(TSingleton)])();
                yes = true;
            }
            if (yes || Singletons.ContainsKey(typeof(TSingleton)))
                return (TSingleton)Singletons[typeof(TSingleton)];
            return default(TSingleton);
        }

        public static void RegisterSingleton<TSingleton>(TSingleton singletonObject)
        {
            if (Singletons.ContainsKey(typeof(TSingleton)))
            {
                Singletons[typeof(TSingleton)] = singletonObject;
            }
            else
            {
                Singletons.Add(typeof(TSingleton), singletonObject);
            }
        }

        public static void RegisterSingleton<TSingleton>(Func<TSingleton> singletonObject)
        {
            if (Singletons.ContainsKey(typeof(TSingleton)))
            {
                Singletons[typeof(TSingleton)] = singletonObject;
            }
            else
            {
                Singletons.Add(typeof(TSingleton), singletonObject);
            }
        }
    }
}