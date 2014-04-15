using System.Collections.Generic;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class ParameterToFieldComparerTest
    {
        [Fact]
        public void SutIsEqualityComparer()
        {
            var sut = new ParameterToFieldComparer(new DelegatingTestFixture());
            Assert.IsAssignableFrom<IEqualityComparer<IReflectionElement>>(sut);
        }
    }
}