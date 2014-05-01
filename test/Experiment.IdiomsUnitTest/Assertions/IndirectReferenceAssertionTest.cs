using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Moq;
using Ploeh.Albedo;
using Ploeh.AutoFixture;
using Xunit;

namespace Jwc.Experiment.Idioms.Assertions
{
    public class IndirectReferenceAssertionTest
    {
        [Fact]
        public void SutIsIdiomaticMemberAssertion()
        {
            var sut = new IndirectReferenceAssertion();
            Assert.IsAssignableFrom<IIdiomaticMemberAssertion>(sut);
        }

        [Fact]
        public void SutIsIdiomaticTypeAssertion()
        {
            var sut = new IndirectReferenceAssertion();
            Assert.IsAssignableFrom<IIdiomaticTypeAssertion>(sut);
        }

        [Fact]
        public void SutIsIdiomaticAssemblyAssertion()
        {
            var sut = new IndirectReferenceAssertion();
            Assert.IsAssignableFrom<IIdiomaticAssemblyAssertion>(sut);
        }
        
        [Fact]
        public void IndirectReferencesIsCorrect()
        {
            var indirectReferences = new[]
            {
                typeof(object).Assembly,
                Assembly.Load("Ploeh.AutoFixture")
            };
            var sut = new IndirectReferenceAssertion(indirectReferences);

            var actual = sut.IndirectReferences;

            Assert.Equal(indirectReferences, actual);
        }

        [Fact]
        public void VerifyFieldDoesNotThrowWhenFieldDoesNotExposeAnyIndirectReference()
        {
            // Fixture setup
            var indirectReferences = new[]
            {
                GetType().Assembly
            };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var field = new Fields<ClassForIndirectReference>().Select(x => x.Field);

            // Exercise system and Verify outcome
            Assert.DoesNotThrow(() => sut.Verify(field));
        }

        [Fact]
        public void VerifyFieldThrowsWhenFieldExposesAnyIndirectReference()
        {
            // Fixture setup
            var indirectReferences = new[]
            {
                GetType().Assembly,
                Assembly.Load("Ploeh.AutoFixture"),
                typeof(object).Assembly
            };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var field = new Fields<ClassForIndirectReference>().Select(x => x.Field);

            // Exercise system and Verify outcome
            var exception = Assert.Throws<IndirectReferenceException>(() => sut.Verify(field));
            Assert.Contains("Ploeh.AutoFixture", exception.Message);
        }

        [Fact]
        public void VerifyConstructorDoesNotThrowWhenConstructorDoesNotExposeAnyIndirectReference()
        {
            // Fixture setup
            var indirectReferences = new[]
            {
                GetType().Assembly
            };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var constructor = Constructors.Select(() => new ClassForIndirectReference(null, null));

            // Exercise system and Verify outcome
            Assert.DoesNotThrow(() => sut.Verify(constructor));
        }

        [Fact]
        public void VerifyConstructorThrowsWhenConstructorExposesAnyIndirectReference()
        {
            // Fixture setup
            var indirectReferences = new[]
            {
                GetType().Assembly,
                Assembly.Load("Jwc.Experiment.Idioms")
            };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var constructor = Constructors.Select(() => new ClassForIndirectReference(null, null));

            // Exercise system and Verify outcome
            var exception = Assert.Throws<IndirectReferenceException>(() => sut.Verify(constructor));
            Assert.Contains("Jwc.Experiment.Idioms", exception.Message);
        }

        [Fact]
        public void VerifyFieldIndependentlyVerifies()
        {
            // Fixture setup
            var indirectReferences = new[]
            {
                Assembly.Load("Ploeh.AutoFixture")
            };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var constructor = Constructors.Select(() => new ClassForIndirectReference(null, null));
            var field = new Fields<ClassForIndirectReference>().Select(x => x.Field);

            // Exercise system and Verify outcome
            Assert.Throws<IndirectReferenceException>(() => sut.Verify(field));
            Assert.DoesNotThrow(() => sut.Verify(constructor));
        }

        [Fact]
        public void VerifyConstructorDoesNotAddressMethodBody()
        {
            // Fixture setup
            var indirectReferences = new[]
            {
                Assembly.Load("Ploeh.AutoFixture")
            };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var constructor = Constructors.Select(() => new ClassForIndirectReference());

            // Exercise system and Verify outcome
            Assert.DoesNotThrow(() => sut.Verify(constructor));
        }

        [Fact]
        public void VerifyPropertyDoesNotThrowWhenPropertyDoesNotExposeAnyIndirectReference()
        {
            // Fixture setup
            var indirectReferences = new[]
            {
                GetType().Assembly
            };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var property = new Properties<ClassForIndirectReference>().Select(x => x.Property);

            // Exercise system and Verify outcome
            Assert.DoesNotThrow(() => sut.Verify(property));
        }

        [Fact]
        public void VerifyPropertyThrowsWhenPropertyExposesAnyIndirectReference()
        {
            // Fixture setup
            var indirectReferences = new[]
            {
                GetType().Assembly,
                Assembly.Load("Ploeh.AutoFixture"),
                typeof(object).Assembly
            };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var property = new Properties<ClassForIndirectReference>().Select(x => x.Property);

            // Exercise system and Verify outcome
            var exception = Assert.Throws<IndirectReferenceException>(() => sut.Verify(property));
            Assert.Contains("Ploeh.AutoFixture", exception.Message);
        }

        [Fact]
        public void VerifyMethodDoesNotThrowWhenMethodDoesNotExposeAnyIndirectReference()
        {
            // Fixture setup
            var indirectReferences = new[]
            {
                GetType().Assembly
            };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var method = new Methods<ClassForIndirectReference>().Select(x => x.Method(null, null));

            // Exercise system and Verify outcome
            Assert.DoesNotThrow(() => sut.Verify(method));
        }

        [Fact]
        public void VerifyMethodThrowsWhenMethodExposesAnyIndirectReference()
        {
            // Fixture setup
            var indirectReferences = new[]
            {
                GetType().Assembly,
                Assembly.Load("Jwc.Experiment.Idioms")
            };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var method = new Methods<ClassForIndirectReference>().Select(x => x.Method(null, null));

            // Exercise system and Verify outcome
            var exception = Assert.Throws<IndirectReferenceException>(() => sut.Verify(method));
            Assert.Contains("Jwc.Experiment.Idioms", exception.Message);
        }

        [Fact]
        public void VerifyEventDoesNotThrowWhenMethodDoesNotExposeAnyIndirectReference()
        {
            // Fixture setup
            var indirectReferences = new[]
            {
                GetType().Assembly
            };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var @event = typeof(ClassForIndirectReference).GetEvent("Event");

            // Exercise system and Verify outcome
            Assert.DoesNotThrow(() => sut.Verify(@event));
        }

        [Fact]
        public void VerifyEventThrowsWhenEventExposesAnyIndirectReference()
        {
            // Fixture setup
            var indirectReferences = new[]
            {
                GetType().Assembly,
                Assembly.Load("Jwc.Experiment.Idioms")
            };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var @event = typeof(ClassForIndirectReference).GetEvent("Event");

            // Exercise system and Verify outcome
            var exception = Assert.Throws<IndirectReferenceException>(() => sut.Verify(@event));
            Assert.Contains("Jwc.Experiment.Idioms", exception.Message);
        }

        [Fact]
        public void VerifyTypeCorrectlyVerifies()
        {
            // Fixture setup
            var sut = new Mock<IndirectReferenceAssertion>(new object[] { new Assembly[0] })
                { CallBase = true }.Object;

            var members = new List<MemberInfo>();
            sut.ToMock().Setup(x => x.Verify(It.IsAny<MemberInfo>())).Callback<MemberInfo>(members.Add);

            var type = typeof(ClassWithMembers);
            
            var expected = new TypeMembers(
                type,
                Accessibilities.Protected | Accessibilities.Public);

            // Exercise system
            sut.Verify(type);

            // Verify outcome
            Assert.Equal(expected, members);
        }

        [Fact]
        public void VerifyAssemblyCorrectlyVerifies()
        {
            // Fixture setup
            var sut = new Mock<IndirectReferenceAssertion>(new object[] { new Assembly[0] })
                { CallBase = true }.Object;

            var types = new List<Type>();
            sut.ToMock().Setup(x => x.Verify(It.IsAny<Type>())).Callback<Type>(types.Add);

            var assembly = typeof(object).Assembly;
            var expected = assembly.GetTypes().Where(IsExposed).ToArray();

            Assert.True(expected.Any(t => t.IsNestedPublic), "IsNestedPublic");
            Assert.True(expected.Any(t => t.IsNestedFamORAssem), "IsNestedFamORAssem");
            Assert.True(expected.Any(t => t.IsNestedFamily), "IsNestedFamily");
            Assert.True(expected.All(t => !t.IsNestedAssembly), "IsNestedAssembly");
            Assert.True(expected.All(t => !t.IsNestedPrivate), "IsNestedPrivate.");

            // Exercise system
            sut.Verify(assembly);

            // Verify outcome
            Assert.Equal(expected, types);
        }

        [Fact]
        public void VerifyNullAssemblyThrows()
        {
            var sut = new IndirectReferenceAssertion();
            Assert.Throws<ArgumentNullException>(() => sut.Verify((Assembly)null));
        }

        [Fact]
        public void VerifyTypeThrowsWhenBaseTypeExposesAnyIndirectReference()
        {
            // Fixture setup
            var indirectReferences = new[]
            {
                Assembly.Load("Jwc.Experiment.Idioms")
            };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var type = typeof(ClassForIndirectReference);

            // Exercise system and Verify outcome
            var exception = Assert.Throws<IndirectReferenceException>(() => sut.Verify(type));
            Assert.Contains("Jwc.Experiment.Idioms", exception.Message);
        }

        private static bool IsExposed(Type type)
        {
            var accessibilityCollector = new AccessibilityCollector();

            return (type.ToElement().Accept(accessibilityCollector).Value.Single()
                & (Accessibilities.Public | Accessibilities.Protected))
                != Accessibilities.None;
        }

        private class ClassForIndirectReference : IdiomaticMemberAssertion
        {
#pragma warning disable 649
            public Fixture Field;
#pragma warning restore 649

            public ClassForIndirectReference()
            {
                var fixture = new Fixture();
                fixture.ToString();
            }

            public ClassForIndirectReference(IIdiomaticTypeAssertion arg1, object arg2)
            {
            }

            public Fixture Property
            {
                get;
                set;
            }

            public void Method(IIdiomaticTypeAssertion arg1, object arg2)
            {
            }

#pragma warning disable 67
            public event Func<IIdiomaticTypeAssertion> Event;
#pragma warning restore 67
        }
    }
}