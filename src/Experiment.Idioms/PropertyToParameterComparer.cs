namespace Jwc.Experiment
{
    using System.Collections.Generic;
    using Ploeh.Albedo;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    /// <summary>
    /// Represent comparer to determine that a property value equals to a parameter value.
    /// </summary>
    public class PropertyToParameterComparer : IEqualityComparer<IReflectionElement>
    {
        private readonly ISpecimenBuilder builder;
        private readonly IEqualityComparer<IReflectionElement> comparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyToParameterComparer" /> class.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        public PropertyToParameterComparer(ISpecimenBuilder builder)
        {
            this.builder = builder;
            this.comparer = new ParameterToPropertyComparer(builder);
        }

        /// <summary>
        /// Gets a value indicating the builder.
        /// </summary>
        public ISpecimenBuilder Builder
        {
            get { return this.builder; }
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