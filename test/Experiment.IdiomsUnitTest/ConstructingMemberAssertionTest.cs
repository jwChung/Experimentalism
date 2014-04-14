using System.Collections.Generic;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class ConstructingMemberAssertionTest
    {
        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new ConstructingMemberAssertion(
                EqualityComparer<IReflectionElement>.Default,
                EqualityComparer<IReflectionElement>.Default);
            Assert.IsAssignableFrom<IReflectionVisitor<object>>(sut);
        } 
    }
}