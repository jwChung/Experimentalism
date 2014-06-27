using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment
{
    public class AccessibilityCollectorTest
    {
        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new AccessibilityCollector();
            Assert.IsAssignableFrom<ReflectionVisitor<IEnumerable<Accessibilities>>>(sut);
        }

        [Fact]
        public void ValueIsCorrect()
        {
            var sut = new AccessibilityCollector();
            var actual = sut.Value;
            Assert.Empty(actual);
        }

        [Theory]
        [TypeElementData]
        public void VisitTypeElementProducesCorrectValue(
            Func<Type, bool> predicate, Accessibilities expected)
        {
            var sut = new AccessibilityCollector();
            var typeElement = typeof(object).Assembly
                .GetTypes().Where(predicate).First().ToElement();

            var actual = sut.Visit(typeElement);

            Assert.Empty(sut.Value);
            Assert.Equal(expected, actual.Value.Single());
        }

        [Fact]
        public void VisitTypeElementManyTimeProducesCorrectValues()
        {
            var sut = new AccessibilityCollector();
            var types = typeof(object).Assembly.GetTypes();
            var typeElement1 = types.First(x => x.IsPublic).ToElement();
            var typeElement2 = types.First(x => x.IsNotPublic).ToElement();

            var actual = sut.Visit(typeElement1).Visit(typeElement2);

            Assert.Equal(
                new[] { Accessibilities.Public, Accessibilities.Internal },
                actual.Value.ToArray());
        }

        [Fact]
        public void VisitNullTypeElementThrows()
        {
            var sut = new AccessibilityCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((TypeElement)null));
        }

        [Theory]
        [FieldInfoElementData]
        public void VisitFieldInfoElementProducesCorrectValue(
            Func<FieldInfo, bool> predicate, Accessibilities expected)
        {
            var sut = new AccessibilityCollector();
            BindingFlags bindingFlags =
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.Static;
            var fieldInfoElement = typeof(object).Assembly
                .GetTypes().SelectMany(t => t.GetFields(bindingFlags))
                .Where(predicate).First().ToElement();

            var actual = sut.Visit(fieldInfoElement);

            Assert.Empty(sut.Value);
            Assert.Equal(expected, actual.Value.Single());
        }

        [Fact]
        public void VisitFieldInfoElementManyTimeProducesCorrectValues()
        {
            var sut = new AccessibilityCollector();
            BindingFlags bindingFlags =
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.Static;
            var fieldInfos = typeof(object).Assembly
                .GetTypes().SelectMany(t => t.GetFields(bindingFlags)).ToArray();
            var fieldInfoElement1 = fieldInfos.First(x => x.IsPublic).ToElement();
            var fieldInfoElement2 = fieldInfos.First(x => x.IsPrivate).ToElement();

            var actual = sut.Visit(fieldInfoElement1).Visit(fieldInfoElement2);

            Assert.Equal(
                new[] { Accessibilities.Public, Accessibilities.Private },
                actual.Value.ToArray());
        }

        [Fact]
        public void VisitNullFieldInfoElementThrows()
        {
            var sut = new AccessibilityCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((FieldInfoElement)null));
        }

        [Theory]
        [MethodBaseElementData]
        public void VisitConstructorInfoElementProducesCorrectValue(
            Func<MethodBase, bool> predicate, Accessibilities expected)
        {
            var sut = new AccessibilityCollector();
            BindingFlags bindingFlags =
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.Static;
            var constructorInfoElement = typeof(object).Assembly
                .GetTypes().SelectMany(t => t.GetConstructors(bindingFlags))
                .Where(predicate).Cast<ConstructorInfo>().First().ToElement();

            var actual = sut.Visit(constructorInfoElement);

            Assert.Empty(sut.Value);
            Assert.Equal(expected, actual.Value.Single());
        }

        [Fact]
        public void VisitConstructorInfoElementManyTimeProducesCorrectValues()
        {
            var sut = new AccessibilityCollector();
            BindingFlags bindingFlags =
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.Static;
            var constructorInfos = typeof(object).Assembly
                .GetTypes().SelectMany(t => t.GetConstructors(bindingFlags)).ToArray();
            var constructorInfoElement1 = constructorInfos.First(x => x.IsFamily).ToElement();
            var constructorInfoElement2 = constructorInfos.First(x => x.IsPrivate).ToElement();

            var actual = sut.Visit(constructorInfoElement1).Visit(constructorInfoElement2);

            Assert.Equal(
                new[] { Accessibilities.Protected, Accessibilities.Private },
                actual.Value.ToArray());
        }

        [Fact]
        public void VisitNullConstructorInfoElementThrows()
        {
            var sut = new AccessibilityCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((ConstructorInfoElement)null));
        }

        [Theory]
        [MethodBaseElementData]
        public void VisitMethodInfoElementProducesCorrectValue(
            Func<MethodBase, bool> predicate, Accessibilities expected)
        {
            var sut = new AccessibilityCollector();
            BindingFlags bindingFlags =
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.Static;
            var methodInfoElement = typeof(object).Assembly
                .GetTypes().SelectMany(t => t.GetMethods(bindingFlags))
                .Where(predicate).Cast<MethodInfo>().First().ToElement();

            var actual = sut.Visit(methodInfoElement);

            Assert.Empty(sut.Value);
            Assert.Equal(expected, actual.Value.Single());
        }

        [Fact]
        public void VisitMethodInfoElementManyTimeProducesCorrectValues()
        {
            var sut = new AccessibilityCollector();
            BindingFlags bindingFlags =
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.Static;
            var constructorInfos = typeof(object).Assembly
                .GetTypes().SelectMany(t => t.GetMethods(bindingFlags)).ToArray();
            var methodInfoElement1 = constructorInfos.First(x => x.IsFamily).ToElement();
            var methodInfoElement2 = constructorInfos.First(x => x.IsFamilyOrAssembly).ToElement();

            var actual = sut.Visit(methodInfoElement1).Visit(methodInfoElement2);

            Assert.Equal(
                new[] { Accessibilities.Protected, Accessibilities.ProtectedInternal },
                actual.Value.ToArray());
        }

        [Fact]
        public void VisitNullMethodInfoElementThrows()
        {
            var sut = new AccessibilityCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((MethodInfoElement)null));
        }

        [Theory]
        [EventInfoElementData]
        public void VisitEventInfoElementProducesCorrectValue(
            Func<EventInfo, bool> predicate, Accessibilities expected)
        {
            var sut = new AccessibilityCollector();
            BindingFlags bindingFlags =
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.Static;
            var eventInfoElement = typeof(object).Assembly
                .GetTypes().Concat(new[] { typeof(ClassWithMembers) })
                .SelectMany(t => t.GetEvents(bindingFlags))
                .Where(predicate).First().ToElement();

            var actual = sut.Visit(eventInfoElement);

            Assert.Empty(sut.Value);
            Assert.Equal(expected, actual.Value.Single());
        }

        [Fact]
        public void VisitEventInfoElementManyTimeProducesCorrectValues()
        {
            var sut = new AccessibilityCollector();
            BindingFlags bindingFlags =
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.Static;
            var eventInfos = typeof(object).Assembly
                .GetTypes().SelectMany(t => t.GetEvents(bindingFlags)).ToArray();
            var eventInfoElement1 = eventInfos.First(x => x.GetAddMethod(true).IsFamily).ToElement();
            var eventInfoElement2 = eventInfos.First(x => x.GetRemoveMethod(true).IsPublic).ToElement();

            var actual = sut.Visit(eventInfoElement1).Visit(eventInfoElement2);

            Assert.Equal(
                new[] { Accessibilities.Protected, Accessibilities.Public },
                actual.Value.ToArray());
        }

        [Fact]
        public void VisitNullEventInfoElementThrows()
        {
            var sut = new AccessibilityCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((EventInfoElement)null));
        }

        [Fact]
        public void VisitParameterInfoElementReturnsSutItself()
        {
            var sut = new AccessibilityCollector();
            var actual = sut.Visit((ParameterInfoElement)null);
            Assert.Equal(sut, actual);
        }

        [Fact]
        public void VisitLocalVariableInfoElementReturnsSutItself()
        {
            var sut = new AccessibilityCollector();
            var actual = sut.Visit((LocalVariableInfoElement)null);
            Assert.Equal(sut, actual);
        }

        [Theory]
        [PropertyInfoElementData]
        public void VisitPropertyInfoElementProducesCorrectValue(
            Func<PropertyInfo, bool> predicate, Accessibilities expected)
        {
            var sut = new AccessibilityCollector();
            BindingFlags bindingFlags =
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.Static;
            var propertyInfoElement = typeof(object).Assembly
                .GetTypes().SelectMany(t => t.GetProperties(bindingFlags))
                .Where(predicate).First().ToElement();

            var actual = sut.Visit(propertyInfoElement);

            Assert.Empty(sut.Value);
            Assert.Equal(expected, actual.Value.Single());
        }

        [Fact]
        public void VisitPropertyInfoElementManyTimeProducesCorrectValues()
        {
            // Fixture setup
            var sut = new AccessibilityCollector();

            BindingFlags bindingFlags =
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.Static;

            var propertyInfos = typeof(object).Assembly
                .GetTypes().SelectMany(t => t.GetProperties(bindingFlags)).ToArray();

            var propertyInfoElement1 = propertyInfos.Where(
                x =>
                {
                    var getMethod = x.GetGetMethod(true);
                    var setMethod = x.GetSetMethod(true);
                    return getMethod != null && getMethod.IsAssembly &&
                           setMethod != null && setMethod.IsPrivate;
                })
                .First().ToElement();

            var propertyInfoElement2 = propertyInfos.Where(
                x =>
                {
                    var getMethod = x.GetGetMethod(true);
                    var setMethod = x.GetSetMethod(true);
                    return getMethod != null && getMethod.IsPublic &&
                           setMethod != null && setMethod.IsPrivate;
                })
                .First().ToElement();

            // Exercise system
            var actual = sut.Visit(propertyInfoElement1).Visit(propertyInfoElement2);

            // Verify outcome
            Assert.Equal(
                new[]
                {
                    Accessibilities.Internal | Accessibilities.Private,
                    Accessibilities.Public | Accessibilities.Private
                },
                actual.Value.ToArray());
        }

        [Fact]
        public void VisitNullPropertyInfoElementThrows()
        {
            var sut = new AccessibilityCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((PropertyInfoElement)null));
        }

        private class TypeElementDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                yield return new object[]
                {
                    new Func<Type, bool>(x => x.IsPublic), Accessibilities.Public
                };
                yield return new object[]
                {
                    new Func<Type, bool>(x => x.IsNotPublic), Accessibilities.Internal
                };
                yield return new object[]
                {
                    new Func<Type, bool>(x => x.IsNestedPublic), Accessibilities.Public
                };
                yield return new object[]
                {
                    new Func<Type, bool>(x => x.IsNestedFamORAssem), Accessibilities.ProtectedInternal
                };
                yield return new object[]
                {
                    new Func<Type, bool>(x => x.IsNestedFamily), Accessibilities.Protected
                };
                yield return new object[]
                {
                    new Func<Type, bool>(x => x.IsNestedAssembly), Accessibilities.Internal
                };
                yield return new object[]
                {
                    new Func<Type, bool>(x => x.IsNestedPrivate), Accessibilities.Private
                };
            }
        }

        private class FieldInfoElementDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                yield return new object[]
                {
                    new Func<FieldInfo, bool>(x => x.IsPublic), Accessibilities.Public
                };
                yield return new object[]
                {
                    new Func<FieldInfo, bool>(x => x.IsFamilyOrAssembly), Accessibilities.ProtectedInternal
                };
                yield return new object[]
                {
                    new Func<FieldInfo, bool>(x => x.IsFamily), Accessibilities.Protected
                };
                yield return new object[]
                {
                    new Func<FieldInfo, bool>(x => x.IsAssembly), Accessibilities.Internal
                };
                yield return new object[]
                {
                    new Func<FieldInfo, bool>(x => x.IsPrivate), Accessibilities.Private
                };
            }
        }

        private class MethodBaseElementDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                yield return new object[]
                {
                    new Func<MethodBase, bool>(x => x.IsPublic), Accessibilities.Public
                };
                yield return new object[]
                {
                    new Func<MethodBase, bool>(x => x.IsFamilyOrAssembly), Accessibilities.ProtectedInternal
                };
                yield return new object[]
                {
                    new Func<MethodBase, bool>(x => x.IsFamily), Accessibilities.Protected
                };
                yield return new object[]
                {
                    new Func<MethodBase, bool>(x => x.IsAssembly), Accessibilities.Internal
                };
                yield return new object[]
                {
                    new Func<MethodBase, bool>(x => x.IsPrivate), Accessibilities.Private
                };
            }
        }

        private class EventInfoElementDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                yield return new object[]
                {
                    new Func<EventInfo, bool>(
                        x => x.GetAddMethod(true).IsPublic && x.GetRemoveMethod(true).IsPublic),
                    Accessibilities.Public
                };
                yield return new object[]
                {
                    new Func<EventInfo, bool>(
                        x => x.GetAddMethod(true).IsFamilyOrAssembly && x.GetRemoveMethod(true).IsFamilyOrAssembly),
                    Accessibilities.ProtectedInternal
                };
                yield return new object[]
                {
                    new Func<EventInfo, bool>(
                        x => x.GetAddMethod(true).IsFamily && x.GetRemoveMethod(true).IsFamily),
                    Accessibilities.Protected
                };
                yield return new object[]
                {
                    new Func<EventInfo, bool>(
                        x => x.GetAddMethod(true).IsAssembly && x.GetRemoveMethod(true).IsAssembly),
                    Accessibilities.Internal
                };
                yield return new object[]
                {
                    new Func<EventInfo, bool>(
                        x => x.GetAddMethod(true).IsPrivate && x.GetRemoveMethod(true).IsPrivate),
                    Accessibilities.Private
                };
            }
        }

        private class PropertyInfoElementDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                yield return new object[]
                {
                    new Func<PropertyInfo, bool>(
                        x => x.GetGetMethod(true) != null && x.GetGetMethod(true).IsPublic &&
                             x.GetSetMethod(true) != null && x.GetSetMethod(true).IsPublic),
                    Accessibilities.Public
                };
                yield return new object[]
                {
                    new Func<PropertyInfo, bool>(
                        x => x.GetGetMethod(true) != null && x.GetGetMethod(true).IsFamilyOrAssembly &&
                             x.GetSetMethod(true) != null && x.GetSetMethod(true).IsFamilyOrAssembly),
                    Accessibilities.ProtectedInternal
                };
                yield return new object[]
                {
                    new Func<PropertyInfo, bool>(
                        x => x.GetGetMethod(true) != null && x.GetGetMethod(true).IsFamily &&
                             x.GetSetMethod(true) != null && x.GetSetMethod(true).IsFamily),
                    Accessibilities.Protected
                };
                yield return new object[]
                {
                    new Func<PropertyInfo, bool>(
                        x => x.GetGetMethod(true) != null && x.GetGetMethod(true).IsAssembly &&
                             x.GetSetMethod(true) != null && x.GetSetMethod(true).IsAssembly),
                    Accessibilities.Internal
                };
                yield return new object[]
                {
                    new Func<PropertyInfo, bool>(
                        x => x.GetGetMethod(true) != null && x.GetGetMethod(true).IsPrivate &&
                             x.GetSetMethod(true) != null && x.GetSetMethod(true).IsPrivate),
                    Accessibilities.Private
                };
                yield return new object[]
                {
                    new Func<PropertyInfo, bool>(
                        x => x.GetGetMethod(true) == null && x.GetSetMethod(true) != null &&
                             x.GetSetMethod(true).IsPublic),
                    Accessibilities.Public
                };
                yield return new object[]
                {
                    new Func<PropertyInfo, bool>(
                        x => x.GetGetMethod(true) != null && x.GetGetMethod(true).IsAssembly
                             && x.GetSetMethod(true) == null),
                    Accessibilities.Internal
                };
                yield return new object[]
                {
                    new Func<PropertyInfo, bool>(
                        x => x.GetGetMethod(true) != null && x.GetGetMethod(true).IsPublic &&
                             x.GetSetMethod(true) != null && x.GetSetMethod(true).IsPrivate),
                    Accessibilities.Public | Accessibilities.Private
                };
                yield return new object[]
                {
                    new Func<PropertyInfo, bool>(
                        x => x.GetGetMethod(true) != null && x.GetGetMethod(true).IsAssembly &&
                             x.GetSetMethod(true) != null && x.GetSetMethod(true).IsPrivate),
                    Accessibilities.Internal | Accessibilities.Private
                };
            }
        }
    }
}