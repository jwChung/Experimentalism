using System;
using System.Collections;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="TheoremAttribute"/> class.
        /// </summary>
        public TheoremAttribute()
        {
            _fixtureFactory = () => new NotSupportedFixture();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TheoremAttribute"/> class.
        /// </summary>
        /// <param name="fixtureType">Type of the fixture.</param>
        /// <exception cref="System.ArgumentNullException">fixtureType</exception>
        public TheoremAttribute(Type fixtureType)
        {
            if (fixtureType == null)
            {
                throw new ArgumentNullException("fixtureType");
            }

            _fixtureFactory = () => (ITestFixture)Activator.CreateInstance(fixtureType);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TheoremAttribute"/> class.
        /// </summary>
        /// <param name="fixtureFactory">The fixture factory.</param>
        /// <exception cref="System.ArgumentNullException">fixtureFactory</exception>
        protected TheoremAttribute(Func<ITestFixture> fixtureFactory)
        {
            if (fixtureFactory == null)
            {
                throw new ArgumentNullException("fixtureFactory");
            }

            _fixtureFactory = fixtureFactory;
        }

        /// <summary>
        /// Gets a value indicating the fixture factory which is passed from a constructor.
        /// </summary>
        /// <value>
        /// The fixture factory.
        /// </value>
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

            var dataAttributes = ((IEnumerable<DataAttribute>)method
                .MethodInfo
                .GetCustomAttributes(typeof(DataAttribute), false)).ToArray();
            var testData = new TestData(method.MethodInfo, dataAttributes);

            if (!testData.Any())
            {
                yield return CreateSingleTestCommand(method);
                yield break;
            }

            foreach (var testCommand in CreateManyTestCommands(method, testData))
            {
                yield return testCommand;
            }
        }

        private ITestCommand CreateSingleTestCommand(IMethodInfo method)
        {
            var autoArguments = new AutoArgumentCollection(
                method.MethodInfo.GetParameters(),
                FixtureFactory);

            if (autoArguments.HasAutoParemeters)
            {
                return new TheoryCommand(method, autoArguments.ToArray());
            }

            return base.EnumerateTestCommands(method).Single();
        }

        private IEnumerable<ITestCommand> CreateManyTestCommands(
            IMethodInfo method, IEnumerable<object[]> testData)
        {
            return from testCaseData in testData
                   let autoArguments = new AutoArgumentCollection(
                       method.MethodInfo.GetParameters(),
                       FixtureFactory,
                       testCaseData.Length)
                   select testCaseData.Concat(autoArguments)
                   into argument
                   select new TheoryCommand(method, argument.ToArray());
        }

        private class TestData : IEnumerable<object[]>
        {
            private readonly MethodInfo _method;
            private readonly DataAttribute[] _dataAttributes;

            public TestData(MethodInfo method, DataAttribute[] dataAttributes)
            {
                _method = method;
                _dataAttributes = dataAttributes;
            }

            public IEnumerator<object[]> GetEnumerator()
            {
                var parameterTypes = _method.GetParameters().Select(pi => pi.ParameterType).ToArray();
                return _dataAttributes.SelectMany(da => da.GetData(_method, parameterTypes)).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class AutoArgumentCollection : IEnumerable<object>
        {
            private readonly ParameterInfo[] _parameters;
            private readonly Func<ITestFixture> _fixtureFactory;
            private readonly int _skipCount;

            public AutoArgumentCollection(
                ParameterInfo[] parameters,
                Func<ITestFixture> fixtureFactory,
                int skipCount = 0)
            {
                _parameters = parameters;
                _fixtureFactory = fixtureFactory;
                _skipCount = skipCount;
            }

            public bool HasAutoParemeters
            {
                get
                {
                    return _parameters.Length != 0;
                }
            }

            public IEnumerator<object> GetEnumerator()
            {
                var testFixture = _fixtureFactory.Invoke();

                return _parameters
                    .Skip(_skipCount)
                    .Select(pi => testFixture.Create(pi.ParameterType))
                    .GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}