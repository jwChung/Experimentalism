using System.Collections;
using System.Collections.Generic;
using Ploeh.Albedo.Refraction;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents all kinds of <see cref="ReflectionElementRefraction{T}"/>
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "The main responsibility of this class isn't to be a 'collection' (which, by the way, it isn't - it's just an Iterator).")]
    public class AllRefractions : IEnumerable<IReflectionElementRefraction<object>>
    {
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{T}" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<IReflectionElementRefraction<object>> GetEnumerator()
        {
            yield return new AssemblyElementRefraction<object>();
            yield return new ConstructorInfoElementRefraction<object>();
            yield return new EventInfoElementRefraction<object>();
            yield return new FieldInfoElementRefraction<object>();
            yield return new LocalVariableInfoElementRefraction<object>();
            yield return new MethodInfoElementRefraction<object>();
            yield return new ParameterInfoElementRefraction<object>();
            yield return new PropertyInfoElementRefraction<object>();
            yield return new TypeElementRefraction<object>();
            yield return new ReflectionElementRefraction<object>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}