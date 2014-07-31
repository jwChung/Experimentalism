using System;

namespace Jwc.Experiment.Xunit
{
    public class DelegatingTestFixture : ITestFixture
    {
        public Func<object, object> OnCreate { get; set; }

        public object Create(object request)
        {
            return this.OnCreate(request);
        }
    }
}