using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace Jwc.Experiment
{
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