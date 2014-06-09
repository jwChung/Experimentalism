﻿using System;
using System.Reflection;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Kernel;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    ///     Encapsulates a unit test that verifies that a method or constructor has appropriate
    ///     Null Guard Clauses in place.
    /// </summary>
    public class NullGuardClauseAssertion : IdiomaticAssertion
    {
        private readonly ITestFixture _testFixture;
        private readonly IIdiomaticAssertion _assertion;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NullGuardClauseAssertion" /> class.
        /// </summary>
        /// <param name="testFixture">
        ///     A test fixture to create auto-data.
        /// </param>
        public NullGuardClauseAssertion(ITestFixture testFixture)
        {
            if (testFixture == null)
                throw new ArgumentNullException("testFixture");

            _testFixture = testFixture;
            _assertion = new GuardClauseAssertion(new SpecimenBuilder(testFixture));
        }

        /// <summary>
        ///     Gets a value indicating the test fixture.
        /// </summary>
        public ITestFixture TestFixture
        {
            get
            {
                return _testFixture;
            }
        }

        /// <summary>
        ///     Verifies that a constructor has appropriate Null Guard Clause in place. specified
        ///     constructor.
        /// </summary>
        /// <param name="constructor">
        ///     The constructor.
        /// </param>
        public override void Verify(ConstructorInfo constructor)
        {
            if (constructor == null)
                throw new ArgumentNullException("constructor");

            _assertion.Verify(constructor);
        }

        /// <summary>
        ///     Verifies that a property has appropriate Null Guard Clause in place.
        /// </summary>
        /// <param name="property">
        ///     The property.
        /// </param>
        public override void Verify(PropertyInfo property)
        {
            if (property == null)
                throw new ArgumentNullException("property");

            if (property.IsAbstract())
                return;

            _assertion.Verify(property);
        }

        /// <summary>
        ///     Verifies that a method has appropriate Null Guard Clause in place.
        /// </summary>
        /// <param name="method">
        ///     The method.
        /// </param>
        public override void Verify(MethodInfo method)
        {
            if (method == null)
                throw new ArgumentNullException("method");

            if (method.IsAbstract)
                return;

            _assertion.Verify(method);
        }

        private class SpecimenBuilder : ISpecimenBuilder
        {
            private readonly ITestFixture _testFixture;

            public SpecimenBuilder(ITestFixture testFixture)
            {
                _testFixture = testFixture;
            }

            public object Create(object request, ISpecimenContext context)
            {
                return _testFixture.Create(request);
            }
        }
    }
}