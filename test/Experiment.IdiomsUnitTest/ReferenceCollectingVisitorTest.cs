﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Moq;
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
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            var sut = new Mock<ReferenceCollectingVisitor> { CallBase = true }.Object;
            sut.ToMock().Setup(x => x.Visit(It.IsAny<FieldInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<ConstructorInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<PropertyInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<MethodInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<EventInfoElement>())).Returns(visitor);

            var actual = sut.Visit(type.ToElement());

            Assert.Equal(visitor, actual);
            Assert.Equal(expected.Length, sut.Value.Count());
            Assert.Empty(sut.Value.Except(expected));
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
            var sut = new ReferenceCollectingVisitor();
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
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            var sut = new Mock<ReferenceCollectingVisitor> { CallBase = true }.Object;
            sut.ToMock().Setup(x => x.Visit(It.IsAny<ParameterInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<LocalVariableInfoElement>())).Returns(visitor);
            var expected = new[]
            {
                typeof(TypeImplementingMultiple).Assembly,
                typeof(IDisposable).Assembly,
                typeof(ISpecimenContext).Assembly
            };
            var methodInfoElement = new Methods<TypeForCollectingReference>()
                .Select(x => x.ReturnMethod()).ToElement();

            var actual = sut.Visit(methodInfoElement);

            Assert.Equal(visitor, actual);
            Assert.Equal(expected.Length, sut.Value.Count());
            Assert.Empty(expected.Except(sut.Value));
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
            var sut = new ReferenceCollectingVisitor();
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
            var sut = new ReferenceCollectingVisitor();
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
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            var sut = new Mock<ReferenceCollectingVisitor> { CallBase = true }.Object;
            sut.ToMock().Setup(x => x.Visit(It.IsAny<ParameterInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<LocalVariableInfoElement>())).Returns(visitor);
            var expected = new[]
            {
                typeof(IDisposable).Assembly,
                typeof(Enumerable).Assembly
            };
            var methodInfoElement = new Methods<TypeForCollectingReference>()
                .Select(x => x.MethodCallInMethodBody()).ToElement();

            sut.Visit(methodInfoElement);

            Assert.Equal(expected.Length, sut.Value.Count());
            Assert.Empty(expected.Except(sut.Value));
        }

        [Fact]
        public void VisitMethodInfoElementCollectsCorrectAssembliesForConstructInMethodBody()
        {
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            var sut = new Mock<ReferenceCollectingVisitor> { CallBase = true }.Object;
            sut.ToMock().Setup(x => x.Visit(It.IsAny<ParameterInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<LocalVariableInfoElement>())).Returns(visitor);
            var expected = new[]
            {
                typeof(IDisposable).Assembly,
                GetType().Assembly,
                typeof(Fixture).Assembly
            };
            var methodInfoElement = new Methods<TypeForCollectingReference>()
                .Select(x => x.ConstructInMethodBody()).ToElement();

            sut.Visit(methodInfoElement);

            Assert.Equal(expected.Length, sut.Value.Count());
            Assert.Empty(expected.Except(sut.Value));
        }

        [Fact]
        public void VisitMethodInfoElementCollectsCorrectAssembliesForReturnValueInMethodBody()
        {
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            var sut = new Mock<ReferenceCollectingVisitor> { CallBase = true }.Object;
            sut.ToMock().Setup(x => x.Visit(It.IsAny<ParameterInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<LocalVariableInfoElement>())).Returns(visitor);
            var expected = new[]
            {
                typeof(IDisposable).Assembly,
                GetType().Assembly,
                typeof(Fixture).Assembly
            };
            var methodInfoElement = new Methods<TypeForCollectingReference>()
                .Select(x => x.RetrunValueInMethodBody()).ToElement();

            sut.Visit(methodInfoElement);

            Assert.Equal(expected.Length, sut.Value.Count());
            Assert.Empty(expected.Except(sut.Value));
        }

        [Fact]
        public void VisitMethodInfoElementCollectsCorrectAssembliesForPassParameterInMethodBody()
        {
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            var sut = new Mock<ReferenceCollectingVisitor> { CallBase = true }.Object;
            sut.ToMock().Setup(x => x.Visit(It.IsAny<ParameterInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<LocalVariableInfoElement>())).Returns(visitor);
            var expected = new[]
            {
                typeof(IDisposable).Assembly,
                GetType().Assembly,
                typeof(Fixture).Assembly
            };
            var methodInfoElement = new Methods<TypeForCollectingReference>()
                .Select(x => x.PassParameterInMethodBody()).ToElement();

            sut.Visit(methodInfoElement);

            Assert.Equal(expected.Length, sut.Value.Count());
            Assert.Empty(expected.Except(sut.Value));
        }

        [Fact]
        public void VisitConstructorInfoElementCollectsCorrectAssembliesForMethodBody()
        {
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            var sut = new Mock<ReferenceCollectingVisitor> { CallBase = true }.Object;
            sut.ToMock().Setup(x => x.Visit(It.IsAny<ParameterInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<LocalVariableInfoElement>())).Returns(visitor);
            var expected = new[]
            {
                typeof(IDisposable).Assembly,
                GetType().Assembly,
                typeof(Fixture).Assembly
            };
            var constructorInfoElement = Constructors.Select(() => new TypeForCollectingReference(0)).ToElement();

            var actual = sut.Visit(constructorInfoElement);

            Assert.Equal(visitor, actual);
            Assert.Equal(expected.Length, sut.Value.Count());
            Assert.Empty(expected.Except(sut.Value));
        }
        
        [Fact]
        public void VisitNullConstructorInfoElementThrows()
        {
            var sut = new ReferenceCollectingVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((ConstructorInfoElement)null));
        }

        [Fact]
        public void VisitFieldInfoElementsCallsBaseMethod()
        {
            var sut = new Mock<ReferenceCollectingVisitor> { CallBase = true }.Object;
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            sut.ToMock().Setup(x => x.Visit(It.IsAny<FieldInfoElement>())).Returns(visitor);
            var fieldInfoElements = typeof(TypeWithMembers).GetFields(_bindingFlags)
                .Select(x => x.ToElement()).ToArray();

            var actual = sut.Visit(fieldInfoElements);

            Assert.Equal(visitor, actual);
        }
        
        [Fact]
        public void VisitPropertyInfoElementsCallsBaseMethod()
        {
            var sut = new Mock<ReferenceCollectingVisitor> { CallBase = true }.Object;
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            sut.ToMock().Setup(x => x.Visit(It.IsAny<PropertyInfoElement>())).Returns(visitor);
            var propertyInfoElements = typeof(TypeWithMembers).GetProperties(_bindingFlags)
                .Select(x => x.ToElement()).ToArray();

            var actual = sut.Visit(propertyInfoElements);

            Assert.Equal(visitor, actual);
        }

        [Fact]
        public void VisitMethodInfoElementsCallsBaseMethod()
        {
            var sut = new Mock<ReferenceCollectingVisitor> { CallBase = true }.Object;
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            sut.ToMock().Setup(x => x.Visit(It.IsAny<MethodInfoElement>())).Returns(visitor);
            var methodInfoElements = typeof(TypeWithMembers).GetMethods(_bindingFlags)
                .Select(x => x.ToElement()).ToArray();

            var actual = sut.Visit(methodInfoElements);

            Assert.Equal(visitor, actual);
        }

        [Fact]
        public void VisitEventInfoElementsCallsBaseMethod()
        {
            var sut = new Mock<ReferenceCollectingVisitor> { CallBase = true }.Object;
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            sut.ToMock().Setup(x => x.Visit(It.IsAny<EventInfoElement>())).Returns(visitor);
            var eventInfoElements = typeof(TypeWithMembers).GetEvents(_bindingFlags)
                .Select(x => x.ToElement()).ToArray();

            var actual = sut.Visit(eventInfoElements);

            Assert.Equal(visitor, actual);
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
                    typeof(List<TypeImplementingHierarchical>),
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
            public TypeForCollectingReference(int arg)
            {
                PrivateMethod1(null);
            }

            public TypeForCollectingReference(object arg)
            {
            }

#pragma warning disable 649
            public TypeImplementingMultiple Field;
#pragma warning restore 649

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