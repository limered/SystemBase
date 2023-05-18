using SystemBase.Core.Components;
using Unity.Mathematics;
using UnityEngine.InputSystem.EnhancedTouch;

namespace SystemBase.CommonSystems.control
{
    [SingletonComponent]
    public class ControlledByPlayerComponent : GameComponent
    {
        public float2 stickSize = new float2(1, 1) * 300f;
        public FloatingJoystickComponent joystick;
        public float2 deadZone = new float2(1, 1) * 0.1f;

        public Finger Finger { get; set; }
        public float2 MovementAmount { get; set; }
    }
}