namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using global::Xunit;
    using global::Xunit.Extensions;
    using global::Xunit.Sdk;

    /// <summary>
    /// 이 attribute는 method위에 선언되어 해당 method가 test라는 것을 지칭하게 되며,
    /// non-parameterized test 뿐 아니라 parameterized test에도 사용될 수 있다.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "This attribue can be inherited by custom attribute.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "The warnigns are from Korean.")]
    public class TestAttribute : FactAttribute
    {
        /// <summary>
        /// Enumerates the test commands represented by this test method. Derived classes should
        /// override this method to return instances of <see cref="ITestCommand" />, one per
        /// execution of a test method.
        /// </summary>
        /// <param name="method">
        /// The test method
        /// </param>
        /// <returns>
        /// The test commands which will execute the test runs for the given method.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is suppressed to catch unhandled exception thrown by test assembly configuration.")]
        protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
        {
            if (method == null)
                throw new ArgumentNullException("method");

            try
            {
                TestAssemblyConfigurationAttribute.Configure(
                    method.MethodInfo.ReflectedType.Assembly);
            }
            catch (Exception exception)
            {
                return new[] { new ExceptionCommand(method, exception) };
            }

            return new TestCommandCollection(method, this.GetTestCommands(method));
        }

        /// <summary>
        /// Creates an instance of <see cref="ITestFixture" />.
        /// </summary>
        /// <param name="testMethod">
        /// The test method
        /// </param>
        /// <returns>
        /// The created fixture.
        /// </returns>
        protected virtual ITestFixture CreateTestFixture(MethodInfo testMethod)
        {
            return DefaultFixtureFactory.Current.Create(testMethod);
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is suppressed to catch unhandled exception thrown when creating test commands.")]
        private IEnumerable<ITestCommand> GetTestCommands(IMethodInfo method)
        {
            try
            {
                var specifiedArgumentSet = new SpecifiedArgumentSet(method.MethodInfo);
                if (!specifiedArgumentSet.Any())
                    return new[] { this.CreateSingleTestCommand(method) };

                return specifiedArgumentSet.Select(sa => this.CreateEachTestCommand(method, sa));
            }
            catch (Exception exception)
            {
                return new ITestCommand[] { new ExceptionCommand(method, exception) };
            }
        }

        private ITestCommand CreateSingleTestCommand(IMethodInfo method)
        {
            var arguments = new TestArgumentCollection(
                new Lazy<ITestFixture>(() => this.CreateTestFixture(method.MethodInfo)),
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
                    new Lazy<ITestFixture>(() => this.CreateTestFixture(method.MethodInfo)),
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
            private readonly MethodInfo method;

            public SpecifiedArgumentSet(MethodInfo method)
            {
                this.method = method;
            }

            public IEnumerator<object[]> GetEnumerator()
            {
                return this.GetDataAttributes()
                    .SelectMany(da => da.GetData(this.method, this.GetParameterTypes()))
                    .GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            private IEnumerable<DataAttribute> GetDataAttributes()
            {
                return this.method.GetCustomAttributes(typeof(DataAttribute), false).Cast<DataAttribute>();
            }

            private Type[] GetParameterTypes()
            {
                return this.method.GetParameters().Select(pi => pi.ParameterType).ToArray();
            }
        }

        private class TestArgumentCollection : IEnumerable<object>
        {
            private readonly Lazy<ITestFixture> fixture;
            private readonly ParameterInfo[] parameters;
            private readonly object[] specifiedArguments;

            public TestArgumentCollection(
                Lazy<ITestFixture> fixture,
                ParameterInfo[] parameters,
                params object[] specifiedArguments)
            {
                this.fixture = fixture;
                this.parameters = parameters;
                this.specifiedArguments = specifiedArguments;
            }

            public bool HasParemeters
            {
                get
                {
                    return this.parameters.Length != 0;
                }
            }

            public IEnumerator<object> GetEnumerator()
            {
                return this.specifiedArguments.Concat(this.GetAutoArguments()).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            private IEnumerable<object> GetAutoArguments()
            {
                return this.parameters
                    .Skip(this.specifiedArguments.Length)
                    .Select(pi => this.fixture.Value.Create(pi.ParameterType));
            }
        }
    }
}