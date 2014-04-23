using Xunit;

namespace Jwc.Experiment
{
    public class ElementReferenceCollectingVisitorTest
    {
        [Fact]
        public void SutIsReferenceCollectingVisitor()
        {
            var sut = new ElementReferenceCollectingVisitor();
            Assert.IsAssignableFrom<ReferenceCollectingVisitor>(sut);
        }
    }
}