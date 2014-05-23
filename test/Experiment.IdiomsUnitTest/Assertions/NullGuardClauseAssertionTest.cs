using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jwc.Experiment.Xunit;
using Moq;
using Ploeh.Albedo;
using Ploeh.AutoFixture.Idioms;
using Xunit;

namespace Jwc.Experiment.Idioms.Assertions
{
    public class NullGuardClauseAssertionTest
    {
        [Fact]
        public void SutIsIdiomaticMemberAssertion()
        {
            var sut = new NullGuardClauseAssertion(new DelegatingTestFixture());
            Assert.IsAssignableFrom<IIdiomaticMemberAssertion>(sut);
        }

        [Fact]
        public void SutIsIdiomaticTypeAssertion()
        {
            var sut = new NullGuardClauseAssertion(new DelegatingTestFixture());
            Assert.IsAssignableFrom<IIdiomaticTypeAssertion>(sut);
        }

        [Fact]
        public void SutIsIdiomaticAssemblyAssertion()
        {
            var sut = new NullGuardClauseAssertion(new DelegatingTestFixture());
            Assert.IsAssignableFrom<IIdiomaticAssemblyAssertion>(sut);
        }

        [Fact]
        public void InitializeWithNullTestFixtureThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new NullGuardClauseAssertion(null));
        }

        [Fact]
        public void TestFixtureIsCorrect()
        {
            var testFixture = new DelegatingTestFixture();
            var sut = new NullGuardClauseAssertion(testFixture);

            var actual = sut.TestFixture;

            Assert.Equal(testFixture, actual);
        }

        [Fact]
        public void VerifyAssemblyCorrectlyVerifies()
        {
            // Fixture setup
            var sut = new Mock<NullGuardClauseAssertion>(new DelegatingTestFixture()).Object;

            var types = new List<MemberInfo>();
            sut.ToMock().Setup(x => x.Verify(It.IsAny<Type>())).Callback<Type>(types.Add);

            var assembly = typeof(TestAttribute).Assembly;

            var expected = assembly.GetExportedTypes();

            // Exercise system
            sut.Verify(assembly);

            // Verify outcome
            Assert.Equal(expected, types);
        }

        [Fact]
        public void VerifyNullAssemblyThrows()
        {
            var sut = new NullGuardClauseAssertion(new DelegatingTestFixture());
            Assert.Throws<ArgumentNullException>(() => sut.Verify((Assembly)null));
        }

        [Fact]
        public void VerifyGuardedTypeDoesNotThrow()
        {
            var sut = new NullGuardClauseAssertion(new FakeTestFixture());
            Assert.DoesNotThrow(() => sut.Verify(typeof(ClassWithGuardedMembers)));
        }

        [Fact]
        public void VerifyUnguardedTypeThrows()
        {
            var sut = new NullGuardClauseAssertion(new FakeTestFixture());
            Assert.Throws<GuardClauseException>(() => sut.Verify(typeof(ClassWithUnguardedMembers)));
        }

        [Fact]
        public void VerifyNullMethodThrows()
        {
            var sut = new NullGuardClauseAssertion(new FakeTestFixture());
            Assert.Throws<ArgumentNullException>(() => sut.Verify((MethodInfo)null));
        }

        [Fact]
        public void VerifyGuardedMethodDoesNotThrow()
        {
            var sut = new NullGuardClauseAssertion(new FakeTestFixture());
            var guardedMethod = new Methods<ClassWithGuardedMembers>().Select(x => x.Method(null, null));
            Assert.DoesNotThrow(() => sut.Verify(guardedMethod));
        }

        [Fact]
        public void VerifyUnguardedMethodThrows()
        {
            var sut = new NullGuardClauseAssertion(new FakeTestFixture());
            var unguardedMethod = new Methods<ClassWithUnguardedMembers>().Select(x => x.Method(null, null));
            Assert.Throws<GuardClauseException>(() => sut.Verify(unguardedMethod));
        }

        [Fact]
        public void VerifyInterfaceMethodDoesNotThrow()
        {
            var sut = new NullGuardClauseAssertion(new FakeTestFixture());
            var method = new Methods<IInterfaceWithMembers>().Select(x => x.Method(null));
            Assert.DoesNotThrow(() => sut.Verify(method));
        }

        [Fact]
        public void VerifyAbstractMethodDoesNotThrow()
        {
            var sut = new NullGuardClauseAssertion(new FakeTestFixture());
            var method = new Methods<AbstractClassWithMembers>().Select(x => x.AbstractMethod(null));
            Assert.DoesNotThrow(() => sut.Verify(method));
        }

        [Fact]
        public void VerifyVirtualUnguardedMethodFromAbstractTypeThrows()
        {
            var sut = new NullGuardClauseAssertion(new FakeTestFixture());
            var method = new Methods<AbstractClassWithMembers>().Select(x => x.VirtualMethod(null));
            Assert.Throws<GuardClauseException>(() => sut.Verify(method));
        }

        [Fact]
        public void VerifyNullPropertyThrows()
        {
            var sut = new NullGuardClauseAssertion(new FakeTestFixture());
            Assert.Throws<ArgumentNullException>(() => sut.Verify((PropertyInfo)null));
        }

        [Fact]
        public void VerifyInterfaceGetPropetyDoesNotThrow()
        {
            var sut = new NullGuardClauseAssertion(new FakeTestFixture());
            var property = new Properties<IInterfaceWithMembers>().Select(x => x.GetProperty);
            Assert.DoesNotThrow(() => sut.Verify(property));
        }

        [Fact]
        public void VerifyInterfaceSetPropetyDoesNotThrow()
        {
            var sut = new NullGuardClauseAssertion(new FakeTestFixture());
            var property = typeof(IInterfaceWithMembers).GetProperty("SetProperty");
            Assert.DoesNotThrow(() => sut.Verify(property));
        }

        [Fact]
        public void VerifyVirtualUnguardedPropertyFromAbstractTypeThrows()
        {
            var sut = new NullGuardClauseAssertion(new FakeTestFixture());
            var property = typeof(AbstractClassWithMembers).GetProperty("SetProperty");
            Assert.Throws<GuardClauseException>(() => sut.Verify(property));
        }

        [Fact]
        public void VerifyGuardedPropertyDoesNotThrow()
        {
            var sut = new NullGuardClauseAssertion(new FakeTestFixture());
            var property = new Properties<ClassWithGuardedMembers>().Select(x => x.Property);
            Assert.DoesNotThrow(() => sut.Verify(property));
        }

        [Fact]
        public void VerifyNullConstructorThrows()
        {
            var sut = new NullGuardClauseAssertion(new FakeTestFixture());
            Assert.Throws<ArgumentNullException>(() => sut.Verify((ConstructorInfo)null));
        }

        [Fact]
        public void VerifyUnguardedConstructorThrows()
        {
            var sut = new NullGuardClauseAssertion(new FakeTestFixture());
            var constructor = Constructors.Select(() => new ClassWithUnguardedMembers(null));
            Assert.Throws<GuardClauseException>(() => sut.Verify(constructor));
        }

        [Fact]
        public void VerifyGuardedConstructorDosNotThrow()
        {
            var sut = new NullGuardClauseAssertion(new FakeTestFixture());
            var constructor = Constructors.Select(() => new ClassWithGuardedMembers(null));
            Assert.DoesNotThrow(() => sut.Verify(constructor));
        }
        
        private class ClassWithGuardedMembers
        {
            public ClassWithGuardedMembers(object arg)
            {
                if (arg == null)
                    throw new ArgumentNullException("arg");
            }

            public object Property
            {
                get
                {
                    return null;
                }
                set
                {
                    if (value == null)
                        throw new ArgumentNullException("value");
                }
            }

            public void Method(string arg1, object arg2)
            {
                if (arg1 == null)
                    throw new ArgumentNullException("arg1");

                if (arg2 == null)
                    throw new ArgumentNullException("arg2");
            }
        }

        private class ClassWithUnguardedMembers
        {
            public ClassWithUnguardedMembers(object arg)
            {
            }

            public void Method(string arg1, object arg2)
            {
                if (arg1 == null)
                    throw new ArgumentNullException("arg1");
            }
        }
    }
}