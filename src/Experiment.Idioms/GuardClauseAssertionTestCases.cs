using System;
using System.Reflection;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents test cases for guard clause assertion.
    /// </summary>
    public class GuardClauseAssertionTestCases : IdiomaticTestCases
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GuardClauseAssertionTestCases"/> class.
        /// </summary>
        /// <param name="type">The target type whose members are verified.</param>
        /// <param name="exceptedMembers">The excepted members.</param>
        public GuardClauseAssertionTestCases(Type type, params MemberInfo[] exceptedMembers)
            : base(new IReflectionElement[0], new GuardClauseAssertionFactory())
        {
        }
    }
}