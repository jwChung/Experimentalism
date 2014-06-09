using System;
using System.Reflection;

namespace Jwc.Experiment
{
    public class DelegatingAssembly : Assembly
    {
        public Func<bool, object[]> OnGetCustomAttributes { get; set; }

        public Func<Type[]> OnGetTypes { get; set; }

        public override string FullName
        {
            get
            {
                return "DelegatingAssembly";
            }
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            return OnGetCustomAttributes == null ? new object[0] : OnGetCustomAttributes(inherit);
        }

        public override Type[] GetTypes()
        {
            return OnGetTypes == null ? new Type[0] : OnGetTypes();
        }
    }
}