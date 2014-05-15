using System.Linq;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment.Idioms.Assertions
{
    public class RestrictiveReferenceAssertionTest
    {
        [Fact]
        public void SutIsIdiomaticAssemblyAssertion()
        {
            var sut = new RestrictiveReferenceAssertion();
            Assert.IsAssignableFrom<IIdiomaticAssemblyAssertion>(sut);
        }

        [Fact]
        public void RestrictiveReferencesIsCorrect()
        {
            var restrictiveReferences = new[]
            {
                typeof(object).Assembly,
                typeof(Enumerable).Assembly
            };
            var sut = new RestrictiveReferenceAssertion(restrictiveReferences);

            var actual = sut.RestrictiveReferences;

            Assert.Equal(restrictiveReferences, actual);
        }

        [Fact]
        public void VerifyAssemblyDoesNotThrowWhenAllReferencesAreSpecified()
        {
            var restrictiveReferences = new[]
            {
                typeof(object).Assembly,
                typeof(Enumerable).Assembly,
                typeof(FactAttribute).Assembly,
                typeof(TheoryAttribute).Assembly
            };
            var sut = new RestrictiveReferenceAssertion(restrictiveReferences);
            Assert.DoesNotThrow(() => sut.Verify(typeof(ExamAttribute).Assembly));
        }

        [Fact]
        public void VerifyAssemblyThrowsWhenAnyReferenceIsNotSpecified()
        {
            var restrictiveReferences = new[]
            {
                typeof(object).Assembly,
                typeof(FactAttribute).Assembly,
                typeof(TheoryAttribute).Assembly
            };
            var sut = new RestrictiveReferenceAssertion(restrictiveReferences);
            Assert.Throws<RestrictiveReferenceException>(() => sut.Verify(typeof(ExamAttribute).Assembly));
        }
    }
}