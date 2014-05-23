using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Extensions;
using Xunit.Sdk;

namespace Jwc.Experiment.Xunit
{
    /// <summary>
    ///     이 attribute는 method위에 선언되어 해당 method가 test라는 것을 지칭하게 되며, non-parameterized test 뿐
    ///     아니라 parameterized test에도 사용될 수 있다.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "This attribue can be inherited by custom attribute.")]
    public class TestAttribute : FactAttribute
    {
        /// <summary>
        ///     Enumerates the test commands represented by this test method. Derived classes should
        ///     override this method to return instances of
        ///     <see cref="ITestCommand" />, one per execution of a test method.
        /// </summary>
        /// <param name="method">
        ///     The test method
        /// </param>
        /// <returns>
        ///     The test commands which will execute the test runs for the given method
        /// </returns>
        protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
        {
            if (method == null)
                throw new ArgumentNullException("method");

            AssemblyFixtureCustomizationAttribute.Customize(method.MethodInfo.ReflectedType.Assembly);

            var enumerator = GetTestCommands(method).GetEnumerator();

            Func<IMethodInfo, ITestCommand> exceptionCommandFunc;

            while (TryMoveNext(enumerator, out exceptionCommandFunc))
                yield return enumerator.Current;

            if (exceptionCommandFunc != null)
                yield return exceptionCommandFunc.Invoke(method);
        }

        /// <summary>
        ///     Creates an instance of <see cref="ITestFixture" />.
        /// </summary>
        /// <param name="testMethod">
        ///     The test method
        /// </param>
        /// <returns>
        ///     The created fixture.
        /// </returns>
        protected virtual ITestFixture CreateTestFixture(MethodInfo testMethod)
        {
            return DefaultFixtureFactory.Current.Create(testMethod);
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is suppressed to catch unhandled exception thrown when creating test commands.")]
        private static bool TryMoveNext(
            IEnumerator<ITestCommand> enumerator,
            out Func<IMethodInfo, ITestCommand> exceptionCommandFunc)
        {
            try
            {
                var moveNext = enumerator.MoveNext();
                exceptionCommandFunc = null;
                return moveNext;
            }
            catch (Exception exception)
            {
                exceptionCommandFunc = m => new ExceptionCommand(m, exception);
                return false;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is suppressed to catch unhandled exception thrown when creating test commands.")]
        private IEnumerable<ITestCommand> GetTestCommands(IMethodInfo method)
        {
            try
            {
                var specifiedArgumentSet = new SpecifiedArgumentSet(method.MethodInfo);
                if (!specifiedArgumentSet.Any())
                    return new[] { CreateSingleTestCommand(method) };

                return specifiedArgumentSet.Select(sa => CreateEachTestCommand(method, sa));
            }
            catch (Exception exception)
            {
                return new ITestCommand[] { new ExceptionCommand(method, exception) };
            }
        }

        private ITestCommand CreateSingleTestCommand(IMethodInfo method)
        {
            var arguments = new TestArgumentCollection(
                new Lazy<ITestFixture>(() => CreateTestFixture(method.MethodInfo)),
                method.MethodInfo.GetParameters());

            if (!arguments.HasParemeters)
                return base.EnumerateTestCommands(method).Single();

            return new TheoryCommand(method, arguments.ToArray());
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "auto data를 만들 때 발생되는 unhandled exception을 처리하기 위해서 이 경고 무시함.")]
        private ITestCommand CreateEachTestCommand(IMethodInfo method, object[] specifiedArguments)
        {
            try
            {
                var arguments = new TestArgumentCollection(
                    new Lazy<ITestFixture>(() => CreateTestFixture(method.MethodInfo)),
                    method.MethodInfo.GetParameters(),
                    specifiedArguments);

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

            public SpecifiedArgumentSet(MethodInfo method)
            {
                _method = method;
            }

            public IEnumerator<object[]> GetEnumerator()
            {
                return GetDataAttributes()
                    .SelectMany(da => da.GetData(_method, GetParameterTypes()))
                    .GetEnumerator();
            }

            private IEnumerable<DataAttribute> GetDataAttributes()
            {
                return ((IEnumerable<DataAttribute>)_method
                    .GetCustomAttributes(typeof(DataAttribute), false));
            }

            private Type[] GetParameterTypes()
            {
                return _method.GetParameters().Select(pi => pi.ParameterType).ToArray();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class TestArgumentCollection : IEnumerable<object>
        {
            private readonly Lazy<ITestFixture> _fixture;
            private readonly ParameterInfo[] _parameters;
            private readonly object[] _specifiedArguments;

            public TestArgumentCollection(
                Lazy<ITestFixture> fixture,
                ParameterInfo[] parameters,
                params object[] specifiedArguments)
            {
                _fixture = fixture;
                _parameters = parameters;
                _specifiedArguments = specifiedArguments;
            }

            public bool HasParemeters
            {
                get
                {
                    return _parameters.Length != 0;
                }
            }

            public IEnumerator<object> GetEnumerator()
            {
                return _specifiedArguments.Concat(GetAutoArguments()).GetEnumerator();
            }

            private IEnumerable<object> GetAutoArguments()
            {
                return _parameters
                    .Skip(_specifiedArguments.Length)
                    .Select(pi => _fixture.Value.Create(pi.ParameterType));
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}