using System.Reflection;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class IdiomaticExtensionsTest
    {
        [Fact]
        public void TypeToMembersReturnsCorrectMembers()
        {
            var type = typeof(ClassWithMembers);

            var actual = type.ToMembers();

            var members = Assert.IsAssignableFrom<TypeMembers>(actual);
            Assert.Equal(type, members.Type);
            Assert.Equal(MemberKinds.All, members.MemberKinds);
        }

        [Fact]
        public void TypeToMembersWithMemberKindsReturnsCorrectMembers()
        {
            var type = typeof(ClassWithMembers);
            var memberKinds = MemberKinds.Event;

            var actual = type.ToMembers(memberKinds);

            var members = Assert.IsAssignableFrom<TypeMembers>(actual);
            Assert.Equal(type, members.Type);
            Assert.Equal(memberKinds, members.MemberKinds);
        }

        [Fact]
        public void TypeToInstanceMembersReturnsCorrectMembers()
        {
            var type = typeof(ClassWithMembers);

            var actual = type.ToInstanceMembers();

            var members = Assert.IsAssignableFrom<TypeMembers>(actual);
            Assert.Equal(type, members.Type);
            Assert.Equal(MemberKinds.All, members.MemberKinds);
            Assert.Equal(TypeMembers.DefaultBindingFlags & ~BindingFlags.Static, members.BindingFlags);
        }

        [Fact]
        public void TypeToInstanceMembersWithMemberKindsReturnsCorrectMembers()
        {
            var type = typeof(ClassWithMembers);
            var memberKinds = MemberKinds.Method;

            var actual = type.ToInstanceMembers(memberKinds);

            var members = Assert.IsAssignableFrom<TypeMembers>(actual);
            Assert.Equal(type, members.Type);
            Assert.Equal(memberKinds, members.MemberKinds);
            Assert.Equal(TypeMembers.DefaultBindingFlags & ~BindingFlags.Static, members.BindingFlags);
        }

        [Fact]
        public void TypeToStaticMembersReturnsCorrectMembers()
        {
            var type = typeof(ClassWithMembers);

            var actual = type.ToStaticMembers();

            var members = Assert.IsAssignableFrom<TypeMembers>(actual);
            Assert.Equal(type, members.Type);
            Assert.Equal(MemberKinds.All, members.MemberKinds);
            Assert.Equal(TypeMembers.DefaultBindingFlags & ~BindingFlags.Instance, members.BindingFlags);
        }

        [Fact]
        public void TypeToStaticMembersWithMemberKindsReturnsCorrectMembers()
        {
            var type = typeof(ClassWithMembers);
            var memberKinds = MemberKinds.Method;

            var actual = type.ToStaticMembers(memberKinds);

            var members = Assert.IsAssignableFrom<TypeMembers>(actual);
            Assert.Equal(type, members.Type);
            Assert.Equal(memberKinds, members.MemberKinds);
            Assert.Equal(TypeMembers.DefaultBindingFlags & ~BindingFlags.Instance, members.BindingFlags);
        }
    }
}