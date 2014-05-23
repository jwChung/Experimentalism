namespace Jwc.Experiment.Idioms
{
    public interface IInterfaceWithMembers
    {
        void Method(object arg);

        object GetProperty { get; }

        object SetProperty { set; }
    }
}