using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents composite collection of test cases.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "The main responsibility of this class isn't to be a 'collection' (which, by the way, it isn't - it's just an Iterator).")]
    public class CompositeEnumerables : IEnumerable<ITestCase>
    {
        private readonly IEnumerable<ITestCase>[] _testCaseSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeEnumerables"/> class.
        /// </summary>
        /// <param name="testCaseSet">The set of test cases.</param>
        public CompositeEnumerables(params IEnumerable<ITestCase>[] testCaseSet)
        {
            if (testCaseSet == null)
            {
                throw new ArgumentNullException("testCaseSet");
            }

            _testCaseSet = testCaseSet;
        }

        /// <summary>
        /// Gets the test-case set.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "The nested generic signature is desirable.")]
        public IEnumerable<IEnumerable<ITestCase>> TestCaseSet
        {
            get
            {
                return _testCaseSet;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{T}" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<ITestCase> GetEnumerator()
        {
            return TestCaseSet.SelectMany(cases => cases).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}