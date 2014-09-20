// Original source code: https://github.com/AutoFixture/AutoFixture
// Copyright           : Copyright (c) 2013 Mark Seemann  
// License             : The MIT License
namespace Jwc.Experiment.AutoFixture
{
    public class ConcreteType : AbstractType
    {
        public ConcreteType()
        {
        }

        public ConcreteType(object value)
        {
            this.Property1 = value;
        }

        public ConcreteType(object value1, object value2)
        {
            this.Property1 = value1;
            this.Property2 = value2;
        }

        public ConcreteType(object value1, object value2, object value3)
        {
            this.Property1 = value1;
            this.Property2 = value2;
            this.Property3 = value3;
        }

        public ConcreteType(object value1, object value2, object value3, object value4)
        {
            this.Property1 = value1;
            this.Property2 = value2;
            this.Property3 = value3;
            this.Property4 = value4;
        }

        public override object Property4 { get; set; }

        public object Property5 { get; set; }
    }
}