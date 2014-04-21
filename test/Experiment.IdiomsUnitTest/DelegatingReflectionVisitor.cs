using System;
using Ploeh.Albedo;

namespace Jwc.Experiment
{
    public class DelegatingReflectionVisitor<T> : ReflectionVisitor<T>
    {
        private readonly T _value;

        public DelegatingReflectionVisitor() : this(default(T))
        {
        }

        public DelegatingReflectionVisitor(T value)
        {
            _value = value;
            OnVisitTypeElement = e => base.Visit(e);
        }

        public override T Value
        {
            get
            {
                return _value;
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