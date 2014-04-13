using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class ExcludingMembersTest
    {
        [Fact]
        public void SutIsEnumerableOfMemberInfo()
        {
            var sut = new ExcludingMembers(new MemberInfo[0]);
            Assert.IsAssignableFrom<IEnumerable<MemberInfo>>(sut);
        }

        [Fact]
        public void ExcludedMembersIsCorrect()
        {
            var excludedMembers = GetType().GetMembers();
            var sut = new ExcludingMembers(excludedMembers);

            var actual = sut.ExcludedMembers;

            Assert.Equal(excludedMembers, actual);
        }

        [Fact]
        public void InitializeWithNullExcludedMembersThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new ExcludingMembers(null));
        }
    }
}