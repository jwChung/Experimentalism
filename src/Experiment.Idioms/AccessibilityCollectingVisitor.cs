using System;
using System.Collections.Generic;
using System.Linq;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents a class to collect <see cref="Accessibilities"/>.
    /// </summary>
    public class AccessibilityCollectingVisitor : ReflectionVisitor<IEnumerable<Accessibilities>>
    {
        private readonly IEnumerable<Accessibilities> _values;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessibilityCollectingVisitor"/> class.
        /// </summary>
        public AccessibilityCollectingVisitor() : this(new Accessibilities[0])
        {
        }

        private AccessibilityCollectingVisitor(IEnumerable<Accessibilities> values)
        {
            _values = values;
        }

        /// <summary>
        /// Gets the observation or value produced by this instance.
        /// </summary>
        public override IEnumerable<Accessibilities> Value
        {
            get
            {
                return _values;
            }
        }

        /// <summary>
        /// Allows an <see cref="AssemblyElement"/> to be visited. 
        /// This method is called when the element accepts this visitor
        /// instance.
        /// </summary>
        /// <param name="assemblyElement">
        /// The <see cref="AssemblyElement"/> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Accessibilities>> Visit(
            AssemblyElement assemblyElement)
        {
            throw new NotSupportedException("An assembly does not has any accessibilities.");
        }

        /// <summary>
        /// Allows an <see cref="TypeElement"/> to be visited. This method is
        /// called when the element accepts this visitor instance.
        /// </summary>
        /// <param name="typeElement">
        /// The <see cref="TypeElement"/> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Accessibilities>> Visit(
            TypeElement typeElement)
        {
            if (typeElement == null)
            {
                throw new ArgumentNullException("typeElement");
            }

            var type = typeElement.Type;
            var accessibilities = Accessibilities.None;

            if (type.IsPublic) accessibilities = Accessibilities.Public;
            else if (type.IsNotPublic) accessibilities = Accessibilities.Internal;
            else if (type.IsNestedPublic) accessibilities = Accessibilities.Public;
            else if (type.IsNestedFamORAssem) accessibilities = Accessibilities.ProtectedInternal;
            else if (type.IsNestedFamily) accessibilities = Accessibilities.Protected;
            else if (type.IsNestedAssembly) accessibilities = Accessibilities.Internal;
            else if (type.IsNestedPrivate) accessibilities = Accessibilities.Private;

            return new AccessibilityCollectingVisitor(Value.Concat(new[] { accessibilities }));
        }

        /// <summary>
        /// Allows an <see cref="FieldInfoElement"/> to be visited. 
        /// This method is called when the element accepts this visitor
        /// instance.
        /// </summary>
        /// <param name="fieldInfoElement">
        /// The <see cref="FieldInfoElement"/> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Accessibilities>> Visit(
            FieldInfoElement fieldInfoElement)
        {
            if (fieldInfoElement == null)
            {
                throw new ArgumentNullException("fieldInfoElement");
            }

            var fieldInfo = fieldInfoElement.FieldInfo;
            var accessibilities = Accessibilities.None;

            if (fieldInfo.IsPublic) accessibilities = Accessibilities.Public;
            else if (fieldInfo.IsFamilyOrAssembly) accessibilities = Accessibilities.ProtectedInternal;
            else if (fieldInfo.IsFamily) accessibilities = Accessibilities.Protected;
            else if (fieldInfo.IsAssembly) accessibilities = Accessibilities.Internal;
            else if (fieldInfo.IsPrivate) accessibilities = Accessibilities.Private;

            return new AccessibilityCollectingVisitor(Value.Concat(new[] { accessibilities }));
        }
    }
}