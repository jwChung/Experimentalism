using System;
using System.Collections.Generic;
using Ploeh.Albedo;
using Xunit;

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
    }
}