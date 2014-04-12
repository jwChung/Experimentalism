using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents composite collection of test cases.
    /// </summary>
    public class CompositeTestCaseCollection : IEnumerable<ITestCase>
    {
        private readonly IEnumerable<ITestCase>[] _testCaseSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeTestCaseCollection"/> class.
        /// </summary>
        /// <param name="testCaseSet">The set of test cases.</param>
        public CompositeTestCaseCollection(params IEnumerable<ITestCase>[] testCaseSet)
        {
            if (testCaseSet == null)
            {
                throw new ArgumentNullException("testCaseSet");
            }

            _testCaseSet = testCaseSet;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{T}" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<ITestCase> GetEnumerator()
        {
            return _testCaseSet.SelectMany(cases => cases).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}