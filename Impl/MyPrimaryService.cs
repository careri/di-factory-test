namespace DiFactoryTest.Impl
{
    public class MyPrimaryService : IMyService
    {
        public PrimaryDependency Dep1
        {
            get;
        }
        public MyPrimaryService (PrimaryDependency dep1)
        {
            this.Dep1 = dep1;

        }
        public string GetInfo () => $"Primary: {Dep1}";
    }
}