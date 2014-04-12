using System.Collections.Generic;
using System.Linq;
using Ploeh.Albedo.Refraction;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class AllMemberRefractionsTest
    {
        [Fact]
        public void SutIsEnumerableOfRefraction()
        {
            var sut = new AllMemberRefractions();
            Assert.IsAssignableFrom<IEnumerable<IReflectionElementRefraction<object>>>(sut);
        }

        [Fact]
        public void SutEnumeratesAllKindsOfRefraction()
        {
            var refractions = new[]
            {
                typeof(ConstructorInfoElementRefraction<object>),
                typeof(EventInfoElementRefraction<object>),
                typeof(FieldInfoElementRefraction<object>),
                typeof(MethodInfoElementRefraction<object>),
                typeof(PropertyInfoElementRefraction<object>)
            };
            var sut = new AllMemberRefractions();

            var actual = sut.Select(r => r.GetType());

            Assert.Equal(
                refractions.OrderBy(t => t.FullName),
                actual.OrderBy(t => t.FullName));
        }
    }
}