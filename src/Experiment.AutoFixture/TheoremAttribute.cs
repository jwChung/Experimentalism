using System;
using System.Linq;
using System.Reflection;
using Jwc.Experiment;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Ploeh.AutoFixture.Xunit;

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
    public sealed class TheoremAttribute : NaiveTheoremAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TheoremAttribute"/> class.
        /// </summary>
        public TheoremAttribute() : base(CreateTestFixture)
        {
        }

        private static ITestFixture CreateTestFixture(MethodInfo testMethod)
        {
            var fixture = new Fixture();

            foreach (var parameter in testMethod.GetParameters())
            {
                var attribute = (CustomizeAttribute)parameter
                .GetCustomAttributes(typeof(CustomizeAttribute), false)
                .SingleOrDefault();

                if (attribute != null)
                {
                    var customization = attribute.GetCustomization(parameter);
                    fixture.Customize(customization);
                }
            }

            return new TestFixtureAdapter(new SpecimenContext(fixture));
        }
    }
}