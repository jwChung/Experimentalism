using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Jwc.Experiment
{
    public static class AssemblyExtensions
    {
        public static string[] GetActualReferencedAssemblies(this Assembly assembly)
        {
            return assembly.GetExportedTypes()
                .SelectMany(GetReferencesFromAscendants)
                .Concat(assembly.GetReferencedAssemblies().Select(an => an.Name))
                .Distinct()
                .Except(new[] { assembly.GetName().Name })
                .ToArray();
        }

        public static void VerifyDoesNotExpose(this Assembly assembly, IEnumerable<Type> typesNotExposed)
        {
            var hashSet = new HashSet<Type>(typesNotExposed);
            foreach (var type in assembly.GetExportedTypes())
            {
                AssertDoesNotExpose(type, hashSet);
                foreach (var member in type.GetMembers())
                {
                    AssertDoesNotExpose(member, hashSet);
                }
            }
        }

        private static IEnumerable<string> GetReferencesFromAscendants(Type type)
        {
            var assemblies = new List<string>();
            while (type != null)
            {
                assemblies.AddRange(type.GetInterfaces().Select(i => i.Assembly.GetName().Name));
                assemblies.Add(type.Assembly.GetName().Name);
                type = type.BaseType;
            }
            return assemblies;
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

            var property = member as PropertyInfo;
            if (property != null)
            {
                AssertDoesNotContain(property.PropertyType, typesNotExposed);
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