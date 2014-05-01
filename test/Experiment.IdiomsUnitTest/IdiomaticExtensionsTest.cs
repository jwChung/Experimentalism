using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class IdiomaticExtensionsTest
    {
        [Fact]
        public void GetIdiomaticMembersReturnsCorrectMembers()
        {
            var type = typeof(ClassWithMembers);

            var actual = type.GetIdiomaticMembers();

            var members = Assert.IsAssignableFrom<TypeMembers>(actual);
            Assert.Equal(type, members.Type);
            Assert.Equal(MemberKinds.All, members.MemberKinds);
        }

        [Fact]
        public void GetIdiomaticMembersWithMemberKindsReturnsCorrectMembers()
        {
            var type = typeof(ClassWithMembers);
            var memberKinds = MemberKinds.Event;

            var actual = type.GetIdiomaticMembers(memberKinds);

            var members = Assert.IsAssignableFrom<TypeMembers>(actual);
            Assert.Equal(type, members.Type);
            Assert.Equal(memberKinds, members.MemberKinds);
        }
    }
}