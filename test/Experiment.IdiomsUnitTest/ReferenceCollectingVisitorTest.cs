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
    public class ReferenceCollectingVisitorTest
    {
        private const BindingFlags _bindingFlags =
            BindingFlags.Static | BindingFlags.Instance |
            BindingFlags.Public | BindingFlags.NonPublic;

        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new ReferenceCollectingVisitor();
            Assert.IsAssignableFrom<IReflectionVisitor<IEnumerable<Assembly>>>(sut);
        }

        [Fact]
        public void ValueIsCorrect()
        {
            var sut = new ReferenceCollectingVisitor();
            var actual = sut.Value;
            Assert.Empty(actual);
        }
        
        [Theory]
        [ReferenceCollectingData]
        public void VisitTypeEelementCollectsCorrectAssemblies(
            Type type, Assembly[] expected)
        {
            var dummyVisitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>(new Assembly[0]);
            var sut = new TestSpecificReferenceCollectingVisitor
            {
                OnVisitFiledInfoElement = e => dummyVisitor,
                OnVisitConstructorInfoElement = e => dummyVisitor,
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
            var sut = new TestSpecificReferenceCollectingVisitor
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
            var sut = new ReferenceCollectingVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((TypeElement)null));
        }

        [Fact]
        public void VisitFieldInfoElementCollectsCorrectAssemblies()
        {
            var sut = new TestSpecificReferenceCollectingVisitor();
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
            var sut = new ReferenceCollectingVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((FieldInfoElement)null));
        }

        [Fact]
        public void VisitMethodInfoElementCollectsCorrectAssemblies()
        {
            var dummyVisitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>(new Assembly[0]);
            var sut = new TestSpecificReferenceCollectingVisitor
            {
                OnVisitParameterInfoElement = e => dummyVisitor,
                OnVisitLocalVariableInfoElement = e => dummyVisitor
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
            var sut = new TestSpecificReferenceCollectingVisitor
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
            var sut = new ReferenceCollectingVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((MethodInfoElement)null));
        }

        [Fact]
        public void VisitParameterInfoElementCollectsCorrectAssemblies()
        {
            var sut = new TestSpecificReferenceCollectingVisitor();
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
            var sut = new ReferenceCollectingVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((ParameterInfoElement)null));
        }

        [Fact]
        public void VisitLocalVariableInfoElementCollectsCorrectAssemblies()
        {
            var sut = new TestSpecificReferenceCollectingVisitor();
            var expected = new[]
            {
                typeof(TypeImplementingMultiple).Assembly,
                typeof(IDisposable).Assembly,
                typeof(ISpecimenContext).Assembly
            };
            var localVariableInfoElement = new Methods<TypeForCollectingReference>()
                .Select(x => x.ReturnMethod()).GetMethodBody()
                .LocalVariables.Single().ToElement();

            var actual = sut.Visit(localVariableInfoElement);

            Assert.Equal(expected.Length, actual.Value.Count());
            Assert.Empty(expected.Except(actual.Value));
        }

        [Fact]
        public void VisitNullLocalVariableInfoElementThrows()
        {
            var sut = new ReferenceCollectingVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((LocalVariableInfoElement)null));
        }

        [Fact]
        public void VisitNonDeclaredFieldInfoElementsFiltersIt()
        {
            var fieldInfoElements = typeof(SubTypeWithMembers).GetFields(_bindingFlags)
                .Select(f => f.ToElement()).ToArray();
            var sut = new ReferenceCollectingVisitor();

            var actual = sut.Visit(fieldInfoElements);

            Assert.Empty(actual.Value);
        }

        [Fact]
        public void VisitDeclaredFieldInfoElementsDoesNotFilterIt()
        {
            var fieldInfoElements = typeof(TypeWithMembers).GetFields(_bindingFlags)
                .Select(f => f.ToElement()).ToArray();
            var sut = new ReferenceCollectingVisitor();

            var actual = sut.Visit(fieldInfoElements);

            Assert.NotEmpty(actual.Value);
        }

        [Fact]
        public void VisitNullFieldInfoElementsThrows()
        {
            var sut = new ReferenceCollectingVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((FieldInfoElement[])null));
        }

        [Fact]
        public void VisitNonDeclaredPropertyInfoElementsFiltersIt()
        {
            var propertyInfoElements = typeof(SubTypeWithMembers).GetProperties(_bindingFlags)
                .Select(x => x.ToElement()).ToArray();
            var sut = new ReferenceCollectingVisitor();
            
            var actual = sut.Visit(propertyInfoElements);

            Assert.Empty(actual.Value);
        }

        [Fact]
        public void VisitDeclaredPropertyInfoElementsDoesNotFilterIt()
        {
            var propertyInfoElements = typeof(TypeWithMembers).GetProperties(_bindingFlags)
                .Select(x => x.ToElement()).ToArray();
            var sut = new ReferenceCollectingVisitor();

            var actual = sut.Visit(propertyInfoElements);

            Assert.NotEmpty(actual.Value);
        }

        [Fact]
        public void VisitNullPropertyInfoElementsThrows()
        {
            var sut = new ReferenceCollectingVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((PropertyInfoElement[])null));
        }

        [Fact]
        public void VisitNonDeclaredMethodInfoElementsFiltersIt()
        {
            var methodInfoElements = typeof(SubTypeWithMembers).GetMethods(_bindingFlags)
                .Select(x => x.ToElement()).ToArray();
            var sut = new ReferenceCollectingVisitor();

            var actual = sut.Visit(methodInfoElements);

            Assert.Empty(actual.Value);
        }

        [Fact]
        public void VisitDeclaredMethodInfoElementsDoesNotFilterIt()
        {
            var methodInfoElements = typeof(TypeWithMembers).GetMethods(_bindingFlags)
                .Select(x => x.ToElement()).ToArray();
            var sut = new ReferenceCollectingVisitor();

            var actual = sut.Visit(methodInfoElements);

            Assert.NotEmpty(actual.Value);
        }

        [Fact]
        public void VisitNullMethodInfoElementsThrows()
        {
            var sut = new ReferenceCollectingVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((MethodInfoElement[])null));
        }

        [Fact]
        public void VisitNonDeclaredEventInfoElementsFiltersIt()
        {
            var eventInfoElements = typeof(SubTypeWithMembers).GetEvents(_bindingFlags)
                .Select(x => x.ToElement()).ToArray();
            var sut = new ReferenceCollectingVisitor();

            var actual = sut.Visit(eventInfoElements);

            Assert.Empty(actual.Value);
        }

        [Fact]
        public void VisitDeclaredEventInfoElementsDoesNotFilterIt()
        {
            var eventInfoElements = typeof(TypeWithMembers).GetEvents(_bindingFlags)
                .Select(x => x.ToElement()).ToArray();
            var sut = new ReferenceCollectingVisitor();

            var actual = sut.Visit(eventInfoElements);

            Assert.NotEmpty(actual.Value);
        }

        [Fact]
        public void VisitNullEventInfoElementsThrows()
        {
            var sut = new ReferenceCollectingVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((EventInfoElement[])null));
        }

        [Fact]
        public void VisitMethodInfoElementCollectsCorrectAssembliesForMethodCallInMethodBody()
        {
            var dummyVisitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>(new Assembly[0]);
            var sut = new TestSpecificReferenceCollectingVisitor
            {
                OnVisitParameterInfoElement = e => dummyVisitor,
                OnVisitLocalVariableInfoElement = e => dummyVisitor
            };
            var expected = new[]
            {
                typeof(IDisposable).Assembly,
                typeof(Enumerable).Assembly
            };
            var methodInfoElement = new Methods<TypeForCollectingReference>()
                .Select(x => x.MethodCallInMethodBody()).ToElement();

            var actual = sut.Visit(methodInfoElement);

            Assert.Equal(expected.Length, actual.Value.Count());
            Assert.Empty(expected.Except(actual.Value));
        }

        [Fact]
        public void VisitMethodInfoElementCollectsCorrectAssembliesForConstructInMethodBody()
        {
            var dummyVisitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>(new Assembly[0]);
            var sut = new TestSpecificReferenceCollectingVisitor
            {
                OnVisitParameterInfoElement = e => dummyVisitor,
                OnVisitLocalVariableInfoElement = e => dummyVisitor
            };
            var expected = new[]
            {
                typeof(IDisposable).Assembly,
                GetType().Assembly,
                typeof(Fixture).Assembly
            };
            var methodInfoElement = new Methods<TypeForCollectingReference>()
                .Select(x => x.ConstructInMethodBody()).ToElement();

            var actual = sut.Visit(methodInfoElement);

            Assert.Equal(expected.Length, actual.Value.Count());
            Assert.Empty(expected.Except(actual.Value));
        }

        [Fact]
        public void VisitMethodInfoElementCollectsCorrectAssembliesForReturnValueInMethodBody()
        {
            var dummyVisitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>(new Assembly[0]);
            var sut = new TestSpecificReferenceCollectingVisitor
            {
                OnVisitParameterInfoElement = e => dummyVisitor,
                OnVisitLocalVariableInfoElement = e => dummyVisitor
            };
            var expected = new[]
            {
                typeof(IDisposable).Assembly,
                GetType().Assembly,
                typeof(Fixture).Assembly
            };
            var methodInfoElement = new Methods<TypeForCollectingReference>()
                .Select(x => x.RetrunValueInMethodBody()).ToElement();

            var actual = sut.Visit(methodInfoElement);

            Assert.Equal(expected.Length, actual.Value.Count());
            Assert.Empty(expected.Except(actual.Value));
        }

        [Fact]
        public void VisitMethodInfoElementCollectsCorrectAssembliesForPassParameterInMethodBody()
        {
            var dummyVisitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>(new Assembly[0]);
            var sut = new TestSpecificReferenceCollectingVisitor
            {
                OnVisitParameterInfoElement = e => dummyVisitor,
                OnVisitLocalVariableInfoElement = e => dummyVisitor
            };
            var expected = new[]
            {
                typeof(IDisposable).Assembly,
                GetType().Assembly,
                typeof(Fixture).Assembly
            };
            var methodInfoElement = new Methods<TypeForCollectingReference>()
                .Select(x => x.PassParameterInMethodBody()).ToElement();

            var actual = sut.Visit(methodInfoElement);

            Assert.Equal(expected.Length, actual.Value.Count());
            Assert.Empty(expected.Except(actual.Value));
        }

        private class TestSpecificReferenceCollectingVisitor : ReferenceCollectingVisitor
        {
            public TestSpecificReferenceCollectingVisitor()
            {
                OnVisitFiledInfoElement = e => base.Visit(e);
                OnVisitConstructorInfoElement = e => base.Visit(e);
                OnVisitMethodInfoElement = e => base.Visit(e);
                OnVisitParameterInfoElement = e => base.Visit(e);
                OnVisitLocalVariableInfoElement = e => base.Visit(e);
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

            public Func<LocalVariableInfoElement, IReflectionVisitor<IEnumerable<Assembly>>>
                OnVisitLocalVariableInfoElement
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
                LocalVariableInfoElement localVariableInfoElement)
            {
                return OnVisitLocalVariableInfoElement(localVariableInfoElement);
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
                    typeof(ReferenceCollectingVisitorTest),
                    new[]
                    {
                        typeof(object).Assembly,
                        typeof(ReferenceCollectingVisitorTest).Assembly
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

        private class SubTypeWithMembers : TypeWithMembers
        {
        }

        private class TypeForCollectingReference
        {
            public TypeImplementingMultiple Field;

            public TypeImplementingMultiple ReturnMethod()
            {
                var typeImplementingMultiple = new TypeImplementingMultiple();
                return typeImplementingMultiple;
            }

            public void ParameterizedMethod(TypeImplementingMultiple arg)
            {
            }

            public object MethodCallInMethodBody()
            {
                return new[] { "a", "b" }.ToArray();
            }

            public void ConstructInMethodBody()
            {
                PrivateMethod1(new Fixture());
            }

            public void RetrunValueInMethodBody()
            {
                PrivateMethod2();
            }

            public void PassParameterInMethodBody()
            {
                PrivateMethod1(null);
            }

            private void PrivateMethod1(Fixture fixture)
            {
            }

            private Fixture PrivateMethod2()
            {
                return null;
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