namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    internal class TestCasesWithAuto<T1, T2> : ITestCasesWithAuto<T1, T2>
    {
        private readonly IEnumerable[] argumentSet;

        public TestCasesWithAuto(params IEnumerable[] argumentSet)
        {
            this.argumentSet = argumentSet;
        }

        public IEnumerable<ITestCase> Create(Action<T1, T2> delegator)
        {
            var enumerators = this.argumentSet.Select(x => x.GetEnumerator()).ToArray();

            while (enumerators.All(e => e.MoveNext()))
                yield return new TestCase(delegator, enumerators.Select(x => x.Current).ToArray());
        }
    }

    internal class TestCasesWithAuto<T1, T2, T3> : ITestCasesWithAuto<T1, T2, T3>
    {
        private readonly IEnumerable[] argumentSet;
        
        public TestCasesWithAuto(params IEnumerable[] argumentSet)
        {
            this.argumentSet = argumentSet;
        }

        public IEnumerable<ITestCase> Create(Action<T1, T2, T3> delegator)
        {
            var enumerators = this.argumentSet.Select(x => x.GetEnumerator()).ToArray();

            while (enumerators.All(e => e.MoveNext()))
                yield return new TestCase(delegator, enumerators.Select(x => x.Current).ToArray());
        }
    }
}