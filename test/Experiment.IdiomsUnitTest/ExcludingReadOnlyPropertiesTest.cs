﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public void SutDoesNotEnumerateReadOnlyProperties()
        {
            var targetMembers = typeof(ClassWithProperties).GetProperties(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var sut = new ExcludingReadOnlyProperties(targetMembers);
            var expected = new[]
            {
                typeof(ClassWithProperties).GetProperty("GetSetProperty"),
                typeof(ClassWithProperties).GetProperty("SetProperty")
            };

            var actual = sut.Cast<PropertyInfo>().ToArray();

            Assert.Equal(expected, actual);
        }
    }
}