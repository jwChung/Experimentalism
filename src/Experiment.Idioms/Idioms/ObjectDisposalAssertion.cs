namespace Jwc.Experiment.Idioms
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    /// <summary>
    /// Encapsulates a unit test that verifies that members throw
    /// <see cref="ObjectDisposedException" /> after its owner(object) is disposed.
    /// </summary>
    public class ObjectDisposalAssertion : IdiomaticMemberAssertion, IIdiomaticTypeAssertion
    {
        private readonly ISpecimenBuilder builder;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectDisposalAssertion" /> class.
        /// </summary>
        /// <param name="builder">
        /// The builder to create an owner object.
        /// </param>
        public ObjectDisposalAssertion(ISpecimenBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            this.builder = builder;
        }

        /// <summary>
        /// Gets a value indicating the builder.
        /// </summary>
        public ISpecimenBuilder Builder
        {
            get { return this.builder; }
        }

        /// <summary>
        /// Verifies that instance properties and methods of a given type throw
        /// <see cref="ObjectDisposedException" /> after the instance of the type is disposed.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        public void Verify(Type type)
        {
            foreach (var member in type.GetIdiomaticMembers())
                this.Verify(member);
        }

        /// <summary>
        /// Verifies that a property throws <see cref="ObjectDisposedException" /> after its
        /// owner(object) is disposed.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        public override void Verify(PropertyInfo property)
        {
            if (property == null)
                throw new ArgumentNullException("property");

            var getMethod = property.GetGetMethod(true);
            if (getMethod != null)
                this.Verify(getMethod);

            var setMethod = property.GetSetMethod(true);
            if (setMethod != null)
                this.Verify(setMethod);
        }

        /// <summary>
        /// Verifies that a method throws <see cref="ObjectDisposedException" /> after its
        /// owner(object) is disposed.
        /// </summary>
        /// <param name="method">
        /// The method.
        /// </param>
        public override void Verify(MethodInfo method)
        {
            if (method == null)
                throw new ArgumentNullException("method");

            if (method.IsStatic || method.IsAbstract)
                return;

            var owner = this.GetOwner(method);
            if (owner == null)
                ObjectDisposalAssertion.ThrowDoesNotImplementIDisposable(method);

            owner.Dispose();

            this.VerifyThrowsObjectDisposedException(owner, method);
        }
        
        private static void ThrowDoesNotImplementIDisposable(MethodInfo method)
        {
            var messageFormat = @"The owner(object) of the method does not implement IDisposable.
Owner : {0}
Method: {1}";

            throw new ArgumentException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    messageFormat,
                    method.ReflectedType,
                    method));
        }

        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ObjectDisposedException", Justification = "The word is a type name.")]
        private static void ThrowDoesNotThrowObjectDisposedException(MethodInfo method)
        {
            var messageFormat = @"After the owner of the method is disposed, the method does not throw ObjectDisposedException.
Owner : {0}
Method: {1}";

            throw new ObjectDisposalException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    messageFormat,
                    method.ReflectedType,
                    method));
        }

        private void VerifyThrowsObjectDisposedException(IDisposable owner, MethodInfo method)
        {
            try
            {
                method.Invoke(owner, this.GetArguments(method.GetParameters()));
            }
            catch (TargetInvocationException exception)
            {
                if (exception.InnerException is ObjectDisposedException)
                    return;

                throw;
            }

            ObjectDisposalAssertion.ThrowDoesNotThrowObjectDisposedException(method);
        }

        private IDisposable GetOwner(MethodInfo method)
        {
            return this.builder.CreateAnonymous(method.ReflectedType) as IDisposable;
        }

        private object[] GetArguments(IEnumerable<ParameterInfo> parameters)
        {
            return parameters.Select(pi => this.builder.CreateAnonymous(pi)).ToArray();
        }
    }
}