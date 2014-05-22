using System;
using System.Reflection;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Kernel;

namespace Jwc.Experiment.Idioms.Assertions
{
    /// <summary>
    /// Encapsulates a unit test that verifies that a method or constructor has
    /// appropriate Null Guard Clauses in place.
    /// </summary>
    public class NullGuardClauseAssertion :
        IIdiomaticAssemblyAssertion, IIdiomaticTypeAssertion, IIdiomaticMemberAssertion
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
        /// Verifies that all types of an assembly has appropriate Null Guard
        /// Clause in place.
        /// </summary>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        public void Verify(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            foreach (var type in assembly.GetExportedTypes())
                Verify(type);
        }

        /// <summary>
        /// Verifies that a type has appropriate Null Guard Clause in place.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        public virtual void Verify(Type type)
        {
            foreach (var member in type.GetIdiomaticMembers())
                Verify(member);
        }

        /// <summary>
        /// Verifies that a member has appropriate Null Guard Clause in place.
        /// </summary>
        /// <param name="member">
        /// The member.
        /// </param>
        public void Verify(MemberInfo member)
        {
            var method = member as MethodInfo;
            if (method != null && method.IsAbstract)
                return;

            var property = member as PropertyInfo;
            if (property != null && IsAbstract(property))
                return;

            _assertion.Verify(member);
        }

        private static bool IsAbstract(PropertyInfo property)
        {
            var getMethod = property.GetGetMethod(true);
            if (getMethod != null)
                return getMethod.IsAbstract;

            return property.GetSetMethod(true).IsAbstract;
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