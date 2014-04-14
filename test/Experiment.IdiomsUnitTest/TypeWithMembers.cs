using System;

namespace Jwc.Experiment.Idioms
{
    public class TypeWithMembers
    {
        public object PublicField;
        protected internal object ProtectedInternalField;
        protected object ProtectedField;
        internal object InternalField = null;
#pragma warning disable 169
        private object PrivateField;
#pragma warning restore 169

        public TypeWithMembers()
        {
        }

        public TypeWithMembers(string arg1, int arg2)
        {
        }

        protected internal TypeWithMembers(string arg)
        {
        }

        protected TypeWithMembers(object arg)
        {
        }

        internal TypeWithMembers(int arg)
        {
        }

        private TypeWithMembers(double arg)
        {
        }

        public object PublicProperty
        {
            get;
            set;
        }

        public object ReadOnlyProperty
        {
            get
            {
                return null;
            }
        }

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

#pragma warning disable 67
        public event EventHandler PublicEvent;
        protected internal event EventHandler ProtectedInternalEvent;
        protected event EventHandler ProtectedEvent;
        internal event EventHandler InternalEvent;
        private event EventHandler PrivateEvent;
#pragma warning restore 67
    }
}