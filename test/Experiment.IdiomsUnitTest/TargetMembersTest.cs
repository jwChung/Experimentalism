using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;
using Ploeh.Albedo.Refraction;
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
        public void InitializeModestCtorWithNullTypeThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TargetMembers(null));
        }

        [Fact]
        public void TypeIsCorrectWhenInitializedWithModestCtor()
        {
            var type = GetType();
            var sut = new TargetMembers(type);

            var actual = sut.Type;

            Assert.Equal(type, actual);
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
                .Select(x => x.PublicProperty).GetAccessors();

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

        [Fact]
        public void AccessibilitiesIsCorrectWhenInitializedWithModestCtor()
        {
            var sut = new TargetMembers(typeof(object));
            var actual = sut.Accessibilities;
            Assert.Equal(Accessibilities.Default, actual);
        }

        [Fact]
        public void TypeIsCorrectWhenInitializedWithGreedyCtor()
        {
            var type = GetType();
            var sut = new TargetMembers(type, Accessibilities.Default);

            var actual = sut.Type;

            Assert.Equal(type, actual);
        }

        [Fact]
        public void AccessibilitiesIsCorrectWhenInitializedWithGreedyCtor()
        {
            var accessibilities = Accessibilities.Private;
            var sut = new TargetMembers(typeof(object), accessibilities);
            var actual = sut.Accessibilities;
            Assert.Equal(accessibilities, actual);
        }

        [Fact]
        public void InitializeGreedyCtorWithNullTypeThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TargetMembers(null, Accessibilities.Default));
        }

        [Fact]
        public void SutEnumeratesCorrectMembersForPrivateAccessibilities()
        {
            var sut = new TargetMembers(typeof(ClassWithTestMembers), Accessibilities.Private);

            var actual = sut.Select(
                m => m.ToReflectionElement()
                    .Accept(new AccessibilityCollectingVisitor())
                    .Value.Single())
                .ToArray();

            Assert.True(
                actual.All(a => a != Accessibilities.Public), "All Not Public.");
            Assert.True(
                actual.All(a => a != Accessibilities.ProtectedInternal), "All Not ProtectedInternal.");
            Assert.True(
                actual.All(a => a != Accessibilities.Protected), "All Not Protected.");
            Assert.True(
                actual.All(a => a != Accessibilities.Internal), "All Not Internal.");
            Assert.True(
                actual.All(a => a == Accessibilities.Private), "All Private.");
        }

        [Fact]
        public void SutEnumeratesCorrectMembersForProtectedAccessibilities()
        {
            var sut = new TargetMembers(typeof(ClassWithTestMembers), Accessibilities.Protected);

            var actual = sut.Select(
                m => m.ToReflectionElement()
                    .Accept(new AccessibilityCollectingVisitor())
                    .Value.Single())
                .ToArray();

            Assert.True(
                actual.All(a => a != Accessibilities.Public), "All Not Public.");
            Assert.True(
                actual.Any(a => a == Accessibilities.ProtectedInternal), "Any ProtectedInternal.");
            Assert.True(
                 actual.Any(a => a == Accessibilities.Protected), "Any Protected.");
            Assert.True(
               actual.All(a => a != Accessibilities.Internal), "All Not Internal.");
            Assert.True(
                actual.All(a => a != Accessibilities.Private), "All Not Private.");
        }

        [Fact]
        public void SutEnumeratesCorrectMembersForInternalAndPrivateAccessibilities()
        {
            var sut = new TargetMembers(
                typeof(ClassWithTestMembers),
                Accessibilities.Internal | Accessibilities.Private);

            var actual = sut.Select(
                m => m.ToReflectionElement()
                    .Accept(new AccessibilityCollectingVisitor())
                    .Value.Single())
                .ToArray();

            Assert.True(
                actual.All(a => a != Accessibilities.Public), "All Not Public.");
            Assert.True(
                actual.Any(a => a == Accessibilities.ProtectedInternal), "Any ProtectedInternal.");
            Assert.True(
                 actual.All(a => a != Accessibilities.Protected), "All Not Protected.");
            Assert.True(
               actual.Any(a => a == Accessibilities.Internal), "Any Internal.");
            Assert.True(
                actual.Any(a => a == Accessibilities.Private), "Any Private.");
        }
    }
}