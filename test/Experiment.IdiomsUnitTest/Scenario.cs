using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jwc.Experiment.Idioms.Assertions;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class Scenario
    {
        [Fact]
        public void NullGuardClasuseAssertionCorrectlyVerifiesMember()
        {
            typeof(ClassForNullGuardClause)
                .ToMembers()
                .Except(
                    new MemberInfo[]
                    {
                        Constructors.Select(() => new ClassForNullGuardClause("anonymous")),
                        new Properties<ClassForNullGuardClause>().Select(x => x.UnguradedProperty)
                    })
                .ToList()
                .ForEach(new NullGuardClauseAssertion(new FakeTestFixture()).Verify);
        }

        [FirstClassTheorem]
        public IEnumerable<ITestCase> SutWithNullGuardClasuseAssertionCorrectlyCreatesTestCases()
        {
            return typeof(ClassForNullGuardClause)
                .ToMembers()
                .Except(
                    new MemberInfo[]
                    {
                        Constructors.Select(() => new ClassForNullGuardClause("anonymous")),
                        new Properties<ClassForNullGuardClause>().Select(x => x.UnguradedProperty)
                    })
                .Select(m => new TestCase<ITestFixture>(f => new NullGuardClauseAssertion(f).Verify(m)));
        }

        [Fact]
        public void MemberInitializationAssertionCorrectlyVerifiesMember()
        {
            typeof(ClassWithMembersInitializedByConstructor)
                .ToMembers()
                .ToList()
                .ForEach(new MemberInitializationAssertion(new FakeTestFixture()).Verify);
        }

        [FirstClassTheorem]
        public IEnumerable<ITestCase> SutWithMemberInitializationAssertionCorrectlyCreatesTestCases()
        {
            return typeof(ClassWithMembersInitializedByConstructor)
                .ToMembers()
                .Select(m => new TestCase<ITestFixture>(f => new MemberInitializationAssertion(f).Verify(m)));
        }

        [Fact]
        public void NullGuardClasuseAssertionCorrectlyVerifiesType()
        {
            new NullGuardClauseAssertion(new FakeTestFixture()).Verify(typeof(Random));
        }

        [Fact]
        public void RestrictiveAssertionCorrectlyVerifiesAssembly()
        {
            new RestrictiveReferenceAssertion(
                Assembly.Load("mscorlib"),
                typeof(Uri).Assembly, // System.dll
                typeof(Enumerable).Assembly, // System.Core.dll
                Assembly.Load("Jwc.Experiment"),
                Assembly.Load("Ploeh.Albedo"),
                Assembly.Load("Ploeh.AutoFixture"),
                Assembly.Load("Ploeh.AutoFixture.Idioms"),
                Assembly.Load("Mono.Reflection"))
            .Verify(Assembly.Load("Jwc.Experiment.Idioms"));
        }

        [Fact]
        public void IndirectAssertionCorrectlyVerifiesAssembly()
        {
            new IndirectReferenceAssertion(
                ////Assembly.Load("mscorlib"),
                ////typeof(Uri).Assembly,
                ////typeof(Enumerable).Assembly,
                ////Assembly.Load("Jwc.Experiment"),
                ////Assembly.Load("Ploeh.Albedo"),
                Assembly.Load("Ploeh.AutoFixture"),
                Assembly.Load("Ploeh.AutoFixture.Idioms"),
                Assembly.Load("Mono.Reflection"))
            .Verify(Assembly.Load("Jwc.Experiment.Idioms"));
        }

        private class ClassForNullGuardClause
        {
            public ClassForNullGuardClause(object arg)
            {
                if (arg == null)
                    throw new ArgumentNullException("arg");
            }

            public ClassForNullGuardClause(string arg)
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