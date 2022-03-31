using SystemBase.Utils;

namespace SystemBaseTests.Utils.IoC
{
    public class TestAutomaticIoCRegistration : IIocRegistration
    {
        public void Register()
        {
            SystemBase.Utils.IoC.RegisterType<AutomaticRegistrationTest>();
        }
    }
    
    // ReSharper disable once ClassNeverInstantiated.Global
    public class AutomaticRegistrationTest{}
}