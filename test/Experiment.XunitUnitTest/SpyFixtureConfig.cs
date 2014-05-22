using System;

namespace Jwc.Experiment.Xunit
{
    public class SpyFixtureConfig : IDisposable
    {
        public SpyFixtureConfig()
        {
            SetupCount++;
        }

        public static int SetupCount
        {
            get;
            set;
        }

        public static int TearDownCount
        {
            get;
            set;
        }

        public void Dispose()
        {
            TearDownCount++;
        }
    }
}