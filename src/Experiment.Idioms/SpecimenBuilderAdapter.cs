using Ploeh.AutoFixture.Kernel;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Adapts <see cref="ITestFixture"/> to <see cref="ISpecimenBuilder"/>.
    /// </summary>
    public class SpecimenBuilderAdapter : ISpecimenBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpecimenBuilderAdapter"/> class.
        /// </summary>
        /// <param name="fixture">The fixture.</param>
        public SpecimenBuilderAdapter(ITestFixture fixture)
        {
        }

        /// <summary>
        /// Creates a new specimen based on a request.
        /// </summary>
        /// <param name="request">The request that describes what to create.</param>
        /// <param name="context">A context that can be used to create other specimens.</param>
        /// <returns>
        /// The requested specimen if possible; otherwise a <see cref="NoSpecimen" /> instance.
        /// </returns>
        public object Create(object request, ISpecimenContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}