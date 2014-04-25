using System;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo.Refraction;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class ConstructingMemberAssertionTestCasesTest
    {
        [Fact]
        public void SutIsIdiomaticTestCases()
        {
            var sut = new ConstructingMemberAssertionTestCases(GetType());
            Assert.IsAssignableFrom<IdiomaticTestCases>(sut);
        }

        [Fact]
        public void TypeIsCorrect()
        {
            Type type = typeof(string);
            var sut = new ConstructingMemberAssertionTestCases(type);

            var actual = sut.Type;

            Assert.Equal(type, actual);
        }

        [Fact]
        public void ExcludedMembersIsCorrectWhenInitalizedWithType()
        {
            MemberInfo[] excludedMembers = GetType().GetMembers();
            var sut = new ConstructingMemberAssertionTestCases(GetType(), excludedMembers);

            var actual = sut.ExcludedMembers;

            Assert.Equal(excludedMembers, actual);
        }

        [Fact]
        public void ReflectionElementsHasCorrectExcludingMembersWhenInitalizedWithType()
        {
            var excludedMembers = typeof(object).GetMembers();
            var sut = new ConstructingMemberAssertionTestCases(GetType(), excludedMembers);

            var actual = sut.ReflectionElements;

            var excludingMembers = Assert.IsAssignableFrom<ExcludingMembers>(
                Assert.IsAssignableFrom<ExcludingWriteOnlyProperties>(
                    Assert.IsAssignableFrom<ReflectionElements>(actual).Sources)
                .TargetMembers);
            Assert.Equal(excludedMembers, excludingMembers.ExcludedMembers);
        }

        [Fact]
        public void ReflectionElementsHasCorrectTypeMembersWhenInitalizedWithType()
        {
            var type = GetType();
            var sut = new ConstructingMemberAssertionTestCases(type);

            var actual = sut.ReflectionElements;

            var typeMembers = Assert.IsAssignableFrom<TypeMembers>(
                Assert.IsAssignableFrom<ExcludingMembers>(
                    Assert.IsAssignableFrom<ExcludingWriteOnlyProperties>(
                        Assert.IsAssignableFrom<ReflectionElements>(actual).Sources)
                    .TargetMembers)
                .TargetMembers);
            Assert.Equal(type, typeMembers.Type);
            Assert.Equal(Accessibilities.Public, typeMembers.Accessibilities);
        }

        [Fact]
        public void ReflectionElementsHasCorrectRefractionsWhenInitalizedWithType()
        {
            var sut = new ConstructingMemberAssertionTestCases(GetType());

            var actual = sut.ReflectionElements;

            var refractions = Assert.IsAssignableFrom<ReflectionElements>(actual).Refractions;
            Assert.Equal(
                new[]
                {
                    typeof(ConstructorInfoElementRefraction<object>),
                    typeof(PropertyInfoElementRefraction<object>),
                    typeof(FieldInfoElementRefraction<object>)
                }.OrderBy(t => t.FullName),
                refractions.Select(r => r.GetType()).OrderBy(t => t.FullName));
        }

        [Fact]
        public void AssertionFactoryIsCorrect()
        {
            var sut = new ConstructingMemberAssertionTestCases(GetType());
            var actual = sut.AssertionFactory;
            Assert.IsAssignableFrom<ConstructingMemberAssertionFactory>(actual);
        }

        [Fact]
        public void AssemblyIsCorrect()
        {
            var assembly = typeof(string).Assembly;
            var sut = new ConstructingMemberAssertionTestCases(assembly);

            var actual = sut.Assembly;

            Assert.Equal(assembly, actual);
        }

        [Fact]
        public void ExcludedMembersIsCorrectWhenInitalizedWithAssembly()
        {
            MemberInfo[] excludedMembers = GetType().GetMembers();
            var sut = new ConstructingMemberAssertionTestCases(GetType().Assembly, excludedMembers);

            var actual = sut.ExcludedMembers;

            Assert.Equal(excludedMembers, actual);
        }

        [Fact]
        public void ReflectionElementsHasCorrectExcludingMembersWhenInitalizedWithAssembly()
        {
            var excludedMembers = typeof(object).GetMembers();
            var sut = new ConstructingMemberAssertionTestCases(GetType().Assembly, excludedMembers);

            var actual = sut.ReflectionElements;

            var excludingMembers = Assert.IsAssignableFrom<ExcludingMembers>(
                Assert.IsAssignableFrom<ExcludingWriteOnlyProperties>(
                    Assert.IsAssignableFrom<ReflectionElements>(actual).Sources)
                .TargetMembers);
            Assert.Equal(excludedMembers, excludingMembers.ExcludedMembers);
        }

        [Fact]
        public void ReflectionElementsHasCorrectTypeMembersWhenInitalizedWithAssembly()
        {
            var assembly = typeof(object).Assembly;
            var sut = new ConstructingMemberAssertionTestCases(assembly);

            var actual = sut.ReflectionElements;

            var compositeTypeMembers = Assert.IsAssignableFrom<CompositeEnumerable<MemberInfo>>(
                Assert.IsAssignableFrom<ExcludingMembers>(
                    Assert.IsAssignableFrom<ExcludingWriteOnlyProperties>(
                        Assert.IsAssignableFrom<ReflectionElements>(actual).Sources)
                    .TargetMembers)
                .TargetMembers)
            .ItemSet.Cast<TypeMembers>().ToArray();
            Assert.Equal(
                assembly.GetExportedTypes().Where(t => !t.IsInterface),
                compositeTypeMembers.Select(t => t.Type));
            Assert.True(compositeTypeMembers.All(t => t.Accessibilities == Accessibilities.Public));
        }

        [Fact]
        public void ReflectionElementsHasCorrectRefractionsWhenInitalizedWithAssembly()
        {
            var sut = new ConstructingMemberAssertionTestCases(GetType().Assembly);

            var actual = sut.ReflectionElements;

            var refractions = Assert.IsAssignableFrom<ReflectionElements>(actual).Refractions;
            Assert.Equal(
                new[]
                {
                    typeof(ConstructorInfoElementRefraction<object>),
                    typeof(PropertyInfoElementRefraction<object>),
                    typeof(FieldInfoElementRefraction<object>)
                }.OrderBy(t => t.FullName),
                refractions.Select(r => r.GetType()).OrderBy(t => t.FullName));
        }

        [Fact]
        public void InitializeWithNullAssemblyThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new ConstructingMemberAssertionTestCases((Assembly)null));
        }
    }
}