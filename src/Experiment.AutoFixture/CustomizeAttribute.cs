// Original source code is from https://github.com/AutoFixture/AutoFixture.

using System;
using System.Reflection;
using Ploeh.AutoFixture;

namespace Jwc.Experiment.AutoFixture
{
    /// <summary>
    /// Base class for customizing a test fixture from test parameters.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
    public abstract class CustomizeAttribute : Attribute
    {
        /// <summary>
        /// Gets a customization for a parameter.
        /// </summary>
        /// <param name="parameter">The parameter for which the customization is requested.</param>
        /// <returns></returns>
        public abstract ICustomization GetCustomization(ParameterInfo parameter);
    }
}