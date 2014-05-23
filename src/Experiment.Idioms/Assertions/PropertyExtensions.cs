using System.Reflection;

namespace Jwc.Experiment.Idioms.Assertions
{
    internal static class PropertyExtensions
    {
        public static bool IsStatic(this PropertyInfo property)
        {
            MethodInfo setMethod = property.GetSetMethod(true);
            if (setMethod != null)
                return setMethod.IsStatic;

            return property.GetGetMethod(true).IsStatic;
        }

        public static bool IsAbstract(this PropertyInfo property)
        {
            var getMethod = property.GetGetMethod(true);
            if (getMethod != null)
                return getMethod.IsAbstract;

            return property.GetSetMethod(true).IsAbstract;
        }
    }
}