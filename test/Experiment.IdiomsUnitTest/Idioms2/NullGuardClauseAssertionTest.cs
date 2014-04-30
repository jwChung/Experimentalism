using System;
using Ploeh.Albedo;
using Ploeh.AutoFixture.Idioms;
using Xunit;

namespace Jwc.Experiment.Idioms2
{
    public class NullGuardClauseAssertionTest
    {
        [Fact]
        public void SutIsIdiomaticMemberAssertion()
        {
            var sut = new NullGuardClauseAssertion(new DelegatingTestFixture());
            Assert.IsAssignableFrom<IIdiomaticMemberAssertion>(sut);
        }

        [Fact]
        public void InitializeWithNullTestFixtureThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new NullGuardClauseAssertion(null));
        }

        [Fact]
        public void TestFixtureIsCorrect()
        {
            var testFixture = new DelegatingTestFixture();
            var sut = new NullGuardClauseAssertion(testFixture);

            var actual = sut.TestFixture;

            Assert.Equal(testFixture, actual);
        }

        [Fact]
        public void VerityGuardedMethodDoesNotThrow()
        {
            var sut = new NullGuardClauseAssertion(new FakeTestFixture());
            var guardedMethod = new Methods<ClassForGuardClause>().Select(x => x.GuardedMethod(null, null));
            Assert.DoesNotThrow(() => sut.Verify(guardedMethod));
        }

        [Fact]
        public void VerityUnguardedMethodThrows()
        {
            var sut = new NullGuardClauseAssertion(new FakeTestFixture());
            var unguardedMethod = new Methods<ClassForGuardClause>().Select(x => x.UnguardedMethod(null, null));
            Assert.Throws<GuardClauseException>(() => sut.Verify(unguardedMethod));
        }

        private class ClassForGuardClause
        {
            public void GuardedMethod(string arg1, object arg2)
            {
                if (arg1 == null)
                    throw new ArgumentNullException("arg1");

                if (arg2 == null)
                    throw new ArgumentNullException("arg2");
            }

            public void UnguardedMethod(string arg1, object arg2)
            {
                if (arg1 == null)
                    throw new ArgumentNullException("arg1");
            }
        }
    }
}