namespace Jwc.Experiment
{
    using System;
    using Ploeh.Albedo;

    public class DelegatingReflectionVisitor<T> : ReflectionVisitor<T>
    {
        public DelegatingReflectionVisitor()
        {
            this.OnVisitTypeElement = e => this;
        }

        public override T Value
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public Func<TypeElement, IReflectionVisitor<T>> OnVisitTypeElement { get; set; }

        public override IReflectionVisitor<T> Visit(TypeElement typeElement)
        {
            return this.OnVisitTypeElement(typeElement);
        }
    }
}