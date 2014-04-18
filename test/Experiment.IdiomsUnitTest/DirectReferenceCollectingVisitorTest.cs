using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment
{
    public class DirectReferenceCollectingVisitorTest
    {
        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new DirectReferenceCollectingVisitor();
            Assert.IsAssignableFrom<IReflectionVisitor<IEnumerable<Assembly>>>(sut);
        }

        [Fact]
        public void ValueIsCorrect()
        {
            var sut = new DirectReferenceCollectingVisitor();
            var actual = sut.Value;
            Assert.Empty(actual);
        }
        
        [Theory]
        [ReferenceCollectingData]
        public void VisitTypeEelementCollectsCorrectAssemblies(
            Type type, Assembly[] expected)
        {
            var sut = new TestSpecificDirectReferenceCollectingVisitor();

            var actual = sut.Visit(type.ToElement());

            var result = actual.Value.ToArray();
            Assert.Equal(expected.Length, result.Length);
            Assert.Empty(result.Except(expected));
        }

        [Fact]
        public void VisitTypeElementCallsBaseMethod()
        {
            ConstructorInfo constructor = null;
            var type = typeof(object);
            var visitor = new TestSpecificDirectReferenceCollectingVisitor(type.Assembly);
            var sut = new TestSpecificDirectReferenceCollectingVisitor
            {
                OnVisitConstructorInfoElement = c =>
                {
                    constructor = c.ConstructorInfo;
                    return visitor;
                }
            };
            
            var actual = sut.Visit(type.ToElement());

            Assert.Equal(new[] { type.Assembly }, actual.Value);
            Assert.Equal(constructor, type.GetConstructors().Single());
        }

        [Fact]
        public void VisitNullTypeElementThrows()
        {
            var sut = new DirectReferenceCollectingVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((TypeElement)null));
        }

        [Fact]
        public void VisitFieldInfoElementCollectsCorrectAssemblies()
        {
            var sut = new DirectReferenceCollectingVisitor();
            var expected = new[]
            {
                typeof(TypeImplementingMultiple).Assembly,
                typeof(IDisposable).Assembly,
                typeof(ISpecimenContext).Assembly
            };
            var fieldInfoElements = new Fields<TypeForCollectingReference>()
                .Select(x => x.Field).ToElement();

            var actual = sut.Visit(fieldInfoElements);

            Assert.Equal(expected.Length, actual.Value.Count());
            Assert.Empty(expected.Except(actual.Value));
        }

        [Fact]
        public void VisitNulFieldInfoElementThrows()
        {
            var sut = new DirectReferenceCollectingVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((FieldInfoElement)null));
        }

        private class TestSpecificDirectReferenceCollectingVisitor : DirectReferenceCollectingVisitor
        {
            public TestSpecificDirectReferenceCollectingVisitor()
                : this(new Assembly[0])
            {
            }

            public TestSpecificDirectReferenceCollectingVisitor(params Assembly[] assemblies)
                : base(assemblies)
            {
                OnVisitFiledInfoElement = e => this;
                OnVisitConstructorInfoElement = e => this;
            }

            public Func<FieldInfoElement, IReflectionVisitor<IEnumerable<Assembly>>>
                OnVisitFiledInfoElement
            {
                get;
                set;
            }

            public Func<ConstructorInfoElement, IReflectionVisitor<IEnumerable<Assembly>>>
                OnVisitConstructorInfoElement
            {
                get;
                set;
            }

            public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
                FieldInfoElement fieldInfoElement)
            {
                return OnVisitFiledInfoElement(fieldInfoElement);
            }

            public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
                ConstructorInfoElement constructorInfoElement)
            {
                return OnVisitConstructorInfoElement(constructorInfoElement);
            }
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
                    typeof(DirectReferenceCollectingVisitorTest),
                    new[]
                    {
                        typeof(object).Assembly,
                        typeof(DirectReferenceCollectingVisitorTest).Assembly
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
                    typeof(SpecimenBuilderAdapter),
                    new[]
                    {
                        typeof(SpecimenBuilderAdapter).Assembly,
                        typeof(object).Assembly,
                        typeof(Fixture).Assembly
                    }
                };
                yield return new object[]
                {
                    typeof(TypeImplementingMultiple),
                    new[]
                    {
                        typeof(TypeImplementingMultiple).Assembly,
                        typeof(IDisposable).Assembly,
                        typeof(ISpecimenContext).Assembly
                    }
                };
                yield return new object[]
                {
                    typeof(TypeImplementingHierarchical),
                    new[]
                    {
                        typeof(TypeImplementingMultiple).Assembly,
                        typeof(IDisposable).Assembly,
                        typeof(IReflectionElement).Assembly,
                        typeof(Fixture).Assembly
                    }
                };

                yield return new object[]
                {
                    typeof(IEnumerable<TypeImplementingHierarchical>),
                    new[]
                    {
                        typeof(TypeImplementingMultiple).Assembly,
                        typeof(IDisposable).Assembly,
                        typeof(IReflectionElement).Assembly,
                        typeof(Fixture).Assembly
                    }
                };
            }
        }

        private class TypeForCollectingReference
        {
            public TypeImplementingMultiple Field;
        }

        public class TypeImplementingMultiple : IDisposable, ISpecimenContext
        {
            public void Dispose()
            {
                throw new NotSupportedException();
            }

            public object Resolve(object request)
            {
                throw new NotSupportedException();
            }
        }

        public class TypeImplementingHierarchical : IDisposable, IHierarchical
        {
            public void Dispose()
            {
                throw new NotSupportedException();
            }

            public object Resolve(object request)
            {
                throw new NotSupportedException();
            }

            public IReflectionVisitor<T> Accept<T>(IReflectionVisitor<T> visitor)
            {
                throw new NotSupportedException();
            }
        }

        public interface IHierarchical : ISpecimenContext, IReflectionElement
        {
        }
    }
}