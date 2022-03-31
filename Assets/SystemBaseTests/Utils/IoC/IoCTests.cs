using NUnit.Framework;

namespace SystemBaseTests.Utils.IoC
{
    public class IoCTests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            SystemBase.Utils.IoC.Reset();
            SystemBase.Utils.IoC.RegisterType<ITestClass, TestClass>();
            SystemBase.Utils.IoC.RegisterType<TestClass>();
            SystemBase.Utils.IoC.RegisterSingleton<ITestSingletonWithInterface, TestSingletonWithInterface>();
            SystemBase.Utils.IoC.RegisterSingleton(new TestSingletonWithoutInterface());
            SystemBase.Utils.IoC.RegisterSingleton<ITestSingletonWithInstance>(new TestSingletonWithInstance());
            SystemBase.Utils.IoC.RegisterSingleton<ITestSingletonWithLazyInit>(()=>new TestSingletonWithLazyInit());
        }
        
        [Test]
        public void RegisterType_Interfaced_ResolvesNewInstanceEveryTime()
        {
            var instance1 = SystemBase.Utils.IoC.Resolve<ITestClass>();
            var instance2 = SystemBase.Utils.IoC.Resolve<ITestClass>();
            
            Assert.AreNotEqual(instance1, instance2);
        }
        
        [Test]
        public void RegisterType_NotInterfaced_ResolvesNewInstanceEveryTime()
        {
            var instance1 = SystemBase.Utils.IoC.Resolve<TestClass>();
            var instance2 = SystemBase.Utils.IoC.Resolve<TestClass>();
            
            Assert.AreNotEqual(instance1, instance2);
        }

        [Test]
        public void Overwrite_OverwritesTypeWithNewInstance()
        {
            var instance1 = SystemBase.Utils.IoC.Resolve<TestClass>();
            
            var overwriteInstance = new TestClass();
            SystemBase.Utils.IoC.Overwrite<TestClass>(overwriteInstance);
            var instance2 = SystemBase.Utils.IoC.Resolve<TestClass>();
            
            Assert.AreNotEqual(instance1, instance2);
            Assert.AreEqual(overwriteInstance, instance2);
            
            SystemBase.Utils.IoC.RemoveOverwrite<TestClass>();
        }

        [Test]
        public void ResolveSingletonWithInterface()
        {
            var instance1 = SystemBase.Utils.IoC.Resolve<ITestSingletonWithInterface>();
            var instance2 = SystemBase.Utils.IoC.Resolve<ITestSingletonWithInterface>();
            
            Assert.AreEqual(instance1, instance2);
        }
        
        [Test]
        public void ResolveSingletonWithoutInterface()
        {
            var instance1 = SystemBase.Utils.IoC.Resolve<TestSingletonWithoutInterface>();
            var instance2 = SystemBase.Utils.IoC.Resolve<TestSingletonWithoutInterface>();
            
            Assert.AreEqual(instance1, instance2);
        }
        
        [Test]
        public void ResolveSingletonInstance()
        {
            var instance1 = SystemBase.Utils.IoC.Resolve<ITestSingletonWithInstance>();
            var instance2 = SystemBase.Utils.IoC.Resolve<ITestSingletonWithInstance>();
            
            Assert.AreEqual(instance1, instance2);
        }
        
        [Test]
        public void ResolveSingletonLazyInstance()
        {
            var instance1 = SystemBase.Utils.IoC.Resolve<ITestSingletonWithLazyInit>();
            var instance2 = SystemBase.Utils.IoC.Resolve<ITestSingletonWithLazyInit>();
            
            Assert.AreEqual(instance1, instance2);
        }

        [Test]
        public void OverwriteSingleton()
        {
            var instance1 = SystemBase.Utils.IoC.Resolve<ITestSingletonWithInterface>();
            
            var replacement = new TestSingletonWithInterface();
            SystemBase.Utils.IoC.Overwrite<ITestSingletonWithInterface>(replacement);
            var instance2 = SystemBase.Utils.IoC.Resolve<ITestSingletonWithInterface>();
            
            Assert.AreNotEqual(instance1, instance2);
            Assert.AreEqual(replacement, instance2);
            
            SystemBase.Utils.IoC.RemoveOverwrite<ITestSingletonWithInterface>();
        }

        [Test]
        public void ResolveAutomaticRegistrations()
        {
            Assert.DoesNotThrow(() =>
            {
                var instance = SystemBase.Utils.IoC.Resolve<AutomaticRegistrationTest>();
                Assert.IsNotNull(instance);
            });
        }
    }
}