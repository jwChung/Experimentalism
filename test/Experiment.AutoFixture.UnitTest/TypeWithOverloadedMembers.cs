// Original source code: https://github.com/AutoFixture/AutoFixture
// Copyright           : Copyright (c) 2013 Mark Seemann  
// License             : The MIT License
namespace Jwc.Experiment.AutoFixture
{
    public class TypeWithOverloadedMembers
    {
        public object SomeProperty { get; set; }

        public void DoSomething()
        {
        }

        public void DoSomething(object value)
        {
        }

        public void DoSomething(object value1, object value2)
        {
        }

        public void DoSomething(object value1, object value2, object value3)
        {
        }
    }
}