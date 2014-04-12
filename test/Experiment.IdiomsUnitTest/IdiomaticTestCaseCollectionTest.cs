using System.Collections.Generic;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class IdiomaticTestCaseCollectionTest
    {
        [Fact]
        public void SutIsEnumerableOfTestCase()
        {
            var sut = new IdiomaticTestCaseCollection();
            Assert.IsAssignableFrom<IEnumerable<ITestCase>>(sut);
        }
    }
}