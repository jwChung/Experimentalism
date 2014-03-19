namespace Jwc.Experiment
{
    public class NotSupportedFixture : ITestFixture
    {
        public object Create(object request)
        {
            throw new System.NotImplementedException();
        }
    }
}