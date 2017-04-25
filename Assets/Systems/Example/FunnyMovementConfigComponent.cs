using Assets.SystemBase;
using UniRx;

namespace Assets.Systems.Example
{
    public class FunnyMovementConfigComponent : GameComponent
    {
        public FloatReactiveProperty Speed = new FloatReactiveProperty(10);
    }
}