using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jwc.Experiment.Idioms;
using Jwc.Experiment.Idioms.Assertions;
using Jwc.Experiment.Xunit;
using Ploeh.Albedo;
using Xunit;

[assembly: TestFixtureFactoryType(typeof(Scenario.FakeTestFixtureFactory))]

namespace Jwc.Experiment.Idioms
{
    public class Scenario
    {
        [Fact]
        public void NullGuardClasuseAssertionCorrectlyVerifiesMember()
        {
            typeof(ClassForNullGuardClause)
                .GetIdiomaticMembers()
                .Except(
                    new MemberInfo[]
                    {
                        Constructors.Select(() => new ClassForNullGuardClause("anonymous")),
                        new Properties<ClassForNullGuardClause>().Select(x => x.UnguradedProperty)
                    })
                .ToList()
                .ForEach(new NullGuardClauseAssertion(new FakeTestFixture()).Verify);
        }

        [FirstClassExam]
        public IEnumerable<ITestCase> SutWithNullGuardClasuseAssertionCorrectlyCreatesTestCases()
        {
            return typeof(ClassForNullGuardClause)
                .GetIdiomaticMembers()
                .Except(
                    new MemberInfo[]
                    {
                        Constructors.Select(() => new ClassForNullGuardClause("anonymous")),
                        new Properties<ClassForNullGuardClause>().Select(x => x.UnguradedProperty)
                    })
                .Select(m => new TestCase<NullGuardClauseAssertion>(a => a.Verify(m)));
        }

        [Fact]
        public void MemberInitializationAssertionCorrectlyVerifiesMember()
        {
            typeof(ClassWithMembersInitializedByConstructor)
                .GetIdiomaticMembers()
                .ToList()
                .ForEach(new MemberInitializationAssertion(new FakeTestFixture()).Verify);
        }

        [FirstClassExam]
        public IEnumerable<ITestCase> SutWithMemberInitializationAssertionCorrectlyCreatesTestCases()
        {
            return typeof(ClassWithMembersInitializedByConstructor)
                .GetIdiomaticMembers()
                .Select(m => new TestCase<MemberInitializationAssertion>(a => a.Verify(m)));
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

        internal class FakeTestFixtureFactory : ITestFixtureFactory
        {
            public ITestFixture Create(MethodInfo testMethod)
            {
                return new FakeTestFixture();
            }
        }
    }
}