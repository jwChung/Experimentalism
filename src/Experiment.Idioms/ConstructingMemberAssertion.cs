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
        private readonly IEqualityComparer<IReflectionElement> _parameterToMemberComparer;
        private readonly IEqualityComparer<IReflectionElement> _memberToParameterComparer;

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
            _parameterToMemberComparer = parameterToMemberComparer;
            _memberToParameterComparer = memberToParameterComparer;
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

        /// <summary>
        /// Gets a value to compare a parameter with a member(field or propety).
        /// </summary>
        public IEqualityComparer<IReflectionElement> ParameterToMemberComparer
        {
            get
            {
                return _parameterToMemberComparer;
            }
        }

        /// <summary>
        /// Gets a value to compare a member(field or propety) with a parameter.
        /// </summary>
        public IEqualityComparer<IReflectionElement> MemberToParameterComparer
        {
            get
            {
                return _memberToParameterComparer;
            }
        }
    }
}