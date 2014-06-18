using System;
using System.Reflection;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment
{
    public class MemberInfoExtensionsTest
    {
        [Fact]
        public void GetDisplayNameWithNullMemberThrows()
        {
            Assert.Throws<ArgumentNullException>(() => MemberInfoExtensions.GetDisplayName(null));
        }

        [Fact]
        public void GetDisplayMethodNameReturnsCorrectString()
        {
            var member = MethodBase.GetCurrentMethod();
            const string expected = "[[Jwc.Experiment.MemberInfoExtensionsTest]" +
                                    "[Void GetDisplayMethodNameReturnsCorrectString()]] " +
                                    "(method)";

            var actual = member.GetDisplayName();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDisplayPropertyNameReturnsCorrectString()
        {
            var member = new Properties<ClassWithMembers>().Select(x => x.PublicProperty);
            const string expected = "[[Jwc.Experiment.ClassWithMembers]" +
                                    "[System.Object PublicProperty]] " +
                                    "(property)";

            var actual = member.GetDisplayName();

            Assert.Equal(expected, actual);
        }
    }
}