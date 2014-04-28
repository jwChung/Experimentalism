﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jwc.Experiment;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit;

namespace NuGet.Jwc.Experiment
{
    /// <summary>
    /// A test attribute declared on a test method to indicate a test case.
    /// This attribute can be used for non-parameterized test as well as
    /// parameterized test, and supports to generate auto data using
    /// the AutoFixture library.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "This attribute is part of an inheritance hierarchy and can be inherited in order to extend its behavior.")]
    [AttributeUsage(AttributeTargets.Method)]
    public class TheoremAttribute : TheoremBaseAttribute
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
        protected override ITestFixture CreateTestFixture(MethodInfo testMethod)
        {
            if (testMethod == null)
            {
                throw new ArgumentNullException("testMethod");
            }

            return new AutoFixtureAdapter(
                CustomizeFixture(
                    CreateFixture(testMethod),
                    testMethod.GetParameters()));
        }

        /// <summary>
        /// Creates the fixture.
        /// </summary>
        /// <returns>The new fixture instance.</returns>
        protected virtual IFixture CreateFixture(MethodInfo testMethod)
        {
            return new Fixture();
        }

        private static IFixture CustomizeFixture(
            IFixture fixture, IEnumerable<ParameterInfo> parameters)
        {
            return parameters.SelectMany(SelectCustomizations)
                .Aggregate(fixture, (f, c) => f.Customize(c));
        }

        private static IEnumerable<ICustomization> SelectCustomizations(ParameterInfo parameter)
        {
            return parameter.GetCustomAttributes(typeof(CustomizeAttribute), false)
                .Cast<CustomizeAttribute>()
                .Select(ca => ca.GetCustomization(parameter));
        }
    }
}