using System;

namespace Jwc.Experiment.Idioms
{
    public class FakeTestFixture : ITestFixture
    {
        public object Create(object request)
        {
            throw new NotSupportedException();
        }
    }
}