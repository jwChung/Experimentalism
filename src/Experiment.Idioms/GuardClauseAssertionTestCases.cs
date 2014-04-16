using System;
using System.Collections.Generic;
using System.Reflection;
using Ploeh.Albedo;
using Ploeh.Albedo.Refraction;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents test cases for guard clause assertion.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "The main responsibility of this class isn't to be a 'collection' (which, by the way, it isn't - it's just an Iterator).")]
    public class GuardClauseAssertionTestCases : IdiomaticTestCases
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GuardClauseAssertionTestCases"/> class.
        /// </summary>
        /// <param name="type">The target type whose members are verified.</param>
        /// <param name="excludedMembers">The excluded members.</param>
        public GuardClauseAssertionTestCases(Type type, params MemberInfo[] excludedMembers)
            : base(CreateReflectionElements(type, excludedMembers), new GuardClauseAssertionFactory())
        {
        }

        private static IEnumerable<IReflectionElement> CreateReflectionElements(
            Type type, IEnumerable<MemberInfo> excludedMembers)
        {
            return new ReflectionElements(
                new ExcludingReadOnlyProperties(
                    new ExcludingMembers(
                        new TypeMembers(type, Accessibilities.Public),
                        excludedMembers)),
                new ConstructorInfoElementRefraction<object>(),
                new PropertyInfoElementRefraction<object>(),
                new MethodInfoElementRefraction<object>());
        }
    }
}