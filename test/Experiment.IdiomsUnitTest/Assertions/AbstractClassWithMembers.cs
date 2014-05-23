namespace Jwc.Experiment.Idioms.Assertions
{
    public abstract class AbstractClassWithMembers
    {
        public abstract void AbstractMethod(object arg);

        public virtual object SetProperty
        {
            set
            {
            }
        }

        public virtual void VirtualMethod(object arg)
        {
        }
    }
}