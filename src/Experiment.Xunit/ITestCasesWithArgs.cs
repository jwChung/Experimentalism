namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;

    public interface ITestCasesWithArgs<T1>
    {
        IEnumerable<ITestCase> Create(Action<T1> delegator);

        ITestCasesWithAuto<T1, T2> WithAuto<T2>();

        ITestCasesWithAuto<T1, T2, T3> WithAuto<T2, T3>();
    }

    public interface ITestCasesWithArgs<T1, T2>
    {
        IEnumerable<ITestCase> Create(Action<T1, T2> delegator);

        ITestCasesWithAuto<T1, T2, T3> WithAuto<T3>();
    }

    public interface ITestCasesWithArgs<T1, T2, T3>
    {
        IEnumerable<ITestCase> Create(Action<T1, T2, T3> delegator);
    }
}