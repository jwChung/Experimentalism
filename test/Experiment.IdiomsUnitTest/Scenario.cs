using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jwc.Experiment.Idioms;
using Mono.Reflection;
using Ploeh.Albedo;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace Jwc.Experiment
{
    public class Scenario
    {
        [Fact]
        public void HidingReferenceAssertionVerifiesSpecifiedAssembliesNotExposed()
        {
            var assertion = new HidingReferenceAssertion(typeof(ILPattern).Assembly);
            typeof(ConstructingMemberAssertion).Assembly.ToElement().Accept(assertion);
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

        private class FirstClassTheoremAttribute : FirstClassTheoremBaseAttribute
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