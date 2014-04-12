using System.Collections.Generic;
using System.Linq;
using Ploeh.Albedo.Refraction;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class AllRefractionsTest
    {
        [Fact]
        public void SutIsEnumerableOfRefraction()
        {
            var sut = new AllRefractions();
            Assert.IsAssignableFrom<IEnumerable<IReflectionElementRefraction<object>>>(sut);
        }

        [Fact]
        public void SutEnumeratesAllKindsOfRefraction()
        {
            var refractions = new[]
            {
                typeof(AssemblyElementRefraction<object>),
                typeof(ConstructorInfoElementRefraction<object>),
                typeof(EventInfoElementRefraction<object>),
                typeof(FieldInfoElementRefraction<object>),
                typeof(LocalVariableInfoElementRefraction<object>),
                typeof(MethodInfoElementRefraction<object>),
                typeof(ParameterInfoElementRefraction<object>),
                typeof(PropertyInfoElementRefraction<object>),
                typeof(TypeElementRefraction<object>),
                typeof(ReflectionElementRefraction<object>)
            };
            var sut = new AllRefractions();

            var actual = sut.Select(r => r.GetType());

            Assert.Equal(
                refractions.OrderBy(t => t.FullName),
                actual.OrderBy(t => t.FullName));
        }
    }
}