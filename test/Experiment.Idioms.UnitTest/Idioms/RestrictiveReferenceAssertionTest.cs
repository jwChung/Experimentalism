namespace Jwc.Experiment.Idioms
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Xml;
    using Jwc.Experiment.Xunit;
    using global::Xunit;
    using global::Xunit.Extensions;

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
                Assembly.Load("mscorlib"),
                Assembly.Load("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"),
                Assembly.Load("System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"),
                Assembly.Load("System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"),
                Assembly.Load("Jwc.Experiment"),
                Assembly.Load("xunit"),
                Assembly.Load("xunit.extensions")
            };
            var sut = new RestrictiveReferenceAssertion(restrictiveReferences);
            Assert.DoesNotThrow(() => sut.Verify(typeof(TestBaseAttribute).Assembly));
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
            Assert.Throws<RestrictiveReferenceException>(() => sut.Verify(typeof(TestBaseAttribute).Assembly));
        }

        [Fact]
        public void VerifyAssemblyThrowsWhenAnyUnusedReferenceIsSpecified()
        {
            var restrictiveReferences = new[]
            {
                typeof(object).Assembly,
                typeof(XmlNode).Assembly,
                typeof(ISet<>).Assembly,
                typeof(TheoryAttribute).Assembly // Unused reference
            };
            var sut = new RestrictiveReferenceAssertion(restrictiveReferences);
            Assert.Throws<RestrictiveReferenceException>(() => sut.Verify(typeof(FactAttribute).Assembly));
        }
    }
}