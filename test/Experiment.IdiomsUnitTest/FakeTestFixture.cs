using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace Jwc.Experiment.Idioms
{
    public class FakeTestFixture : ITestFixture
    {
        private readonly ISpecimenContext _context;

        public FakeTestFixture()
        {
            var fixture = new Fixture();
            fixture.Inject<ITestFixture>(this);
            _context = new SpecimenContext(fixture);
        }

        public object Create(object request)
        {
            return _context.Resolve(request);
        }
    }
}