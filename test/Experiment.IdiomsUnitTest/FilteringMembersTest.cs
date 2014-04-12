using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class FilteringMembersTest
    {
        [Fact]
        public void SutIsEnumerableMemberInfo()
        {
            var sut = new FilteringMembers(new MemberInfo[0]);
            Assert.IsAssignableFrom<IEnumerable<MemberInfo>>(sut);
        }

        [Fact]
        public void TargetMembersIsCorrect()
        {
            var targetMembers = GetType().GetMembers();
            var sut = new FilteringMembers(targetMembers);

            var actual = sut.TargetMembers;

            Assert.Equal(targetMembers, actual);
        }

        [Fact]
        public void ExceptedMembersIsCorrect()
        {
            MemberInfo[] exceptedMembers = GetType().GetMembers();
            var sut = new FilteringMembers(new MemberInfo[0], exceptedMembers);

            var actual = sut.ExceptedMembers;

            Assert.Equal(exceptedMembers, actual);
        }

        [Fact]
        public void InitializeWithNullTargetMembersThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new FilteringMembers(null));
        }

        [Fact]
        public void InitializeWithNullExceptedMembersThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new FilteringMembers(new MemberInfo[0], null));
        }
    }
}