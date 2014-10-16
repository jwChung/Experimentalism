namespace Jwc.Experiment.AutoFixture
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;
    using Ploeh.AutoFixture.Kernel;

    /// <summary>
    /// <see cref="IFixture" />를 <see cref="ITestFixture" /> 인터페이스에 맞춘다. auto data기능을
    /// AutoFixture library로부터 채용하게 된다.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "The warnings are from Korean.")]
    public class TestFixture : ITestFixture
    {
        private readonly IFixture fixture;
        private readonly ISpecimenContext specimenContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestFixture"/> class.
        /// </summary>
        [Obsolete("Do not use this constructor but instead, use TestFixture(IFixture) to explicitly customize the fixture.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public TestFixture() : this(
            new Fixture { OmitAutoProperties = true }
                .Customize(new AutoMoqCustomization()))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestFixture" /> class.
        /// </summary>
        /// <param name="fixture">
        /// The fixture.
        /// </param>
        public TestFixture(IFixture fixture)
        {
            if (fixture == null)
            {
                throw new ArgumentNullException("fixture");
            }

            this.fixture = fixture;
            this.specimenContext = new SpecimenContext(fixture);
            this.RegisterTestFixture();
        }

        /// <summary>
        /// Gets the fixture.
        /// </summary>
        public IFixture Fixture
        {
            get
            {
                return this.fixture;
            }
        }

        /// <summary>
        /// request를 통해 테스트에 필요한 specimen를 만듦.
        /// </summary>
        /// <param name="request">
        /// specimen을 만들기 위해 필요한 정보를 제공. 일반적으로 <see cref="Type" />을 많이 활용.
        /// </param>
        /// <returns>
        /// 만들어진 specimen 객체.
        /// </returns>
        public object Create(object request)
        {
            return this.specimenContext.Resolve(request);
        }

        private void RegisterTestFixture()
        {
            this.Fixture.Inject<ITestFixture>(this);
            this.Fixture.Inject(this);
        }
    }
}