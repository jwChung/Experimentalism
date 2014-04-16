using System;
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
        public IEnumerable<ITestCase> GuardClauseAssertionVerifiesGuardClauses()
        {
            return new IdiomaticTestCases(
                new ReflectionElements(
                    new TypeMembers(typeof(Scenario)),
                    new ConstructorInfoElementRefraction<object>(),
                    new PropertyInfoElementRefraction<object>(),
                    new MethodInfoElementRefraction<object>()),
                new GuardClauseAssertionFactory());
        }

        [FirstClassTheorem(Skip="As this test fails, run explicitly.")]
        public IEnumerable<ITestCase> GuardClauseAssertionThrowsWhenVerifyingNonGuardClauses()
        {
            return new IdiomaticTestCases(
                new ReflectionElements(
                    new TypeMembers(typeof(TypeWithGuardTestMembers)),
                    new ConstructorInfoElementRefraction<object>(),
                    new PropertyInfoElementRefraction<object>(),
                    new MethodInfoElementRefraction<object>()),
                new GuardClauseAssertionFactory());
        }

        [FirstClassTheorem]
        public IEnumerable<ITestCase> GuardClauseAssertionTestCasesVerifiesGuardClauses()
        {
            return new GuardClauseAssertionTestCases(GetType());
        }

        [FirstClassTheorem]
        public IEnumerable<ITestCase> GuardClauseAssertionTestCasesCanExceptCertainMembers()
        {
            return new GuardClauseAssertionTestCases(
                typeof(TypeWithGuardTestMembers),
                new Methods<TypeWithGuardTestMembers>().Select(x => x.NonGuardedMethod(null)));
        }

        [FirstClassTheorem]
        public IEnumerable<ITestCase> ConstructingMemberAssertionTestCasesVerifiesMembersInitiazliedByConstructor()
        {
            return new ConstructingMemberAssertionTestCases(typeof(TypeWithMembersInitializedByConstructor));
        }

        private class TypeWithGuardTestMembers
        {
            public void NonGuardedMethod(object arg)
            {
            }

            public void GuardedMethod(object arg)
            {
                if (arg == null)
                {
                    throw new ArgumentNullException("arg");
                }
            }
        }

        private class TypeWithMembersInitializedByConstructor
        {
            private readonly int _x;
            private readonly IList<object> _y;
            private readonly string _z;

            public TypeWithMembersInitializedByConstructor(int x, object[] y, string z)
            {
                _x = x;
                _y = y.ToList();
                _z = z;
            }

            public int X
            {
                get
                {
                    return _x;
                }
            }

            public IEnumerable<object> Y
            {
                get
                {
                    return _y;
                }
            }

            public object Z
            {
                get
                {
                    return _z;
                }
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