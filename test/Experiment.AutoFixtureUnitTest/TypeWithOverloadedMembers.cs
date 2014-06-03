// Original source code: https://github.com/AutoFixture/AutoFixture.
// Copyright:            Copyright (c) 2013 Mark Seemann  
// License:              The MIT License

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