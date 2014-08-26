﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jwc.Experiment;
using Jwc.Experiment.Idioms;
using Jwc.Experiment.Xunit;
using Ploeh.Albedo;

[assembly: Scenario.ScenarioFixtureConfiguration]

namespace Jwc.Experiment
{
    public class Scenario
    {
        [Test]
        public void NullGuardClasuseAssertionCorrectlyVerifiesMembers(
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

        [Test]
        public void MemberInitializationAssertionCorrectlyVerifiesMembers(
            MemberInitializationAssertion assertion)
        {
            typeof(ClassWithMembersInitializedByConstructor)
                .GetIdiomaticMembers()
                .ToList()
                .ForEach(assertion.Verify);
        }

        [FirstClassTest]
        public IEnumerable<ITestCase> NullGuardClasuseAssertionCanBeUsedInTestCases()
        {
            return typeof(ClassForNullGuardClause)
                .GetIdiomaticMembers()
                .Except(
                    new MemberInfo[]
                    {
                        Constructors.Select(() => new ClassForNullGuardClause("anonymous")),
                        new Properties<ClassForNullGuardClause>().Select(x => x.UnguradedProperty)
                    })
                .Select(m => TestCase.New<NullGuardClauseAssertion>(
                    a => a.Verify(m),
                    m.GetDisplayName()));
        }

        [FirstClassTest]
        public IEnumerable<ITestCase> MemberInitializationAssertionCanBeUsedInTestCases()
        {
            return typeof(ClassWithMembersInitializedByConstructor)
                .GetIdiomaticMembers()
                .Select(m => TestCase.New<MemberInitializationAssertion>(
                    a => a.Verify(m),
                    m.GetDisplayName()));
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
                    Assembly.Load("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"),
                    Assembly.Load("System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"),
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
                    Assembly.Load("Ploeh.AutoFixture"),
                    Assembly.Load("Ploeh.AutoFixture.Idioms"),
                    Assembly.Load("Mono.Reflection"))
                .Verify(Assembly.Load("Jwc.Experiment.Idioms"));
        }

        public class ScenarioFixtureConfigurationAttribute : TestAssemblyConfigurationAttribute
        {
            protected override void Setup(Assembly testAssembly)
            {
                DefaultFixtureFactory.SetCurrent(new FakeTestFixtureFactory());
            }
        }

        private class FakeTestFixtureFactory : ITestFixtureFactory
        {
            public ITestFixture Create(MethodInfo testMethod)
            {
                return new FakeTestFixture();
            }
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
                    if (value == null)
                        throw new ArgumentNullException("value");
                }
            }

            public object UnguradedProperty { get; set; }

            public void Method(object arg)
            {
                if (arg == null)
                    throw new ArgumentNullException("arg");
            }
        }

        private class ClassWithMembersInitializedByConstructor
        {
            private readonly int x;
            private readonly IList<object> y;
            private readonly string z;

            public ClassWithMembersInitializedByConstructor(int x, object[] y, string z)
            {
                this.x = x;
                this.y = y.ToList();
                this.z = z;
            }

            public int X
            {
                get
                {
                    return this.x;
                }
            }

            public IEnumerable<object> Y
            {
                get
                {
                    return this.y;
                }
            }

            public object Z
            {
                get
                {
                    return this.z;
                }
            }
        }
    }
}