using System;

namespace Jwc.Experiment
{
    public class ClassWithMembers
    {
        public static object StaticField;
        public object PublicField;
        public object OtherPublicField;
        protected internal object ProtectedInternalField;
        protected object ProtectedField;
        internal object InternalField = null;
#pragma warning disable 169
        private object _privateField;
#pragma warning restore 169

        static ClassWithMembers()
        {
        }

        public ClassWithMembers()
        {
        }

        public ClassWithMembers(string arg1, int arg2)
        {
        }

        protected internal ClassWithMembers(string arg)
        {
        }

        protected ClassWithMembers(object arg)
        {
        }

        internal ClassWithMembers(int arg)
        {
        }

        private ClassWithMembers(double arg)
        {
        }

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

        protected internal object ProtectedInternalProperty { get; set; }

        protected object ProtectedProperty { get; set; }

        internal object InternalProperty { get; set; }

        private object PrivateProperty { get; set; }

        public void PublicMethod()
        {
        }

        protected internal void ProtectedInternalMethod()
        {
        }

        protected void ProtectedMethod()
        {
        }

        internal void InternalMethod()
        {
        }

        private void PrivateMethod()
        {
        }

        public static void PublicStaticMethod()
        {
        }

        protected internal static void ProtectedInternalStaticMethod()
        {
        }

        protected static void ProtectedStaticMethod()
        {
        }

        internal static void InternalStaticMethod()
        {
        }

        private static void PrivateStaticMethod()
        {
        }

#pragma warning disable 67
        public static event EventHandler StaticEvent;
        public event EventHandler PublicEvent;
        protected internal event EventHandler ProtectedInternalEvent;
        protected event EventHandler ProtectedEvent;
        internal event EventHandler InternalEvent;
        private event EventHandler PrivateEvent;
#pragma warning restore 67
    }
}