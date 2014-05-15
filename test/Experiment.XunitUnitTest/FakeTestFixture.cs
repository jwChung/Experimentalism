using System;

namespace Jwc.Experiment.Xunit
{
    public class FakeTestFixture : ITestFixture
    {
        private readonly string _stringValue = Guid.NewGuid().ToString();
        private readonly int _intValue = new Random().Next();

        public object Create(object request)
        {
            var type = request as Type;
            if (type != null)
            {
                if (type == typeof(string))
                {
                    return _stringValue;
                }
                if (type == typeof(int))
                {
                    return _intValue;
                }
            }

            throw new NotSupportedException();
        }
    }
}