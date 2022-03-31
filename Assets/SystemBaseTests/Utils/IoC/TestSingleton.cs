namespace SystemBaseTests.Utils.IoC
{
    public class TestSingletonWithInterface : ITestSingletonWithInterface {}

    public interface ITestSingletonWithInterface {}

    public class TestSingletonWithoutInterface {}

    public interface ITestSingletonWithInstance {}

    public class TestSingletonWithInstance : ITestSingletonWithInstance {}

    public interface ITestSingletonWithLazyInit {}

    public class TestSingletonWithLazyInit : ITestSingletonWithLazyInit {}
}