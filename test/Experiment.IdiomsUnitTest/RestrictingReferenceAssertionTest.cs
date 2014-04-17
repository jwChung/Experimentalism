using System;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class RestrictingReferenceAssertionTest
    {
        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new RestrictingReferenceAssertion();
            Assert.IsAssignableFrom<IReflectionVisitor<object>>(sut);
        } 

        [Fact]
        public void AssembliesIsCorrect()
        {
            var assemblies = new[]
            {
                GetType().Assembly,
                typeof(RestrictingReferenceAssertion).Assembly
            };
            var sut = new RestrictingReferenceAssertion(assemblies);

            var actual = sut.Assemblies;

            Assert.Equal(assemblies, actual);
        }

        [Fact]
        public void InitializeWithNullAssembliesThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new RestrictingReferenceAssertion(null));
        }
    }
}