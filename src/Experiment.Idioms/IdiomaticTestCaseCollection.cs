using System.Collections;
using System.Collections.Generic;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents a collection of <see cref="IdiomaticTestCase"/>
    /// </summary>
    public class IdiomaticTestCaseCollection : IEnumerable<ITestCase>
    {
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