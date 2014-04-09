using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment
{
    public class AssemblyLevelTest
    {
        [Fact]
        public void SutReferencesOnlySpecifiedAssemblies()
        {
            var sut = typeof(BaseTheoremAttribute).Assembly;
            var specifiedAssemblies = new []
            {
                "mscorlib",
                "System.Core",
                "xunit",
                "xunit.extensions"
            };

            var actual = sut.GetReferencedAssemblies().Select(an => an.Name).Distinct().ToArray();

            Assert.Equal(specifiedAssemblies.Length, actual.Length);
            Assert.False(specifiedAssemblies.Except(actual).Any(), "Empty");
        }

        [Theory]
        [InlineData("xunit.extensions")]
        public void SutDoesNotExposeAnyTypeOfSpecifiedReference(string name)
        {
            // Fixture setup
            var sut = typeof(BaseTheoremAttribute).Assembly;
            var assemblyName = sut.GetReferencedAssemblies().Where(an => an.Name == name).Single();
            var typesNotExposed = new HashSet<Type>(Assembly.Load(assemblyName).GetExportedTypes());

            // Exercise system and Verify outcome
            foreach (var type in sut.GetExportedTypes())
            {
                AssertDoesNotExpose(type, typesNotExposed);
                foreach (var member in type.GetMembers())
                {
                    AssertDoesNotExpose(member, typesNotExposed);
                }
            }
        }

        private static void AssertDoesNotExpose(Type type, ICollection<Type> typesNotExposed)
        {
            while (type != null)
            {
                AssertDoesNotContain(type, typesNotExposed);
                AssertDoesNotContain(type.GetInterfaces(), typesNotExposed);
                type = type.BaseType;
            }
        }

        private static void AssertDoesNotExpose(MemberInfo member, ICollection<Type> typesNotExposed)
        {
            var type = member as FieldInfo;
            if (type != null)
            {
                AssertDoesNotContain(type.FieldType, typesNotExposed);
                return;
            }

            var constructor = member as ConstructorInfo;
            if (constructor != null)
            {
                AssertDoesNotContain(
                    constructor.GetParameters().Select(pi => pi.ParameterType), typesNotExposed);
                return;
            }

            var propety = member as PropertyInfo;
            if (propety != null)
            {
                AssertDoesNotContain(propety.PropertyType, typesNotExposed);
                return;
            }

            var method = member as MethodInfo;
            if (method != null)
            {
                AssertDoesNotContain(method.ReturnType, typesNotExposed);
                AssertDoesNotContain(
                    method.GetParameters().Select(pi => pi.ParameterType), typesNotExposed);
                return;
            }

            var @event = member as EventInfo;
            if (@event != null)
            {
                AssertDoesNotContain(@event.EventHandlerType, typesNotExposed);
                return;
            }
        }

        private static void AssertDoesNotContain(IEnumerable<Type> types, ICollection<Type> typesNotExposed)
        {
            foreach (var type in types)
            {
                AssertDoesNotContain(type, typesNotExposed);
            }
        }

        private static void AssertDoesNotContain(Type type, ICollection<Type> typesNotExposed)
        {
            Assert.False(
                typesNotExposed.Contains(type),
                type + " should not be exposed.");
        }
    }
}