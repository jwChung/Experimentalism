namespace Jwc.Experiment
{
    public interface IInterfaceWithMembers
    {
        void Method(object arg);

        object GetProperty { get; }

        object SetProperty { set; }
    }
}