using System;
using System.Diagnostics.CodeAnalysis;

namespace Jwc.Experiment
{
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "The field is to test.")]
    public class ClassWithMembers
    {
        public static object StaticField;
        public object PublicField;
        public object OtherPublicField;
        internal object InternalField = null;
        protected internal object ProtectedInternalField;
        protected object protectedField;
#pragma warning disable 169
        private object privateField;
#pragma warning restore 169

        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1409:RemoveUnnecessaryCode", Justification = "The static constructor is to test.")]
        static ClassWithMembers()
        {
        }

        public ClassWithMembers()
        {
        }

        public ClassWithMembers(string arg1, int arg2)
        {
        }

        internal ClassWithMembers(int arg)
        {
        }

        protected internal ClassWithMembers(string arg)
        {
        }

        protected ClassWithMembers(object arg)
        {
        }

        private ClassWithMembers(double arg)
        {
        }

#pragma warning disable 67
        public static event EventHandler StaticEvent;

        public event EventHandler PublicEvent;

        internal event EventHandler InternalEvent;

        protected internal event EventHandler ProtectedInternalEvent;

        protected event EventHandler ProtectedEvent;

        private event EventHandler PrivateEvent;
#pragma warning restore 67

        public static object StaticProperty { get; set; }

        public static object StaticReadOnlyProperty
        {
            get
            {
                return null;
            }
        }

        public static object StaticWriteOnlyProperty
        {
            set
            {
            }
        }

        public object PublicProperty { get; set; }

        public object ReadOnlyProperty
        {
            get
            {
                return null;
            }
        }

        public object WriteOnlyProperty
        {
            set
            {
            }
        }

        public object PrivateSetProperty { get; private set; }

        public object PrivateGetProperty
        {
            private get
            {
                return null;
            }

            set
            {
            }
        }

        internal object InternalProperty { get; set; }

        protected internal object ProtectedInternalProperty { get; set; }

        protected object ProtectedProperty { get; set; }

        private object PrivateProperty { get; set; }

        public static void PublicStaticMethod()
        {
        }

        public void PublicMethod()
        {
        }

        internal static void InternalStaticMethod()
        {
        }

        internal void InternalMethod()
        {
        }

        protected internal static void ProtectedInternalStaticMethod()
        {
        }

        protected internal void ProtectedInternalMethod()
        {
        }

        protected static void ProtectedStaticMethod()
        {
        }
        
        protected void ProtectedMethod()
        {
        }

        private static void PrivateStaticMethod()
        {
        }

        private void PrivateMethod()
        {
        }
    }
}