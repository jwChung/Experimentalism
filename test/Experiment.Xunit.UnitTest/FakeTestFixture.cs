namespace Jwc.Experiment.Xunit
{
    using System;

    public class FakeTestFixture : ITestFixture
    {
        private readonly string stringValue = Guid.NewGuid().ToString();
        private readonly int intValue = new Random().Next();

        public object Create(object request)
        {
            var type = request as Type;
            if (type != null)
            {
                if (type == typeof(string))
                {
                    return this.stringValue;
                }

                if (type == typeof(int))
                {
                    return this.intValue;
                }
            }

            throw new NotSupportedException();
        }

        public void Freeze<T>(T specimen)
        {
            throw new NotSupportedException();
        }
    }
}