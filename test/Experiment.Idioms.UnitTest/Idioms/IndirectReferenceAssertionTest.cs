namespace Jwc.Experiment.Idioms
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using Moq;
    using Ploeh.Albedo;
    using Ploeh.AutoFixture;
    using global::Xunit;
    using global::Xunit.Extensions;

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
        public void InitializeWithNullIndirectReferencesThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new IndirectReferenceAssertion(null));
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
        public void VerifyAssemblyCorrectlyVerifies()
        {
            // Fixture setup
            var arguments = new object[] { new Assembly[0] };
            var sut = new Mock<IndirectReferenceAssertion>(arguments) { CallBase = true }.Object;
            
            var types = new List<Type>();
            sut.ToMock().Setup(x => x.Verify(It.IsAny<Type>())).Callback<Type>(types.Add);

            var assembly = typeof(object).Assembly;
            var expected = assembly.GetTypes().ToArray();

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
        public void VerifyTypeCorrectlyVerifies()
        {
            // Fixture setup
            var arguments = new object[] { new Assembly[0] };
            var sut = new Mock<IndirectReferenceAssertion>(arguments) { CallBase = true }.Object;

            var members = new List<MemberInfo>();
            sut.ToMock().Setup(x => x.Verify(It.IsAny<MemberInfo>())).Callback<MemberInfo>(members.Add);

            var type = typeof(ClassWithMembers);
            var expected = type.GetIdiomaticMembers();

            // Exercise system
            sut.Verify(type);

            // Verify outcome
            Assert.Equal(expected.OrderBy(m => m.Name), members.OrderBy(m => m.Name));
        }

        [Fact]
        public void VerifyTypeThrowsWhenBaseTypeExposesAnyIndirectReference()
        {
            var indirectReferences = new[]
            {
                Assembly.Load("Jwc.Experiment.Idioms")
            };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var type = typeof(TypeForIndirectReference);

            var exception = Assert.Throws<IndirectReferenceException>(() => sut.Verify(type));
            Assert.Contains("Jwc.Experiment.Idioms", exception.Message);
        }

        [Fact]
        public void VerifyUnexposedTypeIgnoresVerifying()
        {
            var type = typeof(object).Assembly.GetTypes().First(t => t.IsNotPublic);
            var sut = new IndirectReferenceAssertion(new[] { typeof(object).Assembly });
            Assert.DoesNotThrow(() => sut.Verify(type));
        }
        
        [Fact]
        public void VerifyFieldDoesNotThrowWhenFieldDoesNotExposeAnyIndirectReference()
        {
            var indirectReferences = new[]
            {
                GetType().Assembly
            };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var field = new Fields<ClassForIndirectReference>().Select(x => x.Field);

            Assert.DoesNotThrow(() => sut.Verify(field));
        }

        [Fact]
        public void VerifyFieldThrowsWhenFieldExposesAnyIndirectReference()
        {
            var indirectReferences = new[]
            {
                GetType().Assembly,
                Assembly.Load("Ploeh.AutoFixture"),
                typeof(object).Assembly
            };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var field = new Fields<ClassForIndirectReference>().Select(x => x.Field);

            var exception = Assert.Throws<IndirectReferenceException>(() => sut.Verify(field));
            Assert.Contains("Ploeh.AutoFixture", exception.Message);
        }

        [Fact]
        public void VerifyFieldIndependentlyVerifies()
        {
            var indirectReferences = new[]
            {
                Assembly.Load("Ploeh.AutoFixture")
            };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var constructor = Constructors.Select(() => new ClassForIndirectReference(null, null));
            var field = new Fields<ClassForIndirectReference>().Select(x => x.Field);

            Assert.Throws<IndirectReferenceException>(() => sut.Verify(field));
            Assert.DoesNotThrow(() => sut.Verify(constructor));
        }

        [Theory]
        [InlineData(FieldAttributes.Private)]
        [InlineData(FieldAttributes.Assembly)]
        public void VerifyUnexposedFieldIgnoresVerifying(FieldAttributes attributes)
        {
            var field = Mock.Of<FieldInfo>(x =>
                x.Attributes == attributes &&
                x.FieldType == typeof(object));
            var sut = new IndirectReferenceAssertion(new[] { typeof(object).Assembly });
            Assert.DoesNotThrow(() => sut.Verify(field));
        }

        [Fact]
        public void VerifyConstructorDoesNotThrowWhenConstructorDoesNotExposeAnyIndirectReference()
        {
            var indirectReferences = new[]
            {
                GetType().Assembly
            };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var constructor = Constructors.Select(() => new ClassForIndirectReference(null, null));

            Assert.DoesNotThrow(() => sut.Verify(constructor));
        }

        [Fact]
        public void VerifyConstructorThrowsWhenConstructorExposesAnyIndirectReference()
        {
            var indirectReferences = new[]
            {
                GetType().Assembly,
                Assembly.Load("Jwc.Experiment.Idioms")
            };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var constructor = Constructors.Select(() => new ClassForIndirectReference(null, null));

            var exception = Assert.Throws<IndirectReferenceException>(() => sut.Verify(constructor));
            Assert.Contains("Jwc.Experiment.Idioms", exception.Message);
        }

        [Fact]
        public void VerifyConstructorDoesNotAddressMethodBody()
        {
            var indirectReferences = new[]
            {
                Assembly.Load("Ploeh.AutoFixture")
            };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var constructor = Constructors.Select(() => new ClassForIndirectReference());

            Assert.DoesNotThrow(() => sut.Verify(constructor));
        }

        [Theory]
        [InlineData(MethodAttributes.Private)]
        [InlineData(MethodAttributes.Assembly)]
        public void VerifyUnexposedConstructorIgnoresVerifying(MethodAttributes attributes)
        {
            var parameterInfos = new[] { Mock.Of<ParameterInfo>(p => p.ParameterType == typeof(object)) };
            var constructor = Mock.Of<ConstructorInfo>(
                x =>
                x.Attributes == attributes &&
                x.GetParameters() == parameterInfos);
            var sut = new IndirectReferenceAssertion(new[] { typeof(object).Assembly });
            Assert.DoesNotThrow(() => sut.Verify(constructor));
        }

        [Fact]
        public void VerifyPropertyDoesNotThrowWhenPropertyDoesNotExposeAnyIndirectReference()
        {
            var indirectReferences = new[]
            {
                GetType().Assembly
            };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var property = new Properties<ClassForIndirectReference>().Select(x => x.Property);

            Assert.DoesNotThrow(() => sut.Verify(property));
        }

        [Fact]
        public void VerifyPropertyThrowsWhenPropertyExposesAnyIndirectReference()
        {
            var indirectReferences = new[]
            {
                GetType().Assembly,
                Assembly.Load("Ploeh.AutoFixture"),
                typeof(object).Assembly
            };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var property = new Properties<ClassForIndirectReference>().Select(x => x.Property);

            var exception = Assert.Throws<IndirectReferenceException>(() => sut.Verify(property));
            Assert.Contains("Ploeh.AutoFixture", exception.Message);
        }

        [Theory]
        [InlineData(MethodAttributes.Private)]
        [InlineData(MethodAttributes.Assembly)]
        public void VerifyUnexposedPropertyIgnoresVerifying(MethodAttributes attributes)
        {
            var getMethod = Mock.Of<MethodInfo>(x => x.Attributes == attributes && x.ReturnType == typeof(object));
            var propertyInfo = Mock.Of<PropertyInfo>(x => x.GetGetMethod(true) == getMethod);
            var sut = new IndirectReferenceAssertion(new[] { typeof(object).Assembly });

            Assert.DoesNotThrow(() => sut.Verify(propertyInfo));
        }

        [Fact]
        public void VerifyMethodDoesNotThrowWhenMethodDoesNotExposeAnyIndirectReference()
        {
            var indirectReferences = new[] { GetType().Assembly };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var method = new Methods<ClassForIndirectReference>().Select(x => x.Method(null, null));

            Assert.DoesNotThrow(() => sut.Verify(method));
        }

        [Fact]
        public void VerifyMethodThrowsWhenMethodExposesAnyIndirectReference()
        {
            var indirectReferences = new[] { GetType().Assembly, Assembly.Load("Jwc.Experiment.Idioms") };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var method = new Methods<ClassForIndirectReference>().Select(x => x.Method(null, null));

            var exception = Assert.Throws<IndirectReferenceException>(() => sut.Verify(method));
            Assert.Contains("Jwc.Experiment.Idioms", exception.Message);
        }

        [Theory]
        [InlineData(MethodAttributes.Private)]
        [InlineData(MethodAttributes.Assembly)]
        public void VerifyUnexposedMethodIgnoresVerifying(MethodAttributes attributes)
        {
            var method = Mock.Of<MethodInfo>(x => x.Attributes == attributes && x.ReturnType == typeof(object));
            var sut = new IndirectReferenceAssertion(new[] { typeof(object).Assembly });
            Assert.DoesNotThrow(() => sut.Verify(method));
        }

        [Fact]
        public void VerifyEventDoesNotThrowWhenMethodDoesNotExposeAnyIndirectReference()
        {
            var indirectReferences = new[] { GetType().Assembly };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var @event = typeof(ClassForIndirectReference).GetEvent("Event");

            Assert.DoesNotThrow(() => sut.Verify(@event));
        }

        [Fact]
        public void VerifyEventThrowsWhenEventExposesAnyIndirectReference()
        {
            var indirectReferences = new[] { GetType().Assembly, Assembly.Load("Jwc.Experiment.Idioms") };
            var sut = new IndirectReferenceAssertion(indirectReferences);
            var @event = typeof(ClassForIndirectReference).GetEvent("Event");

            var exception = Assert.Throws<IndirectReferenceException>(() => sut.Verify(@event));
            Assert.Contains("Jwc.Experiment.Idioms", exception.Message);
        }

        [Theory]
        [InlineData(MethodAttributes.Private)]
        [InlineData(MethodAttributes.Assembly)]
        public void VerifyUnexposedEventIgnoresVerifying(MethodAttributes attributes)
        {
            var addMethod = Mock.Of<MethodInfo>(x => x.Attributes == attributes && x.ReturnType == typeof(object));
            var @event = Mock.Of<EventInfo>(
                x => x.GetAddMethod(true) == addMethod && x.GetRemoveMethod(true) == addMethod);
            var sut = new IndirectReferenceAssertion(new[] { typeof(object).Assembly });

            Assert.DoesNotThrow(() => sut.Verify(@event));
        }

        public class TypeForIndirectReference : IdiomaticMemberAssertion
        {
        }

        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "The field is to test.")]
        public class ClassForIndirectReference : IdiomaticMemberAssertion
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

#pragma warning disable 67
            public event Func<IIdiomaticTypeAssertion> Event;
#pragma warning restore 67

            public Fixture Property { get; set; }

            public void Method(IIdiomaticTypeAssertion arg1, object arg2)
            {
            }
        }
    }
}