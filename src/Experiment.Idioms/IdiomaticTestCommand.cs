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
            : base(EnsureIsNotNull(method), "anonymous", 100)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            if (assertion == null)
            {
                throw new ArgumentNullException("assertion");
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="testClass"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <inheritdoc />
        public override MethodResult Execute(object testClass)
        {
            throw new System.NotImplementedException();
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