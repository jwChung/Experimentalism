using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public TheoremAttribute(Type fixtureType)
        {
            if (fixtureType == null)
            {
                throw new ArgumentNullException("fixtureType");
            }

            _fixtureFactory = () => (ITestFixture)Activator.CreateInstance(fixtureType);
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

            object[] dataAttributes = method.MethodInfo.GetCustomAttributes(typeof(DataAttribute), false);
            ParameterInfo[] parameters = method.MethodInfo.GetParameters();

            if (dataAttributes.Length == 0)
            {
                if (parameters.Length == 0)
                {
                    yield return base.EnumerateTestCommands(method).Single();
                }
                else
                {
                    object[] arguments = parameters
                        .Select(pi => FixtureFactory.Invoke().Create(pi.ParameterType))
                        .ToArray();
                    yield return new TheoryCommand(method, arguments);
                }

                yield break;
            }

            IEnumerable<object[]> testData = dataAttributes.Cast<DataAttribute>()
                .SelectMany(da => da.GetData(method.MethodInfo, null));

            foreach (var testCaseData in testData)
            {
                var arguments = parameters
                    .Skip(testCaseData.Length)
                    .Select(pi => FixtureFactory.Invoke().Create(pi.ParameterType));

                yield return new TheoryCommand(method, testCaseData.Concat(arguments).ToArray());
            }
        }
    }
}