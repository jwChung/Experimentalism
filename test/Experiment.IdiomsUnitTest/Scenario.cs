using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class Scenario
    {
        [Fact]
        public void NullGuardClasuseAssertionCorrectlyVerifiesMember()
        {
            typeof(ClassWithGuardedMembers)
                .GetIdiomaticMembers()
                .Except(
                    new MemberInfo[]
                    {
                        Constructors.Select(() => new ClassWithGuardedMembers("anonymous")),
                        new Properties<ClassWithGuardedMembers>().Select(x => x.UnguradedProperty)
                    })
                .ToList()
                .ForEach(new NullGuardClauseAssertion(new FakeTestFixture()).Verify);
        }

        [FirstClassTheorem]
        public IEnumerable<ITestCase> SutWithNullGuardClasuseAssertionCorrectlyCreatesTestCases()
        {
            return typeof(ClassWithGuardedMembers)
                .GetIdiomaticMembers()
                .Except(
                    new MemberInfo[]
                    {
                        Constructors.Select(() => new ClassWithGuardedMembers("anonymous")),
                        new Properties<ClassWithGuardedMembers>().Select(x => x.UnguradedProperty)
                    })
                .Select(m => new TestCase<ITestFixture>(f => new NullGuardClauseAssertion(f).Verify(m)));
        }

        [Fact]
        public void MemberInitializationAssertionCorrectlyVerifiesMember()
        {
            typeof(ClassWithMembersInitializedByConstructor)
                .GetIdiomaticMembers()
                .ToList()
                .ForEach(new MemberInitializationAssertion(new FakeTestFixture()).Verify);
        }

        [FirstClassTheorem]
        public IEnumerable<ITestCase> SutWithMemberInitializationAssertionCorrectlyCreatesTestCases()
        {
            return typeof(ClassWithMembersInitializedByConstructor)
                .GetIdiomaticMembers()
                .Select(m => new TestCase<ITestFixture>(f => new MemberInitializationAssertion(f).Verify(m)));
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