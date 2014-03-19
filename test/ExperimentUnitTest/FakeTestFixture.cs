using System;

namespace Jwc.Experiment
{
    public class FakeTestFixture : ITestFixture
    {
        public readonly static string StringValue = Guid.NewGuid().ToString();
        public readonly static int IntValue = new Random().Next();

        private Func<object, object> _onCreate = r =>
        {
            var type = r as Type;
            if (type != null)
            {
                if (type == typeof(string))
                {
                    return StringValue;
                }
                if (type == typeof(int))
                {
                    return IntValue;
                }
            }

            throw new NotSupportedException();
        };

        public Func<object, object> OnCreate
        {
            get
            {
                return _onCreate;
            }
            set
            {
                _onCreate = value;
            }
        }

        public object Create(object request)
        {
            return OnCreate(request);
        }
    }
}