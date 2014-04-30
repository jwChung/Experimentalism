using System.Reflection;

namespace Jwc.Experiment.Idioms2
{
    /// <summary>
    /// Base implementation of <see cref="IIdiomaticMemberAssertion"/>.
    /// </summary>
    public abstract class IdiomaticMemberAssertion : IIdiomaticMemberAssertion
    {
        /// <summary>
        /// Verifies that the idiomatic assertion can be verified for the
        /// specified member.
        /// </summary>
        /// <param name="member">The member.</param>
        public void Verify(MemberInfo member)
        {
            var field = member as FieldInfo;
            if (field != null)
                Verify(field);

            var constructor = member as ConstructorInfo;
            if (constructor != null)
                Verify(constructor);

            var property = member as PropertyInfo;
            if (property != null)
                Verify(property);

            var method = member as MethodInfo;
            if (method != null)
                Verify(method);

            var @event = member as EventInfo;
            if (@event != null)
                Verify(@event);
        }

        /// <summary>
        /// Verifies that the idiomatic assertion can be verified for the
        /// specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        public virtual void Verify(FieldInfo field)
        {
        }

        /// <summary>
        /// Verifies that the idiomatic assertion can be verified for the
        /// specified constructor.
        /// </summary>
        /// <param name="constructor">The constructor.</param>
        public virtual void Verify(ConstructorInfo constructor)
        {
        }

        /// <summary>
        /// Verifies that the idiomatic assertion can be verified for the
        /// specified property.
        /// </summary>
        /// <param name="property">The property.</param>
        public virtual void Verify(PropertyInfo property)
        {
        }

        /// <summary>
        /// Verifies that the idiomatic assertion can be verified for the
        /// specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        public virtual void Verify(MethodInfo method)
        {
        }

        /// <summary>
        /// Verifies that the idiomatic assertion can be verified for the
        /// specified event.
        /// </summary>
        /// <param name="event">The event.</param>
        public virtual void Verify(EventInfo @event)
        {
        }
    }
}