namespace Jwc.Experiment.Xunit
{
    public class SpyOtherInitalizer
    {
        public SpyOtherInitalizer()
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