using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class ExcludingReadOnlyPropertiesTest
    {
        [Fact]
        public void SutIsEnumerableOfMemberInfo()
        {
            var sut = new ExcludingReadOnlyProperties(new MemberInfo[0]);
            Assert.IsAssignableFrom<IEnumerable<MemberInfo>>(sut);
        } 

        [Fact]
        public void TargetMembersIsCorrect()
        {
            var targetMembers = GetType().GetMembers();
            var sut = new ExcludingReadOnlyProperties(targetMembers);

            var expected = sut.TargetMembers;

            Assert.Equal(targetMembers, expected);
        }

        [Fact]
        public void InitializeWithNullTargetMembersThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new ExcludingReadOnlyProperties(null));
        }
    }
}