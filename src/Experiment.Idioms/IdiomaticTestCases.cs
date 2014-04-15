﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents a collection of <see cref="IdiomaticTestCase"/>
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "The main responsibility of this class isn't to be a 'collection' (which, by the way, it isn't - it's just an Iterator).")]
    public class IdiomaticTestCases : IEnumerable<ITestCase>
    {
        private readonly IEnumerable<IReflectionElement> _elements;
        private readonly IAssertionFactory _assertionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdiomaticTestCases"/> class.
        /// </summary>
        /// <param name="elements">The reflection elements to be verified.</param>
        /// <param name="assertionFactory">The assertion factory.</param>
        public IdiomaticTestCases(
            IEnumerable<IReflectionElement> elements,
            IAssertionFactory assertionFactory)
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
        public IAssertionFactory AssertionFactory
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