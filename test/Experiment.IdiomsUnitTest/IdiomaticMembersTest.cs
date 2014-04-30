using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;
using Ploeh.Albedo.Refraction;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class IdiomaticMembersTest
    {
        [Fact]
        public void SutIsEnumerable()
        {
            var sut = new IdiomaticMembers(typeof(object));
            Assert.IsAssignableFrom<IEnumerable<MemberInfo>>(sut);
        }

        [Fact]
        public void InitializeWithNullTypeThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new IdiomaticMembers(null));
        }

        [Fact]
        public void InitializeWithNullTypeAndMemberKindsThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new IdiomaticMembers(null, MemberKinds.Field));
        }

        [Fact]
        public void TypeIsCorrect()
        {
            var type = GetType();
            var sut = new IdiomaticMembers(type);

            var actual = sut.Type;

            Assert.Equal(type, actual);
        }

        [Fact]
        public void TypeIsCorrectWhenInitializedWithMemberKinds()
        {
            var expected = typeof(int);
            var sut = new IdiomaticMembers(expected, MemberKinds.All);
            var actual = sut.Type;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MemberKindsIsCorrect()
        {
            var sut = new IdiomaticMembers(GetType());
            var actual = sut.MemberKinds;
            Assert.Equal(MemberKinds.All, actual);
        }

        [Fact]
        public void MemberKindsIsCorrectWhenInitializedWithMemberKinds()
        {
            var expected = MemberKinds.Event;
            var sut = new IdiomaticMembers(GetType(), expected);
            var actual = sut.MemberKinds;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SutEnumeratesAllKindsOfMember()
        {
            var sut = new IdiomaticMembers(typeof(ClassWithMembers));

            var actual = sut.ToArray();

            Assert.True(actual.Any(m => m is FieldInfo), "FieldInfo.");
            Assert.True(actual.Any(m => m is ConstructorInfo), "ConstructorInfo.");
            Assert.True(actual.Any(m => m is PropertyInfo), "PropertyInfo.");
            Assert.True(actual.Any(m => m is MethodInfo), "MethodInfo.");
            Assert.True(actual.Any(m => m is EventInfo), "EventInfo.");
        }

        [Fact]
        public void SutEnumeratesOnlyDeclaredMembers()
        {
            var nonDeclaredMember = new Methods<ClassWithMembers>().Select(x => x.ToString());
            var sut = new IdiomaticMembers(typeof(ClassWithMembers));

            var actual = sut.ToArray();

            Assert.DoesNotContain(nonDeclaredMember, actual);
        }

        [Fact]
        public void SutEnumeratesStaticMembers()
        {
            var sut = new IdiomaticMembers(typeof(ClassWithMembers));
            var actual = sut.OfType<MethodInfo>().ToArray();
            Assert.True(actual.Any(m => m.IsStatic), "Static Member.");
        }

        [Fact]
        public void SutDoesNotEnumeratesAnyAccessors()
        {
            var sut = new IdiomaticMembers(typeof(ClassWithMembers));

            var actual = sut.OfType<MethodInfo>().ToArray();

            var result = actual.All(m => !m.Name.StartsWith("set_") && !m.Name.StartsWith("get_"));
            Assert.True(result, "Do not enumerate any accessors.");
        }

        [Fact]
        public void SutDoesNotEnumeratesAnyEventMethods()
        {
            var sut = new IdiomaticMembers(typeof(ClassWithMembers));

            var actual = sut.ToArray();

            var result = actual.All(m => !m.Name.StartsWith("add_") && !m.Name.StartsWith("remove_"));
            Assert.True(result, "Do not enumerate any helper methods of EventInfo.");
        }

        [Fact]
        public void SutEnumeratesOnlyPublicMembers()
        {
            var sut = new IdiomaticMembers(typeof(ClassWithMembers));

            var actual = sut.ToArray();

            var result = actual.All(m => (GetAccessibilities(m) & Accessibilities.Public) == Accessibilities.Public);
            Assert.True(result, "Enumerate only public members.");
        }

        [Fact]
        public void SutEnumeratesNoMembersWhenMemberKindsIsNone()
        {
            var sut = new IdiomaticMembers(typeof(ClassWithMembers), MemberKinds.None);
            var actual = sut.ToArray();
            Assert.Empty(actual);
        }

        [Fact]
        public void SutEnumeratesOnlyPropertiesWhenMemberKindsIsProperty()
        {
            var sut = new IdiomaticMembers(typeof(ClassWithMembers), MemberKinds.Property);

            var actual = sut.ToArray();

            var result = actual.All(IsProperty);
            Assert.True(result, "Only Properties.");
        }

        [Fact]
        public void SutEnumeratesCorrectMembersWhenMemberKindsIsConstructorOrMethod()
        {
            var sut = new IdiomaticMembers(typeof(ClassWithMembers), MemberKinds.Constructor | MemberKinds.Method);

            var actual = sut.ToArray();

            var result = actual.All(
                m => GetMemberKinds(m) == MemberKinds.Constructor
                    || GetMemberKinds(m) == MemberKinds.Method);
            Assert.True(result, "Constructor or Method.");
        }

        [Fact]
        public void SutEnumeratesWritablePropertiesWhenMemberKindsIsGetProperty()
        {
            var sut = new IdiomaticMembers(typeof(ClassWithMembers), MemberKinds.GetProperty);

            var actual = sut.ToArray();

            var result = actual.All(IsWritableProperty);
            Assert.True(result, "Only Properties.");
        }

        private static Accessibilities GetAccessibilities(MemberInfo member)
        {
            return member.ToReflectionElement().Accept(new AccessibilityCollector()).Value.Single();
        }

        private static MemberKinds GetMemberKinds(MemberInfo member)
        {
            return member.ToReflectionElement().Accept(new MemberKindCollector()).Value.Single();
        }

        private static bool IsProperty(MemberInfo member)
        {
            return GetMemberKinds(member) == MemberKinds.GetProperty
                || GetMemberKinds(member) == MemberKinds.SetProperty
                || GetMemberKinds(member) == MemberKinds.Property;
        }

        private static bool IsWritableProperty(MemberInfo member)
        {
            return GetMemberKinds(member) == MemberKinds.GetProperty
                || GetMemberKinds(member) == MemberKinds.Property;
        }
    }
}