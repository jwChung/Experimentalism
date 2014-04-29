using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;
using Ploeh.Albedo.Refraction;
using Xunit;

namespace Jwc.Experiment
{
    public class TypeMembersTest
    {
        [Fact]
        public void SutIsEnumerableOfMemberInfo()
        {
            var sut = new TypeMembers(typeof(object));
            Assert.IsAssignableFrom<IEnumerable<MemberInfo>>(sut);
        }

        [Fact]
        public void InitializeModestCtorWithNullTypeThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TypeMembers(null));
        }

        [Fact]
        public void TypeIsCorrectWhenInitializedWithModestCtor()
        {
            var type = GetType();
            var sut = new TypeMembers(type);

            var actual = sut.Type;

            Assert.Equal(type, actual);
        }

        [Fact]
        public void SutEnumeratesAllKindsOfMemberInfo()
        {
            var sut = new TypeMembers(typeof(TypeWithMembers));

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
            var type = typeof(TypeWithMembers);
            var toStringMethod = new Methods<TypeWithMembers>().Select(x => x.ToString());
            var sut = new TypeMembers(type);

            var actual = sut.ToArray();

            Assert.DoesNotContain(toStringMethod, actual);
        }

        [Fact]
        public void SutEnumeratesStaticMember()
        {
            var sut = new TypeMembers(typeof(TypeWithMembers));
            var actual = sut.OfType<MethodInfo>().ToArray();
            Assert.True(actual.Any(m => m.IsStatic), "Static Member.");
        }

        [Fact]
        public void SutDoesNotEnumeratesAnyAccessors()
        {
            var sut = new TypeMembers(typeof(TypeWithMembers));
            var actual = sut.OfType<MethodInfo>().ToArray();
            Assert.True(
                actual.All(m => !m.Name.StartsWith("set_") && !m.Name.StartsWith("get_")),
                "Does not contain any accessors.");
        }

        [Fact]
        public void SutDoesNotEnumeratesAnyHelperMethodsOfEvent()
        {
            var sut = new TypeMembers(typeof(TypeWithMembers));
            var actual = sut.ToArray();
            Assert.True(
                actual.All(m => !m.Name.StartsWith("add_") && !m.Name.StartsWith("remove_")),
                "Does not contain any helper methods of EventInfo.");
        }

        [Fact]
        public void AccessibilitiesIsCorrectWhenInitializedWithModestCtor()
        {
            var sut = new TypeMembers(typeof(object));
            var actual = sut.Accessibilities;
            Assert.Equal(Accessibilities.Default, actual);
        }

        [Fact]
        public void TypeIsCorrectWhenInitializedWithGreedyCtor()
        {
            var type = GetType();
            var sut = new TypeMembers(type, Accessibilities.Default);

            var actual = sut.Type;

            Assert.Equal(type, actual);
        }

        [Fact]
        public void AccessibilitiesIsCorrectWhenInitializedWithGreedyCtor()
        {
            var accessibilities = Accessibilities.Private;
            var sut = new TypeMembers(typeof(object), accessibilities);
            var actual = sut.Accessibilities;
            Assert.Equal(accessibilities, actual);
        }

        [Fact]
        public void InitializeGreedyCtorWithNullTypeThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TypeMembers(null, Accessibilities.Default));
        }

        [Fact]
        public void SutEnumeratesCorrectMembersForPrivateAccessibilities()
        {
            var sut = new TypeMembers(typeof(TypeWithMembers), Accessibilities.Private);

            var actual = sut.Select(
                m => m.ToReflectionElement()
                    .Accept(new AccessibilityCollector())
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
                actual.All(a => (a & Accessibilities.Private) == Accessibilities.Private), "All Private.");
        }

        [Fact]
        public void SutEnumeratesCorrectMembersForProtectedAccessibilities()
        {
            var sut = new TypeMembers(typeof(TypeWithMembers), Accessibilities.Protected);

            var actual = sut.Select(
                m => m.ToReflectionElement()
                    .Accept(new AccessibilityCollector())
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
            var sut = new TypeMembers(
                typeof(TypeWithMembers),
                Accessibilities.Internal | Accessibilities.Private);

            var actual = sut.Select(
                m => m.ToReflectionElement()
                    .Accept(new AccessibilityCollector())
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