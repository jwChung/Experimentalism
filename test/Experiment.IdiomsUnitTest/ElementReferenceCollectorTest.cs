using Moq;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment
{
    public class ElementReferenceCollectorTest
    {
        [Fact]
        public void SutIsReferenceCollectingVisitor()
        {
            var sut = new ElementReferenceCollector();
            Assert.IsAssignableFrom<ReferenceCollector>(sut);
        }

        [Fact]
        public void VisitFieldInfoElementsPassesEmptyToBaseMethod()
        {
            var sut = new Mock<ElementReferenceCollector> { CallBase = true }.Object;

            var actual = sut.Visit((FieldInfoElement[])null);

            Assert.Equal(sut, actual);
            sut.ToMock().Verify(x => x.Visit(It.IsAny<FieldInfoElement>()), Times.Never());
        }

        [Fact]
        public void VisitConstructorInfoElementsPassesEmptyToBaseMethod()
        {
            var sut = new Mock<ElementReferenceCollector> { CallBase = true }.Object;

            var actual = sut.Visit((ConstructorInfoElement[])null);

            Assert.Equal(sut, actual);
            sut.ToMock().Verify(x => x.Visit(It.IsAny<ConstructorInfoElement>()), Times.Never());
        }

        [Fact]
        public void VisitPropertyInfoElementsPassesEmptyToBaseMethod()
        {
            var sut = new Mock<ElementReferenceCollector> { CallBase = true }.Object;

            var actual = sut.Visit((PropertyInfoElement[])null);

            Assert.Equal(sut, actual);
            sut.ToMock().Verify(x => x.Visit(It.IsAny<PropertyInfoElement>()), Times.Never());
        }

        [Fact]
        public void VisitMethodInfoElementsPassesEmptyToBaseMethod()
        {
            var sut = new Mock<ElementReferenceCollector> { CallBase = true }.Object;

            var actual = sut.Visit((MethodInfoElement[])null);

            Assert.Equal(sut, actual);
            sut.ToMock().Verify(x => x.Visit(It.IsAny<MethodInfoElement>()), Times.Never());
        }

        [Fact]
        public void VisitEventInfoElementsPassesEmptyToBaseMethod()
        {
            var sut = new Mock<ElementReferenceCollector> { CallBase = true }.Object;

            var actual = sut.Visit((EventInfoElement[])null);

            Assert.Equal(sut, actual);
            sut.ToMock().Verify(x => x.Visit(It.IsAny<EventInfoElement>()), Times.Never());
        }

        [Fact]
        public void VisitParameterInfoElementsPassesEmptyToBaseMethod()
        {
            var sut = new Mock<ElementReferenceCollector> { CallBase = true }.Object;

            var actual = sut.Visit((ParameterInfoElement[])null);

            Assert.Equal(sut, actual);
            sut.ToMock().Verify(x => x.Visit(It.IsAny<ParameterInfoElement>()), Times.Never());
        }

        [Fact]
        public void VisitLocalVariableInfoElementsPassesEmptyToBaseMethod()
        {
            var sut = new Mock<ElementReferenceCollector> { CallBase = true }.Object;

            var actual = sut.Visit((LocalVariableInfoElement[])null);

            Assert.Equal(sut, actual);
            sut.ToMock().Verify(x => x.Visit(It.IsAny<LocalVariableInfoElement>()), Times.Never());
        }

        [Fact]
        public void VisitMethodInfoElementDoesNotRelayMethodBody()
        {
            var sut = new Mock<ElementReferenceCollector> { CallBase = true }.Object;
            var methodInfoElement = new Methods<TypeForCollectingReference>()
                .Select(x => x.ConstructInMethodBody()).ToElement();
            var expected = new[] { typeof(object).Assembly };

            var actual = sut.Visit(methodInfoElement);

            Assert.Equal(sut, actual);
            Assert.Equal(expected, sut.Value);
        }
    }
}