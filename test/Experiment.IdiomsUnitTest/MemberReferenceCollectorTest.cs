using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Moq;
using Ploeh.Albedo;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment.Idioms
{
    public class MemberReferenceCollectorTest
    {
        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new MemberReferenceCollector();
            Assert.IsAssignableFrom<IReflectionVisitor<IEnumerable<Assembly>>>(sut);
        }

        [Fact]
        public void InitializeWithNullReferencesThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new MemberReferenceCollector(null));
        }

        [Fact]
        public void ValueIsCorrectWhenInitializedByDefaultCtor()
        {
            var sut = new MemberReferenceCollector();
            var actual = sut.Value;
            Assert.Empty(actual);
        }

        [Fact]
        public void ValueIsCorrectWhenInitializedByCtorWithReferences()
        {
            var references = new[] { typeof(object).Assembly, typeof(object).Assembly, typeof(Enumerable).Assembly };
            var sut = new MemberReferenceCollector(references);

            var actual = sut.Value;

            Assert.Equal(references.Distinct(), actual);
        }

        [Theory]
        [ReferenceCollectingDataAttribute]
        public void VisitTypeElementCollectsCorrectAssemblies(
            Type type, Assembly[] expected)
        {
            var refereces = new[] { typeof(ISet<>).Assembly };
            var sut = new Mock<MemberReferenceCollector>(new object[] { refereces })
                { CallBase = true }.Object;
            sut.ToMock().Setup(x => x.Visit(It.IsAny<FieldInfoElement>())).Throws<InvalidOperationException>();
            sut.ToMock().Setup(x => x.Visit(It.IsAny<ConstructorInfoElement>())).Throws<InvalidOperationException>();
            sut.ToMock().Setup(x => x.Visit(It.IsAny<PropertyInfoElement>())).Throws<InvalidOperationException>();
            sut.ToMock().Setup(x => x.Visit(It.IsAny<MethodInfoElement>())).Throws<InvalidOperationException>();
            sut.ToMock().Setup(x => x.Visit(It.IsAny<EventInfoElement>())).Throws<InvalidOperationException>();

            var actual = sut.Visit(type.ToElement());

            Assert.NotSame(sut, actual);
            Assert.Equal(
                refereces.Concat(expected).OrderBy(x => x.ToString()),
                actual.Value.OrderBy(x => x.ToString()));
        }

        [Fact]
        public void VisitNullTypeElementThrows()
        {
            var sut = new MemberReferenceCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((TypeElement)null));
        }

        [Fact]
        public void VisitFieldInfoElementCollectsCorrectAssemblies()
        {
            var refereces = new[] { typeof(ISet<>).Assembly };
            var sut = new MemberReferenceCollector(refereces);
            var expected = new[]
            {
                typeof(ClassImplementingMultiple).Assembly,
                typeof(IDisposable).Assembly,
                typeof(ISpecimenContext).Assembly,
                refereces[0]
            };
            var fieldInfoElements = new Fields<ClassForCollectingReference>()
                .Select(x => x.Field).ToElement();

            var actual = sut.Visit(fieldInfoElements);

            Assert.NotSame(sut, actual);
            Assert.Equal(
                expected.OrderBy(x => x.ToString()),
                actual.Value.OrderBy(x => x.ToString()));
        }

        [Fact]
        public void VisitNullFieldInfoElementThrows()
        {
            var sut = new MemberReferenceCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((FieldInfoElement)null));
        }

        [Fact]
        public void VisitMethodInfoElementCollectsCorrectAssembliesForReturnType()
        {
            var references = new[] { typeof(ISet<>).Assembly };
            var sut = new MemberReferenceCollector(references);
            var expected = new[]
            {
                typeof(ClassImplementingMultiple).Assembly,
                typeof(IDisposable).Assembly,
                typeof(ISpecimenContext).Assembly,
                references[0]
            };
            var methodInfoElement = new Methods<ClassForCollectingReference>()
                .Select(x => x.ReturnMethod()).ToElement();

            var actual = sut.Visit(methodInfoElement);

            Assert.NotSame(sut, actual);
            Assert.Equal(
                expected.OrderBy(x => x.ToString()),
                actual.Value.OrderBy(x => x.ToString()));
        }

        [Fact]
        public void VisitMethodInfoElementCollectsAssembliesFromBaseMethod()
        {
            var references = new[] { typeof(ISet<>).Assembly };
            var sut = new Mock<MemberReferenceCollector> { CallBase = true }.Object;
            sut.ToMock().Setup(x => x.Visit(It.IsAny<ParameterInfoElement[]>())).Returns(sut);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<LocalVariableInfoElement[]>()))
                .Returns(new MemberReferenceCollector(references));
            var expected = new[]
            {
                typeof(ClassImplementingMultiple).Assembly,
                typeof(IDisposable).Assembly,
                typeof(ISpecimenContext).Assembly,
                references[0]
            };
            var dummyMethodInfoElement = new Methods<ClassForCollectingReference>()
                .Select(x => x.ReturnMethod()).ToElement();

            var actual = sut.Visit(dummyMethodInfoElement);

            sut.ToMock().VerifyAll();
            Assert.Equal(
                expected.OrderBy(x => x.ToString()),
                actual.Value.OrderBy(x => x.ToString()));
        }

        [Fact]
        public void VisitNullMethodInfoElementThrows()
        {
            var sut = new MemberReferenceCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((MethodInfoElement)null));
        }

        [Fact]
        public void VisitLocalVariableInfoElementsReturnsItself()
        {
            var sut = new Mock<MemberReferenceCollector> { CallBase = true }.Object;
            sut.ToMock().Setup(x => x.Visit(It.IsAny<LocalVariableInfoElement>())).Throws<InvalidOperationException>();
            var localVariableInfoElement = new Methods<ClassForCollectingReference>()
                .Select(x => x.ReturnMethod()).GetMethodBody()
                .LocalVariables.First().ToElement();

            var actual = sut.Visit(new[] { localVariableInfoElement });

            Assert.Same(sut, actual);
        }

        [Fact]
        public void VisitParameterInfoElementCollectsCorrectAssemblies()
        {
            var references = new[] { typeof(ISet<>).Assembly };
            var sut = new MemberReferenceCollector(references);
            var expected = new[]
            {
                typeof(ClassImplementingMultiple).Assembly,
                typeof(IDisposable).Assembly,
                typeof(ISpecimenContext).Assembly,
                references[0]
            };
            var parameterInfoElement = new Methods<ClassForCollectingReference>()
                .Select(x => x.ParameterizedMethod(null)).GetParameters()
                .First().ToElement();

            var actual = sut.Visit(parameterInfoElement);

            Assert.NotSame(sut, actual);
            Assert.Equal(expected.OrderBy(x => x.ToString()), actual.Value.OrderBy(x => x.ToString()));
        }

        [Fact]
        public void VisitNullParameterInfoElementThrows()
        {
            var sut = new MemberReferenceCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((ParameterInfoElement)null));
        }

        private class ReferenceCollectingDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                yield return new object[]
                {
                    typeof(object), new[] { typeof(object).Assembly }
                };
                yield return new object[]
                {
                    typeof(ReferenceCollectorTest),
                    new[]
                    {
                        typeof(object).Assembly,
                        typeof(ReferenceCollectorTest).Assembly
                    }
                };
                yield return new object[]
                {
                    typeof(TheoryAttribute),
                    new[]
                    {
                        typeof(FactAttribute).Assembly,
                        typeof(object).Assembly,
                        typeof(TheoryAttribute).Assembly
                    }
                };
                yield return new object[]
                {
                    typeof(SpecimenBuilder),
                    new[]
                    {
                        typeof(SpecimenBuilder).Assembly,
                        typeof(object).Assembly,
                        typeof(Fixture).Assembly
                    }
                };
                yield return new object[]
                {
                    typeof(ClassImplementingMultiple),
                    new[]
                    {
                        typeof(ClassImplementingMultiple).Assembly,
                        typeof(IDisposable).Assembly,
                        typeof(ISpecimenContext).Assembly
                    }
                };
                yield return new object[]
                {
                    typeof(ClassImplementingHierarchical),
                    new[]
                    {
                        typeof(ClassImplementingMultiple).Assembly,
                        typeof(IDisposable).Assembly,
                        typeof(IReflectionElement).Assembly,
                        typeof(Fixture).Assembly
                    }
                };
                yield return new object[]
                {
                    typeof(List<ClassImplementingHierarchical>),
                    new[]
                    {
                        typeof(ClassImplementingMultiple).Assembly,
                        typeof(IDisposable).Assembly,
                        typeof(IReflectionElement).Assembly,
                        typeof(Fixture).Assembly
                    }
                };
            }
        }
    }
}