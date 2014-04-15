using System;
using Ploeh.AutoFixture.Kernel;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Adapts <see cref="ITestFixture" /> to <see cref="ISpecimenBuilder" />.
    /// </summary>
    public class SpecimenBuilderAdapter : ISpecimenBuilder
    {
        private readonly ITestFixture _testFixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecimenBuilderAdapter"/> class.
        /// </summary>
        /// <param name="testFixture">The test fixture.</param>
        public SpecimenBuilderAdapter(ITestFixture testFixture)
        {
            if (testFixture == null)
            {
                throw new ArgumentNullException("testFixture");
            }

            _testFixture = testFixture;
        }

        /// <summary>
        /// Gets the test fixture.
        /// </summary>
        public ITestFixture TestFixture
        {
            get
            {
                return _testFixture;
            }
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
            return TestFixture.Create(request);
        }
    }
}