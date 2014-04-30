using System;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    public class DelegatingReflectionVisitor<T> : ReflectionVisitor<T>
    {
        public DelegatingReflectionVisitor()
        {
            OnVisitTypeElement = e => this;
        }

        public override T Value
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public Func<TypeElement, IReflectionVisitor<T>> OnVisitTypeElement
        {
            get;
            set;
        }

        public override IReflectionVisitor<T> Visit(TypeElement typeElement)
        {
            return OnVisitTypeElement(typeElement);
        }
    }
}