using System;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    public class DelegatingReflectionVisitor : ReflectionVisitor<object>
    {
        public override object Value
        {
            get
            {
                throw new NotSupportedException();
            }
        }
    }
}