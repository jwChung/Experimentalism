using System.Collections.Generic;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment
{
    public class DisplayNameVisitorTest
    {
        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new DisplayNameVisitor();
            Assert.IsAssignableFrom<IReflectionVisitor<IEnumerable<string>>>(sut);
        }
    }
}