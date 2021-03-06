﻿namespace Jwc.Experiment
{
    using global::Xunit;

    public class TypeExtensionsTest
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
            var memberKinds = MemberKinds.InstanceEvent;

            var actual = type.GetIdiomaticMembers(memberKinds);

            var members = Assert.IsAssignableFrom<IdiomaticMembers>(actual);
            Assert.Equal(type, members.Type);
            Assert.Equal(memberKinds, members.MemberKinds);
        }
    }
}