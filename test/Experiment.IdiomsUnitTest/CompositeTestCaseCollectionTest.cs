using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class CompositeTestCaseCollectionTest
    {
        [Fact]
        public void SutIsEnumerableOfTestCase()
        {
            var sut = new CompositeTestCaseCollection();
            Assert.IsAssignableFrom<ITestCase>(sut);
        }
    }
}