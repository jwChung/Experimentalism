using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;
using Ploeh.Albedo.Refraction;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents test cases for guard clause assertion.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "The main responsibility of this class isn't to be a 'collection' (which, by the way, it isn't - it's just an Iterator).")]
    public class GuardClauseAssertionTestCases : IdiomaticTestCases
    {
        private readonly Assembly _assembly;
        private readonly Type _type;
        private readonly MemberInfo[] _excludedMembers;

        /// <summary>
        /// Initializes a new instance of the <see cref="GuardClauseAssertionTestCases"/> class.
        /// </summary>
        /// <param name="assembly">The assembly to be verified.</param>
        /// <param name="excludedMembers">The excluded members.</param>
        public GuardClauseAssertionTestCases(Assembly assembly, params MemberInfo[] excludedMembers)
            : base(CreateReflectionElements(assembly, excludedMembers), new GuardClauseAssertionFactory())
        {
            _assembly = assembly;
            _excludedMembers = excludedMembers;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GuardClauseAssertionTestCases"/> class.
        /// </summary>
        /// <param name="type">The target type whose members are verified.</param>
        /// <param name="excludedMembers">The excluded members.</param>
        public GuardClauseAssertionTestCases(Type type, params MemberInfo[] excludedMembers)
            : base(CreateReflectionElements(type, excludedMembers), new GuardClauseAssertionFactory())
        {
            _type = type;
            _excludedMembers = excludedMembers;
        }

        /// <summary>
        /// Gets a value indicating the type.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "This name is desirable to indicate the target type.")]
        public Type Type
        {
            get
            {
                return _type;
            }
        }

        /// <summary>
        /// Gets a value indicating the excluded members.
        /// </summary>
        public IEnumerable<MemberInfo> ExcludedMembers
        {
            get
            {
                return _excludedMembers;
            }
        }

        /// <summary>
        /// Gets a value indicating the assembly.
        /// </summary>
        public Assembly Assembly
        {
            get
            {
                return _assembly;
            }
        }

        private static IEnumerable<IReflectionElement> CreateReflectionElements(
            Type type, IEnumerable<MemberInfo> excludedMembers)
        {
            return new ReflectionElements(
                new ExcludingReadOnlyProperties(
                    new ExcludingMembers(
                        new TypeMembers(type, Accessibilities.Public),
                        excludedMembers)),
                new ConstructorInfoElementRefraction<object>(),
                new PropertyInfoElementRefraction<object>(),
                new MethodInfoElementRefraction<object>());
        }

        private static IEnumerable<IReflectionElement> CreateReflectionElements(
            Assembly assembly, IEnumerable<MemberInfo> excludedMembers)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }

            var typeMemberses = assembly.GetExportedTypes()
                .Select(t => new TypeMembers(t, Accessibilities.Public))
                .Cast<IEnumerable<MemberInfo>>()
                .ToArray();

            return new ReflectionElements(
                new ExcludingReadOnlyProperties(
                    new ExcludingMembers(
                        new CompositeEnumerable<MemberInfo>(typeMemberses),
                        excludedMembers)),
                new ConstructorInfoElementRefraction<object>(),
                new PropertyInfoElementRefraction<object>(),
                new MethodInfoElementRefraction<object>());
        }
    }
}