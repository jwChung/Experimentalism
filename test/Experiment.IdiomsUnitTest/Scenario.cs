using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jwc.Experiment.Idioms2;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment
{
    public class Scenario
    {
        [Fact]
        public void VerifyNullGuardClasuseAssertionCorrectlyVerifies()
        {
            var assertion = new NullGuardClauseAssertion(new FakeTestFixture());

            typeof(ClassWithGuardedMembers)
                .GetIdiomaticMembers()
                .Except(
                    new MemberInfo[]
                    {
                        Constructors.Select(() => new ClassWithGuardedMembers("anonymous")),
                        new Properties<ClassWithGuardedMembers>().Select(x => x.UnguradedProperty)
                    })
                .Verify(assertion);
        }

        [FirstClassTheorem]
        public IEnumerable<ITestCase> MembersToTestCasesWithNullGuardClasuseAssertionCorrectlyCreatesTestCases()
        {
            var assertion = new NullGuardClauseAssertion(new FakeTestFixture());

            return typeof(ClassWithGuardedMembers)
                .GetIdiomaticMembers()
                .Except(
                    new MemberInfo[]
                    {
                        Constructors.Select(() => new ClassWithGuardedMembers("anonymous")),
                        new Properties<ClassWithGuardedMembers>().Select(x => x.UnguradedProperty)
                    })
                .ToTestCases(assertion);
        }

        [Fact]
        public void VerifyMemberInitializationAssertionCorrectlyVerifies()
        {
            var assertion = new MemberInitializationAssertion(new FakeTestFixture());

            typeof(ClassWithMembersInitializedByConstructor)
                .GetIdiomaticMembers()
                .Verify(assertion);
        }

        [FirstClassTheorem]
        public IEnumerable<ITestCase> MembersToTestCasesWithMemberInitializationAssertionCorrectlyCreatesTestCases()
        {
            var assertion = new MemberInitializationAssertion(new FakeTestFixture());

            return typeof(ClassWithMembersInitializedByConstructor)
                .GetIdiomaticMembers()
                .ToTestCases(assertion);
        }

        private class ClassWithGuardedMembers
        {
            public ClassWithGuardedMembers(object arg)
            {
                if (arg == null)
                    throw new ArgumentNullException("arg");
            }

            public ClassWithGuardedMembers(string arg)
            {
            }

            public object Property
            {
                set
                {
                    if(value == null)
                        throw new ArgumentNullException("value");
                }
            }

            public object UnguradedProperty
            {
                get;
                set;
            }

            public void Method(object arg)
            {
                if (arg == null)
                    throw new ArgumentNullException("arg");
            }
        }

        private class ClassWithMembersInitializedByConstructor
        {
            private readonly int _x;
            private readonly IList<object> _y;
            private readonly string _z;

            public ClassWithMembersInitializedByConstructor(int x, object[] y, string z)
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

        private class FirstClassTheoremAttribute : FirstClassTheoremBaseAttribute
        {
            protected override ITestFixture CreateTestFixture(MethodInfo testMethod)
            {
                return new FakeTestFixture();
            }
        }
    }
}