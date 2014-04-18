using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace Jwc.Experiment
{
    public class FakeTestFixture : ITestFixture
    {
        readonly ISpecimenContext _context = new SpecimenContext(new Fixture());

        public object Create(object request)
        {
            return _context.Resolve(request);
        }
    }
}