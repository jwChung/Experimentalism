﻿using System;
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
            var dummyVisitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>(new Assembly[0]);
            var sut = new TestSpecificDirectReferenceCollectingVisitor()
            {
                OnVisitFiledInfoElement = e => dummyVisitor,
                OnVisitMethodInfoElement = e => dummyVisitor
            };

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
            var sut = new TestSpecificDirectReferenceCollectingVisitor
            {
                OnVisitConstructorInfoElement = c =>
                {
                    constructor = c.ConstructorInfo;
                    return new DelegatingReflectionVisitor<IEnumerable<Assembly>>(new Assembly[0]);
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
            var sut = new TestSpecificDirectReferenceCollectingVisitor();
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
        public void VisitNullFieldInfoElementThrows()
        {
            var sut = new DirectReferenceCollectingVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((FieldInfoElement)null));
        }

        [Fact]
        public void VisitMethodInfoElementCollectsCorrectAssemblies()
        {
            var dummyVisitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>(new Assembly[0]);
            var sut = new TestSpecificDirectReferenceCollectingVisitor()
            {
                OnVisitParameterInfoElement = e => dummyVisitor,
                OnVisitLocalVariableInfoElements = e => dummyVisitor
            };
            var expected = new[]
            {
                typeof(TypeImplementingMultiple).Assembly,
                typeof(IDisposable).Assembly,
                typeof(ISpecimenContext).Assembly
            };
            var methodInfoElement = new Methods<TypeForCollectingReference>()
                .Select(x => x.ReturnMethod()).ToElement();

            var actual = sut.Visit(methodInfoElement);

            Assert.Equal(expected.Length, actual.Value.Count());
            Assert.Empty(expected.Except(actual.Value));
        }

        [Fact]
        public void VisitMethodInfoElementCallsBaseMethod()
        {
            // Fixture setup
            bool verify = false;
            var method = new Methods<TypeForCollectingReference>()
                .Select(x => x.ParameterizedMethod(null));
            ParameterInfo parameter = method.GetParameters().First();
            var sut = new TestSpecificDirectReferenceCollectingVisitor
            {
                OnVisitParameterInfoElement = p =>
                {
                    Assert.Equal(parameter, p.ParameterInfo);
                    verify = true;
                    return new DelegatingReflectionVisitor<IEnumerable<Assembly>>(new Assembly[0]);
                }
            };
            
            // Exercise system
            var actual = sut.Visit(method.ToElement());

            // Verify outcome
            Assert.Equal(new[] {typeof(object).Assembly }, actual.Value);
            Assert.True(verify, "Verify.");
        }

        [Fact]
        public void VisitNullMethodInfoElementThrows()
        {
            var sut = new DirectReferenceCollectingVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((MethodInfoElement)null));
        }

        [Fact]
        public void VisitLocalVariableElementsReturnsSutItself()
        {
            var sut = new DirectReferenceCollectingVisitor();
            var actual = sut.Visit((LocalVariableInfoElement[])null);
            Assert.Equal(sut, actual);
        }

        [Fact]
        public void VisitParameterInfoElementCollectsCorrectAssemblies()
        {
            var sut = new TestSpecificDirectReferenceCollectingVisitor();
            var expected = new[]
            {
                typeof(TypeImplementingMultiple).Assembly,
                typeof(IDisposable).Assembly,
                typeof(ISpecimenContext).Assembly
            };
            var parameterInfoElement = new Methods<TypeForCollectingReference>()
                .Select(x => x.ParameterizedMethod(null)).GetParameters()
                .First().ToElement();

            var actual = sut.Visit(parameterInfoElement);

            Assert.Equal(expected.Length, actual.Value.Count());
            Assert.Empty(expected.Except(actual.Value));
        }

        [Fact]
        public void VisitNullParameterInfoElementThrows()
        {
            var sut = new DirectReferenceCollectingVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((ParameterInfoElement)null));
        }

        private class TestSpecificDirectReferenceCollectingVisitor : DirectReferenceCollectingVisitor
        {
            public TestSpecificDirectReferenceCollectingVisitor()
            {
                OnVisitFiledInfoElement = e => base.Visit(e);
                OnVisitConstructorInfoElement = e => base.Visit(e);
                OnVisitMethodInfoElement = e => base.Visit(e);
                OnVisitParameterInfoElement = e => base.Visit(e);
                OnVisitLocalVariableInfoElements = e => base.Visit(e);
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

            public Func<MethodInfoElement, IReflectionVisitor<IEnumerable<Assembly>>>
                OnVisitMethodInfoElement
            {
                get;
                set;
            }

            public Func<ParameterInfoElement, IReflectionVisitor<IEnumerable<Assembly>>>
                OnVisitParameterInfoElement
            {
                get;
                set;
            }

            public Func<LocalVariableInfoElement[], IReflectionVisitor<IEnumerable<Assembly>>>
                OnVisitLocalVariableInfoElements
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

            public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
                MethodInfoElement methodInfoElement)
            {
                return OnVisitMethodInfoElement(methodInfoElement);
            }

            public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
                ParameterInfoElement parameterInfoElement)
            {
                return OnVisitParameterInfoElement(parameterInfoElement);
            }

            public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
                params LocalVariableInfoElement[] localVariableInfoElement)
            {
                return OnVisitLocalVariableInfoElements(localVariableInfoElement);
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

            public TypeImplementingMultiple ReturnMethod()
            {
                return null;
            }

            public void ParameterizedMethod(TypeImplementingMultiple arg)
            {
            }
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