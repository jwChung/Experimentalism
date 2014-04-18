using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;
using Ploeh.Albedo.Refraction;
using Xunit;

namespace Jwc.Experiment
{
    public class ReflectionElementsTest
    {
        [Fact]
        public void SutIsEnumerableOfReflectionElement()
        {
            var sut = new ReflectionElements(new object[0]);
            Assert.IsAssignableFrom<IEnumerable<IReflectionElement>>(sut);
        }

        [Fact]
        public void InitializeWithNullSourcesThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new ReflectionElements(null));
        }

        [Fact]
        public void InitializeWithNullRefractionsThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new ReflectionElements(new object[0], null));
        }

        [Fact]
        public void SourcesIsCorrect()
        {
            var sources = new object[0];
            var sut = new ReflectionElements(sources, new TypeElementRefraction<object>());

            var actual = sut.Sources;

            Assert.Equal(sources, actual);
        }

        [Fact]
        public void RefractionsIsCorrect()
        {
            var refraction1 = new TypeElementRefraction<object>();
            var refraction2 = new AssemblyElementRefraction<object>();
            var sut = new ReflectionElements(new object[0], refraction1, refraction2);

            var actual = sut.Refractions;

            Assert.Equal(
                new IReflectionElementRefraction<object>[] { refraction1, refraction2 },
                actual);
        }

        [Fact]
        public void SutEnumeratesCorrectReflectionElements()
        {
            // Fixture setup
            var refraction1 = new TypeElementRefraction<object>();
            var refraction2 = new AssemblyElementRefraction<object>();
            var sources = new[]
            {
                new object(),
                GetType(),
                GetType().Assembly,
                MethodBase.GetCurrentMethod()
            };
            var sut = new ReflectionElements(sources, refraction1, refraction2);

            // Exercise system
            IReflectionElement[] actual = sut.ToArray();

            // Verify outcome
            Assert.Equal(2, actual.Length);

            var typeElement = Assert.IsType<TypeElement>(actual[0]);
            Assert.Equal(GetType(), typeElement.Type);

            var assemblyElement = Assert.IsType<AssemblyElement>(actual[1]);
            Assert.Equal(GetType().Assembly, assemblyElement.Assembly);
        }
    }
}