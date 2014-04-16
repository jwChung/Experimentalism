using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;
using Ploeh.Albedo.Refraction;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents test cases for constructing member assertion.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "The main responsibility of this class isn't to be a 'collection' (which, by the way, it isn't - it's just an Iterator).")]
    public class ConstructingMemberAssertionTestCases : IdiomaticTestCases
    {
        private readonly Type _type;
        private readonly Assembly _assembly;
        private readonly MemberInfo[] _excludedMembers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructingMemberAssertionTestCases"/> class.
        /// </summary>
        /// <param name="type">
        /// The type to be verified.
        /// </param>
        /// <param name="excludedMembers">
        /// The members to be ignored from the assertion.
        /// </param>
        public ConstructingMemberAssertionTestCases(Type type, params MemberInfo[] excludedMembers)
            : base(CreateReflectionElements(type, excludedMembers), new ConstructingMemberAssertionFactory())
        {
            _type = type;
            _excludedMembers = excludedMembers;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructingMemberAssertionTestCases"/> class.
        /// </summary>
        /// <param name="assembly">
        /// The assembly to be verified.
        /// </param>
        /// <param name="excludedMembers">
        /// The members to be ignored from the assertion.
        /// </param>
        public ConstructingMemberAssertionTestCases(Assembly assembly, params MemberInfo[] excludedMembers)
            : base(CreateReflectionElements(assembly, excludedMembers), new GuardClauseAssertionFactory())
        {
            _assembly = assembly;
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
            return CreateReflectionElements(
                new TypeMembers(type, Accessibilities.Public), excludedMembers);
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

            return CreateReflectionElements(
                new CompositeEnumerable<MemberInfo>(typeMemberses), excludedMembers);
        }

        private static IEnumerable<IReflectionElement> CreateReflectionElements(
            IEnumerable<MemberInfo> members, IEnumerable<MemberInfo> excludedMembers)
        {
            return new ReflectionElements(
                new ExcludingWriteOnlyProperties(
                    new ExcludingMembers(
                        members,
                        excludedMembers)),
                new ConstructorInfoElementRefraction<object>(),
                new PropertyInfoElementRefraction<object>(),
                new FieldInfoElementRefraction<object>());
        }
    }
}