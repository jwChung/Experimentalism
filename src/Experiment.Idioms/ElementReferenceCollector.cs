using System.Collections.Generic;
using System.Reflection;
using Ploeh.Albedo;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents <see cref="IReflectionVisitor{T}" /> to collect references
    /// for a given assembly on only element level.
    /// </summary>
    public class ElementReferenceCollector : ReferenceCollector
    {
        /// <summary>
        /// Allows <see cref="FieldInfoElement" /> instances to be 'visited'.
        /// This method is called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="fieldInfoElements">
        /// The <see cref="FieldInfoElement" /> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}" /> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            params FieldInfoElement[] fieldInfoElements)
        {
            return base.Visit(new FieldInfoElement[0]);
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
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            params ConstructorInfoElement[] constructorInfoElements)
        {
            return base.Visit(new ConstructorInfoElement[0]);
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
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            params PropertyInfoElement[] propertyInfoElements)
        {
            return base.Visit(new PropertyInfoElement[0]);
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
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            params MethodInfoElement[] methodInfoElements)
        {
            return base.Visit(new MethodInfoElement[0]);
        }

        /// <summary>
        /// Allows <see cref="EventInfoElement" /> instances to be 'visited'.
        /// This method is called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="eventInfoElements">
        /// The <see cref="EventInfoElement" /> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}" /> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            params EventInfoElement[] eventInfoElements)
        {
            return base.Visit(new EventInfoElement[0]);
        }

        /// <summary>
        /// Allows <see cref="ParameterInfoElement" /> instances to be 'visited'.
        /// This method is called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="parameterInfoElements">
        /// The <see cref="ParameterInfoElement" /> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}" /> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            params ParameterInfoElement[] parameterInfoElements)
        {
            return base.Visit(new ParameterInfoElement[0]);
        }

        /// <summary>
        /// Allows <see cref="LocalVariableInfoElement" /> instances to be 'visited'.
        /// This method is called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="localVariableInfoElements">
        /// The <see cref="LocalVariableInfoElement" /> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}" /> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            params LocalVariableInfoElement[] localVariableInfoElements)
        {
            return base.Visit(new LocalVariableInfoElement[0]);
        }

        /// <summary>
        /// Visits the specified method base.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        protected override void Visit(MethodBase methodBase)
        {
        }
    }
}