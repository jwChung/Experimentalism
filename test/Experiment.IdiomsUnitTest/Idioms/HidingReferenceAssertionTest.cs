using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Moq;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class HidingReferenceAssertionTest
    {
        private const BindingFlags _bindingFlags =
            BindingFlags.Static | BindingFlags.Instance |
            BindingFlags.Public | BindingFlags.NonPublic;

        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new HidingReferenceAssertion();
            Assert.IsAssignableFrom<IReflectionVisitor<object>>(sut);
        }

        [Fact]
        public void AssembliesIsCorrect()
        {
            var assemblies = new[]
            {
                typeof(object).Assembly,
                typeof(Enumerable).Assembly
            };
            var sut = new HidingReferenceAssertion(assemblies);

            var actual = sut.Assemblies;

            Assert.Equal(assemblies, actual);
        }

        [Fact]
        public void InitializeWithNullAssembliesThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new HidingReferenceAssertion(null));
        }

        [Fact]
        public void VisitAssemblyElementsPassesVisibleElementsToBaseMethod()
        {
            var sut = new Mock<HidingReferenceAssertion> { CallBase = true }.Object;
            var visitor = new Mock<HidingReferenceAssertion> { CallBase = true }.Object;
            var expectedElements = new List<TypeElement>();
            sut.ToMock().Setup(x => x.Visit(It.IsAny<TypeElement>()))
                .Returns(visitor)
                .Callback<TypeElement>(expectedElements.Add);
            visitor.ToMock().Setup(x => x.Visit(It.IsAny<TypeElement>()))
                .Returns(visitor)
                .Callback<TypeElement>(expectedElements.Add);
            var typeElements = typeof(object).Assembly.GetTypes().Select(t => t.ToElement()).ToArray();

            var actual = sut.Visit(typeElements);

            Assert.Equal(visitor, actual);
            AssertAreVisibleElements(expectedElements);
        }

        [Fact]
        public void VisitFieldInfoElementsPassesVisibleElementsToBaseMethod()
        {
            var sut = new Mock<HidingReferenceAssertion> { CallBase = true }.Object;
            var visitor = new Mock<HidingReferenceAssertion> { CallBase = true }.Object;
            var expectedElements = new List<FieldInfoElement>();
            sut.ToMock().Setup(x => x.Visit(It.IsAny<FieldInfoElement>()))
                .Returns(visitor)
                .Callback<FieldInfoElement>(expectedElements.Add);
            visitor.ToMock().Setup(x => x.Visit(It.IsAny<FieldInfoElement>()))
                .Returns(visitor)
                .Callback<FieldInfoElement>(expectedElements.Add);
            var fieldInfoElements = typeof(TypeWithMembers).GetFields(_bindingFlags)
                .Select(t => t.ToElement()).ToArray();

            var actual = sut.Visit(fieldInfoElements);

            Assert.Equal(visitor, actual);
            AssertAreVisibleElements(expectedElements);
        }

        [Fact]
        public void VisitConstructorInfoElementsPassesVisibleElementsToBaseMethod()
        {
            var sut = new Mock<HidingReferenceAssertion> { CallBase = true }.Object;
            var visitor = new Mock<HidingReferenceAssertion> { CallBase = true }.Object;
            var expectedElements = new List<ConstructorInfoElement>();
            sut.ToMock().Setup(x => x.Visit(It.IsAny<ConstructorInfoElement>()))
                .Returns(visitor)
                .Callback<ConstructorInfoElement>(expectedElements.Add);
            visitor.ToMock().Setup(x => x.Visit(It.IsAny<ConstructorInfoElement>()))
                .Returns(visitor)
                .Callback<ConstructorInfoElement>(expectedElements.Add);
            var constructorInfoElements = typeof(TypeWithMembers).GetConstructors(_bindingFlags)
                .Select(t => t.ToElement()).ToArray();

            var actual = sut.Visit(constructorInfoElements);

            Assert.Equal(visitor, actual);
            AssertAreVisibleElements(expectedElements);
        }

        [Fact]
        public void VisitPropertyInfoElementsPassesVisibleElementsToBaseMethod()
        {
            var sut = new Mock<HidingReferenceAssertion> { CallBase = true }.Object;
            var visitor = new Mock<HidingReferenceAssertion> { CallBase = true }.Object;
            var expectedElements = new List<PropertyInfoElement>();
            sut.ToMock().Setup(x => x.Visit(It.IsAny<PropertyInfoElement>()))
                .Returns(visitor)
                .Callback<PropertyInfoElement>(expectedElements.Add);
            visitor.ToMock().Setup(x => x.Visit(It.IsAny<PropertyInfoElement>()))
                .Returns(visitor)
                .Callback<PropertyInfoElement>(expectedElements.Add);
            var propertyInfoElements = typeof(TypeWithMembers).GetProperties(_bindingFlags)
                .Select(t => t.ToElement()).ToArray();

            var actual = sut.Visit(propertyInfoElements);

            Assert.Equal(visitor, actual);
            AssertAreVisibleElements(expectedElements);
        }

        [Fact]
        public void VisitMethodInfoElementsPassesVisibleElementsToBaseMethod()
        {
            var sut = new Mock<HidingReferenceAssertion> { CallBase = true }.Object;
            var visitor = new Mock<HidingReferenceAssertion> { CallBase = true }.Object;
            var expectedElements = new List<MethodInfoElement>();
            sut.ToMock().Setup(x => x.Visit(It.IsAny<MethodInfoElement>()))
                .Returns(visitor)
                .Callback<MethodInfoElement>(expectedElements.Add);
            visitor.ToMock().Setup(x => x.Visit(It.IsAny<MethodInfoElement>()))
                .Returns(visitor)
                .Callback<MethodInfoElement>(expectedElements.Add);
            var methodInfoElements = typeof(TypeWithMembers).GetMethods(_bindingFlags)
                .Select(t => t.ToElement()).ToArray();

            var actual = sut.Visit(methodInfoElements);

            Assert.Equal(visitor, actual);
            AssertAreVisibleElements(expectedElements);
        }

        [Fact]
        public void VisitEventInfoElementsPassesVisibleElementsToBaseMethod()
        {
            var sut = new Mock<HidingReferenceAssertion> { CallBase = true }.Object;
            var visitor = new Mock<HidingReferenceAssertion> { CallBase = true }.Object;
            var expectedElements = new List<EventInfoElement>();
            sut.ToMock().Setup(x => x.Visit(It.IsAny<EventInfoElement>()))
                .Returns(visitor)
                .Callback<EventInfoElement>(expectedElements.Add);
            visitor.ToMock().Setup(x => x.Visit(It.IsAny<EventInfoElement>()))
                .Returns(visitor)
                .Callback<EventInfoElement>(expectedElements.Add);
            var methodInfoElements = typeof(TypeWithMembers).GetEvents(_bindingFlags)
                .Select(t => t.ToElement()).ToArray();

            var actual = sut.Visit(methodInfoElements);

            Assert.Equal(visitor, actual);
            AssertAreVisibleElements(expectedElements);
        }

        [Fact]
        public void VisitLocalVariableElementsPassesEmptyToBaseMethod()
        {
            var sut = new Mock<HidingReferenceAssertion> { CallBase = true }.Object;

            var actual = sut.Visit((LocalVariableInfoElement[])null);

            Assert.Equal(sut, actual);
            sut.ToMock().Verify(x => x.Visit(It.IsAny<LocalVariableInfoElement>()), Times.Never());
        }

        private static void AssertAreVisibleElements(IEnumerable<IReflectionElement> reflectionElements)
        {
            var result = reflectionElements.ToArray();

            Assert.True(
                result.ToArray().Any(e => GetAccessibilities(e) == Accessibilities.Public),
                "Public");
            Assert.True(
                result.ToArray().Any(e => GetAccessibilities(e) == Accessibilities.ProtectedInternal),
                "ProtectedInternal");
            Assert.True(
                result.ToArray().Any(e => GetAccessibilities(e) == Accessibilities.Protected),
                "Protected");
            Assert.True(
                result.ToArray().All(e => GetAccessibilities(e) != Accessibilities.Internal),
                "Internal");
            Assert.True(
                result.ToArray().All(e => GetAccessibilities(e) != Accessibilities.Private),
                "Private");
        }

        private static Accessibilities GetAccessibilities(IReflectionElement e)
        {
            return e.Accept(new AccessibilityCollectingVisitor()).Value.Single();
        }
    }
}