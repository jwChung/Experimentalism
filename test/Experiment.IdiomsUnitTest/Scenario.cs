using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;
using Ploeh.Albedo.Refraction;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace Jwc.Experiment.Idioms
{
    public class Scenario
    {
        [FirstClassTheorem]
        public IEnumerable<ITestCase> GuardClauseAssertionVerifiesGuardedClass()
        {
            return new IdiomaticTestCases(
                new ReflectionElements(
                    new TargetMembers(typeof(Scenario)),
                    new ConstructorInfoElementRefraction<object>(),
                    new PropertyInfoElementRefraction<object>(),
                    new MethodInfoElementRefraction<object>()),
                new GuardClauseAssertionFactory());
        }

        [FirstClassTheorem(Skip="As this test fails, run explicitly.")]
        public IEnumerable<ITestCase> GuardClauseAssertionThrowsWhenVerifyingNonGuardedClass()
        {
            return new IdiomaticTestCases(
                new ReflectionElements(
                    new TargetMembers(typeof(ClassWithNonGuardedMembers)),
                    new ConstructorInfoElementRefraction<object>(),
                    new PropertyInfoElementRefraction<object>(),
                    new MethodInfoElementRefraction<object>()),
                new GuardClauseAssertionFactory());
        }

        [FirstClassTheorem]
        public IEnumerable<ITestCase> GuardClauseAssertionTestCasesVerifiesGuardedClass()
        {
            return new GuardClauseAssertionTestCases(
                GetType(),
                new Methods<Scenario>().Select(x => x.GuardClauseAssertionTestCasesVerifiesGuardedClass()));
        }

        [FirstClassTheorem(Skip = "As this test fails, run explicitly.")]
        public IEnumerable<ITestCase> GuardClauseAssertionTestCasesThrowsWhenVerifyingNonGuardedClass()
        {
            return new GuardClauseAssertionTestCases(typeof(ClassWithNonGuardedMembers));
        }

        private class ClassWithNonGuardedMembers
        {
            public void NonGuardedMethod(object arg)
            {
            }
        }

        private class FirstClassTheoremAttribute : BaseFirstClassTheoremAttribute
        {
            protected override ITestFixture CreateTestFixture(MethodInfo testMethod)
            {
                return new CustomTestFixture();
            }
        }

        private class CustomTestFixture : ITestFixture
        {
            private readonly ISpecimenContext _context = new SpecimenContext(new Fixture());

            public object Create(object request)
            {
                return _context.Resolve(request);
            }
        }
    }
}