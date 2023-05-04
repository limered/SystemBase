﻿using ExampleSystems.Example;
using SystemBase.Core.GameSystems;

namespace ExampleSystems.DependencyExample
{
    [GameSystem(typeof(DependencySystemMaster))]
    public class DependencySystemOne : GameSystem<FunnyMovementComponent>
    {
        public override void Register(FunnyMovementComponent component)
        {
        }
    }
}