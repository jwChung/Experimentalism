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
        public void DefaultBindingFlagsIsCorrect()
        {
            BindingFlags expected =
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly |
            BindingFlags.Static | BindingFlags.Instance;
            var actual = TypeMembers.DefaultBindingFlags;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InitializeWithNullTypeThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TypeMembers(null));
        }

        [Fact]
        public void TypeIsCorrect()
        {
            var type = GetType();
            var sut = new TypeMembers(type);

            var actual = sut.Type;

            Assert.Equal(type, actual);
        }

        [Fact]
        public void MemberKindsIsCorrect()
        {
            var memberKinds = MemberKinds.Method;
            var sut = new TypeMembers(GetType(), memberKinds);

            var actual = sut.MemberKinds;

            Assert.Equal(memberKinds, actual);
        }

        [Fact]
        public void MemberKindsCorrectDefaultValue()
        {
            var type = GetType();
            var sut = new TypeMembers(type);

            var actual = sut.Type;

            Assert.Equal(type, actual);
        }
        
        [Fact]
        public void AccessibilitiesIsCorrect()
        {
            var accessibilities = Accessibilities.Protected;
            var sut = new TypeMembers(GetType(), accessibilities: accessibilities);

            var actual = sut.Accessibilities;

            Assert.Equal(accessibilities, actual);
        }

        [Fact]
        public void AccessibilitiesReturnsCorrectDefaultValue()
        {
            var sut = new TypeMembers(GetType());
            var actual = sut.Accessibilities;
            Assert.Equal(Accessibilities.Public, actual);
        }

        [Fact]
        public void BindingFalgsIsCorrect()
        {
            var bindingFlags = BindingFlags.IgnoreReturn;
            var sut = new TypeMembers(GetType(), bindingFlags: bindingFlags);

            var actual = sut.BindingFlags;

            Assert.Equal(bindingFlags, actual);
        }

        [Fact]
        public void BindingFalgsReturnsCorrectValue()
        {
            var sut = new TypeMembers(GetType());
            BindingFlags bindingFlags =
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly |
            BindingFlags.Static | BindingFlags.Instance;

            var actual = sut.BindingFlags;

            Assert.Equal(bindingFlags, actual);
        }

        [Fact]
        public void SutEnumeratesAllKindsOfMemberWhenMemberKindsIsDefault()
        {
            var sut = new TypeMembers(typeof(ClassWithMembers), accessibilities: Accessibilities.All);

            var actual = sut.ToArray();

            Assert.True(actual.Any(m => m is FieldInfo), "FieldInfo.");
            Assert.True(actual.Any(m => m is ConstructorInfo), "ConstructorInfo.");
            Assert.True(actual.Any(m => m is PropertyInfo), "PropertyInfo.");
            Assert.True(actual.Any(m => m is MethodInfo), "MethodInfo.");
            Assert.True(actual.Any(m => m is EventInfo), "EventInfo.");
        }

        [Fact]
        public void SutEnumeratesOnlyDeclaredMembersWhenBindingFlagsIsDefault()
        {
            var nonDeclaredMember = new Methods<ClassWithMembers>().Select(x => x.ToString());
            var sut = new TypeMembers(typeof(ClassWithMembers), accessibilities: Accessibilities.All);

            var actual = sut.ToArray();

            Assert.DoesNotContain(nonDeclaredMember, actual);
        }

        [Fact]
        public void SutEnumeratesStaticMembersWhenBindingFlagsIsDefault()
        {
            var sut = new TypeMembers(typeof(ClassWithMembers), accessibilities: Accessibilities.All);
            var actual = sut.OfType<MethodInfo>().ToArray();
            Assert.True(actual.Any(m => m.IsStatic), "Static Member.");
        }

        [Fact]
        public void SutDoesNotEnumeratesAnyAccessors()
        {
            var sut = new TypeMembers(typeof(ClassWithMembers), accessibilities: Accessibilities.All);

            var actual = sut.OfType<MethodInfo>().ToArray();

            var result = actual.All(m => !m.Name.StartsWith("set_") && !m.Name.StartsWith("get_"));
            Assert.True(result, "Do not enumerate any accessors.");
        }

        [Fact]
        public void SutDoesNotEnumeratesAnyEventMethods()
        {
            var sut = new TypeMembers(typeof(ClassWithMembers), accessibilities: Accessibilities.All);

            var actual = sut.ToArray();

            var result = actual.All(m => !m.Name.StartsWith("add_") && !m.Name.StartsWith("remove_"));
            Assert.True(result, "Do not enumerate any helper methods of EventInfo.");
        }

        [Fact]
        public void SutEnumeratesNoMembersWhenMemberKindsIsNone()
        {
            var sut = new TypeMembers(
                typeof(ClassWithMembers), MemberKinds.None, Accessibilities.All);
            var actual = sut.ToArray();
            Assert.Empty(actual);
        }

        [Fact]
        public void SutEnumeratesOnlyPropertiesWhenMemberKindsIsProperty()
        {
            var sut = new TypeMembers(
                typeof(ClassWithMembers), MemberKinds.Property, Accessibilities.All);

            var actual = sut.ToArray();

            Assert.True(actual.Any(m => GetMemberKinds(m) == MemberKinds.GetProperty), "GetProperty.");
            Assert.True(actual.Any(m => GetMemberKinds(m) == MemberKinds.SetProperty), "SetProperty.");
            Assert.True(actual.Any(m => GetMemberKinds(m) == MemberKinds.Property), "Property.");
        }

        [Fact]
        public void SutEnumeratesCorrectMembersWhenMemberKindsIsConstructorOrMethod()
        {
            var sut = new TypeMembers(
                typeof(ClassWithMembers), MemberKinds.Constructor | MemberKinds.Method, Accessibilities.All);

            var actual = sut.ToArray();

            var result = actual.All(
                m => GetMemberKinds(m) == MemberKinds.Constructor
                    || GetMemberKinds(m) == MemberKinds.Method);
            Assert.True(result, "Constructor or Method.");
        }

        [Fact]
        public void SutEnumeratesWritablePropertiesWhenMemberKindsIsGetProperty()
        {
            var sut = new TypeMembers(
                typeof(ClassWithMembers), MemberKinds.GetProperty, Accessibilities.All);

            var actual = sut.ToArray();

            Assert.True(actual.Any(m => GetMemberKinds(m) == MemberKinds.GetProperty), "GetProperty.");
            Assert.True(actual.Any(m => GetMemberKinds(m) == MemberKinds.Property), "Property.");
        }

        [Fact]
        public void SutEnumeratesNoMembersWhenAccessibilitiesIsNone()
        {
            var sut = new TypeMembers(typeof(ClassWithMembers), accessibilities: Accessibilities.None);
            var actual = sut.ToArray();
            Assert.Empty(actual);
        }

        [Fact]
        public void SutEnumeratesCorrectMembersWhenAccessibilitiesIsProtected()
        {
            var sut = new TypeMembers(typeof(ClassWithMembers), accessibilities: Accessibilities.Protected);

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
            var sut = new TypeMembers(typeof(ClassWithMembers));

            var actual = sut.ToArray();

            var result = actual.All(m => (GetAccessibilities(m) & Accessibilities.Public) == Accessibilities.Public);
            Assert.True(result, "Enumerate only public members.");
        }

        [Fact]
        public void SutDoesNotEnumeratNestedTypes()
        {
            var sut = new TypeMembers(typeof(IndirectReferenceAssertionTest));
            var actual = sut.ToArray();
            Assert.True(actual.All(m => !(m is Type)), "No Types.");
        }

        [Fact]
        public void SutEnumeratesCorrectMembersWhenBindingFlagsIsNotInstanceAndMemberKindsIsEvent()
        {
            var sut = new TypeMembers(
                typeof(ClassWithMembers),
                MemberKinds.Event,
                Accessibilities.All,
                TypeMembers.DefaultBindingFlags & ~BindingFlags.Instance);
            var actual = sut.ToArray();
            Assert.True(actual.Cast<EventInfo>().All(ei => ei.GetAddMethod(true).IsStatic));
        }

        [Fact]
        public void SutEnumeratesCorrectMembersWhenBindingFlagsIsNotDeclaredOnlyAndMemberKindsIsEventAndMethod()
        {
            var sut = new TypeMembers(
                typeof(SubClassWithMembers),
                MemberKinds.Event | MemberKinds.Method,
                Accessibilities.All,
                TypeMembers.DefaultBindingFlags & ~BindingFlags.DeclaredOnly);

            var actual = sut.ToArray();

            var result = actual.All(m => !m.Name.StartsWith("add_") && !m.Name.StartsWith("remove_"));
            Assert.True(result, "Do not enumerate any helper methods of EventInfo.");
        }

        [Fact]
        public void SutEnumeratesCorrectMembersWhenBindingFlagsIsNotDeclaredOnlyAndMemberKindsIsPropertyAndMethod()
        {
            var sut = new TypeMembers(
                typeof(SubClassWithMembers),
                MemberKinds.Property | MemberKinds.Method,
                Accessibilities.All,
                TypeMembers.DefaultBindingFlags & ~BindingFlags.DeclaredOnly);

            var actual = sut.ToArray();

            var result = actual.All(m => !m.Name.StartsWith("get_") && !m.Name.StartsWith("set_"));
            Assert.True(result, "Do not enumerate any accessors.");
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