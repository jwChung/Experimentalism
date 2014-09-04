namespace Jwc.Experiment.Xunit
{
    using System;

    public class DelegatingTestFixture : ITestFixture
    {
        public Func<object, object> OnCreate { get; set; }

        public object Create(object request)
        {
            return this.OnCreate(request);
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