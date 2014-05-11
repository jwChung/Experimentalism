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

        [Fact]
        public void TypeToIdiomaticInstanceMembersReturnsCorrectMembers()
        {
            var type = typeof(ClassWithMembers);

            var actual = type.ToIdiomaticInstanceMembers();

            var members = Assert.IsAssignableFrom<IdiomaticMembers>(actual);
            Assert.Equal(type, members.Type);
            Assert.Equal(MemberKinds.All, members.MemberKinds);
        }

        [Fact]
        public void TypeToIdiomaticInstanceMembersWithMemberKindsReturnsCorrectMembers()
        {
            var type = typeof(ClassWithMembers);
            var memberKinds = MemberKinds.Method;

            var actual = type.ToIdiomaticInstanceMembers(memberKinds);

            var members = Assert.IsAssignableFrom<IdiomaticMembers>(actual);
            Assert.Equal(type, members.Type);
            Assert.Equal(memberKinds, members.MemberKinds);
        }

        [Fact]
        public void TypeToIdiomaticStaticMembersReturnsCorrectMembers()
        {
            var type = typeof(ClassWithMembers);

            var actual = type.ToIdiomaticStaticMembers();

            var members = Assert.IsAssignableFrom<IdiomaticMembers>(actual);
            Assert.Equal(type, members.Type);
            Assert.Equal(MemberKinds.All, members.MemberKinds);
        }

        [Fact]
        public void TypeToIdiomaticStaticMembersWithMemberKindsReturnsCorrectMembers()
        {
            var type = typeof(ClassWithMembers);
            var memberKinds = MemberKinds.Method;

            var actual = type.ToIdiomaticStaticMembers(memberKinds);

            var members = Assert.IsAssignableFrom<IdiomaticMembers>(actual);
            Assert.Equal(type, members.Type);
            Assert.Equal(memberKinds, members.MemberKinds);
        }
    }
}