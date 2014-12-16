namespace Jwc.Experiment.Idioms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Moq;
    using Ploeh.Albedo;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;
    using global::Xunit;

    public class ObjectDisposalAssertionTest
    {
        [Fact]
        public void SutIsIdiomaticMemberAssertion()
        {
            var sut = new ObjectDisposalAssertion(new Fixture());
            Assert.IsAssignableFrom<IdiomaticAssertion>(sut);
        }

        [Fact]
        public void InitializeWithNullTestFixtureThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new ObjectDisposalAssertion((IFixture)null));
        }

        [Fact]
        public void BuilderIsCorrect()
        {
            var builder = new Fixture();
            var sut = new ObjectDisposalAssertion(builder);

            var actual = sut.Builder;

            Assert.Same(builder, actual);
        }

        [Fact]
        public void VerifyTypeVerifiesCorrectMembers()
        {
            var sut = new Mock<ObjectDisposalAssertion>(new Fixture()) { CallBase = true }.Object;
            var members = new List<MemberInfo>();
            sut.ToMock().Setup(x => x.Verify(It.IsAny<MemberInfo>())).Callback<MemberInfo>(members.Add);
            var type = typeof(ClassWithMembers);
            var expected = new IdiomaticMembers(type);

            sut.Verify(type);

            Assert.Equal(expected.OrderBy(m => m.Name), members.OrderBy(m => m.Name));
        }

        [Fact]
        public void VerifyGetSetPropertyCallsVerifyGetMethodAndSetMethod()
        {
            var sut = new Mock<ObjectDisposalAssertion>(new Fixture()) { CallBase = true }.Object;
            var property = new Properties<ClassWithMembers>().Select(x => x.PublicProperty);
            sut.ToMock().Setup(x => x.Verify(It.IsAny<MethodInfo>()));

            sut.Verify(property);

            sut.ToMock().Verify(x => x.Verify(property.GetGetMethod()));
            sut.ToMock().Verify(x => x.Verify(property.GetSetMethod()));
        }

        [Fact]
        public void VerifyPrivateGetPropertyCallsVerifySetMethod()
        {
            var sut = new Mock<ObjectDisposalAssertion>(new Fixture()) { CallBase = true }.Object;
            var property = typeof(ClassWithMembers).GetProperty("PrivateGetProperty");
            sut.ToMock().Setup(x => x.Verify(It.IsAny<MethodInfo>()));

            sut.Verify(property);

            sut.ToMock().Verify(x => x.Verify((MethodInfo)null), Times.Never());
            sut.ToMock().Verify(x => x.Verify(property.GetSetMethod()));
        }

        [Fact]
        public void VerifyPrivateSetPropertyCallsVerifyGetMethod()
        {
            var sut = new Mock<ObjectDisposalAssertion>(new Fixture()) { CallBase = true }.Object;
            var property = new Properties<ClassWithMembers>().Select(x => x.PrivateSetProperty);
            sut.ToMock().Setup(x => x.Verify(It.IsAny<MethodInfo>()));

            sut.Verify(property);

            sut.ToMock().Verify(x => x.Verify((MethodInfo)null), Times.Never());
            sut.ToMock().Verify(x => x.Verify(property.GetGetMethod()));
        }

        [Fact]
        public void VerifyNullPropertyThrows()
        {
            var sut = new ObjectDisposalAssertion(new Fixture());
            Assert.Throws<ArgumentNullException>(() => sut.Verify((PropertyInfo)null));
        }

        [Fact]
        public void VerifyStaticSetPropertyDoesNotThrow()
        {
            // Fixture setup
            var sut = new ObjectDisposalAssertion(new Fixture());
            var property = typeof(ClassWithMembers).GetProperty("StaticWriteOnlyProperty");
            Assert.NotNull(property);

            // Exercise system and Verify outcome
            Assert.DoesNotThrow(() => sut.Verify(property));
        }

        [Fact]
        public void VerifyStaticGetPropertyDoesNotThrow()
        {
            // Fixture setup
            var sut = new ObjectDisposalAssertion(new Fixture());
            var property = typeof(ClassWithMembers).GetProperty("StaticReadOnlyProperty");
            Assert.NotNull(property);

            // Exercise system and Verify outcome
            Assert.DoesNotThrow(() => sut.Verify(property));
        }

        [Fact]
        public void VerifyInterfaceSetPropertyDoesNotThrow()
        {
            // Fixture setup
            var sut = new ObjectDisposalAssertion(new Fixture());
            var property = typeof(IInterfaceWithMembers).GetProperty("SetProperty");
            Assert.NotNull(property);

            // Exercise system and Verify outcome
            Assert.DoesNotThrow(() => sut.Verify(property));
        }

        [Fact]
        public void VerifyMethodDoesNotThrowWhenMethodThrowsObjectDisposedException()
        {
            var sut = new ObjectDisposalAssertion(new Fixture());
            var method = new Methods<ClassForDisposable>().Select(x => x.ThrowObjectDisposedException());
            Assert.DoesNotThrow(() => sut.Verify(method));
        }

        [Fact]
        public void VerifyMethodThrowsWhenTargetIsNotDisposable()
        {
            var sut = new ObjectDisposalAssertion(new Fixture());
            var method = new Methods<ClassForNonDisposable>().Select(x => x.Method());
            Assert.Throws<ArgumentException>(() => sut.Verify(method));
        }

        [Fact]
        public void VerifyMethodThrowsWhenMethodDoesNotThrowObjectDisposedException()
        {
            var sut = new ObjectDisposalAssertion(new Fixture());
            var method = new Methods<ClassForDisposable>().Select(x => x.DoNotThrowException());
            Assert.Throws<ObjectDisposalException>(() => sut.Verify(method));
        }

        [Fact]
        public void VerifyMethodThrowsWhenMethodThrowsOtherException()
        {
            var sut = new ObjectDisposalAssertion(new Fixture());
            var method = new Methods<ClassForDisposable>().Select(x => x.ThrowNotSupportedException());
            Assert.Throws<TargetInvocationException>(() => sut.Verify(method));
        }

        [Fact]
        public void VerifyNullMethodThrows()
        {
            var sut = new ObjectDisposalAssertion(new Fixture());
            Assert.Throws<ArgumentNullException>(() => sut.Verify((MethodInfo)null));
        }

        [Fact]
        public void VerifyStaticMethodDoesNotThrow()
        {
            // Fixture setup
            var sut = new ObjectDisposalAssertion(new Fixture());
            var method = typeof(ClassWithMembers).GetMethod("PublicStaticMethod");
            Assert.NotNull(method);

            // Exercise system and Verify outcome
            Assert.DoesNotThrow(() => sut.Verify(method));
        }

        [Fact]
        public void VerifyInterfaceMethodDoesNotThrow()
        {
            // Fixture setup
            var sut = new ObjectDisposalAssertion(new Fixture());
            var method = typeof(IInterfaceWithMembers).GetMethod("Method");
            Assert.NotNull(method);

            // Exercise system and Verify outcome
            Assert.DoesNotThrow(() => sut.Verify(method));
        }

        [Fact]
        public void VerifyMethodUsesParameterInfoToCreateArguments()
        {
            var parameters = new List<ParameterInfo>();
            var fixture = new Fixture();
            var tracingBuilder = new TracingBuilder(fixture);
            tracingBuilder.SpecimenCreated += (sender, args) =>
            {
                var parameter = args.Request as ParameterInfo;
                    if (parameter != null)
                        parameters.Add(parameter);
            };

            var sut = new ObjectDisposalAssertion(tracingBuilder);
            var method = new Methods<ClassForDisposable>().Select(
                x => x.ThrowObjectDisposedException(null));

            sut.Verify(method);

            Assert.Equal(1, parameters.Count);
        }

        private class ClassForDisposable : IDisposable
        {
            private bool disposed;

            public void ThrowObjectDisposedException()
            {
                if (this.disposed)
                    throw new ObjectDisposedException(ToString());
            }

            public void ThrowObjectDisposedException(string value)
            {
                if (this.disposed)
                    throw new ObjectDisposedException(ToString());
            }

            public void DoNotThrowException()
            {
            }

            public void ThrowNotSupportedException()
            {
                throw new NotSupportedException();
            }

            public void Dispose()
            {
                this.disposed = true;
            }
        }

        private class ClassForNonDisposable
        {
            public void Method()
            {
            }
        }
    }
}