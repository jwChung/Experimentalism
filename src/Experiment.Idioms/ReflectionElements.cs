using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ploeh.Albedo;
using Ploeh.Albedo.Refraction;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents collection of reflection elements.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "The main responsibility of this class isn't to be a 'collection' (which, by the way, it isn't - it's just an Iterator).")]
    public class ReflectionElements : IEnumerable<IReflectionElement>
    {
        private readonly IEnumerable<object> _sources;
        private readonly IReflectionElementRefraction<object>[] _refractions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectionElements"/> class.
        /// </summary>
        /// <param name="sources">The sources to be refracted.</param>
        /// <param name="refractions">The reflection-element refractions.</param>
        public ReflectionElements(
            IEnumerable<object> sources, params IReflectionElementRefraction<object>[] refractions)
        {
            if (sources == null)
            {
                throw new ArgumentNullException("sources");
            }

            if (refractions == null)
            {
                throw new ArgumentNullException("refractions");
            }

            _sources = sources;
            _refractions = refractions;
        }

        /// <summary>
        /// Gets the sources.
        /// </summary>
        public IEnumerable<object> Sources
        {
            get
            {
                return _sources;
            }
        }

        /// <summary>
        /// Gets the reflection-element refractions.
        /// </summary>
        /// <value>
        /// The refractions.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "The nested generic signature is desirable.")]
        public IEnumerable<IReflectionElementRefraction<object>> Refractions
        {
            get
            {
                return _refractions;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{T}" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<IReflectionElement> GetEnumerator()
        {
            return new CompositeReflectionElementRefraction<object>(Refractions.ToArray())
                .Refract(Sources)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}