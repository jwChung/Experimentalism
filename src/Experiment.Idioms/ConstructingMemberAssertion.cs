using System.Collections.Generic;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Encapsulates a unit test that verifies that members (property or field)
    /// are correctly intialized by a constructor.
    /// </summary>
    public class ConstructingMemberAssertion : ReflectionVisitor<object>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructingMemberAssertion" /> class.
        /// </summary>
        /// <param name="parameterToMemberComparer">
        /// The comparer to determine if a given parameter value of
        /// a constructor equals to a member(property or field) value.
        /// </param>
        /// <param name="memberToParameterComparer">
        /// The comparer to determine if a given member(property or field) value
        /// equals to a parameter value of a constructor.
        /// </param>
        public ConstructingMemberAssertion(
            IEqualityComparer<IReflectionElement> parameterToMemberComparer,
            IEqualityComparer<IReflectionElement> memberToParameterComparer)
        {
        }

        /// <summary>
        /// Gets the observation or value produced by this instance.
        /// </summary>
        public override object Value
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
    }
}