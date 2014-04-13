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
    }
}