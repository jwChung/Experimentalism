using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class IdiomaticExtensionsTest
    {
        [Fact]
        public void TypeToIdiomaticMembersReturnsCorrectMembers()
        {
            var type = typeof(ClassWithMembers);

            var actual = type.ToIdiomaticMembers();

            var members = Assert.IsAssignableFrom<IdiomaticMembers>(actual);
            Assert.Equal(type, members.Type);
            Assert.Equal(MemberKinds.All, members.MemberKinds);
        }

        [Fact]
        public void TypeToIdiomaticMembersWithMemberKindsReturnsCorrectMembers()
        {
            var type = typeof(ClassWithMembers);
            var memberKinds = MemberKinds.Event;

            var actual = type.ToIdiomaticMembers(memberKinds);

            var members = Assert.IsAssignableFrom<IdiomaticMembers>(actual);
            Assert.Equal(type, members.Type);
            Assert.Equal(memberKinds, members.MemberKinds);
        }
    }
}