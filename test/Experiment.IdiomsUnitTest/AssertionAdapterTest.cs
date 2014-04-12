using Ploeh.Albedo;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class AssertionAdapterTest
    {
        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new AssertionAdapter(new GuardClauseAssertion(new ArrayRelay()));
            Assert.IsAssignableFrom<IReflectionVisitor<object>>(sut);
        }
    }
}