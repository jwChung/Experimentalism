﻿using System;
using System.Reflection;
using Ploeh.AutoFixture.Idioms;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Encapsulates a unit test that verifies that a method or constructor has
    /// appropriate Null Guard Clauses in place.
    /// </summary>
    public class NullGuardClauseAssertion : IIdiomaticMemberAssertion
    {
        private readonly ITestFixture _testFixture;
        private readonly IIdiomaticAssertion _assertion;

        /// <summary>
        /// Initializes a new instance of the <see cref="NullGuardClauseAssertion"/> class.
        /// </summary>
        /// <param name="testFixture">
        /// A test fixture to create auto-data.
        /// </param>
        public NullGuardClauseAssertion(ITestFixture testFixture)
        {
            if (testFixture == null)
                throw new ArgumentNullException("testFixture");

            _testFixture = testFixture;
            _assertion = new GuardClauseAssertion(new SpecimenBuilder(testFixture));
        }

        /// <summary>
        /// Gets a value indicating the test fixture.
        /// </summary>
        public ITestFixture TestFixture
        {
            get
            {
                return _testFixture;
            }
        }

        /// <summary>
        /// Verifies that a member has appropriate Null Guard Clause in place.
        /// </summary>
        /// <param name="member">
        /// The member.
        /// </param>
        public void Verify(MemberInfo member)
        {
            _assertion.Verify(member);
        }
    }
}