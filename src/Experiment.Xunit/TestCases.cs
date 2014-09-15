namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TestCases
    {
        public static ITestCasesWithArgs<T1> WithArgs<T1>(
            IEnumerable<T1> args1)
        {
            return new TestCasesWithArgs<T1>(args1);
        }

        public static ITestCasesWithArgs<T1, T2> WithArgs<T1, T2>(
            IEnumerable<T1> args1, IEnumerable<T2> args2)
        {
            return new TestCasesWithArgs<T1, T2>(args1, args2);
        }

        public static ITestCasesWithArgs<T1, T2, T3> WithArgs<T1, T2, T3>(
            IEnumerable<T1> args1, IEnumerable<T2> args2, IEnumerable<T3> args3)
        {
            return new TestCasesWithArgs<T1, T2, T3>(args1, args2, args3);
        }
    }
}