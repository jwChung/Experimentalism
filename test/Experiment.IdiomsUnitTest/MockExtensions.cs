using Moq;

namespace Jwc.Experiment
{
    public static class MockExtensions
    {
        public static Mock<T> ToMock<T>(this T mocked) where T : class
        {
            return Mock.Get(mocked);
        }
    }
}