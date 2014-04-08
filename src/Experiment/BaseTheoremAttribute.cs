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
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class BaseTheoremAttribute : FactAttribute
    {
        /// <summary>
        /// Creates an instance of <see cref="ITestFixture"/>.
        /// </summary>
        /// <param name="testMethod">
        /// The test method
        /// </param>
        /// <returns>
        /// The created fixture.
        /// </returns>
        public abstract ITestFixture CreateTestFixture(MethodInfo testMethod);

        /// <summary>
        /// Enumerates the test commands represented by this test method.
        /// Derived classes should override this method to return instances of
        /// <see cref="ITestCommand" />, one per execution of a test method.
        /// </summary>
        /// <param name="method">The test method</param>
        /// <returns>
        /// The test commands which will execute the test runs for the given method
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "auto data를 만들 때 발생되는 unhandled exception을 처리하기 위해서 이 경고 무시함.")]
        protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            try
            {
                var specifiedArgumentSet = CreateSpecifiedArgumentSet(method);
                if (!specifiedArgumentSet.Any())
                {
                    return new[] { CreateSingleTestCommand(method) };
                }

                return specifiedArgumentSet.Select(sa => CreateEachTestCommand(method, sa));
            }
            catch (Exception exception)
            {
                return new ITestCommand[] { new ExceptionCommand(method, exception) };
            }
        }

        private static SpecifiedArgumentSet CreateSpecifiedArgumentSet(IMethodInfo method)
        {
            var dataAttributes = ((IEnumerable<DataAttribute>)method
                .MethodInfo
                .GetCustomAttributes(typeof(DataAttribute), false)).ToArray();
            return new SpecifiedArgumentSet(method.MethodInfo, dataAttributes);
        }

        private ITestCommand CreateSingleTestCommand(IMethodInfo method)
        {
            var autoArguments = new AutoArgumentCollection(
                new Lazy<ITestFixture>(() => CreateTestFixture(method.MethodInfo)),
                method.MethodInfo.GetParameters());

            if (!autoArguments.HasAutoParemeters)
            {
                return base.EnumerateTestCommands(method).Single();
            }

            return new TheoryCommand(method, autoArguments.ToArray());
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "auto data를 만들 때 발생되는 unhandled exception을 처리하기 위해서 이 경고 무시함.")]
        private ITestCommand CreateEachTestCommand(IMethodInfo method, object[] specifiedArguments)
        {
            try
            {
                var autoArguments = new AutoArgumentCollection(
                    new Lazy<ITestFixture>(() => CreateTestFixture(method.MethodInfo)),
                    method.MethodInfo.GetParameters(),
                    specifiedArguments.Length);
                var arguments = specifiedArguments.Concat(autoArguments);

                return new TheoryCommand(method, arguments.ToArray());
            }
            catch (Exception exception)
            {
                return new ExceptionCommand(method, exception);
            }
        }

        private class SpecifiedArgumentSet : IEnumerable<object[]>
        {
            private readonly MethodInfo _method;
            private readonly DataAttribute[] _dataAttributes;

            public SpecifiedArgumentSet(MethodInfo method, DataAttribute[] dataAttributes)
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
            private readonly Lazy<ITestFixture> _fixture;
            private readonly ParameterInfo[] _parameters;
            private readonly int _skipCount;

            public AutoArgumentCollection(
                Lazy<ITestFixture> fixture,
                ParameterInfo[] parameters,
                int skipCount = 0)
            {
                _fixture = fixture;
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
                return _parameters
                    .Skip(_skipCount)
                    .Select(pi => _fixture.Value.Create(pi.ParameterType))
                    .GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}