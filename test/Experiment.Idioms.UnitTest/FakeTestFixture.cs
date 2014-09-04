namespace Jwc.Experiment
{
    using System;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    public class FakeTestFixture : ITestFixture
    {
        private readonly ISpecimenContext context;

        public FakeTestFixture()
        {
            var fixture = new Fixture();
            fixture.Inject<ITestFixture>(this);
            this.context = new SpecimenContext(fixture);
        }

        public object Create(object request)
        {
            return this.context.Resolve(request);
        }

        public void Freeze<T>(T specimen)
        {
            throw new NotSupportedException();
        }

        public T Create<T>()
        {
            throw new NotSupportedException();
        }
    }
}