using SystemBase.Utils;

namespace SystemBase.Adapter
{
    public class IoCAdapterRegistrations : IIocRegistration
    {
        public void Register()
        {
            IoC.RegisterType<ISystemRandom, SystemRandomAdapter>();
            IoC.RegisterType<IMathf, MathfAdapter>();
        }
    }
}