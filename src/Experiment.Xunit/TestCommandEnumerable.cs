using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit.Sdk;

namespace Jwc.Experiment.Xunit
{
    internal class TestCommandEnumerable: IEnumerable<ITestCommand>
    {
        private readonly IMethodInfo _testMethod;
        private readonly IEnumerable<ITestCommand> _testCommands;

        public TestCommandEnumerable(IMethodInfo testMethod, IEnumerable<ITestCommand> testCommands)
        {
            _testMethod = testMethod;
            _testCommands = testCommands;
        }

        public IEnumerator<ITestCommand> GetEnumerator()
        {
            return new TestCommandEnumerator(_testMethod, _testCommands.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class TestCommandEnumerator : IEnumerator<ITestCommand>
        {
            private readonly IMethodInfo _testMethod;
            private readonly IEnumerator<ITestCommand> _enumerator;
            private ITestCommand _current;

            public TestCommandEnumerator(IMethodInfo testMethod, IEnumerator<ITestCommand> enumerator)
            {
                _testMethod = testMethod;
                _enumerator = enumerator;
            }

            public ITestCommand Current
            {
                get { return _current; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public void Dispose()
            {
                _enumerator.Dispose();
            }

            [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is suppressed to catch unhandled exception thrown when enumerating test commands.")]
            public bool MoveNext()
            {
                try
                {
                    if (!_enumerator.MoveNext())
                        return false;

                    _current = _enumerator.Current;
                }
                catch (Exception exception)
                {
                    _current = new ExceptionCommand(_testMethod, exception);
                }

                return true;
            }

            public void Reset()
            {
                _enumerator.Reset();
            }
        }
    }
}