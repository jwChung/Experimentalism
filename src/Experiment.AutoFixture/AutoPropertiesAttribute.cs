using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Ploeh.AutoFixture;

namespace Jwc.Experiment.AutoFixture
{
    /// <summary>
    /// An attribute that can be applied to parameters to indicate that the parameter value should
    /// have properties auto populated when the <see cref="IFixture" /> creates an instance of that
    /// type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class AutoPropertiesAttribute : CustomizeAttribute
    {
        /// <summary>
        /// Gets a customization that enables auto population of properties for the type of the
        /// parameter.
        /// </summary>
        /// <param name="parameter">
        /// The parameter for which the customization is requested.
        /// </param>
        /// <returns>
        /// A customization that enables auto population of the <see cref="Type" /> of the
        /// parameter.
        /// </returns>
        public override ICustomization GetCustomization(ParameterInfo parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException("parameter");

            return new AutoPropertiesCustomization(parameter.ParameterType);
        }

        private class AutoPropertiesCustomization : ICustomization
        {
            private readonly Type targetType;

            public AutoPropertiesCustomization(Type targetType)
            {
                this.targetType = targetType;
            }

            public void Customize(IFixture fixture)
            {
                this.GetType().GetMethod("Customize", BindingFlags.NonPublic | BindingFlags.Static)
                    .MakeGenericMethod(this.targetType)
                    .Invoke(null, new object[] { fixture });
            }

            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "This method is called through reflection.")]
            private static void Customize<T>(IFixture fixture)
            {
                fixture.Customize<T>(c => c.WithAutoProperties());
            }
        }
    }
}