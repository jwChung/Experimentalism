namespace Jwc.Experiment.Idioms
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Ploeh.AutoFixture.Idioms;
    using Ploeh.AutoFixture.Xunit;
    using global::Xunit;
    using global::Xunit.Extensions;

    public class CompositeIdiomaticAssertionTest
    {
        [Theory, TestData]
        public void SutHasCorrectGuardClauses(
            GuardClauseAssertion assertion)
        {
            var members = new IdiomaticMembers(
                typeof(CompositeIdiomaticAssertion),
                MemberKinds.Constructor);

            foreach (var member in members)
                assertion.Verify(member);
        }

        [Theory, TestData]
        public void SutIsIdiomaticAssertion(CompositeIdiomaticAssertion sut)
        {
            Assert.IsAssignableFrom<IIdiomaticAssertion>(sut);
        }

        [Theory, TestData]
        public void AssertionsIsCorrectWhenInitializedWithIEnumerable(
            [Frozen] IEnumerable<IIdiomaticAssertion> assertions,
            [FavorEnumerables] CompositeIdiomaticAssertion sut)
        {
            var actual = sut.Assertions;
            Assert.Equal(assertions, actual);
        }

        [Theory, TestData]
        public void AssertionsIsCorrectWhenInitializedWithArray(
            [Frozen] IIdiomaticAssertion[] assertions,
            [FavorArrays] CompositeIdiomaticAssertion sut)
        {
            var actual = sut.Assertions;
            Assert.Equal(assertions, actual);
        }

        [Theory, TestData]
        public void VerifyAssemblyCorrectlyVerifies(
            CompositeIdiomaticAssertion sut,
            Assembly assembly)
        {
            sut.Verify(assembly);
            foreach (var assertion in sut.Assertions)
                assertion.ToMock().Verify(x => x.Verify(assembly));
        }

        [Theory, TestData]
        public void VerifyTypeCorrectlyVerifies(
            CompositeIdiomaticAssertion sut,
            Type type)
        {
            sut.Verify(type);
            foreach (var assertion in sut.Assertions)
                assertion.ToMock().Verify(x => x.Verify(type));
        }

        [Theory, TestData]
        public void VerifyMemberCorrectlyVerifies(
            CompositeIdiomaticAssertion sut,
            MemberInfo member)
        {
            sut.Verify(member);
            foreach (var assertion in sut.Assertions)
                assertion.ToMock().Verify(x => x.Verify(member));
        }
    }
}