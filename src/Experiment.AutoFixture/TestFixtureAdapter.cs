using System;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace Jwc.Experiment
{
    /// <summary>
    /// <see cref="ISpecimenContext"/>를 <see cref="ITestFixture"/>에 맞춘다.
    /// auto data기능을 AutoFixture library로 부터 채용하게 된다.
    /// </summary>
    public class TestFixtureAdapter : ITestFixture
    {
        private readonly ISpecimenContext _specimenContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestFixtureAdapter"/> class.
        /// </summary>
        public TestFixtureAdapter() : this(
            new SpecimenContext(new Fixture()))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestFixtureAdapter"/> class.
        /// </summary>
        /// <param name="specimenContext">The specimen context.</param>
        /// <exception cref="System.ArgumentNullException">specimenContext</exception>
        public TestFixtureAdapter(ISpecimenContext specimenContext)
        {
            if (specimenContext == null)
            {
                throw new ArgumentNullException("specimenContext");
            }

            _specimenContext = specimenContext;
        }

        /// <summary>
        /// Gets the specimen context.
        /// </summary>
        /// <value>
        /// The specimen context.
        /// </value>
        public ISpecimenContext SpecimenContext
        {
            get
            {
                return _specimenContext;
            }
        }

        /// <summary>
        /// request를 통해 테스트에 필요한 specimen를 만듦.
        /// </summary>
        /// <param name="request">specimen을 만들기 위해 필요한 정보를 제공.
        /// 일반적으로 
        /// <see cref="Type" />을 많이 활용.</param>
        /// <returns>
        /// 만들어진 specimen 객체.
        /// </returns>
        public object Create(object request)
        {
            return SpecimenContext.Resolve(request);
        }
    }
}