using Assets.SystemBase;
using UnityEngine;

namespace Assets.Systems.Example
{
    [RequireComponent(typeof(Rigidbody))]
    public class FunnyMovementComponent : GameComponent
    {
        public void Move(Vector3 direction)
        {
            
        }
    }
}