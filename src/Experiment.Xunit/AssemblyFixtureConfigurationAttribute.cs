using System;
using System.Reflection;

namespace Jwc.Experiment.Xunit
{
    /// <summary>
    ///     Attribute to set up or tear down all fixtures in a test assembly.
    /// </summary>
    public abstract class AssemblyFixtureConfigurationAttribute : Attribute
    {
        /// <summary>
        ///     Sets up all fixtures in a test assembly.
        /// </summary>
        /// <param name="testAssembly">
        ///     The test assembly.
        /// </param>
        public virtual void SetUp(Assembly testAssembly)
        {
        }

        /// <summary>
        ///     Tears down all fixtures in a test assembly.
        /// </summary>
        /// <param name="assembly">
        ///     The test assembly.
        /// </param>
        public virtual void TearDown(Assembly assembly)
        {
        }
    }
}