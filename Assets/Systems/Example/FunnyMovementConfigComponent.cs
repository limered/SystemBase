using SystemBase;
using UniRx;

namespace Systems.Example
{
    public class FunnyMovementConfigComponent : GameComponent
    {
        public FloatReactiveProperty Speed = new FloatReactiveProperty(10);
    }
}