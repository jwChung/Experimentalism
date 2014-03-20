using System;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace Jwc.Experiment
{
    public class TestFixture : ITestFixture
    {
        private readonly ISpecimenContext _specimenContext;

        public TestFixture() : this(
            new SpecimenContext(new Fixture()))
        {
        }

        public TestFixture(ISpecimenContext specimenContext)
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