﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace Jwc.Experiment
{
    /// <summary>
    /// Attribute to customize default fixture with a <see cref="ITestFixtureFactory"/> type.
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "This attribute can be inherited to supply custom ITestFixtureFactory.")]
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class DefaultFixtureCustomizationAttribute : AssemblyCustomizationAttribute
    {
        private readonly Type _factoryType;
        private readonly ITestFixtureFactory _factory;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="DefaultFixtureCustomizationAttribute" /> class.
        /// </summary>
        /// <param name="factoryType">
        ///     Type of the test fixture factory.
        /// </param>
        public DefaultFixtureCustomizationAttribute(Type factoryType)
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

            _factoryType = factoryType;
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="DefaultFixtureCustomizationAttribute" /> class.
        /// </summary>
        /// <param name="factory">
        ///     The test fixture factory.
        /// </param>
        protected DefaultFixtureCustomizationAttribute(ITestFixtureFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            _factory = factory;
        }

        /// <summary>
        ///     Gets a value indicating the type of the test fixture factory.
        /// </summary>
        public Type FactoryType
        {
            get
            {
                return _factoryType ?? _factory.GetType();
            }
        }

        /// <summary>
        ///     Gets a value indicating the test fixture factory.
        /// </summary>
        public ITestFixtureFactory Factory
        {
            get
            {
                return _factory ?? (ITestFixtureFactory)Activator.CreateInstance(FactoryType);
            }
        }

        /// <summary>
        ///     Sets up the default factory of test fixture in a test assembly.
        /// </summary>
        /// <param name="testAssembly">
        ///     The test assembly.
        /// </param>
        protected override void Setup(Assembly testAssembly)
        {
            DefaultFixtureFactory.SetCurrent(Factory);
        }
    }
}