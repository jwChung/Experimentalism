using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class DefaultMembersTest
    {
        [Fact]
        public void SutIsEnumerableOfMemberInfo()
        {
            var sut = new DefaultMembers(typeof(object));
            Assert.IsAssignableFrom<IEnumerable<MemberInfo>>(sut);
        }

        [Fact]
        public void InitializeWithNullTypeThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new DefaultMembers(null));
        }

        [Fact]
        public void TypeIsCorrect()
        {
            var type = GetType();
            var sut = new DefaultMembers(type);

            var actual = sut.Type;

            Assert.Equal(type, actual);
        }
    }
}