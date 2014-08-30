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
    }
}