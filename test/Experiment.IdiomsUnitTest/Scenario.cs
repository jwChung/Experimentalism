using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jwc.Experiment.Idioms;
using Jwc.Experiment.Idioms.Assertions;
using Jwc.Experiment.Xunit;
using Ploeh.Albedo;

[assembly: AssemblyFixtureConfig(typeof(Scenario.FixtureFactoryConfig))]

namespace Jwc.Experiment.Idioms
{
    public class Scenario
    {
        [Test]
        public void NullGuardClasuseAssertionCorrectlyVerifiesMember(
            NullGuardClauseAssertion assertion)
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
                .ForEach(assertion.Verify);
        }

        [FirstClassTest]
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
                .Select(m => new TestCase(new Action<NullGuardClauseAssertion>(a => a.Verify(m))));
        }

        [Test]
        public void MemberInitializationAssertionCorrectlyVerifiesMember(
            MemberInitializationAssertion assertion)
        {
            typeof(ClassWithMembersInitializedByConstructor)
                .GetIdiomaticMembers()
                .ToList()
                .ForEach(assertion.Verify);
        }

        [FirstClassTest]
        public IEnumerable<ITestCase> SutWithMemberInitializationAssertionCorrectlyCreatesTestCases()
        {
            return typeof(ClassWithMembersInitializedByConstructor)
                .GetIdiomaticMembers()
                .Select(m => new TestCase(new Action<MemberInitializationAssertion>(a => a.Verify(m))));
        }

        [Test]
        public void NullGuardClasuseAssertionCorrectlyVerifiesType(
            NullGuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(Random));
        }

        [Test]
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

        [Test]
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

        internal class FixtureFactoryConfig
        {
            public FixtureFactoryConfig()
            {
                DefaultFixtureFactory.SetCurrent(new FakeTestFixtureFactory());
            }
        }
    }
}