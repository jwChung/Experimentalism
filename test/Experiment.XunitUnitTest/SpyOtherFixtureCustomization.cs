namespace Jwc.Experiment.Xunit
{
    public class SpyOtherFixtureCustomization
    {
        public SpyOtherFixtureCustomization()
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