using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents a collection of <see cref="IdiomaticTestCase"/>
    /// </summary>
    public class IdiomaticTestCaseCollection : IEnumerable<ITestCase>
    {
        private readonly IEnumerable<IReflectionElement> _elements;
        private readonly Func<ITestFixture, IReflectionVisitor<object>> _assertionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdiomaticTestCaseCollection"/> class.
        /// </summary>
        /// <param name="elements">The reflection elements to be verified.</param>
        /// <param name="assertionFactory">The assertion factory.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "The nested generic signature is desirable.")]
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

            _elements = elements;
            _assertionFactory = assertionFactory;
        }

        /// <summary>
        /// Gets the reflection elements.
        /// </summary>
        public IEnumerable<IReflectionElement> ReflectionElements
        {
            get
            {
                return _elements;
            }
        }

        /// <summary>
        /// Gets the assertion factory.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "The nested generic signature is desirable.")]
        public Func<ITestFixture, IReflectionVisitor<object>> AssertionFactory
        {
            get
            {
                return _assertionFactory;
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
            return ReflectionElements
                .Select(e => new IdiomaticTestCase(e, AssertionFactory))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}