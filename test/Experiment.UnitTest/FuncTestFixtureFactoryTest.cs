namespace Jwc.Experiment
{
    using System;
    using System.Reflection;
    using Moq;
    using Xunit;

    public class FuncTestFixtureFactoryTest
    {
        [Fact]
        public void SutIsTestFixtureFactory()
        {
            var sut = new FuncTestFixtureFactory(m => null);
            Assert.IsAssignableFrom<ITestFixtureFactory>(sut);
        }

        [Fact]
        public void InitializeWithNullFuncThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new FuncTestFixtureFactory(null));
        }

        [Fact]
        public void FuncIsCorrect()
        {
            Func<MethodInfo, ITestFixture> func = m => null;
            var sut = new FuncTestFixtureFactory(func);

            var actual = sut.Func;

            Assert.Equal(func, actual);
        }

        [Fact]
        public void CreateReturnsCorrectResult()
        {
            var method = Mock.Of<MethodInfo>();
            var fixture = Mock.Of<ITestFixture>();
            Func<MethodInfo, ITestFixture> func = m =>
            {
                Assert.Equal(method, m);
                return fixture;
            };
            var sut = new FuncTestFixtureFactory(func);

            var actual = sut.Create(method);

            Assert.Equal(fixture, actual);
        }

        [Fact]
        public void CreateWithNullTestMethodThrows()
        {
            var sut = new FuncTestFixtureFactory(x => null);
            Assert.Throws<ArgumentNullException>(() => sut.Create(null));
        }
    }
}