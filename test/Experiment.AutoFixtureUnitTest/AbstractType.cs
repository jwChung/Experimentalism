// Original source code: https://github.com/AutoFixture/AutoFixture
// Copyright           : Copyright (c) 2013 Mark Seemann  
// License             : The MIT License

namespace Jwc.Experiment.AutoFixture
{
    public abstract class AbstractType
    {
        protected AbstractType()
        {
        }

        public object Property1 { get; set; }

        public object Property2 { get; set; }

        public object Property3 { get; set; }

        public virtual object Property4 { get; set; }
    }
}