using System;
using Ploeh.Albedo;
using Ploeh.AutoFixture.Idioms;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents an assertion to adapt <see cref="IIdiomaticAssertion"/>
    /// to <see cref="IReflectionVisitor{T}"/>.
    /// </summary>
    public class AssertionAdapter : ReflectionVisitor<object>
    {
        private readonly IIdiomaticAssertion _assertion;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssertionAdapter"/> class.
        /// </summary>
        /// <param name="assertion">The assertion.</param>
        public AssertionAdapter(IIdiomaticAssertion assertion)
        {
            _assertion = assertion;
            if (assertion == null)
            {
                throw new ArgumentNullException("assertion");
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AssertionAdapter", Justification="This word is a class name.")]
        public override object Value
        {
            get
            {
                throw new NotSupportedException(
                    "This Value property does not support to return a value as the main purpose of this " +
                    "AssertionAdapter class is to verify assertions.");
            }
        }

        /// <summary>
        /// Gets the assertion.
        /// </summary>
        /// <value>
        /// The assertion.
        /// </value>
        public IIdiomaticAssertion Assertion
        {
            get
            {
                return _assertion;
            }
        }

        /// <summary>
        /// Visits the specified assembly element.
        /// </summary>
        /// <param name="assemblyElement">The assembly element.</param>
        /// <returns>The current assertion.</returns>
        public override IReflectionVisitor<object> Visit(AssemblyElement assemblyElement)
        {
            if (assemblyElement == null)
            {
                throw new ArgumentNullException("assemblyElement");
            }

            _assertion.Verify(assemblyElement.Assembly);
            return this;
        }

        /// <summary>
        /// Visits the specified type element.
        /// </summary>
        /// <param name="typeElement">The type element.</param>
        /// <returns>The current assertion.</returns>
        public override IReflectionVisitor<object> Visit(TypeElement typeElement)
        {
            if (typeElement == null)
            {
                throw new ArgumentNullException("typeElement");
            }

            _assertion.Verify(typeElement.Type);
            return this;
        }

        /// <summary>
        /// Visits the specified constructor information element.
        /// </summary>
        /// <param name="constructorInfoElement">The constructor information element.</param>
        /// <returns>The current assertion.</returns>
        public override IReflectionVisitor<object> Visit(ConstructorInfoElement constructorInfoElement)
        {
            if (constructorInfoElement == null)
            {
                throw new ArgumentNullException("constructorInfoElement");
            }

            _assertion.Verify(constructorInfoElement.ConstructorInfo);
            return this;
        }

        /// <summary>
        /// Visits the specified property information element.
        /// </summary>
        /// <param name="propertyInfoElement">The property information element.</param>
        /// <returns>The current assertion.</returns>
        public override IReflectionVisitor<object> Visit(PropertyInfoElement propertyInfoElement)
        {
            if (propertyInfoElement == null)
            {
                throw new ArgumentNullException("propertyInfoElement");
            }

            _assertion.Verify(propertyInfoElement.PropertyInfo);
            return this;
        }

        /// <summary>
        /// Visits the specified method information element.
        /// </summary>
        /// <param name="methodInfoElement">The method information element.</param>
        /// <returns>The current assertion.</returns>
        public override IReflectionVisitor<object> Visit(MethodInfoElement methodInfoElement)
        {
            if (methodInfoElement == null)
            {
                throw new ArgumentNullException("methodInfoElement");
            }

            _assertion.Verify(methodInfoElement.MethodInfo);
            return this;
        }
    }
}