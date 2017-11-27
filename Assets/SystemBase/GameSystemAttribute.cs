using System;

namespace SystemBase
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GameSystemAttribute : Attribute
    {
        public IGameSystem[] Dependencies { get; set; }

        public GameSystemAttribute(params IGameSystem[] dependencies)
        {
            Dependencies = dependencies;
        }

        public GameSystemAttribute()
        {
        }
    }
}
