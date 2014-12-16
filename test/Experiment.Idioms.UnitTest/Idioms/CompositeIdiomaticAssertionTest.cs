namespace Jwc.Experiment.Idioms
{
    using System.Collections.Generic;
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
        public void SutCorrectlyInitializesMembers(
            ConstructorInitializedMemberAssertion assertion)
        {
            assertion.Verify(typeof(CompositeIdiomaticAssertion));
        }

        [Theory, TestData]
        public void SutIsIdiomaticAssertion(CompositeIdiomaticAssertion sut)
        {
            Assert.IsAssignableFrom<IIdiomaticAssertion>(sut);
        }
    }
}