using System.Collections.Generic;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represent comaprer to determine that a parameter value equals to
    /// a field value.
    /// </summary>
    public class ParameterToFieldComparer : IEqualityComparer<IReflectionElement>
    {
        private readonly ITestFixture _testFixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterToFieldComparer"/> class.
        /// </summary>
        /// <param name="testFixture">
        /// The test fixture to create an anonymous specimen.
        /// </param>
        public ParameterToFieldComparer(ITestFixture testFixture)
        {
            _testFixture = testFixture;
        }

        /// <summary>
        /// Gets a vlaue indicating the test fixture.
        /// </summary>
        public ITestFixture TestFixture
        {
            get
            {
                return _testFixture;
            }
        }

        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        /// <param name="x">
        /// The first object of type <paramref name="x" /> to compare.
        /// </param>
        /// <param name="y">
        /// The second object of type <paramref name="y" /> to compare.
        /// </param>
        public bool Equals(IReflectionElement x, IReflectionElement y)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <returns>
        /// A hash code for the specified object.
        /// </returns>
        /// <param name="obj">
        /// The <see cref="object" /> for which a hash code is to be returned.
        /// </param>
        public int GetHashCode(IReflectionElement obj)
        {
            throw new System.NotImplementedException();
        }
    }
}