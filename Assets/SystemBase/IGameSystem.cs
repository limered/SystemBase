using System;

namespace Assets.SystemBase
{
    public interface IGameSystem
    {
        int Priority { get; }

        Type[] ComponentsToRegister { get; }

        void Init();

        void RegisterComponent(IGameComponent component);
    }
}
