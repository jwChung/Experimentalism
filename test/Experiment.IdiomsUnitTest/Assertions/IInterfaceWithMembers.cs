namespace Jwc.Experiment.Idioms.Assertions
{
    public interface IInterfaceWithMembers
    {
        void Method(object arg);

        object GetProperty { get; }

        object SetProperty { set; }
    }
}