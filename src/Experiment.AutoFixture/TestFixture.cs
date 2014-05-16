using System;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace Jwc.Experiment.AutoFixture
{
    /// <summary>
    /// <see cref="IFixture"/>를 <see cref="ITestFixture"/> 인터페이스에 맞춘다.
    /// auto data기능을 AutoFixture library로부터 채용하게 된다.
    /// </summary>
    public class TestFixture : ITestFixture
    {
        private readonly IFixture _fixture;
        private readonly ISpecimenContext _specimenContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestFixture"/> class.
        /// </summary>
        /// <param name="fixture">The fixture.</param>
        public TestFixture(IFixture fixture)
        {
            if (fixture == null)
            {
                throw new ArgumentNullException("fixture");
            }

            _fixture = fixture;
            _fixture.Inject<ITestFixture>(this);
            _fixture.Inject(this);
            _specimenContext = new SpecimenContext(fixture);
        }

        /// <summary>
        /// Gets the fixture.
        /// </summary>
        public IFixture Fixture
        {
            get
            {
                return _fixture;
            }
        }

        /// <summary>
        /// request를 통해 테스트에 필요한 specimen를 만듦.
        /// </summary>
        /// <param name="request">specimen을 만들기 위해 필요한 정보를 제공.
        /// 일반적으로 <see cref="Type" />을 많이 활용.</param>
        /// <returns>
        /// 만들어진 specimen 객체.
        /// </returns>
        public object Create(object request)
        {
            return _specimenContext.Resolve(request);
        }
    }
}