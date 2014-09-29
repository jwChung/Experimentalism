namespace Jwc.Experiment
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Moq;

    public static class Mocked
    {
        private static readonly Type TypeOfMockQueryable
            = typeof(Mock).Assembly.GetTypes().Single(t => t.Name == "MockQueryable`1");

        public static T Of<T>(this T mocked, Expression<Func<T, bool>> predicate) where T : class
        {
            mocked.ToMock().SetupAllProperties();
            return CreateMockQuery<T>(mocked).First(predicate);
        }

        public static T Of<T>(object[] arguments, Expression<Func<T, bool>> predicate) where T : class
        {
            return Of<T>(arguments).Of(predicate);
        }

        public static T Of<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return Of<T>().Of(predicate);
        }

        public static T Of<T>() where T : class
        {
            return Of<T>(new object[0]);
        }

        public static T Of<T>(params object[] arguments) where T : class
        {
            var mock = new Mock<T>(arguments) { DefaultValue = DefaultValue.Mock, CallBase = true };
            mock.SetupAllProperties();
            return mock.Object;
        }

        public static Mock<T> ToMock<T>(this T mocked) where T : class
        {
            return Mock.Get(mocked);
        }

        private static IQueryable<T> CreateMockQuery<T>(T mocked) where T : class
        {
            return (IQueryable<T>)Mocked.GetConstructorOfMockQueryable<T>()
                .Invoke(new object[] { Mocked.GetMethodCallExpression(mocked) });
        }

        private static ConstructorInfo GetConstructorOfMockQueryable<T>()
        {
            return Mocked.TypeOfMockQueryable
                .MakeGenericType(typeof(T))
                .GetConstructor(new[] { typeof(MethodCallExpression) });
        }

        private static MethodCallExpression GetMethodCallExpression<T>(T mocked) where T : class
        {
            return Expression.Call(
                new Func<T, IQueryable<T>>(m => new[] { m }.AsQueryable()).Method,
                Expression.Constant(mocked));
        }
    }
}