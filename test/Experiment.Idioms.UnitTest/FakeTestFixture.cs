namespace Jwc.Experiment
{
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;
    using Xunit;

    public class FakeTestFixture : ITestFixture
    {
        private readonly ISpecimenContext context;

        public FakeTestFixture()
        {
            var fixture = new Fixture();
            fixture.Inject<ITestFixture>(this);
            this.context = new SpecimenContext(fixture);
        }

        public object Create(object request)
        {
            return this.context.Resolve(request);
        }
    }
}