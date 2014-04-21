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
            var types = new HashSet<Type>(typesNotExposed);
            foreach (var type in assembly.GetExportedTypes())
            {
                VerifyDoesNotExpose(type, types);
                const BindingFlags bindingFlags = 
                    BindingFlags.Public | BindingFlags.NonPublic |
                    BindingFlags.Static | BindingFlags.Instance;
                foreach (var member in type.GetMembers(bindingFlags))
                {
                    VerifyDoesNotExpose(member, types);
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

        private static void VerifyDoesNotExpose(Type type, ICollection<Type> typesNotExposed)
        {
            while (type != null)
            {
                AssertDoesNotContain(type, typesNotExposed);
                AssertDoesNotContain(type.GetInterfaces(), typesNotExposed);
                type = type.BaseType;
            }
        }

        private static void VerifyDoesNotExpose(MemberInfo member, ICollection<Type> typesNotExposed)
        {
            var constructor = member as ConstructorInfo;
            if (constructor != null)
            {
                VerifyDoesNotExpose(constructor, typesNotExposed);
                return;
            }

            var property = member as PropertyInfo;
            if (property != null)
            {
                var getMethod = property.GetGetMethod();
                if (getMethod != null)
                {
                    VerifyDoesNotExpose(getMethod, typesNotExposed);
                }

                var setMethod = property.GetSetMethod();
                if (setMethod != null)
                {
                    VerifyDoesNotExpose(setMethod, typesNotExposed);
                }

                return;
            }

            var method = member as MethodInfo;
            if (method != null)
            {
                VerifyDoesNotExpose(method, typesNotExposed);
                return;
            }

            var @event = member as EventInfo;
            if (@event != null)
            {
                VerifyDoesNotExpose(@event.GetAddMethod(true), typesNotExposed);
                VerifyDoesNotExpose(@event.GetRemoveMethod(true), typesNotExposed);
            }
        }

        private static void VerifyDoesNotExpose(MethodBase method, ICollection<Type> typesNotExposed)
        {
            if (!method.IsPublic && !method.IsFamily && !method.IsFamilyOrAssembly)
            {
                return;
            }

            var methodInfo = method as MethodInfo;
            if (methodInfo != null)
            {
                AssertDoesNotContain(methodInfo.ReturnType, typesNotExposed);
            }

            AssertDoesNotContain(method.GetParameters().Select(pi => pi.ParameterType), typesNotExposed);
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
            Assert.False(typesNotExposed.Contains(type), type + " should not be exposed.");
        }
    }
}