namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;

    public interface ITestCasesWithAuto<T1, T2>
    {
        IEnumerable<ITestCase> Create(Action<T1, T2> delegator);
    }

    public interface ITestCasesWithAuto<T1, T2, T3>
    {
        IEnumerable<ITestCase> Create(Action<T1, T2, T3> delegator);
    }
}