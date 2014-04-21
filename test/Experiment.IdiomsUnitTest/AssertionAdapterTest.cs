using System;
using System.Linq;
using Ploeh.Albedo;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace Jwc.Experiment
{
    public class AssertionAdapterTest
    {
        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new AssertionAdapter(new GuardClauseAssertion(new ArrayRelay()));
            Assert.IsAssignableFrom<IReflectionVisitor<object>>(sut);
        }

        [Fact]
        public void InitializeWithNullAssertionThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new AssertionAdapter(null));
        }

        [Fact]
        public void AssertionIsCorrect()
        {
            var assertion = new GuardClauseAssertion(new ArrayRelay());
            var sut = new AssertionAdapter(assertion);

            var actual = sut.Assertion;

            Assert.Equal(assertion, actual);
        }

        [Fact]
        public void VisitAssemblyElementVerifiesAssertionCorrectly()
        {
            bool verify = false;
            var assembly = GetType().Assembly;
            var assertion = new DelegatingIdiomaticAssertion
            {
                OnVerifyAssembly = a =>
                {
                    Assert.Equal(assembly, a);
                    verify = true;
                }
            };
            var sut = new AssertionAdapter(assertion);

            var actual = sut.Visit(assembly.ToElement());

            Assert.Equal(sut, actual);
            Assert.True(verify, "Verify.");
        }

        [Fact]
        public void VisitNullAssemblyElementThrows()
        {
            var sut = new AssertionAdapter(new DelegatingIdiomaticAssertion());
            Assert.Throws<ArgumentNullException>(() => sut.Visit((AssemblyElement)null));
        }

        [Fact]
        public void ValueIsCorrect()
        {
            var sut = new AssertionAdapter(new DelegatingIdiomaticAssertion());
            Assert.Throws<NotSupportedException>(() => sut.Value);
        }

        [Fact]
        public void VisitTypeElementVerifiesAssertionCorrectly()
        {
            bool verify = false;
            var type = GetType();
            var assertion = new DelegatingIdiomaticAssertion
            {
                OnVerifyType = t =>
                {
                    Assert.Equal(type, t);
                    verify = true;
                }
            };
            var sut = new AssertionAdapter(assertion);

            var actual = sut.Visit(type.ToElement());

            Assert.Equal(sut, actual);
            Assert.True(verify, "Verify.");
        }

        [Fact]
        public void VisitNullTypeElementThrows()
        {
            var sut = new AssertionAdapter(new DelegatingIdiomaticAssertion());
            Assert.Throws<ArgumentNullException>(() => sut.Visit((TypeElement)null));
        }

        [Fact]
        public void VisitConstructorInfoElementVerifiesAssertionCorrectly()
        {
            bool verify = false;
            var constructorInfo = GetType().GetConstructors().First();
            var assertion = new DelegatingIdiomaticAssertion
            {
                OnVerifyConstructorInfo = c =>
                {
                    Assert.Equal(constructorInfo, c);
                    verify = true;
                }
            };
            var sut = new AssertionAdapter(assertion);

            var actual = sut.Visit(constructorInfo.ToElement());

            Assert.Equal(sut, actual);
            Assert.True(verify, "Verify.");
        }

        [Fact]
        public void VisitNullConstructorInfoElementThrows()
        {
            var sut = new AssertionAdapter(new DelegatingIdiomaticAssertion());
            Assert.Throws<ArgumentNullException>(() => sut.Visit((ConstructorInfoElement)null));
        }

        [Fact]
        public void VisitPropertyInfoElementVerifiesAssertionCorrectly()
        {
            bool verify = false;
            var propertyInfo = typeof(Version).GetProperties().First();
            var assertion = new DelegatingIdiomaticAssertion
            {
                OnVerifyPropertyInfo = p =>
                {
                    Assert.Equal(propertyInfo, p);
                    verify = true;
                }
            };
            var sut = new AssertionAdapter(assertion);

            var actual = sut.Visit(propertyInfo.ToElement());

            Assert.Equal(sut, actual);
            Assert.True(verify, "Verify.");
        }

        [Fact]
        public void VisitNullPropertyInfoElementThrows()
        {
            var sut = new AssertionAdapter(new DelegatingIdiomaticAssertion());
            Assert.Throws<ArgumentNullException>(() => sut.Visit((PropertyInfoElement)null));
        }

        [Fact]
        public void VisitMethodInfoElementVerifiesAssertionCorrectly()
        {
            bool verify = false;
            var methodInfo = GetType().GetMethods().First();
            var assertion = new DelegatingIdiomaticAssertion
            {
                OnVerifyMethodInfo = m =>
                {
                    Assert.Equal(methodInfo, m);
                    verify = true;
                }
            };
            var sut = new AssertionAdapter(assertion);

            var actual = sut.Visit(methodInfo.ToElement());

            Assert.Equal(sut, actual);
            Assert.True(verify, "Verify.");
        }

        [Fact]
        public void VisitNullMethodInfoElementThrows()
        {
            var sut = new AssertionAdapter(new DelegatingIdiomaticAssertion());
            Assert.Throws<ArgumentNullException>(() => sut.Visit((MethodInfoElement)null));
        }
    }
}