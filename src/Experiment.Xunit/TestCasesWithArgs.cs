using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jwc.Experiment.Xunit
{
    using System.Collections;

    internal class TestCasesWithArgs<T1> : ITestCasesWithArgs<T1>
    {
        private readonly IEnumerable[] argumentSet;

        public TestCasesWithArgs(params IEnumerable[] argumentSet)
        {
            this.argumentSet = argumentSet;
        }

        public IEnumerable<ITestCase> Create(Action<T1> delegator)
        {
            var enumerators = this.argumentSet.Select(x => x.GetEnumerator()).ToArray();

            while (enumerators.All(e => e.MoveNext()))
                yield return new TestCase(delegator, enumerators.Select(x => x.Current).ToArray());
        }

        public ITestCasesWithAuto<T1, T2> WithAuto<T2>()
        {
            return new TestCasesWithAuto<T1, T2>(this.argumentSet);
        }


        public ITestCasesWithAuto<T1, T2, T3> WithAuto<T2, T3>()
        {
            return new TestCasesWithAuto<T1, T2, T3>(this.argumentSet);
        }
    }

    internal class TestCasesWithArgs<T1, T2> : ITestCasesWithArgs<T1, T2>
    {
        private readonly IEnumerable[] argumentSet;

        public TestCasesWithArgs(params IEnumerable[] argumentSet)
        {
            this.argumentSet = argumentSet;
        }

        public IEnumerable<ITestCase> Create(Action<T1, T2> delegator)
        {
            var enumerators = this.argumentSet.Select(x => x.GetEnumerator()).ToArray();

            while (enumerators.All(e => e.MoveNext()))
                yield return new TestCase(delegator, enumerators.Select(x => x.Current).ToArray());
        }

        public ITestCasesWithAuto<T1, T2, T3> WithAuto<T3>()
        {
            return new TestCasesWithAuto<T1, T2, T3>(this.argumentSet);
        }
    }

    internal class TestCasesWithArgs<T1, T2, T3> : ITestCasesWithArgs<T1, T2, T3>
    {
        private readonly IEnumerable[] argumentSet;

        public TestCasesWithArgs(params IEnumerable[] argumentSet)
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
