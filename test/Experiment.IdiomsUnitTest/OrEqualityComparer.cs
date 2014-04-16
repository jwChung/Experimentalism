using System.Collections.Generic;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class OrEqualityComparerTest
    {
        [Fact]
        public void SutIsEqualityComparer()
        {
            var sut = new OrEqualityComparer<object>();
            Assert.IsAssignableFrom<IEqualityComparer<object>>(sut);
        }
    }
}