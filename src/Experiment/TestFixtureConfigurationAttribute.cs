using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace Jwc.Experiment
{
    /// <summary>
    /// Attribute to configure default fixture factory.
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "This attribute can be inherited to supply custom ITestFixtureFactory.")]
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class TestFixtureConfigurationAttribute : TestAssemblyConfigurationAttribute
    {
        private readonly Type factoryType;
        private readonly ITestFixtureFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestFixtureConfigurationAttribute" />
        /// class.
        /// </summary>
        /// <param name="factoryType">
        /// Type of the test fixture factory.
        /// </param>
        public TestFixtureConfigurationAttribute(Type factoryType)
        {
            if (factoryType == null)
                throw new ArgumentNullException("factoryType");

            if (!typeof(ITestFixtureFactory).IsAssignableFrom(factoryType) || factoryType.IsAbstract)
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "The factory type '{0}' should be concrete class of ITestFixtureFactory.",
                        factoryType),
                    "factoryType");

            this.factoryType = factoryType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestFixtureConfigurationAttribute" />
        /// class.
        /// </summary>
        /// <param name="factory">
        /// The test fixture factory.
        /// </param>
        protected TestFixtureConfigurationAttribute(ITestFixtureFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            this.factory = factory;
        }

        /// <summary>
        /// Gets a value indicating the type of the test fixture factory.
        /// </summary>
        public Type FactoryType
        {
            get
            {
                return this.factoryType ?? this.factory.GetType();
            }
        }

        /// <summary>
        /// Gets a value indicating the test fixture factory.
        /// </summary>
        public ITestFixtureFactory Factory
        {
            get
            {
                return this.factory ?? (ITestFixtureFactory)Activator.CreateInstance(this.FactoryType);
            }
        }

        /// <summary>
        /// Sets up the default factory of test fixture in a test assembly.
        /// </summary>
        /// <param name="testAssembly">
        /// The test assembly.
        /// </param>
        protected override void Setup(Assembly testAssembly)
        {
            DefaultFixtureFactory.SetCurrent(this.Factory);
        }
    }
}