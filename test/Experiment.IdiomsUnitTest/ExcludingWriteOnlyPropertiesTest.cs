using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Jwc.Experiment
{
    public class ExcludingWriteOnlyPropertiesTest
    {
        [Fact]
        public void SutIsEnumerableOfMemberInfo()
        {
            var sut = new ExcludingWriteOnlyProperties(new MemberInfo[0]);
            Assert.IsAssignableFrom<IEnumerable<MemberInfo>>(sut);
        }

        [Fact]
        public void TargetMembersIsCorrect()
        {
            var targetMembers = GetType().GetMembers();
            var sut = new ExcludingWriteOnlyProperties(targetMembers);

            var expected = sut.TargetMembers;

            Assert.Equal(targetMembers, expected);
        }

        [Fact]
        public void InitializeWithNullTargetMembersThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new ExcludingWriteOnlyProperties(null));
        }

        [Fact]
        public void SutEnumeratesOnlyReadableProperties()
        {
            var targetMembers = typeof(TypeWithProperties).GetProperties(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var sut = new ExcludingWriteOnlyProperties(targetMembers);
            var expected = new[]
            {
                typeof(TypeWithProperties).GetProperty("GetSetProperty"),
                typeof(TypeWithProperties).GetProperty("GetProperty"),
                typeof(TypeWithProperties).GetProperty("PrivateSetProperty")
            };

            var actual = sut.Cast<PropertyInfo>().ToArray();

            Assert.Equal(expected, actual);
        }
    }
}