using System;
using System.Linq;
using Moq;
using Xunit;

namespace Jwc.Experiment
{
    public class IdiomaticExtensionsTest
    {
        [Fact]
        public void GetIdiomaticMembersReturnsCorrectMembers()
        {
            var type = typeof(ClassWithMembers);

            var actual = type.GetIdiomaticMembers();

            var members = Assert.IsAssignableFrom<IdiomaticMembers>(actual);
            Assert.Equal(type, members.Type);
            Assert.Equal(MemberKinds.All, members.MemberKinds);
        }

        [Fact]
        public void GetIdiomaticMembersWithMemberKindsReturnsCorrectMembers()
        {
            var type = typeof(ClassWithMembers);
            var memberKinds = MemberKinds.Event;

            var actual = type.GetIdiomaticMembers(memberKinds);

            var members = Assert.IsAssignableFrom<IdiomaticMembers>(actual);
            Assert.Equal(type, members.Type);
            Assert.Equal(memberKinds, members.MemberKinds);
        }

        [Fact]
        public void VerifyIdiomaticMemberAssertionCorrectlyVerifiesAssertion()
        {
            var members = typeof(object).GetMembers();
            var assertion = Mock.Of<IIdiomaticMemberAssertion>();

            members.Verify(assertion);

            members.ToList().ForEach(m => assertion.ToMock().Verify(x => x.Verify(m)));
        }

        [Fact]
        public void VerifyIdiomaticMemberAssertionWithNullMembersThrows()
        {
            var assertion = Mock.Of<IIdiomaticMemberAssertion>();
            Assert.Throws<ArgumentNullException>(() => IdiomaticExtensions.Verify(null, assertion));
        }

        [Fact]
        public void VerifyNullIdiomaticMemberAssertionThrows()
        {
            Assert.Throws<ArgumentNullException>(() => typeof(object).GetMembers().Verify(null));
        }

        [Fact]
        public void MembersToTestCasesReturnsCorrectTestCases()
        {
            var members = typeof(object).GetMembers().ToArray();
            var assertion = Mock.Of<IIdiomaticMemberAssertion>();

            var actual = members.ToTestCases(assertion);

            int i = 0;
            foreach (TestCase testCase in actual.Cast<TestCase>())
            {
                assertion.ToMock().Verify(x => x.Verify(members[i]), Times.Never);
                testCase.Delegate.DynamicInvoke();
                assertion.ToMock().Verify(x => x.Verify(members[i]), Times.Once);
                i++;
            }
        }

        [Fact]
        public void NullMemersToTestCasesThrows()
        {
            var assertion = Mock.Of<IIdiomaticMemberAssertion>();
            var exception = Assert.Throws<ArgumentNullException>(
                () => IdiomaticExtensions.ToTestCases(null, assertion));
            Assert.Equal("members", exception.ParamName);
        }

        [Fact]
        public void MemersToTestCasesWithNullIdiomaticMemberAssertionThrows()
        {
            Assert.Throws<ArgumentNullException>(() => typeof(object).GetMembers().ToTestCases(null));
        }
    }
}