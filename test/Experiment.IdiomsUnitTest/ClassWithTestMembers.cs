using System;

namespace Jwc.Experiment.Idioms
{
    public class ClassWithTestMembers
    {
        public object PublicField;
        protected internal object ProtectedInternalField;
        protected object ProtectedField;
        internal object InternalField;
        private object PrivateField;

        public ClassWithTestMembers()
        {
        }

        protected internal ClassWithTestMembers(string arg)
        {
        }

        protected ClassWithTestMembers(object arg)
        {
        }

        internal ClassWithTestMembers(int arg)
        {
        }

        private ClassWithTestMembers(double arg)
        {
        }

        public object PublicProperty
        {
            get;
            set;
        }

        protected internal object ProtectedInternalProperty
        {
            get;
            set;
        }

        protected object ProtectedProperty
        {
            get;
            set;
        }

        internal object InternalProperty
        {
            get;
            set;
        }

        private object PrivateProperty
        {
            get;
            set;
        }

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

        protected internal static void ProtectedInternalMStaticMethod()
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

        public event EventHandler PublicEvent;
        protected internal event EventHandler ProtectedInternalEvent;
        protected event EventHandler ProtectedEvent;
        internal event EventHandler InternalEvent;
        private event EventHandler PrivateEvent;
    }
}