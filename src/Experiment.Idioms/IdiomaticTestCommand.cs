using System;
using Ploeh.Albedo;
using Xunit.Sdk;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents a test command for idiom.
    /// </summary>
    public class IdiomaticTestCommand : TestCommand
    {
        private readonly IMethodInfo _method;
        private readonly IReflectionElement _element;
        private readonly IReflectionVisitor<object> _assertion;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdiomaticTestCommand" /> class.
        /// </summary>
        /// <param name="method">The method under test.</param>
        /// <param name="element">The reflection element to be asserted.</param>
        /// <param name="assertion">The assertion.</param>
        public IdiomaticTestCommand(
            IMethodInfo method,
            IReflectionElement element,
            IReflectionVisitor<object> assertion)
            : base(EnsureIsNotNull(method), null, 0)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            if (assertion == null)
            {
                throw new ArgumentNullException("assertion");
            }

            _method = method;
            _element = element;
            _assertion = assertion;
        }

        /// <summary>
        /// Gets the test method.
        /// </summary>
        public IMethodInfo Method
        {
            get
            {
                return _method;
            }
        }

        /// <summary>
        /// Gets the reflection element.
        /// </summary>
        public IReflectionElement ReflectionElement
        {
            get
            {
                return _element;
            }
        }

        /// <summary>
        /// Gets the assertion.
        /// </summary>
        public IReflectionVisitor<object> Assertion
        {
            get
            {
                return _assertion;
            }
        }

        /// <summary>
        /// Gets a value indicating whether a test-case instance is created.
        /// </summary>
        /// <value>
        /// <c>true</c> if a test-case instance is created; otherwise, <c>false</c>.
        /// </value>
        public override bool ShouldCreateInstance
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Executes verifying the assertion.
        /// </summary>
        /// <param name="testClass">The test class object.</param>
        /// <returns>The result of the execution.</returns>
        public override MethodResult Execute(object testClass)
        {
            ReflectionElement.Accept(Assertion);
            return new PassedResult(Method, DisplayName);
        }

        private static IMethodInfo EnsureIsNotNull(IMethodInfo method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            return method;
        }
    }
}