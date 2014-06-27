namespace Jwc.Experiment
{
    public interface IInterfaceWithMembers
    {
        object GetProperty { get; }

        object SetProperty { set; }

        void Method(object arg);
    }
}