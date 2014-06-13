using System;
using System.Reflection;

namespace Jwc.Experiment
{
    /// <summary>
    ///     Represents a test fixture created by a function delegate.
    /// </summary>
    public class FuncTestFixtureFactory : ITestFixtureFactory
    {
        private readonly Func<MethodInfo, ITestFixtureFactory> _func;

        /// <summary>
        ///     Initializes a new instance of the <see cref="FuncTestFixtureFactory" /> class.
        /// </summary>
        /// <param name="func">
        ///     The function to create the test fixture.
        /// </param>
        public FuncTestFixtureFactory(Func<MethodInfo, ITestFixtureFactory> func)
        {
            _func = func;
        }

        /// <summary>
        /// Gets a value indicating the function supplied by constructor.
        /// </summary>
        public Func<MethodInfo, ITestFixtureFactory> Func
        {
            get { return _func; }
        }

        /// <summary>
        ///     Creates a test fixture.
        /// </summary>
        /// <param name="testMethod">
        ///     The test method in which the test fixture will be used.
        /// </param>
        /// <returns>
        ///     The test fixture.
        /// </returns>
        public ITestFixture Create(MethodInfo testMethod)
        {
            throw new NotImplementedException();
        }
    }
}