using System.Collections.Generic;
using System.Linq;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Encapsulates a unit test that verifies that a method or constructor has
    /// appropriate Guard Clauses in place.
    /// </summary>
    public class GuardClauseAssertion : AssertionAdapter
    {
        private readonly AccessibilityCollectingVisitor _accessibilityCollectingVisitor
            = new AccessibilityCollectingVisitor();
        private readonly ITestFixture _testFixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="GuardClauseAssertion" /> class.
        /// </summary>
        /// <param name="testFixture">
        /// The test fixture.
        /// </param>
        public GuardClauseAssertion(ITestFixture testFixture)
            : base(new Ploeh.AutoFixture.Idioms.GuardClauseAssertion(new SpecimenBuilderAdapter(testFixture)))
        {
            _testFixture = testFixture;
        }

        /// <summary>
        /// Gets a value indicating the test fixture.
        /// </summary>
        public ITestFixture TestFixture
        {
            get
            {
                return _testFixture;
            }
        }

        /// <summary>
        /// Allows <see cref="TypeElement" /> instances to be 'visited'.
        /// This method is called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="typeElements">
        /// The <see cref="TypeElement" /> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}" /> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<object> Visit(
            params TypeElement[] typeElements)
        {
            return base.Visit(GetPublicReflectionElements(typeElements.Where(e => !e.Type.IsInterface)));
        }

        /// <summary>
        /// Allows <see cref="ConstructorInfoElement" /> instances to be 'visited'.
        /// This method is called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="constructorInfoElements">
        /// The <see cref="ConstructorInfoElement" /> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}" /> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<object> Visit(
            params ConstructorInfoElement[] constructorInfoElements)
        {
            return base.Visit(GetPublicReflectionElements(constructorInfoElements));
        }

        /// <summary>
        /// Allows <see cref="PropertyInfoElement"/> instances to be 'visited'.
        /// This method is called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="propertyInfoElements">
        /// The <see cref="PropertyInfoElement"/> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<object> Visit(
            params PropertyInfoElement[] propertyInfoElements)
        {
            var elements = propertyInfoElements.Where(e => e.PropertyInfo.GetSetMethod() != null).ToArray();
            return base.Visit(elements);
        }

        /// <summary>
        /// Allows <see cref="MethodInfoElement"/> instances to be 'visited'.
        /// This method is called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="methodInfoElements">
        /// The <see cref="MethodInfoElement"/> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<object> Visit(
            params MethodInfoElement[] methodInfoElements)
        {
            return base.Visit(GetPublicReflectionElements(methodInfoElements));
        }

        private T[] GetPublicReflectionElements<T>(IEnumerable<T> reflectionElements)
            where T : IReflectionElement
        {
            return reflectionElements.Where(IsPublic).ToArray();
        }

        private bool IsPublic<T>(T e) where T : IReflectionElement
        {
            var accessibilities = e.Accept(_accessibilityCollectingVisitor).Value.Single();
            return (accessibilities & Accessibilities.Public) == Accessibilities.Public;
        }
    }
}