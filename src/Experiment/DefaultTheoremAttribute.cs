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
    /// 이 attribute는 method위에 선언되어 해당 method가 test case라는 것을
    /// 지칭하게 되며, non-parameterized test 뿐 아니라 parameterized test에도
    /// 사용될 수 있다.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "Parameterized test에 auto data를 제공하기 위해, Subclass에서 ITestFixture factory를 제공할 수 있음.")]
    [AttributeUsage(AttributeTargets.Method)]
    public class DefaultTheoremAttribute : FactAttribute
    {
        private readonly ITestFixtureFactory _fixtureFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultTheoremAttribute"/> class.
        /// </summary>
        public DefaultTheoremAttribute()
        {
            _fixtureFactory = new TypeFixtureFactory(typeof(NotSupportedFixture));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultTheoremAttribute"/> class.
        /// </summary>
        /// <param name="fixtureType">Type of the fixture.</param>
        /// <exception cref="System.ArgumentNullException">fixtureType</exception>
        public DefaultTheoremAttribute(Type fixtureType)
        {
            if (fixtureType == null)
            {
                throw new ArgumentNullException("fixtureType");
            }

            _fixtureFactory = new TypeFixtureFactory(fixtureType);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultTheoremAttribute"/> class.
        /// </summary>
        /// <param name="fixtureFactory">The fixture factory.</param>
        protected DefaultTheoremAttribute(ITestFixtureFactory fixtureFactory)
        {
            if (fixtureFactory == null)
            {
                throw new ArgumentNullException("fixtureFactory");
            }

            _fixtureFactory = fixtureFactory;
        }

        /// <summary>
        /// Gets a value indicating the fixture type passed from a constructor.
        /// </summary>
        public Type FixtureType
        {
            get
            {
                var dummyMethod = typeof(object).GetMethod("ToString");
                return FixtureFactory.Create(dummyMethod).GetType();
            }
        }

        /// <summary>
        /// Gets a value indicating the fixture factory passed from a constructor.
        /// </summary>
        public ITestFixtureFactory FixtureFactory
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
            var testData = new TestDataCollection(method.MethodInfo, dataAttributes);

            if (!testData.Any())
            {
                yield return CreateSingleTestCommand(method);
                yield break;
            }

            foreach (var testCommand in testData.Select(tcd => CreateEachTestCommand(method, tcd)))
            {
                yield return testCommand;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification="auto data를 만들 때 발생되는 unhandled exception을 처리하기 위해서 이 경고 무시함.")]
        private ITestCommand CreateSingleTestCommand(IMethodInfo method)
        {
            var autoArguments = new AutoArgumentCollection(
                FixtureFactory, method.MethodInfo, method.MethodInfo.GetParameters());

            if (!autoArguments.HasAutoParemeters)
            {
                return base.EnumerateTestCommands(method).Single();
            }
           
            try
            {
                return new TheoryCommand(method, autoArguments.ToArray());
            }
            catch (Exception exception)
            {
                return new ExceptionCommand(method, exception);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "auto data를 만들 때 발생되는 unhandled exception을 처리하기 위해서 이 경고 무시함.")]
        private ITestCommand CreateEachTestCommand(IMethodInfo method, object[] testCaseData)
        {
            var autoArguments = new AutoArgumentCollection(
                FixtureFactory,
                method.MethodInfo,
                method.MethodInfo.GetParameters(),
                testCaseData.Length);
            var argument = testCaseData.Concat(autoArguments);

            try
            {
                return new TheoryCommand(method, argument.ToArray());
            }
            catch (Exception exception)
            {
                return new ExceptionCommand(method, exception);
            }
        }

        private class TestDataCollection : IEnumerable<object[]>
        {
            private readonly MethodInfo _method;
            private readonly DataAttribute[] _dataAttributes;

            public TestDataCollection(MethodInfo method, DataAttribute[] dataAttributes)
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
            private readonly ITestFixtureFactory _fixtureFactory;
            private readonly MethodInfo _methodInfo;
            private readonly ParameterInfo[] _parameters;
            private readonly int _skipCount;

            public AutoArgumentCollection(
                ITestFixtureFactory fixtureFactory,
                MethodInfo methodInfo,
                ParameterInfo[] parameters,
                int skipCount = 0)
            {
                _fixtureFactory = fixtureFactory;
                _methodInfo = methodInfo;
                _parameters = parameters;
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
                var testFixture = _fixtureFactory.Create(_methodInfo);
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