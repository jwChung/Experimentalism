﻿using System;
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
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "Parameterized test에 auto data를 제공하기 위해, Subclass에서 ITestFixture factory를 제공할 수 있음.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments", Justification="fixtureType은 FixtureFactory property를 통해 얼마든지 얻을 수 있으며, 중요한 정보가 아님.")]
    [AttributeUsage(AttributeTargets.Method)]
    public class NaiveTheoremAttribute : FactAttribute
    {
        private readonly Func<ITestFixture> _fixtureFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="NaiveTheoremAttribute"/> class.
        /// </summary>
        public NaiveTheoremAttribute()
        {
            _fixtureFactory = () => new NotSupportedFixture();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NaiveTheoremAttribute"/> class.
        /// </summary>
        /// <param name="fixtureType">Type of the fixture.</param>
        /// <exception cref="System.ArgumentNullException">fixtureType</exception>
        public NaiveTheoremAttribute(Type fixtureType)
        {
            if (fixtureType == null)
            {
                throw new ArgumentNullException("fixtureType");
            }

            _fixtureFactory = () => (ITestFixture)Activator.CreateInstance(fixtureType);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NaiveTheoremAttribute"/> class.
        /// </summary>
        /// <param name="fixtureFactory">The fixture factory.</param>
        /// <exception cref="System.ArgumentNullException">fixtureFactory</exception>
        protected NaiveTheoremAttribute(Func<ITestFixture> fixtureFactory)
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

            foreach (var testCommand in testData.Select(tcd => CreateEachTestCommand(method, tcd)))
            {
                yield return testCommand;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification="auto data를 만들 때 발생되는 unhandled exception을 처리하기 위해서 이 경고 무시함.")]
        private ITestCommand CreateSingleTestCommand(IMethodInfo method)
        {
            var autoArguments = new AutoArgumentCollection(
                method.MethodInfo.GetParameters(),
                FixtureFactory);

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
                method.MethodInfo.GetParameters(),
                FixtureFactory,
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