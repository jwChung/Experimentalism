using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Ploeh.Albedo.Refraction;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// /// <summary>
    /// Represents all kinds of <see cref="ReflectionElementRefraction{T}"/>
    /// related with <see cref="MemberInfo"/>
    /// </summary>
    /// </summary>
    public class AllMemberRefractions : IEnumerable<IReflectionElementRefraction<object>>
    {
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{T}" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<IReflectionElementRefraction<object>> GetEnumerator()
        {
            yield return new FieldInfoElementRefraction<object>();
            yield return new ConstructorInfoElementRefraction<object>();
            yield return new PropertyInfoElementRefraction<object>();
            yield return new EventInfoElementRefraction<object>();
            yield return new MethodInfoElementRefraction<object>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}