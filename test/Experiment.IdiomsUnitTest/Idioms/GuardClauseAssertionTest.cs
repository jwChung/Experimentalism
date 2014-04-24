using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Moq;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class GuardClauseAssertionTest
    {
        private const BindingFlags _bindingFlags =
           BindingFlags.Static | BindingFlags.Instance |
           BindingFlags.Public | BindingFlags.NonPublic;

        [Fact]
        public void SutIsAssertionAdapter()
        {
            var sut = new GuardClauseAssertion(new DelegatingTestFixture());
            Assert.IsAssignableFrom<AssertionAdapter>(sut);
        }

        [Fact]
        public void AssertionIsCorrect()
        {
            var testFixture = new DelegatingTestFixture();
            var sut = new GuardClauseAssertion(testFixture);

            var actual = sut.Assertion;

            var assertion = Assert.IsAssignableFrom<Ploeh.AutoFixture.Idioms.GuardClauseAssertion>(actual);
            var builder = Assert.IsAssignableFrom<SpecimenBuilderAdapter>(assertion.Builder);
            Assert.Equal(testFixture, builder.TestFixture);
        }

        [Fact]
        public void TestFixtureIsCorrect()
        {
            var testFixture = new DelegatingTestFixture();
            var sut = new GuardClauseAssertion(testFixture);

            var actual = sut.TestFixture;

            Assert.Equal(testFixture, actual);
        }

        [Fact]
        public void VisitTypeElementsPassesPublicElementsToBaseMethod()
        {
            // Fixture setup
            var sut = new Mock<GuardClauseAssertion>(new DelegatingTestFixture()) { CallBase = true }.Object;
            var visitor = new Mock<GuardClauseAssertion>(new DelegatingTestFixture()) { CallBase = true }.Object;

            var expectedElements = new List<TypeElement>();
            sut.ToMock().Setup(x => x.Visit(It.IsAny<TypeElement>())).Returns(visitor)
                .Callback<TypeElement>(expectedElements.Add);
            visitor.ToMock().Setup(x => x.Visit(It.IsAny<TypeElement>())).Returns(visitor)
                .Callback<TypeElement>(expectedElements.Add);
            var typeElements = typeof(object).Assembly.GetTypes().Select(t => t.ToElement()).ToArray();

            // Exercise system
            var actual = sut.Visit(typeElements);

            // Verify outcome
            Assert.Equal(visitor, actual);
            AssertArePublicElements(expectedElements);
        }

        [Fact]
        public void VisitConstructorInfoElementsPassesPublicElementsToBaseMethod()
        {
            // Fixture setup
            var sut = new Mock<GuardClauseAssertion>(new DelegatingTestFixture()) { CallBase = true }.Object;
            var visitor = new Mock<GuardClauseAssertion>(new DelegatingTestFixture()) { CallBase = true }.Object;

            var expectedElements = new List<ConstructorInfoElement>();
            sut.ToMock().Setup(x => x.Visit(It.IsAny<ConstructorInfoElement>())).Returns(visitor)
                .Callback<ConstructorInfoElement>(expectedElements.Add);
            visitor.ToMock().Setup(x => x.Visit(It.IsAny<ConstructorInfoElement>())).Returns(visitor)
                .Callback<ConstructorInfoElement>(expectedElements.Add);
            var constructorInfoElements = typeof(TypeWithMembers).GetConstructors(_bindingFlags)
                .Select(c => c.ToElement()).ToArray();

            // Exercise system
            var actual = sut.Visit(constructorInfoElements);

            // Verify outcome
            Assert.Equal(visitor, actual);
            AssertArePublicElements(expectedElements);
        }

        [Fact]
        public void VisitPropertyInfoElementsPassesPublicWritableElementsToBaseMethod()
        {
            // Fixture setup
            var sut = new Mock<GuardClauseAssertion>(new DelegatingTestFixture()) { CallBase = true }.Object;
            var visitor = new Mock<GuardClauseAssertion>(new DelegatingTestFixture()) { CallBase = true }.Object;

            var expectedElements = new List<PropertyInfoElement>();
            sut.ToMock().Setup(x => x.Visit(It.IsAny<PropertyInfoElement>())).Returns(visitor)
                .Callback<PropertyInfoElement>(expectedElements.Add);
            visitor.ToMock().Setup(x => x.Visit(It.IsAny<PropertyInfoElement>())).Returns(visitor)
                .Callback<PropertyInfoElement>(expectedElements.Add);
            var propertyInfoElements = typeof(TypeWithMembers).GetProperties(_bindingFlags)
                .Select(c => c.ToElement()).ToArray();

            // Exercise system
            var actual = sut.Visit(propertyInfoElements);

            // Verify outcome
            Assert.Equal(visitor, actual);
            AssertArePublicElements(expectedElements);
            Assert.True(expectedElements.All(e => e.PropertyInfo.GetSetMethod() != null));
        }

        [Fact]
        public void VisitMethodInfoElementsPassesPublicElementsToBaseMethod()
        {
            // Fixture setup
            var sut = new Mock<GuardClauseAssertion>(new DelegatingTestFixture()) { CallBase = true }.Object;
            var visitor = new Mock<GuardClauseAssertion>(new DelegatingTestFixture()) { CallBase = true }.Object;

            var expectedElements = new List<MethodInfoElement>();
            sut.ToMock().Setup(x => x.Visit(It.IsAny<MethodInfoElement>())).Returns(visitor)
                .Callback<MethodInfoElement>(expectedElements.Add);
            visitor.ToMock().Setup(x => x.Visit(It.IsAny<MethodInfoElement>())).Returns(visitor)
                .Callback<MethodInfoElement>(expectedElements.Add);
            var methodInfoElements = typeof(TypeWithMembers).GetMethods(_bindingFlags)
                .Select(c => c.ToElement()).ToArray();

            // Exercise system
            var actual = sut.Visit(methodInfoElements);

            // Verify outcome
            Assert.Equal(visitor, actual);
            AssertArePublicElements(expectedElements);
        }

        private static void AssertArePublicElements(IEnumerable<IReflectionElement> reflectionElements)
        {
            var result = reflectionElements.ToArray();

            Assert.True(
                result.ToArray().All(e => (GetAccessibilities(e) & Accessibilities.Public) == Accessibilities.Public),
                "Public");
        }

        private static Accessibilities GetAccessibilities(IReflectionElement e)
        {
            return e.Accept(new AccessibilityCollectingVisitor()).Value.Single();
        }
    }
}