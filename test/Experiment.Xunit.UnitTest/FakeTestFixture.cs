namespace Jwc.Experiment.Xunit
{
    using System;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    public class FakeTestFixture : ITestFixture
    {
        private readonly ISpecimenContext context = new SpecimenContext(new Fixture());

        public FakeTestFixture() : this(new Fixture())
        {
        }

        public FakeTestFixture(IFixture fixture)
        {
            this.context = new SpecimenContext(fixture);
        }

        public object Create(object request)
        {
            return this.context.Resolve(request);
        }
    }
}