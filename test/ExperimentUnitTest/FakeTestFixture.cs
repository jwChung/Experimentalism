using System;

namespace Jwc.Experiment
{
    public class FakeTestFixture : ITestFixture
    {
        private readonly string _stringValue = Guid.NewGuid().ToString();
        private readonly string _intValue = Guid.NewGuid().ToString();

        public string StringValue
        {
            get
            {
                return _stringValue;
            }
        }

        public string IntValue
        {
            get
            {
                return _intValue;
            }
        }
        
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