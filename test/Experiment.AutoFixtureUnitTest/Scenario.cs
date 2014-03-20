using System;
using Xunit;

namespace Jwc.Experiment
{
    public class Scenario
    {
        [AutoDataTheorem]
        public void AutoDataTheoremSupportsParameterizedTestWithAutoData(
            string arg1, Type arg2)
        {
            Assert.NotNull(arg1);
            Assert.NotNull(arg2);
        }
    }
}