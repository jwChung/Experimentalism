using System;

namespace Jwc.Experiment.Idioms
{
    public class ClassWithTestMembers
    {
        public object Field;

        public object Property
        {
            get;
            set;
        }

        private void PrivateMethod()
        {
        }

        public void PublicMethod()
        {
        }

        public static void StaticMethod()
        {
        }

        public event EventHandler PublicEvent;
        protected internal event EventHandler ProtectedInternalEvent;
    }
}