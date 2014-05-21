using System;

namespace Jwc.Experiment.Xunit
{
    public class SpyInitalizer : MarshalByRefObject, IDisposable
    {
        public SpyInitalizer()
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