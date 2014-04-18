using System.Collections.Generic;
using System.Reflection;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment
{
    public class DirectReferenceCollectingVisitorTest
    {
        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new DirectReferenceCollectingVisitor();
            Assert.IsAssignableFrom<IReflectionVisitor<IEnumerable<Assembly>>>(sut);
        }

        [Fact]
        public void ValueIsCorrect()
        {
            var sut = new DirectReferenceCollectingVisitor();
            var actual = sut.Value;
            Assert.Empty(actual);
        }
    }
}