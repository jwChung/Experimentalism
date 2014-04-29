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
            var type = typeof(TypeWithMembers);

            var actual = type.GetIdiomaticMembers();

            var members = Assert.IsAssignableFrom<IdiomaticMembers>(actual);
            Assert.Equal(type, members.Type);
            Assert.Equal(MemberKinds.All, members.MemberKinds);
        }

        [Fact]
        public void GetIdiomaticMembersWithMemberKindsReturnsCorrectMembers()
        {
            var type = typeof(TypeWithMembers);
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
    }
}