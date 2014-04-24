using System;
using System.Collections.Generic;
using System.Linq;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment
{
    public class DisplayNameVisitorTest
    {
        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new DisplayNameVisitor();
            Assert.IsAssignableFrom<IReflectionVisitor<IEnumerable<string>>>(sut);
        }

        [Fact]
        public void ValueIsCorrect()
        {
            var sut = new DisplayNameVisitor();
            var actual = sut.Value;
            Assert.Empty(actual);
        }

        [Fact]
        public void VisitAssemblyElementCollectsCorrectName()
        {
            var sut = new DisplayNameVisitor();
            var assembly = typeof(object).Assembly;
            var expected = assembly.ToElement().ToString();

            var actual = sut.Visit(assembly.ToElement());

            Assert.Equal(expected, actual.Value.Single());
        }

        [Fact]
        public void VisitNullAssemblyElementThrows()
        {
            var sut = new DisplayNameVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((AssemblyElement)null));
        }

        [Fact]
        public void VisitManyAssemblyElementsCollectsCorrectNames()
        {
            var sut = new DisplayNameVisitor();
            var assembly = typeof(object).Assembly;

            var actual1 = sut.Visit(assembly.ToElement());
            var actual2 = actual1.Visit(assembly.ToElement());

            Assert.Equal(2, actual2.Value.Count());
        }
    }
}