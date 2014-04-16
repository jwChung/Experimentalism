using System.Collections.Generic;
using System.Linq;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class ConstructingMemberAssertionFactoryTest
    {
        [Fact]
        public void SutIsAssertionFactory()
        {
            var sut = new ConstructingMemberAssertionFactory();
            Assert.IsAssignableFrom<IAssertionFactory>(sut);
        }

        [Fact]
        public void CreateReturnsCorrectConstructingMemberAssertion()
        {
            // Fixture setup
            var testFixture = new DelegatingTestFixture();
            var sut = new ConstructingMemberAssertionFactory();

            // Exercise system
            var actual = sut.Create(testFixture);

            // Verify outcome
            var assertion = Assert.IsAssignableFrom<ConstructingMemberAssertion>(actual);

            var comparers1 = Assert.IsAssignableFrom<OrEqualityComparer<IReflectionElement>>(
                assertion.ParameterToMemberComparer).EqualityComparers.ToArray();
            Assert.Equal(2, comparers1.Length);
            var comparers11 = Assert.IsAssignableFrom<ParameterToPropertyComparer>(comparers1[0]);
            Assert.Equal(testFixture, comparers11.TestFixture);
            var comparers12 = Assert.IsAssignableFrom<ParameterToFieldComparer>(comparers1[1]);
            Assert.Equal(testFixture, comparers12.TestFixture);

            var comparers2 = Assert.IsAssignableFrom<OrEqualityComparer<IReflectionElement>>(
                assertion.MemberToParameterComparer).EqualityComparers.ToArray();
            Assert.Equal(2, comparers1.Length);
            var comparers21 = Assert.IsAssignableFrom<PropertyToParameterComparer>(comparers2[0]);
            Assert.Equal(testFixture, comparers21.TestFixture);
            var comparers22 = Assert.IsAssignableFrom<FieldToParameterComparer>(comparers2[1]);
            Assert.Equal(testFixture, comparers22.TestFixture);
        }
    }
}