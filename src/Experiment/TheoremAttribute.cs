using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Extensions;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    /// <summary>
    /// 이 attribute는 method위에 선언되어 해당 method가 test-case라는 것을
    /// 지칭하게 되며, non-parameterized test 뿐 아니라 parameterized test에도
    /// 사용될 수 있다.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class TheoremAttribute : FactAttribute
    {
        private readonly Func<ITestFixture> _fixtureFactory;

        public TheoremAttribute()
        {
            _fixtureFactory = () => new NotSupportedFixture();
        }

        protected TheoremAttribute(Func<ITestFixture> fixtureFactory)
        {
            if (fixtureFactory == null)
            {
                throw new ArgumentNullException("fixtureFactory");
            }

            _fixtureFactory = fixtureFactory;
        }

        public Func<ITestFixture> FixtureFactory
        {
            get
            {
                return _fixtureFactory;
            }
        }

        /// <summary>
        /// Enumerates the test commands represented by this test method.
        /// Derived classes should override this method to return instances of 
        /// <see cref="ITestCommand" />, one per execution of a test method.
        /// </summary>
        /// <param name="method">The test method</param>
        /// <returns>
        /// The test commands which will execute the test runs for the given method
        /// </returns>
        protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            return !method.MethodInfo.IsDefined(typeof(DataAttribute), false) 
                ? base.EnumerateTestCommands(method)
                : new TheoryAttribute().CreateTestCommands(method);
        }
    }
}