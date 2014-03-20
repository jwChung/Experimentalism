namespace Jwc.Experiment
{
    public class AutoDataTheoremAttribute : TheoremAttribute
    {
        public AutoDataTheoremAttribute() : base(() => new TestFixtureAdapter())
        {
        }
    }
}