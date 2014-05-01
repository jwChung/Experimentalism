using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jwc.Experiment.Idioms.Assertions;
using Ploeh.Albedo;
using Ploeh.Albedo.Refraction;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class TypeMembersTest
    {
        [Fact]
        public void SutIsEnumerable()
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
        public void InitializeModestCtorWithNullTypeAndMemberKindsThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TypeMembers(null, MemberKinds.Field));
        }

        [Fact]
        public void InitializeGreedyCtorWithNullTypeAndMemberKindsThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new TypeMembers(null, MemberKinds.Field, Accessibilities.Default));
        }

        [Fact]
        public void InitializeModestCtorWithNullTypeAndAccessibilitiesThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new TypeMembers(null, Accessibilities.Default));
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
        public void TypeIsCorrectWhenInitializedWithModestCtorWithMemberKinds()
        {
            var expected = typeof(int);
            var sut = new TypeMembers(expected, MemberKinds.All);

            var actual = sut.Type;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TypeIsCorrectWhenInitializedWithGreedyCtorWithMemberKinds()
        {
            var expected = typeof(string);
            var sut = new TypeMembers(expected, MemberKinds.All, Accessibilities.Default);

            var actual = sut.Type;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TypeIsCorrectWhenInitializedWithModestCtorWithAccessibilities()
        {
            var expected = typeof(string);
            var sut = new TypeMembers(expected, Accessibilities.Default);

            var actual = sut.Type;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MemberKindsIsCorrectWhenInitializedWithModestCtor()
        {
            var sut = new TypeMembers(GetType());
            var actual = sut.MemberKinds;
            Assert.Equal(MemberKinds.All, actual);
        }

        [Fact]
        public void MemberKindsIsCorrectWhenInitializedWithModestCtorWithMemberKinds()
        {
            var expected = MemberKinds.Event;
            var sut = new TypeMembers(GetType(), expected);

            var actual = sut.MemberKinds;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MemberKindsIsCorrectWhenInitializedWithGreedyCtorWithMemberKinds()
        {
            var expected = MemberKinds.Event;
            var sut = new TypeMembers(GetType(), expected, Accessibilities.Default);

            var actual = sut.MemberKinds;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MemberKindsIsCorrectWhenInitializedWithModestCtorWithAccessibilities()
        {
            var expected = MemberKinds.All;
            var sut = new TypeMembers(GetType(), Accessibilities.Default);

            var actual = sut.MemberKinds;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AccessibilitiesIsCorrectWhenInitializedWithModestCtor()
        {
            var sut = new TypeMembers(GetType());
            var actual = sut.Accessibilities;
            Assert.Equal(Accessibilities.Public, actual);
        }

        [Fact]
        public void AccessibilitiesIsCorrectWhenInitializedWithModestCtorWithMemberKinds()
        {
            var sut = new TypeMembers(GetType(), MemberKinds.Default);
            var actual = sut.Accessibilities;
            Assert.Equal(Accessibilities.Public, actual);
        }

        [Fact]
        public void AccessibilitiesIsCorrectWhenInitializedWithGreedyCtorWithMemberKinds()
        {
            var accessibilities = Accessibilities.Private;
            var sut = new TypeMembers(GetType(), MemberKinds.Default, accessibilities);

            var actual = sut.Accessibilities;

            Assert.Equal(accessibilities, actual);
        }

        [Fact]
        public void AccessibilitiesIsCorrectWhenInitializedWithModestCtorWithAccessibilities()
        {
            var accessibilities = Accessibilities.ProtectedInternal;
            var sut = new TypeMembers(GetType(), accessibilities);

            var actual = sut.Accessibilities;

            Assert.Equal(accessibilities, actual);
        }

        [Fact]
        public void SutEnumeratesAllKindsOfMember()
        {
            var sut = new TypeMembers(typeof(ClassWithMembers), Accessibilities.All);

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
            var sut = new TypeMembers(typeof(ClassWithMembers), Accessibilities.All);

            var actual = sut.ToArray();

            Assert.DoesNotContain(nonDeclaredMember, actual);
        }

        [Fact]
        public void SutEnumeratesStaticMembers()
        {
            var sut = new TypeMembers(typeof(ClassWithMembers), Accessibilities.All);
            var actual = sut.OfType<MethodInfo>().ToArray();
            Assert.True(actual.Any(m => m.IsStatic), "Static Member.");
        }

        [Fact]
        public void SutDoesNotEnumeratesAnyAccessors()
        {
            var sut = new TypeMembers(typeof(ClassWithMembers), Accessibilities.All);

            var actual = sut.OfType<MethodInfo>().ToArray();

            var result = actual.All(m => !m.Name.StartsWith("set_") && !m.Name.StartsWith("get_"));
            Assert.True(result, "Do not enumerate any accessors.");
        }

        [Fact]
        public void SutDoesNotEnumeratesAnyEventMethods()
        {
            var sut = new TypeMembers(typeof(ClassWithMembers), Accessibilities.All);

            var actual = sut.ToArray();

            var result = actual.All(m => !m.Name.StartsWith("add_") && !m.Name.StartsWith("remove_"));
            Assert.True(result, "Do not enumerate any helper methods of EventInfo.");
        }

        [Fact]
        public void SutEnumeratesNoMembersWhenMemberKindsIsNone()
        {
            var sut = new TypeMembers(typeof(ClassWithMembers), MemberKinds.None, Accessibilities.All);
            var actual = sut.ToArray();
            Assert.Empty(actual);
        }

        [Fact]
        public void SutEnumeratesOnlyPropertiesWhenMemberKindsIsProperty()
        {
            var sut = new TypeMembers(typeof(ClassWithMembers), MemberKinds.Property, Accessibilities.All);

            var actual = sut.ToArray();

            Assert.True(actual.Any(m => GetMemberKinds(m) == MemberKinds.GetProperty), "GetProperty.");
            Assert.True(actual.Any(m => GetMemberKinds(m) == MemberKinds.SetProperty), "SetProperty.");
            Assert.True(actual.Any(m => GetMemberKinds(m) == MemberKinds.Property), "Property.");
        }

        [Fact]
        public void SutEnumeratesCorrectMembersWhenMemberKindsIsConstructorOrMethod()
        {
            var sut = new TypeMembers(
                typeof(ClassWithMembers),
                MemberKinds.Constructor | MemberKinds.Method,
                Accessibilities.All);

            var actual = sut.ToArray();

            var result = actual.All(
                m => GetMemberKinds(m) == MemberKinds.Constructor
                    || GetMemberKinds(m) == MemberKinds.Method);
            Assert.True(result, "Constructor or Method.");
        }

        [Fact]
        public void SutEnumeratesWritablePropertiesWhenMemberKindsIsGetProperty()
        {
            var sut = new TypeMembers(typeof(ClassWithMembers), MemberKinds.GetProperty, Accessibilities.All);

            var actual = sut.ToArray();

            Assert.True(actual.Any(m => GetMemberKinds(m) == MemberKinds.GetProperty), "GetProperty.");
            Assert.True(actual.Any(m => GetMemberKinds(m) == MemberKinds.Property), "Property.");
        }

        [Fact]
        public void SutEnumeratesNoMembersWhenAccessibilitiesIsNone()
        {
            var sut = new TypeMembers(typeof(ClassWithMembers), Accessibilities.None);
            var actual = sut.ToArray();
            Assert.Empty(actual);
        }

        [Fact]
        public void SutEnumeratesCorrectMembersWhenAccessibilitiesIsProtected()
        {
            var sut = new TypeMembers(typeof(ClassWithMembers), Accessibilities.Protected);

            var actual = sut.ToArray();

            Assert.True(
                actual.Any(m => GetAccessibilities(m) == Accessibilities.Protected),
                "Protected.");
            Assert.True(
                actual.Any(m => GetAccessibilities(m) == Accessibilities.ProtectedInternal),
                "ProtectedInternal.");
        }

        [Fact]
        public void SutEnumeratesOnlyPublicMembersWhenAccessibilitiesIsPublic()
        {
            var sut = new TypeMembers(typeof(ClassWithMembers), Accessibilities.Public);

            var actual = sut.ToArray();

            var result = actual.All(m => (GetAccessibilities(m) & Accessibilities.Public) == Accessibilities.Public);
            Assert.True(result, "Enumerate only public members.");
        }

        [Fact]
        public void SutDoesNotEnumeratNestedTypes()
        {
            var sut = new TypeMembers(typeof(IndirectReferenceAssertionTest), Accessibilities.Public);
            var actual = sut.ToArray();
            Assert.True(actual.All(m => !(m is Type)), "No Types.");
        }
        
        private static Accessibilities GetAccessibilities(MemberInfo member)
        {
            return member.ToReflectionElement().Accept(new AccessibilityCollector()).Value.Single();
        }

        private static MemberKinds GetMemberKinds(MemberInfo member)
        {
            return member.ToReflectionElement().Accept(new MemberKindCollector()).Value.Single();
        }
    }
}