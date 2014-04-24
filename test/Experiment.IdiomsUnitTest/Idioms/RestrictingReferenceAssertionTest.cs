﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Mono.Reflection;
using NuGet.Jwc.Experiment;
using Ploeh.Albedo;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment.Idioms
{
    public class RestrictingReferenceAssertionTest
    {
        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new RestrictingReferenceAssertion();
            Assert.IsAssignableFrom<IReflectionVisitor<object>>(sut);
        } 

        [Fact]
        public void AssembliesIsCorrect()
        {
            var assemblies = new[]
            {
                GetType().Assembly,
                typeof(RestrictingReferenceAssertion).Assembly
            };
            var sut = new RestrictingReferenceAssertion(assemblies);

            var actual = sut.Assemblies;

            Assert.Equal(assemblies, actual);
        }

        [Fact]
        public void InitializeWithNullAssembliesThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new RestrictingReferenceAssertion(null));
        }

        [Theory]
        [ReferencedData]
        public void VisitAssemblyElementReturnsSutItselfWhenAllReferencesAreSpecified(
            Assembly assembly, Assembly[] assemblies)
        {
            var sut = new RestrictingReferenceAssertion(assemblies);
            var actual = sut.Visit(assembly.ToElement());
            Assert.Equal(sut, actual);
        }

        [Theory]
        [ReferencedData]
        public void VisitAssemblyElementThrowsWhenAnyReferenceIsNotSpecified(
             Assembly assembly, Assembly[] assemblies)
        {
            var sut = new RestrictingReferenceAssertion(
                assemblies.Except(new[]{assemblies[1]}).ToArray());
            Assert.Throws<RestrictingReferenceException>(() => sut.Visit(assembly.ToElement()));
        }

        [Fact]
        public void VisitNullAssemblyElementThrows()
        {
            var sut = new RestrictingReferenceAssertion();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((AssemblyElement)null));
        }

        [Fact]
        public void ValueThrowsNotSupportedException()
        {
            var sut = new RestrictingReferenceAssertion();
            Assert.Throws<NotSupportedException>(() => sut.Value);
        }

        [Theory]
        [ReferencedData]
        public void VerifyDoesNotThrowWhenAllReferencesAreSpecified(
            Assembly assembly, Assembly[] assemblies)
        {
            var sut = new RestrictingReferenceAssertion(assemblies);
            Assert.DoesNotThrow(() => sut.Verify(assembly));
        }

        [Theory]
        [ReferencedData]
        public void VerifyThrowsWhenAnyReferenceIsNotSpecified(
             Assembly assembly, Assembly[] assemblies)
        {
            var sut = new RestrictingReferenceAssertion(
                assemblies.Except(new[] { assemblies[1] }).ToArray());
            Assert.Throws<RestrictingReferenceException>(() => sut.Verify(assembly));
        }

        [Fact]
        public void VerifyNullAssemblyThrows()
        {
            var sut = new RestrictingReferenceAssertion();
            Assert.Throws<ArgumentNullException>(() => sut.Verify(null));
        }

        private class ReferencedDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                yield return new object[]
                {
                    typeof(IdiomaticTestCase).Assembly,
                    new[]
                    {
                        typeof(object).Assembly,
                        typeof(Enumerable).Assembly,
                        typeof(BaseTheoremAttribute).Assembly,
                        typeof(IReflectionElement).Assembly,
                        typeof(FactAttribute).Assembly,
                        typeof(Fixture).Assembly,
                        typeof(IIdiomaticAssertion).Assembly,
                        typeof(ILPattern).Assembly,
                        typeof(ISet<object>).Assembly
                    }
                };

                yield return new object[]
                {
                    typeof(AutoFixtureAdapter).Assembly,
                    new[]
                    {
                        typeof(object).Assembly,
                        typeof(Enumerable).Assembly,
                        typeof(BaseTheoremAttribute).Assembly,
                        typeof(Fixture).Assembly,
                        Assembly.LoadFrom(Path.GetFullPath("Ploeh.AutoFixture.Xunit.dll")),
                        typeof(FactAttribute).Assembly
                    }
                };
            }
        }
    }
}