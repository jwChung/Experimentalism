using System;
using System.Collections;
using System.Collections.Generic;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents a collection of <see cref="IdiomaticTestCase"/>
    /// </summary>
    public class IdiomaticTestCaseCollection : IEnumerable<ITestCase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdiomaticTestCaseCollection"/> class.
        /// </summary>
        /// <param name="elements">The reflection elements to be verified.</param>
        /// <param name="assertionFactory">The assertion factory.</param>
        public IdiomaticTestCaseCollection(
            IEnumerable<IReflectionElement> elements,
            Func<ITestFixture, IReflectionVisitor<object>> assertionFactory)
        {
            if (elements == null)
            {
                throw new ArgumentNullException("elements");
            }

            if (assertionFactory == null)
            {
                throw new ArgumentNullException("assertionFactory");
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{T}" /> that can be used to iterate through the collection.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerator<ITestCase> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}