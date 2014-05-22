namespace Jwc.Experiment.Xunit
{
    public class SpyOtherFixtureConfig
    {
        public SpyOtherFixtureConfig()
        {
            SetupCount++;
        }

        public static int SetupCount
        {
            get;
            set;
        }
    }
}