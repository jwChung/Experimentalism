using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class FilteringMembersTest
    {
        [Fact]
        public void SutIsEnumerableMemberInfo()
        {
            var sut = new FilteringMembers(new MemberInfo[0], m => false);
            Assert.IsAssignableFrom<IEnumerable<MemberInfo>>(sut);
        }

        [Fact]
        public void TargetMembersIsCorrect()
        {
            var targetMembers = GetType().GetMembers();
            var sut = new FilteringMembers(targetMembers, m => false);

            var actual = sut.TargetMembers;

            Assert.Equal(targetMembers, actual);
        }

        [Fact]
        public void ConditionIsCorrect()
        {
            Func<MemberInfo, bool> condition = m => false;
            var sut = new FilteringMembers(new MemberInfo[0], condition);

            var actual = sut.Condition;

            Assert.Equal(condition, actual);
        }

        [Fact]
        public void InitializeWithNullTargetMembersThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new FilteringMembers(null, m => false));
        }

        [Fact]
        public void InitializeWithNullConditionThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new FilteringMembers(new MemberInfo[0], null));
        }

        [Fact]
        public void SutEnumeratesCorrectMembers()
        {
            var targetMembers = GetType().GetMembers();
            var excludedMembers = new MemberInfo[]
            {
                new Methods<FilteringMembersTest>().Select(x => x.GetType()),
                new Methods<FilteringMembersTest>().Select(x => x.SutEnumeratesCorrectMembers())
            };
            var expected = targetMembers.Except(excludedMembers);
            var sut = new FilteringMembers(targetMembers, excludedMembers.Contains);

            var actual = sut.ToArray();

            Assert.Equal(expected, actual);
        }
    }
}