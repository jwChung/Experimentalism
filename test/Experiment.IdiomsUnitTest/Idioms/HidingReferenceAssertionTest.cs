using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Moq;
using Ploeh.Albedo;
using Ploeh.AutoFixture;
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
        public void VisitTypeElementsPassesVisibleElementsToBaseMethod()
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

        [Fact]
        public void VisitTypeEelementThrowsIfAnyAssemblyIsSpecified()
        {
            // Fixture setup
            Type type = typeof(SpecimenBuilder);
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            var sut = new Mock<HidingReferenceAssertion>(typeof(Fixture).Assembly) { CallBase = true }.Object;
            sut.ToMock().Setup(x => x.Visit(It.IsAny<FieldInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<ConstructorInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<PropertyInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<MethodInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<EventInfoElement>())).Returns(visitor);

            // Exercise system and Verify outcome
            Assert.Throws<HidingReferenceException>(() => sut.Visit(type.ToElement()));
        }

        [Fact]
        public void VisitTypeEelementDoesNotThrowIfAllAssemblisAreNotSpecified()
        {
            Type type = typeof(SpecimenBuilder);
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            var sut = new Mock<HidingReferenceAssertion>(typeof(TheoremBaseAttribute).Assembly)
            { CallBase = true }.Object;
            sut.ToMock().Setup(x => x.Visit(It.IsAny<FieldInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<ConstructorInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<PropertyInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<MethodInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<EventInfoElement>())).Returns(visitor);

            var actual = sut.Visit(type.ToElement());

            Assert.Equal(visitor, actual);
        }

        [Fact]
        public void VisitFieldInfoEelementThrowsIfAnyAssemblyIsSpecified()
        {
            var sut = new HidingReferenceAssertion(typeof(TypeImplementingMultiple).Assembly);
            var fieldInfoElement = new Fields<TypeForCollectingReference>()
                .Select(x => x.Field).ToElement();
            Assert.Throws<HidingReferenceException>(() => sut.Visit(fieldInfoElement));
        }

        [Fact]
        public void VisitFieldInfoEelementThrowsIfAllAssemblisAreNotSpecified()
        {
            var sut = new HidingReferenceAssertion(typeof(FactAttribute).Assembly);
            var fieldInfoElement = new Fields<TypeForCollectingReference>()
                .Select(x => x.Field).ToElement();

            var actual = sut.Visit(fieldInfoElement);

            Assert.Equal(sut, actual);
        }

        [Fact]
        public void VisitMethodInfoEelementThrowsIfAnyAssemblyIsSpecified()
        {
            var sut = new HidingReferenceAssertion(typeof(TypeImplementingMultiple).Assembly);
            var methodInfoElement = new Methods<TypeForCollectingReference>()
                .Select(x => x.ReturnMethod()).ToElement();
            Assert.Throws<HidingReferenceException>(() => sut.Visit(methodInfoElement));
        }

        [Fact]
        public void VisitMethodInfoEelementThrowsIfAllAssemblisAreNotSpecified()
        {
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            var sut = new Mock<HidingReferenceAssertion>(typeof(FactAttribute).Assembly) { CallBase = true }.Object;
            sut.ToMock().Setup(x => x.Visit(It.IsAny<ParameterInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<LocalVariableInfoElement>())).Returns(visitor);
            var methodInfoElement = new Methods<TypeForCollectingReference>()
                .Select(x => x.ReturnMethod(0)).ToElement();

            var actual = sut.Visit(methodInfoElement);

            Assert.Equal(visitor, actual);
        }

        [Fact]
        public void ValueThrowsNotSupportedException()
        {
            var sut = new HidingReferenceAssertion();
            Assert.Throws<NotSupportedException>(() => sut.Value);
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