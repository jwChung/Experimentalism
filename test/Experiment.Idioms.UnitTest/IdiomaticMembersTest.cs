namespace Jwc.Experiment
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Jwc.Experiment.Idioms;
    using Ploeh.Albedo;
    using Ploeh.Albedo.Refraction;
    using global::Xunit;

    public class IdiomaticMembersTest
    {
        [Fact]
        public void SutIsEnumerable()
        {
            var sut = new IdiomaticMembers(typeof(object), MemberKinds.Default);
            Assert.IsAssignableFrom<IEnumerable<MemberInfo>>(sut);
        }

        [Fact]
        public void InitializeModestCtorWithNullTypeThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new IdiomaticMembers(null));
        }

        [Fact]
        public void InitializeGreedyCtorWithNullTypeThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new IdiomaticMembers(null, MemberKinds.Default));
        }

        [Fact]
        public void TypeIsCorrectWhenInitializedByModestCtor()
        {
            var type = GetType();
            var sut = new IdiomaticMembers(type);

            var actual = sut.Type;

            Assert.Equal(type, actual);
        }

        [Fact]
        public void TypeIsCorrectWhenInitializedByGreedyCtor()
        {
            var type = GetType();
            var sut = new IdiomaticMembers(type, MemberKinds.Default);

            var actual = sut.Type;

            Assert.Equal(type, actual);
        }

        [Fact]
        public void MemberKindsIsCorrectWhenInitializedByModestCtor()
        {
            var sut = new IdiomaticMembers(GetType());
            var actual = sut.MemberKinds;
            Assert.Equal(MemberKinds.Default, actual);
        }

        [Fact]
        public void MemberKindsIsCorrectWhenInitializedByGreedyCtor()
        {
            var memberKinds = MemberKinds.InstanceMethod;
            var sut = new IdiomaticMembers(GetType(), memberKinds);

            var actual = sut.MemberKinds;

            Assert.Equal(memberKinds, actual);
        }

        [Fact]
        public void SutEnumeratesAllKindsOfMembersWhenMemberKindsIsDefault()
        {
            var sut = new IdiomaticMembers(typeof(ClassWithMembers), MemberKinds.Default);

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
            var sut = new IdiomaticMembers(typeof(ClassWithMembers), MemberKinds.Default);

            var actual = sut.ToArray();

            Assert.DoesNotContain(nonDeclaredMember, actual);
        }

        [Fact]
        public void SutEnumeratesStaticMembersWhenMemberKindsIsDefault()
        {
            var sut = new IdiomaticMembers(typeof(ClassWithMembers), MemberKinds.Default);
            var actual = sut.OfType<MethodInfo>().ToArray();
            Assert.True(actual.Any(m => m.IsStatic), "Static Member.");
        }

        [Fact]
        public void SutDoesNotEnumerateAnyAccessors()
        {
            var sut = new IdiomaticMembers(typeof(ClassWithMembers), MemberKinds.Default);

            var actual = sut.OfType<MethodInfo>().ToArray();

            var result = actual.All(m => !m.Name.StartsWith("set_") && !m.Name.StartsWith("get_"));
            Assert.True(result, "Do not enumerate any accessors.");
        }

        [Fact]
        public void SutDoesNotEnumeratesAnyEventMethods()
        {
            var sut = new IdiomaticMembers(typeof(ClassWithMembers), MemberKinds.Default);

            var actual = sut.ToArray();

            var result = actual.All(m => !m.Name.StartsWith("add_") && !m.Name.StartsWith("remove_"));
            Assert.True(result, "Do not enumerate any helper methods of EventInfo.");
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
            var sut = new IdiomaticMembers(typeof(ClassWithMembers), MemberKinds.InstanceProperty);
            var actual = sut.ToArray();
            Assert.True(
                actual.All(m => (GetMemberKinds(m) & MemberKinds.InstanceProperty) != MemberKinds.None),
                "Property.");
        }

        [Fact]
        public void SutEnumeratesCorrectMembersWhenMemberKindsIsConstructorOrMethod()
        {
            var sut = new IdiomaticMembers(
                typeof(ClassWithMembers),
                MemberKinds.InstanceConstructor | MemberKinds.InstanceMethod);

            var actual = sut.ToArray();

            var result = actual.All(
                m => GetMemberKinds(m) == MemberKinds.InstanceConstructor ||
                     GetMemberKinds(m) == MemberKinds.InstanceMethod);
            Assert.True(result, "Constructor or Method.");
        }

        [Fact]
        public void SutEnumeratesWritablePropertiesWhenMemberKindsIsGetProperty()
        {
            var sut = new IdiomaticMembers(typeof(ClassWithMembers), MemberKinds.InstanceGetProperty);

            var actual = sut.ToArray();

            Assert.True(actual.Any(m => GetMemberKinds(m) == MemberKinds.InstanceGetProperty), "GetProperty.");
            Assert.True(actual.Any(m => GetMemberKinds(m) == MemberKinds.InstanceProperty), "Property.");
        }

        [Fact]
        public void SutEnumeratesOnlyPublicMembers()
        {
            var sut = new IdiomaticMembers(typeof(ClassWithMembers), MemberKinds.Default);

            var actual = sut.ToArray();

            var result = actual.All(m => (GetAccessibilities(m) & Accessibilities.Public) == Accessibilities.Public);
            Assert.True(result, "Enumerate only public members.");
        }

        [Fact]
        public void SutDoesNotEnumeratNestedTypes()
        {
            var sut = new IdiomaticMembers(typeof(IndirectReferenceAssertionTest), MemberKinds.Default);
            var actual = sut.ToArray();
            Assert.True(actual.All(m => !(m is Type)), "No Types.");
        }

        [Fact]
        public void SutEnumeratesCorrectMembersWhenMemberKindsIsStaticEvent()
        {
            var sut = new IdiomaticMembers(
                typeof(ClassWithMembers),
                MemberKinds.StaticEvent);
            var actual = sut.ToArray();
            Assert.True(actual.Cast<EventInfo>().All(ei => ei.GetAddMethod(true).IsStatic));
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