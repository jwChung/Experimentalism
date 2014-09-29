namespace Jwc.Experiment
{
    using System;
    using System.Reflection;

    public class DelegatingAssembly : Assembly
    {
        public DelegatingAssembly()
        {
            this.OnGetCustomAttributes = i => new object[0];
            this.OnGetTypes = () => new Type[0];
        }

        public Func<bool, object[]> OnGetCustomAttributes { get; set; }

        public Func<Type, bool, object[]> OnGetCustomAttributesWithType { get; set; }

        public Func<Type[]> OnGetTypes { get; set; }

        public override string FullName
        {
            get { return "DelegatingAssembly"; }
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            return this.OnGetCustomAttributes(inherit);
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return this.OnGetCustomAttributesWithType(attributeType, inherit);
        }

        public override Type[] GetTypes()
        {
            return this.OnGetTypes();
        }
    }
}