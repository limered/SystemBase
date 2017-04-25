using JetBrains.Annotations;
using UnityEngine;

namespace Assets.SystemBase
{
    public interface IGameComponent
    {
        GameObject gameObject { get; }
    }
}
