using System.Reflection;
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

        [Fact]
        public void GetIdiomaticInstanceMembersReturnsCorrectMembers()
        {
            var type = typeof(ClassWithMembers);

            var actual = type.GetIdiomaticInstanceMembers();

            var members = Assert.IsAssignableFrom<TypeMembers>(actual);
            Assert.Equal(type, members.Type);
            Assert.Equal(MemberKinds.All, members.MemberKinds);
            Assert.Equal(TypeMembers.DefaultBindingFlags & ~BindingFlags.Static, members.BindingFlags);
        }

        [Fact]
        public void GetIdiomaticInstanceMembersWithMemberKindsReturnsCorrectMembers()
        {
            var type = typeof(ClassWithMembers);
            var memberKinds = MemberKinds.Method;

            var actual = type.GetIdiomaticInstanceMembers(memberKinds);

            var members = Assert.IsAssignableFrom<TypeMembers>(actual);
            Assert.Equal(type, members.Type);
            Assert.Equal(memberKinds, members.MemberKinds);
            Assert.Equal(TypeMembers.DefaultBindingFlags & ~BindingFlags.Static, members.BindingFlags);
        }

        [Fact]
        public void GetIdiomaticStaticMembersReturnsCorrectMembers()
        {
            var type = typeof(ClassWithMembers);

            var actual = type.GetIdiomaticStaticMembers();

            var members = Assert.IsAssignableFrom<TypeMembers>(actual);
            Assert.Equal(type, members.Type);
            Assert.Equal(MemberKinds.All, members.MemberKinds);
            Assert.Equal(TypeMembers.DefaultBindingFlags & ~BindingFlags.Instance, members.BindingFlags);
        }

        [Fact]
        public void GetIdiomaticStaticMembersWithMemberKindsReturnsCorrectMembers()
        {
            var type = typeof(ClassWithMembers);
            var memberKinds = MemberKinds.Method;

            var actual = type.GetIdiomaticStaticMembers(memberKinds);

            var members = Assert.IsAssignableFrom<TypeMembers>(actual);
            Assert.Equal(type, members.Type);
            Assert.Equal(memberKinds, members.MemberKinds);
            Assert.Equal(TypeMembers.DefaultBindingFlags & ~BindingFlags.Instance, members.BindingFlags);
        }
    }
}