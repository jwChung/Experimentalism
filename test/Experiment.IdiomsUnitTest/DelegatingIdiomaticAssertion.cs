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

        public Action<Type> OnVerifyType
        {
            get;
            set;
        }

        public Action<ConstructorInfo> OnVerifyConstructorInfo
        {
            get;
            set;
        }

        public Action<PropertyInfo> OnVerifyPropertyInfo
        {
            get;
            set;
        }

        public Action<MethodInfo> OnVerifyMethodInfo
        {
            get;
            set;
        }

        public override void Verify(Assembly assembly)
        {
            OnVerifyAssembly(assembly);
        }

        public override void Verify(Type type)
        {
            OnVerifyType(type);
        }

        public override void Verify(ConstructorInfo constructorInfo)
        {
            OnVerifyConstructorInfo(constructorInfo);
        }

        public override void Verify(PropertyInfo propertyInfo)
        {
            OnVerifyPropertyInfo(propertyInfo);
        }

        public override void Verify(MethodInfo methodInfo)
        {
            OnVerifyMethodInfo(methodInfo);
        }
    }
}