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
    }
}