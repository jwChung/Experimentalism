using Xunit;

namespace Jwc.Experiment
{
    public class TestCaseTest
    {
        [Fact]
        public void SutIsTestCase()
        {
            var sut = TestCase.New(() => { });
            Assert.IsAssignableFrom<ITestCase>(sut);
        }

        //[Fact]
        //public void DelegateIsCorrect()
        //{
        //    Action expected = () => { };
        //    var sut = TestCase.New(expected);

        //    var actual = sut.Delegate;

        //    Assert.Equal(expected, actual);
        //}
    }
}