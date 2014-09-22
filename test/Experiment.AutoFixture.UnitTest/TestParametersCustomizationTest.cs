namespace Jwc.Experiment.AutoFixture
{
    using System;
    using Ploeh.Albedo;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit;
    using global::Xunit;

    public class TestParametersCustomizationTest
    {
        [Fact]
        public void SutIsCustomization()
        {
            var sut = new TestParametersCustomization(Mocked.Of<ITestMethodContext>());
            Assert.IsAssignableFrom<ICustomization>(sut);
        }

        [Fact]
        public void InitializeWithNullContextThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TestParametersCustomization(null));
        }

        [Fact]
        public void InitializeCorrectlyInitializesProperties()
        {
            var testMethodContext = Mocked.Of<ITestMethodContext>();
            var sut = new TestParametersCustomization(testMethodContext);
            Assert.Equal(testMethodContext, sut.TestMethodContext);
        }

        [Fact]
        public void CustomizeWitNullFixtureThrows()
        {
            var sut = new TestParametersCustomization(Mocked.Of<ITestMethodContext>());
            Assert.Throws<ArgumentNullException>(() => sut.Customize(null));
        }

        [Fact]
        public void CustomizeCorrectlyCustomizesFixture()
        {
            var context = Mocked.Of<ITestMethodContext>(
                c => c.ActualMethod
                    == new Methods<TestParametersCustomizationTest>()
                    .Select(x => x.TestMethod(null, null)));
            var sut = new TestParametersCustomization(context);
            var fixture = new Fixture();

            sut.Customize(fixture);

            Assert.Equal(fixture.Create<object>(), fixture.Create<object>());
            Assert.NotNull(fixture.Create<Person>().Name);
        }

        private void TestMethod([Frozen] object arg1, [Frozen][Greedy] Person arg2)
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