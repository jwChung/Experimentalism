﻿namespace Jwc.Experiment
{
    using System;
    using System.Reflection;
    using Ploeh.Albedo;
    using global::Xunit;

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
            string expected = "[[Jwc.Experiment.MemberInfoExtensionsTest]" +
                              "[Void GetDisplayMethodNameReturnsCorrectString()]] " +
                              "(method)";

            var actual = member.GetDisplayName();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDisplayPropertyNameReturnsCorrectString()
        {
            var member = new Properties<ClassWithMembers>().Select(x => x.PublicProperty);
            string expected = "[[Jwc.Experiment.ClassWithMembers]" +
                              "[System.Object PublicProperty]] " +
                              "(property)";

            var actual = member.GetDisplayName();

            Assert.Equal(expected, actual);
        }
    }
}