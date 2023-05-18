using SystemBase.Core.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace SystemBase.CommonSystems.control
{
    public class FloatingJoystickComponent : GameComponent
    {
        [FormerlySerializedAs("transform")] public RectTransform rectTransform;
        public RectTransform knob;
    }
}