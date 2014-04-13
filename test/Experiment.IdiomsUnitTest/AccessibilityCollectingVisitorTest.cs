using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment.Idioms
{
    public class AccessibilityCollectingVisitorTest
    {
        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new AccessibilityCollectingVisitor();
            Assert.IsAssignableFrom<ReflectionVisitor<IEnumerable<Accessibilities>>>(sut);
        }

        [Fact]
        public void ValueIsCorrect()
        {
            var sut = new AccessibilityCollectingVisitor();
            var actual = sut.Value;
            Assert.Empty(actual);
        }

        [Fact]
        public void VisitAssemblyElementThrows()
        {
            var sut = new AccessibilityCollectingVisitor();
            Assert.Throws<NotSupportedException>(() => sut.Visit((AssemblyElement)null));
        }

        [Theory]
        [TypeElementData]
        public void VisitTypeElementProducesCorrectValue(
            Func<Type, bool> predicate, Accessibilities expected)
        {
            var sut = new AccessibilityCollectingVisitor();
            var typeElement = typeof(object).Assembly
                .GetTypes().Where(predicate).First().ToElement();

            var actual = sut.Visit(typeElement);

            Assert.Empty(sut.Value);
            Assert.Equal(expected, actual.Value.Single());
        }

        [Fact]
        public void VisitTypeElementManyTimeProducesCorrectValues()
        {
            var sut = new AccessibilityCollectingVisitor();
            var types = typeof(object).Assembly.GetTypes();
            var typeElement1 = types.Where(x => x.IsPublic).First().ToElement();
            var typeElement2 = types.Where(x => x.IsNotPublic).First().ToElement();

            var actual = sut.Visit(typeElement1).Visit(typeElement2);

            Assert.Equal(
                new[]{ Accessibilities.Public, Accessibilities.Internal },
                actual.Value.ToArray());
        }

        [Fact]
        public void VisitNullTypeElementThrows()
        {
            var sut = new AccessibilityCollectingVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((TypeElement)null));
        }

        [Theory]
        [FieldInfoElementData]
        public void VisitFieldInfoElementProducesCorrectValue(
            Func<FieldInfo, bool> predicate, Accessibilities expected)
        {
            var sut = new AccessibilityCollectingVisitor();
            const BindingFlags bindingFlags =
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
            var sut = new AccessibilityCollectingVisitor();
            const BindingFlags bindingFlags =
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.Static;
            var fieldInfos = typeof(object).Assembly
                .GetTypes().SelectMany(t => t.GetFields(bindingFlags));
            var fieldInfoElement1 = fieldInfos.Where(x => x.IsPublic).First().ToElement();
            var fieldInfoElement2 = fieldInfos.Where(x => x.IsPrivate).First().ToElement();

            var actual = sut.Visit(fieldInfoElement1).Visit(fieldInfoElement2);

            Assert.Equal(
                new[] { Accessibilities.Public, Accessibilities.Private },
                actual.Value.ToArray());
        }

        [Fact]
        public void VisitNullFieldInfoElementThrows()
        {
            var sut = new AccessibilityCollectingVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((FieldInfoElement)null));
        }

        [Theory]
        [MethodBaseElementData]
        public void VisitConstructorInfoElementProducesCorrectValue(
            Func<MethodBase, bool> predicate, Accessibilities expected)
        {
            var sut = new AccessibilityCollectingVisitor();
            const BindingFlags bindingFlags =
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
            var sut = new AccessibilityCollectingVisitor();
            const BindingFlags bindingFlags =
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.Static;
            var constructorInfos = typeof(object).Assembly
                .GetTypes().SelectMany(t => t.GetConstructors(bindingFlags));
            var constructorInfoElement1 = constructorInfos.Where(x => x.IsFamily).First().ToElement();
            var constructorInfoElement2 = constructorInfos.Where(x => x.IsPrivate).First().ToElement();

            var actual = sut.Visit(constructorInfoElement1).Visit(constructorInfoElement2);

            Assert.Equal(
                new[] { Accessibilities.Protected, Accessibilities.Private },
                actual.Value.ToArray());
        }

        [Fact]
        public void VisitNullConstructorInfoElementThrows()
        {
            var sut = new AccessibilityCollectingVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((ConstructorInfoElement)null));
        }

        [Theory]
        [MethodBaseElementData]
        public void VisitMethodInfoElementProducesCorrectValue(
            Func<MethodBase, bool> predicate, Accessibilities expected)
        {
            var sut = new AccessibilityCollectingVisitor();
            const BindingFlags bindingFlags =
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
            var sut = new AccessibilityCollectingVisitor();
            const BindingFlags bindingFlags =
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.Static;
            var constructorInfos = typeof(object).Assembly
                .GetTypes().SelectMany(t => t.GetMethods(bindingFlags));
            var methodInfoElement1 = constructorInfos.Where(x => x.IsFamily).First().ToElement();
            var methodInfoElement2 = constructorInfos.Where(x => x.IsFamilyOrAssembly).First().ToElement();

            var actual = sut.Visit(methodInfoElement1).Visit(methodInfoElement2);

            Assert.Equal(
                new[] { Accessibilities.Protected, Accessibilities.ProtectedInternal },
                actual.Value.ToArray());
        }

        [Fact]
        public void VisitNullMethodInfoElementThrows()
        {
            var sut = new AccessibilityCollectingVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((MethodInfoElement)null));
        }

        [Theory]
        [EventInfoElementData]
        public void VisitEventInfoElementProducesCorrectValue(
            Func<EventInfo, bool> predicate, Accessibilities expected)
        {
            var sut = new AccessibilityCollectingVisitor();
            const BindingFlags bindingFlags =
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.Static;
            var eventInfoElement = typeof(object).Assembly
                .GetTypes().Concat(new[] { typeof(ClassWithTestMembers) })
                .SelectMany(t => t.GetEvents(bindingFlags))
                .Where(predicate).First().ToElement();

            var actual = sut.Visit(eventInfoElement);

            Assert.Empty(sut.Value);
            Assert.Equal(expected, actual.Value.Single());
        }

        [Fact]
        public void VisitEventInfoElementManyTimeProducesCorrectValues()
        {
            var sut = new AccessibilityCollectingVisitor();
            const BindingFlags bindingFlags =
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.Static;
            var eventInfos = typeof(object).Assembly
                .GetTypes().SelectMany(t => t.GetEvents(bindingFlags));
            var eventInfoElement1 = eventInfos.Where(x => x.GetAddMethod(true).IsFamily).First().ToElement();
            var eventInfoElement2 = eventInfos.Where(x => x.GetRemoveMethod(true).IsPublic).First().ToElement();

            var actual = sut.Visit(eventInfoElement1).Visit(eventInfoElement2);

            Assert.Equal(
                new[] { Accessibilities.Protected, Accessibilities.Public },
                actual.Value.ToArray());
        }

        [Fact]
        public void VisitNullEventInfoElementThrows()
        {
            var sut = new AccessibilityCollectingVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((EventInfoElement)null));
        }

        [Fact]
        public void VisitParameterInfoElementThrows()
        {
            var sut = new AccessibilityCollectingVisitor();
            Assert.Throws<NotSupportedException>(() => sut.Visit((ParameterInfoElement)null));
        }

        [Fact]
        public void VisitLocalVariableInfoElementThrows()
        {
            var sut = new AccessibilityCollectingVisitor();
            Assert.Throws<NotSupportedException>(() => sut.Visit((LocalVariableInfoElement)null));
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
    }
}