using System;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace Jwc.Experiment
{
    public class TestFixtureAdapter : ITestFixture
    {
        private readonly ISpecimenContext _specimenContext;

        public TestFixtureAdapter() : this(
            new SpecimenContext(new Fixture()))
        {
        }

        public TestFixtureAdapter(ISpecimenContext specimenContext)
        {
            if (specimenContext == null)
            {
                throw new ArgumentNullException("specimenContext");
            }

            _specimenContext = specimenContext;
        }

        public ISpecimenContext SpecimenContext
        {
            get
            {
                return _specimenContext;
            }
        }

        public object Create(object request)
        {
            throw new System.NotImplementedException();
        }
    }
}