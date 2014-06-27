using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit.Sdk;

namespace Jwc.Experiment.Xunit
{
    internal class TestCommandEnumerable : IEnumerable<ITestCommand>
    {
        private readonly IMethodInfo testMethod;
        private readonly IEnumerable<ITestCommand> testCommands;

        public TestCommandEnumerable(IMethodInfo testMethod, IEnumerable<ITestCommand> testCommands)
        {
            this.testMethod = testMethod;
            this.testCommands = testCommands;
        }

        public IEnumerator<ITestCommand> GetEnumerator()
        {
            return new TestCommandEnumerator(this.testMethod, this.testCommands.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class TestCommandEnumerator : IEnumerator<ITestCommand>
        {
            private readonly IMethodInfo testMethod;
            private readonly IEnumerator<ITestCommand> enumerator;
            private ITestCommand current;

            public TestCommandEnumerator(IMethodInfo testMethod, IEnumerator<ITestCommand> enumerator)
            {
                this.testMethod = testMethod;
                this.enumerator = enumerator;
            }

            public ITestCommand Current
            {
                get { return this.current; }
            }

            object IEnumerator.Current
            {
                get { return this.Current; }
            }

            public void Dispose()
            {
                this.enumerator.Dispose();
            }

            [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is suppressed to catch unhandled exception thrown when enumerating test commands.")]
            public bool MoveNext()
            {
                try
                {
                    if (!this.enumerator.MoveNext())
                        return false;

                    this.current = this.enumerator.Current;
                }
                catch (Exception exception)
                {
                    this.current = new ExceptionCommand(this.testMethod, exception);
                }

                return true;
            }

            public void Reset()
            {
                this.enumerator.Reset();
            }
        }
    }
}