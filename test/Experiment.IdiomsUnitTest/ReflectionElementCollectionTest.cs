using System;
using System.Collections.Generic;
using Ploeh.Albedo;
using Ploeh.Albedo.Refraction;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class ReflectionElementCollectionTest
    {
        [Fact]
        public void SutIsEnumerableOfReflectionElement()
        {
            var sut = new ReflectionElementCollection(new object[0]);
            Assert.IsAssignableFrom<IEnumerable<IReflectionElement>>(sut);
        }

        [Fact]
        public void InitializeWithNullSourcesThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new ReflectionElementCollection(null));
        }

        [Fact]
        public void InitializeWithNullRefractionsThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new ReflectionElementCollection(new object[0], null));
        }
    }
}