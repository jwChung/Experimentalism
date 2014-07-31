namespace Jwc.Experiment
{
    public abstract class AbstractClassWithMembers
    {
        public virtual object SetProperty
        {
            set
            {
            }
        }

        public abstract void AbstractMethod(object arg);
        
        public virtual void VirtualMethod(object arg)
        {
        }
    }
}