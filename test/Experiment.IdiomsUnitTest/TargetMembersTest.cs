using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class TargetMembersTest
    {
        [Fact]
        public void SutIsEnumerableOfMemberInfo()
        {
            var sut = new TargetMembers(typeof(object));
            Assert.IsAssignableFrom<IEnumerable<MemberInfo>>(sut);
        }

        [Fact]
        public void InitializeWithNullTypeThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TargetMembers(null));
        }

        [Fact]
        public void TypeIsCorrect()
        {
            var type = GetType();
            var sut = new TargetMembers(type);

            var actual = sut.Type;

            Assert.Equal(type, actual);
        }

        [Fact]
        public void SutEnumeratesPublicMembersOnly()
        {
            var sut = new TargetMembers(typeof(ClassWithTestMembers));
            var actual = sut.OfType<MethodInfo>().ToArray();
            Assert.True(actual.All(m => m.IsPublic), "Public Only.");
        }

        [Fact]
        public void SutEnumeratesAllKindsOfMemberInfo()
        {
            var sut = new TargetMembers(typeof(ClassWithTestMembers));

            var actual = sut.ToArray();

            Assert.True(actual.Any(m => m is FieldInfo), "FieldInfo.");
            Assert.True(actual.Any(m => m is ConstructorInfo), "ConstructorInfo.");
            Assert.True(actual.Any(m => m is PropertyInfo), "PropertyInfo.");
            Assert.True(actual.Any(m => m is MethodInfo), "MethodInfo.");
            Assert.True(actual.Any(m => m is EventInfo), "EventInfo.");
        }

        [Fact]
        public void SutEnumeratesDeclaredMembersOnly()
        {
            var type = typeof(ClassWithTestMembers);
            var toStringMethod = new Methods<ClassWithTestMembers>().Select(x => x.ToString());
            var sut = new TargetMembers(type);

            var actual = sut.ToArray();

            Assert.DoesNotContain(toStringMethod, actual);
        }

        [Fact]
        public void SutEnumeratesStaticMember()
        {
            var sut = new TargetMembers(typeof(ClassWithTestMembers));
            var actual = sut.OfType<MethodInfo>().ToArray();
            Assert.True(actual.Any(m => m.IsStatic), "Static Member.");
        }

        [Fact]
        public void SutDoesNotEnumeratesAnyAccessors()
        {
            var sut = new TargetMembers(typeof(ClassWithTestMembers));
            var accessors = new Properties<ClassWithTestMembers>()
                .Select(x => x.Property).GetAccessors();

            var actual = sut.ToArray();

            Assert.True(
                accessors.All(a => !actual.Contains(a)),
                "Does not contain any accessors.");
        }

        [Fact]
        public void SutDoesNotEnumeratesAnyHelperMethodsOfEvent()
        {
            var sut = new TargetMembers(typeof(ClassWithTestMembers));
            var eventInfo = typeof(ClassWithTestMembers).GetEvents().First();
            var eventMethods = new[] { eventInfo.GetAddMethod(), eventInfo.GetRemoveMethod() };

            var actual = sut.ToArray();

            Assert.True(
                eventMethods.All(a => !actual.Contains(a)),
                "Does not contain any helper methods of event.");
        }
    }
}