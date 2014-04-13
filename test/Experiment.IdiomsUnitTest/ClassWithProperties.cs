namespace Jwc.Experiment.Idioms
{
    public class ClassWithProperties
    {
        public object GetSetProperty
        {
            get;
            set;
        }

        public object GetProperty
        {
            get
            {
                return new object();
            }
        }

        public object SetProperty
        {
            set
            {
            }
        }

        public object PrivateSetProperty
        {
            get
            {
                return new object();
            }
            private set
            {
            }
        }
    }
}