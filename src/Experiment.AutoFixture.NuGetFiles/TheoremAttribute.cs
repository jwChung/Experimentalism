using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jwc.Experiment;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace Jwc.Experiment
{
    /// <summary>
    /// 이 attribute는 method위에 선언되어 해당 method가 test case라는 것을
    /// 지칭하게 되며, non-parameterized test 뿐 아니라 parameterized test에도
    /// 사용될 수 있다.
    /// Parameterized test에 대해 이 attribute는 AutoFixture library를 이용하여
    /// auto data기능을 제공한다.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TheoremAttribute : BaseTheoremAttribute
    {
        /// <summary>
        /// Creates an instance of <see cref="ITestFixture" />.
        /// </summary>
        /// <param name="testMethod">The test method</param>
        /// <returns>
        /// The created fixture.
        /// </returns>
        public override ITestFixture CreateTestFixture(MethodInfo testMethod)
        {
            if (testMethod == null)
            {
                throw new ArgumentNullException("testMethod");
            }

            return new AutoFixtureAdapter(
                new SpecimenContext(
                    CustomizeFixture(
                        CreateFixture(),
                        testMethod.GetParameters())));
        }

        private static IFixture CreateFixture()
        {
            return new Fixture();
        }

        private static IFixture CustomizeFixture(
            IFixture fixture, IEnumerable<ParameterInfo> parameters)
        {
            return parameters.SelectMany(SelectCustomizations)
                .Aggregate(fixture, (f, c) => f.Customize(c));
        }

        private static IEnumerable<ICustomization> SelectCustomizations(ParameterInfo parameter)
        {
            return from attribute in parameter.GetCustomAttributes(false)
                   let method = GetMethod(attribute)
                   where method != null && typeof(ICustomization).IsAssignableFrom(method.ReturnType)
                   select (ICustomization)method.Invoke(attribute, new object[] { parameter });
        }

        private static MethodInfo GetMethod(object attribute)
        {
            return attribute.GetType().GetMethod(
                "GetCustomization", new[] { typeof(ParameterInfo) });
        }
    }
}