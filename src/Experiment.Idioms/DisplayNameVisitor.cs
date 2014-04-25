using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Ploeh.Albedo;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represetns a display name of a reflection member.
    /// </summary>
    public class DisplayNameVisitor : ReflectionVisitor<IEnumerable<string>>
    {
        private readonly IEnumerable<string> _displayNames;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayNameVisitor"/> class.
        /// </summary>
        public DisplayNameVisitor() : this(new string[0])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayNameVisitor"/> class.
        /// </summary>
        /// <param name="displayNames">
        /// The collected display names.
        /// </param>
        protected DisplayNameVisitor(IEnumerable<string> displayNames)
        {
            _displayNames = displayNames;
        }

        /// <summary>
        /// Gets a value indicating the collected dispaly names.
        /// </summary>
        public override IEnumerable<string> Value
        {
            get
            {
                return _displayNames;
            }
        }

        /// <summary>
        /// Visits an assembly element to collect a dispaly name of it.
        /// </summary>
        /// <param name="assemblyElement">
        /// The assembly element whose display name is collected.
        /// </param>
        /// <returns>
        /// The visitor which collected a display name.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<string>> Visit(
            AssemblyElement assemblyElement)
        {
            if (assemblyElement == null)
                throw new ArgumentNullException("assemblyElement");

            return new DisplayNameVisitor(Value.Concat(new[] { assemblyElement.ToString() }));
        }

        /// <summary>
        /// Visits a type element to collect a dispaly name of it.
        /// </summary>
        /// <param name="typeElement">
        /// The type element whose display name is collected.
        /// </param>
        /// <returns>
        /// The visitor which collected a display name.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<string>> Visit(
            TypeElement typeElement)
        {
            if (typeElement == null)
                throw new ArgumentNullException("typeElement");

            return new DisplayNameVisitor(Value.Concat(new[] { typeElement.ToString() }));
        }

        /// <summary>
        /// Visits a field element to collect a dispaly name of it.
        /// </summary>
        /// <param name="fieldInfoElement">
        /// The field element whose display name is collected.
        /// </param>
        /// <returns>
        /// The visitor which collected a display name.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<string>> Visit(
            FieldInfoElement fieldInfoElement)
        {
            if (fieldInfoElement == null)
                throw new ArgumentNullException("fieldInfoElement");

            var fieldInfo = fieldInfoElement.FieldInfo;

            var displayName = string.Format(
                CultureInfo.CurrentCulture,
                "[[{0}][{1}]] (field)",
                fieldInfo.ReflectedType,
                fieldInfo);

            return new DisplayNameVisitor(Value.Concat(new[] { displayName }));
        }

        /// <summary>
        /// Visits a constructor element to collect a dispaly name of it.
        /// </summary>
        /// <param name="constructorInfoElement">
        /// The constructor element whose display name is collected.
        /// </param>
        /// <returns>
        /// The visitor which collected a display name.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<string>> Visit(
            ConstructorInfoElement constructorInfoElement)
        {
            if (constructorInfoElement == null)
                throw new ArgumentNullException("constructorInfoElement");

            var constructorInfo = constructorInfoElement.ConstructorInfo;

            var displayName = string.Format(
                CultureInfo.CurrentCulture,
                "[[{0}][{1}]] (constructor)",
                constructorInfo.ReflectedType,
                constructorInfo);

            return new DisplayNameVisitor(Value.Concat(new[] { displayName }));
        }

        /// <summary>
        /// Visits a property element to collect a dispaly name of it.
        /// </summary>
        /// <param name="propertyInfoElement">
        /// The property element whose display name is collected.
        /// </param>
        /// <returns>
        /// The visitor which collected a display name.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<string>> Visit(
            PropertyInfoElement propertyInfoElement)
        {
            if (propertyInfoElement == null)
                throw new ArgumentNullException("propertyInfoElement");

            var propertyInfo = propertyInfoElement.PropertyInfo;

            var displayName = string.Format(
                CultureInfo.CurrentCulture,
                "[[{0}][{1}]] (property)",
                propertyInfo.ReflectedType,
                propertyInfo);

            return new DisplayNameVisitor(Value.Concat(new[] { displayName }));
        }

        /// <summary>
        /// Visits a method element to collect a dispaly name of it.
        /// </summary>
        /// <param name="metodInfoElement">
        /// The method element whose display name is collected.
        /// </param>
        /// <returns>
        /// The visitor which collected a display name.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<string>> Visit(
            MethodInfoElement metodInfoElement)
        {
            if (metodInfoElement == null)
                throw new ArgumentNullException("metodInfoElement");

            var methodInfo = metodInfoElement.MethodInfo;

            var displayName = string.Format(
                CultureInfo.CurrentCulture,
                "[[{0}][{1}]] (method)",
                methodInfo.ReflectedType,
                methodInfo);

            return new DisplayNameVisitor(Value.Concat(new[] { displayName }));
        }

        /// <summary>
        /// Visits an event element to collect a dispaly name of it.
        /// </summary>
        /// <param name="eventInfoElement">
        /// The event element whose display name is collected.
        /// </param>
        /// <returns>
        /// The visitor which collected a display name.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<string>> Visit(
            EventInfoElement eventInfoElement)
        {
            if (eventInfoElement == null)
                throw new ArgumentNullException("eventInfoElement");

            var eventInfo = eventInfoElement.EventInfo;

            var displayName = string.Format(
                CultureInfo.CurrentCulture,
                "[[{0}][{1}]] (event)",
                eventInfo.ReflectedType,
                eventInfo);

            return new DisplayNameVisitor(Value.Concat(new[] { displayName }));
        }
    }
}