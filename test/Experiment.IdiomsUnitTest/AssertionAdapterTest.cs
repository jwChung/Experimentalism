using System;
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

        [Fact]
        public void InitializeWithNullAssertionThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new AssertionAdapter(null));
        }

        [Fact]
        public void AssertionIsCorrect()
        {
            var assertion = new GuardClauseAssertion(new ArrayRelay());
            var sut = new AssertionAdapter(assertion);

            var actual = sut.Assertion;

            Assert.Equal(assertion, actual);
        }
    }
}