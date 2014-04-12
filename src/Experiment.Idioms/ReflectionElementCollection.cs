using System;
using System.Collections;
using System.Collections.Generic;
using Ploeh.Albedo;
using Ploeh.Albedo.Refraction;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents collection of reflection elements.
    /// </summary>
    public class ReflectionElementCollection : IEnumerable<IReflectionElement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectionElementCollection"/> class.
        /// </summary>
        /// <param name="sources">The sources to be refracted.</param>
        /// <param name="refractions">The reflection element refractions.</param>
        public ReflectionElementCollection(
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
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{T}" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<IReflectionElement> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}