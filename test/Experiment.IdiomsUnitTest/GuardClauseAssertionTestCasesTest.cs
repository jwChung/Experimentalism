using System;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo.Refraction;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class GuardClauseAssertionTestCasesTest
    {
        [Fact]
        public void SutIsIdiomaticTestCases()
        {
            var sut = new GuardClauseAssertionTestCases(GetType());
            Assert.IsAssignableFrom<IdiomaticTestCases>(sut);
        }

        [Fact]
        public void ReflectionElementsHasCorrectRefractionSourcesWhenInitializedWithType()
        {
            // Fixture setup
            var type = GetType();
            var excludedMembers = typeof(object).GetMethods().Cast<MemberInfo>().ToArray();
            var sut = new GuardClauseAssertionTestCases(type, excludedMembers);

            // Excercise system
            var actual = sut.ReflectionElements;

            // Verify outcome
            var excludingMembers = Assert.IsAssignableFrom<ExcludingMembers>(
                Assert.IsAssignableFrom<ExcludingReadOnlyProperties>(
                    Assert.IsAssignableFrom<ReflectionElements>(
                        actual)
                    .Sources)
                .TargetMembers);

            var targetMembers = Assert.IsAssignableFrom<TypeMembers>(excludingMembers.TargetMembers);
            Assert.Equal(type, targetMembers.Type);
            Assert.Equal(Accessibilities.Public, targetMembers.Accessibilities);

            Assert.Equal(excludedMembers, excludingMembers.ExcludedMembers);
        }
        
        [Fact]
        public void ReflectionElementsHasCorrectRefractionsWhenInitializedWithType()
        {
            var sut = new GuardClauseAssertionTestCases(GetType());

            var actual = sut.ReflectionElements;

            var reflectionElements = Assert.IsAssignableFrom<ReflectionElements>(actual);
            Assert.Equal(
                new[]
                {
                    typeof(ConstructorInfoElementRefraction<object>),
                    typeof(PropertyInfoElementRefraction<object>),
                    typeof(MethodInfoElementRefraction<object>)
                }
                .OrderBy(t => t.FullName),
                reflectionElements.Refractions.Select(r => r.GetType()).OrderBy(t => t.FullName));
        }

        [Fact]
        public void AssertionFactoryIsCorrectWhenInitializedWithType()
        {
            var sut = new GuardClauseAssertionTestCases(GetType());
            var actual = sut.AssertionFactory;
            Assert.IsType<GuardClauseAssertionFactory>(actual);
        }

        [Fact]
        public void TypeIsCorrect()
        {
            Type type = GetType();
            var sut = new GuardClauseAssertionTestCases(type);

            var actual = sut.Type;

            Assert.Equal(type, actual);
        }

        [Fact]
        public void ExcludedMembersIsCorrectWhenInitializedWithType()
        {
            MemberInfo[] excludedMembers = GetType().GetMembers();
            var sut = new GuardClauseAssertionTestCases(GetType(), excludedMembers);

            var actual = sut.ExcludedMembers;

            Assert.Equal(excludedMembers, actual);
        }

        [Fact]
        public void ReflectionElementsHasCorrectRefractionSourcesWhenInitializedWithAssembly()
        {
            // Fixture setup
            var assembly = typeof(object).Assembly;
            var excludedMembers = typeof(object).GetMethods().Cast<MemberInfo>().ToArray();
            var sut = new GuardClauseAssertionTestCases(assembly, excludedMembers);

            // Excercise system
            var actual = sut.ReflectionElements;

            // Verify outcome
            var excludingMembers = Assert.IsAssignableFrom<ExcludingMembers>(
                Assert.IsAssignableFrom<ExcludingReadOnlyProperties>(
                    Assert.IsAssignableFrom<ReflectionElements>(
                        actual)
                    .Sources)
                .TargetMembers);

            var compositeEnumerable = Assert.IsAssignableFrom<CompositeEnumerable<MemberInfo>>(
                excludingMembers.TargetMembers);
            Assert.Equal(
                assembly.GetExportedTypes(),
                compositeEnumerable.ItemSet.Cast<TypeMembers>().Select(t => t.Type));
            Assert.True(
                compositeEnumerable.ItemSet.Cast<TypeMembers>()
                .All(t => t.Accessibilities == Accessibilities.Public),
                "Accessibilities.");

            Assert.Equal(excludedMembers, excludingMembers.ExcludedMembers);
        }

        [Fact]
        public void ReflectionElementsHasCorrectRefractionsWhenInitializedWithAssembly()
        {
            var sut = new GuardClauseAssertionTestCases(GetType().Assembly);

            var actual = sut.ReflectionElements;

            var reflectionElements = Assert.IsAssignableFrom<ReflectionElements>(actual);
            Assert.Equal(
                new[]
                {
                    typeof(ConstructorInfoElementRefraction<object>),
                    typeof(PropertyInfoElementRefraction<object>),
                    typeof(MethodInfoElementRefraction<object>)
                }
                .OrderBy(t => t.FullName),
                reflectionElements.Refractions.Select(r => r.GetType()).OrderBy(t => t.FullName));
        }

        [Fact]
        public void AssertionFactoryIsCorrectWhenInitializedWithAssembly()
        {
            var sut = new GuardClauseAssertionTestCases(GetType().Assembly);
            var actual = sut.AssertionFactory;
            Assert.IsType<GuardClauseAssertionFactory>(actual);
        }

        [Fact]
        public void AssemblyIsCorrect()
        {
            var assembly = GetType().Assembly;
            var sut = new GuardClauseAssertionTestCases(assembly);

            var actual = sut.Assembly;

            Assert.Equal(assembly, actual);
        }

        [Fact]
        public void ExcludedMembersIsCorrectWhenInitializedWithAssembly()
        {
            MemberInfo[] excludedMembers = GetType().GetMembers();
            var sut = new GuardClauseAssertionTestCases(GetType().Assembly, excludedMembers);

            var actual = sut.ExcludedMembers;

            Assert.Equal(excludedMembers, actual);
        }

        [Fact]
        public void InitializeWithNullAssemblyThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new GuardClauseAssertionTestCases((Assembly)null));
        }
    }
}