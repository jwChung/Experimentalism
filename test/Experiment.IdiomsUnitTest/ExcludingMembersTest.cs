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
    }
}