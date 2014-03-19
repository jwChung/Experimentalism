using System;

namespace Jwc.Experiment
{
    public class FakeTestFixture : ITestFixture
    {
        private Func<object, object> _onCreate = x => null;

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