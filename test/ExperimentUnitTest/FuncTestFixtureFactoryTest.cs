using System;
using System.Reflection;
using Xunit;

namespace Jwc.Experiment
{
    public class FuncTestFixtureFactoryTest
    {
        [Fact]
        public void SutIsTestFixtureFactory()
        {
            var sut = new FuncTestFixtureFactory(m => null);
            Assert.IsAssignableFrom<ITestFixtureFactory>(sut);
        }

        [Fact]
        public void FuncIsCorrect()
        {
            Func<MethodInfo, ITestFixtureFactory> func = m => null;
            var sut = new FuncTestFixtureFactory(func);

            var actual = sut.Func;

            Assert.Equal(func, actual);
        }
    }
}