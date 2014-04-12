using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                    new DefaultMembers(typeof(Scenario)),
                    new AllMemberRefractions().ToArray()),
                new GuardClauseAssertionFactory());
        }

        [FirstClassTheorem(Skip="As this test fails, run explicitly.")]
        public IEnumerable<ITestCase> GuardClauseAssertionThrowsWhenVerifyingNonGuardedClass()
        {
            return new IdiomaticTestCases(
                new ReflectionElements(
                    new DefaultMembers(typeof(ClassWithNonGuardedMembers)),
                    new AllMemberRefractions().ToArray()),
                new GuardClauseAssertionFactory());
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