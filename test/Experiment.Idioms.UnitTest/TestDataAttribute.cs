namespace Jwc.Experiment
{
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;
    using Ploeh.AutoFixture.Xunit;

    public class TestDataAttribute : AutoDataAttribute
    {
        public TestDataAttribute() : base(
            new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}