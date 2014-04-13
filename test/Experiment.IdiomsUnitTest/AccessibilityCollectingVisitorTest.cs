﻿using System;
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
    }
}