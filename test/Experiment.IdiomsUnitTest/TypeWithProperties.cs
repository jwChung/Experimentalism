namespace Jwc.Experiment.Idioms
{
    public class TypeWithProperties
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

        public object PrivateGetProperty
        {
            private get
            {
                return new object();
            }
            set
            {
            }
        }
    }
}