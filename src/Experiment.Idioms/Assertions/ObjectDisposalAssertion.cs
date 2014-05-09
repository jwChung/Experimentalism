using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Jwc.Experiment.Idioms.Assertions
{
    /// <summary>
    /// Encapsulates a unit test that verifies that members throw
    /// <see cref="ObjectDisposedException"/> after its owner(object) is disposed.
    /// </summary>
    public class ObjectDisposalAssertion : IdiomaticMemberAssertion, IIdiomaticTypeAssertion
    {
        private readonly ITestFixture _testFixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectDisposalAssertion"/> class.
        /// </summary>
        /// <param name="testFixture">
        /// The test fixture to create an owner object.
        /// </param>
        public ObjectDisposalAssertion(ITestFixture testFixture)
        {
            if (testFixture == null)
                throw new ArgumentNullException("testFixture");

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
        /// Verifies that instance preperties and methods of a given type throw
        /// <see cref="ObjectDisposedException" /> after the instance of th type
        /// is disposed.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        public void Verify(Type type)
        {
            var members = type.ToIdiomaticInstanceMembers();
            foreach (var member in members)
                Verify(member);
        }

        /// <summary>
        /// Verifies that a property throws <see cref="ObjectDisposedException"/>
        /// after its owner(object) is disposed.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        public override void Verify(PropertyInfo property)
        {
            if (property == null)
                throw new ArgumentNullException("property");

            var getMethod = property.GetGetMethod();
            if (getMethod != null)
                Verify(getMethod);

            var setMethod = property.GetSetMethod();
            if (setMethod != null)
                Verify(setMethod);
        }

        /// <summary>
        /// Verifies that a method throws <see cref="ObjectDisposedException"/>
        /// after its owner(object) is disposed.
        /// </summary>
        /// <param name="method">
        /// The method.
        /// </param>
        public override void Verify(MethodInfo method)
        {
            if (method == null)
                throw new ArgumentNullException("method");

            var owner = GetOwner(method);
            owner.Dispose();

            try
            {
                method.Invoke(owner, GetArguments(method.GetParameters()));
            }
            catch (TargetInvocationException exception)
            {
                if (exception.InnerException is ObjectDisposedException)
                    return;

                throw;
            }

            throw new ObjectDisposalException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    "After the owner of the method is disposed, the method does not throw " +
                    "ObjectDisposedException.{0}" +
                    "Owner : {1}{0}" +
                    "Method: {2}",
                    Environment.NewLine,
                    method.ReflectedType,
                    method));
        }

        private IDisposable GetOwner(MethodInfo method)
        {
            var disposable = TestFixture.Create(method.ReflectedType) as IDisposable;
            if (disposable != null)
                return disposable;

            throw new ArgumentException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    "The owner(object) of the method does not implement IDisposable.{0}" + 
                    "Owner : {1}{0}" + 
                    "Method: {2}",
                    Environment.NewLine,
                    method.ReflectedType,
                    method));
        }

        private object[] GetArguments(IEnumerable<ParameterInfo> parameters)
        {
            return parameters.Select(pi => TestFixture.Create(pi.ParameterType)).ToArray();
        }
    }
}