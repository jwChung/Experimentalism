using System;
using System.Reflection;
using Ploeh.AutoFixture.Idioms;

namespace Jwc.Experiment.Idioms
{
    public class DelegatingIdiomaticAssertion : IdiomaticAssertion
    {
        public Action<Assembly> OnVerifyAssembly
        {
            get;
            set;
        }

        public override void Verify(Assembly assembly)
        {
            OnVerifyAssembly(assembly);
        }
    }
}