namespace Jwc.Experiment.AutoFixture
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Ploeh.Albedo;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit;
    using global::Xunit;

    public class TestParametersCustomizationTest
    {
        [Fact]
        public void SutIsCustomization()
        {
            var sut = new TestParametersCustomization(Mocked.Of<IEnumerable<ParameterInfo>>());
            Assert.IsAssignableFrom<ICustomization>(sut);
        }

        [Fact]
        public void InitializeWithAnyNullArgumentsThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new TestParametersCustomization((IEnumerable<ParameterInfo>)null));
        }

        [Fact]
        public void InitializeCorrectlyInitializesProperties()
        {
            var parameters = Mocked.Of<IEnumerable<ParameterInfo>>();
            var sut = new TestParametersCustomization(parameters);
            Assert.Equal(parameters, sut.Parameters);
        }

        [Fact]
        public void CustomizeWitNullFixtureThrows()
        {
            var sut = new TestParametersCustomization(Mocked.Of<IEnumerable<ParameterInfo>>());
            Assert.Throws<ArgumentNullException>(() => sut.Customize(null));
        }

        [Fact]
        public void CustomizeCorrectlyCustomizesFixture()
        {
            var parameters = new Methods<TestParametersCustomizationTest>()
                .Select(x => x.TestMethod(null, null))
                .GetParameters();
            var sut = new TestParametersCustomization(parameters);
            var fixture = new Fixture();

            sut.Customize(fixture);

            Assert.Equal(fixture.Create<object>(), fixture.Create<object>());
            Assert.NotNull(fixture.Create<Person>().Name);
        }

        private void TestMethod([Frozen] object arg1, [Frozen] [Greedy] Person arg2)
        {
        }

        private class Person
        {
            private readonly string name;

            public Person()
            {
            }

            public Person(string name)
            {
                this.name = name;
            }

            public string Name
            {
                get { return this.name; }
            }
        }
    }
}