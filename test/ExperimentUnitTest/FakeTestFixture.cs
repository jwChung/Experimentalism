using System;

namespace Jwc.Experiment
{
    public class FakeTestFixture : ITestFixture
    {
        public Func<object, object> OnCreate
        {
            get;
            set;
        }

        public object Create(object request)
        {
            return OnCreate(request);
        }
    }
}