using System;
using System.Collections.Generic;
using System.Reflection;
using Moq;
using Ploeh.Albedo;
using Ploeh.AutoFixture.Idioms;
using Xunit;

namespace Jwc.Experiment.Idioms.Assertions
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
        public void VerifyGuardedMethodDoesNotThrow()
        {
            var sut = new NullGuardClauseAssertion(new FakeTestFixture());
            var guardedMethod = new Methods<ClassWithGuardedMembers>().Select(x => x.Method(null, null));
            Assert.DoesNotThrow(() => sut.Verify(guardedMethod));
        }

        [Fact]
        public void VerifyUnguardedMethodThrows()
        {
            var sut = new NullGuardClauseAssertion(new FakeTestFixture());
            var unguardedMethod = new Methods<ClassWithUnguardedMembers>().Select(x => x.Method(null, null));
            Assert.Throws<GuardClauseException>(() => sut.Verify(unguardedMethod));
        }

        [Fact]
        public void SutIsIdiomaticTypeAssertion()
        {
            var sut = new NullGuardClauseAssertion(new DelegatingTestFixture());
            Assert.IsAssignableFrom<IIdiomaticTypeAssertion>(sut);
        }

        [Fact]
        public void VerifyGuardedTypeDoesNotThrow()
        {
            var sut = new NullGuardClauseAssertion(new FakeTestFixture());
            Assert.DoesNotThrow(() => sut.Verify(typeof(ClassWithGuardedMembers)));
        }

        [Fact]
        public void VerifyUnguardedTypeThrows()
        {
            var sut = new NullGuardClauseAssertion(new FakeTestFixture());
            Assert.Throws<GuardClauseException>(() => sut.Verify(typeof(ClassWithUnguardedMembers)));
        }

        [Fact]
        public void SutIsIdiomaticAssemblyAssertion()
        {
            var sut = new NullGuardClauseAssertion(new DelegatingTestFixture());
            Assert.IsAssignableFrom<IIdiomaticAssemblyAssertion>(sut);
        }

        [Fact]
        public void VerifyAssemblyCorrectlyVerifies()
        {
            // Fixture setup
            var sut = new Mock<NullGuardClauseAssertion>(new DelegatingTestFixture()).Object;

            var types = new List<MemberInfo>();
            sut.ToMock().Setup(x => x.Verify(It.IsAny<Type>())).Callback<Type>(types.Add);

            var assembly = typeof(TheoremBaseAttribute).Assembly;

            var expected = assembly.GetExportedTypes();

            // Exercise system
            sut.Verify(assembly);

            // Verify outcome
            Assert.Equal(expected, types);
        }

        [Fact]
        public void VerifyNullAssemblyThrows()
        {
            var sut = new NullGuardClauseAssertion(new DelegatingTestFixture());
            Assert.Throws<ArgumentNullException>(() => sut.Verify((Assembly)null));
        }

        private class ClassWithGuardedMembers
        {
            public void Method(string arg1, object arg2)
            {
                if (arg1 == null)
                    throw new ArgumentNullException("arg1");

                if (arg2 == null)
                    throw new ArgumentNullException("arg2");
            }
        }

        private class ClassWithUnguardedMembers
        {
            public void Method(string arg1, object arg2)
            {
                if (arg1 == null)
                    throw new ArgumentNullException("arg1");
            }
        }
    }
}