using System;
using System.Reflection;

namespace Jwc.Experiment
{
    public class DelegatingAssembly : Assembly
    {
        public DelegatingAssembly()
        {
            OnGetCustomAttributes = i => new object[0];
            OnGetTypes = () => new Type[0];
        }

        public Func<bool, object[]> OnGetCustomAttributes { get; set; }

        public Func<Type[]> OnGetTypes { get; set; }

        public override string FullName
        {
            get { return "DelegatingAssembly"; }
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            return OnGetCustomAttributes(inherit);
        }

        public override Type[] GetTypes()
        {
            return OnGetTypes();
        }
    }
}