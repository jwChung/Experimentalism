using System.Collections.Generic;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class InverseEqualityComparerTest
    {
        [Fact]
        public void SutIsEqualityComaprer()
        {
            var sut = new InverseEqualityComparer<object>(
                EqualityComparer<object>.Default);
            Assert.IsAssignableFrom<IEqualityComparer<object>>(sut);
        }
    }
}