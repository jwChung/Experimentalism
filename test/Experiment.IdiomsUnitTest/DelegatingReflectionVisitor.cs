using System;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    public class DelegatingReflectionVisitor : ReflectionVisitor<object>
    {
        public DelegatingReflectionVisitor()
        {
            OnVisitTypeElement = e => this;
        }

        public override object Value
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public Func<TypeElement, IReflectionVisitor<object>> OnVisitTypeElement
        {
            get;
            set;
        }

        public override IReflectionVisitor<object> Visit(TypeElement typeElement)
        {
            return OnVisitTypeElement(typeElement);
        }
    }
}