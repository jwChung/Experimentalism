namespace Jwc.Experiment.Xunit
{
    using System.Reflection;
    using global::Xunit;

    internal static class TestMethodContextExtensions
    {
        public static void AssertHasCorrectValues(
            this ITestMethodContext context,
            MethodInfo testMethod,
            MethodInfo actualMethod,
            object testObject,
            object actualObject)
        {
            Assert.Equal(testMethod, context.TestMethod);
            Assert.Equal(actualMethod, context.ActualMethod);
            Assert.Equal(testObject, context.TestObject);
            Assert.Equal(actualObject, context.ActualObject);
        }
    }
}