namespace Jwc.Experiment
{
    using System.Collections.Generic;
    using Ploeh.Albedo;
    using Ploeh.AutoFixture;

    /// <summary>
    /// Represent comparer to determine that a property value equals to a parameter value.
    /// </summary>
    public class PropertyToParameterComparer : IEqualityComparer<IReflectionElement>
    {
        private readonly IFixture fixture;
        private readonly ITestFixture testFixture;
        private readonly IEqualityComparer<IReflectionElement> comparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyToParameterComparer" /> class.
        /// </summary>
        /// <param name="testFixture">
        /// The test fixture.
        /// </param>
        public PropertyToParameterComparer(ITestFixture testFixture)
        {
            this.testFixture = testFixture;
            this.comparer = new ParameterToPropertyComparer(testFixture);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyToParameterComparer" /> class.
        /// </summary>
        /// <param name="fixture">
        /// The fixture.
        /// </param>
        public PropertyToParameterComparer(IFixture fixture)
        {
            this.fixture = fixture;
            this.comparer = new ParameterToPropertyComparer(fixture);
        }

        /// <summary>
        /// Gets a value indicating the test fixture.
        /// </summary>
        public ITestFixture TestFixture
        {
            get
            {
                return this.testFixture;
            }
        }

        /// <summary>
        /// Gets a value indicating the fixture.
        /// </summary>
        public IFixture Fixture
        {
            get { return this.fixture; }
        }

        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">
        /// The first object of type <paramref name="x" /> to compare.
        /// </param>
        /// <param name="y">
        /// The second object of type <paramref name="y" /> to compare.
        /// </param>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        public bool Equals(IReflectionElement x, IReflectionElement y)
        {
            return this.comparer.Equals(y, x);
        }

        /// <summary>
        /// Always returns zero(constant) to use the <see cref="Equals" /> method to determine
        /// whether the specified objects are equal.
        /// </summary>
        /// <param name="obj">
        /// The object to get the hash code.
        /// </param>
        /// <returns>
        /// The zero
        /// </returns>
        public int GetHashCode(IReflectionElement obj)
        {
            return 0;
        }
    }
}