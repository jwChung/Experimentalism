// Original source code is from https://github.com/AutoFixture/AutoFixture.

namespace Jwc.Experiment.AutoFixture
{
    public class TypeWithOverloadedMembers
    {
        public object SomeProperty { get; set; }

        public void DoSomething()
        {
        }

        public void DoSomething(object obj)
        {
        }

        public void DoSomething(object x, object y)
        {
        }

        public void DoSomething(object x, object y, object z)
        {
        }
    }
}