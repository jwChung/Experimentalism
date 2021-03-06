﻿namespace Jwc.Experiment.Idioms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Jwc.Experiment.Xunit;
    using Moq;
    using Ploeh.Albedo;
    using Ploeh.AutoFixture;
    using global::Xunit;
    using global::Xunit.Extensions;

    public class MemberInitializationAssertionTest
    {
        [Fact]
        public void SutIsIdiomaticMemberAssertion()
        {
            var sut = new MemberInitializationAssertion(
                EqualityComparer<IReflectionElement>.Default,
                EqualityComparer<IReflectionElement>.Default);
            Assert.IsAssignableFrom<IdiomaticAssertion>(sut);
        }

        [Fact]
        public void InitializeWithNullParameterToMemberComparerThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new MemberInitializationAssertion(
                    null,
                    EqualityComparer<IReflectionElement>.Default));
        }

        [Fact]
        public void InitializeWithNullMemberToParameterComparerThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new MemberInitializationAssertion(
                    EqualityComparer<IReflectionElement>.Default,
                    null));
        }

        [Fact]
        public void ParameterToMemberComparerIsCorrect()
        {
            var constructorComparer = new DelegatingReflectionElementComparer();
            var sut = new MemberInitializationAssertion(
                constructorComparer,
                EqualityComparer<IReflectionElement>.Default);

            var actual = sut.ParameterToMemberComparer;

            Assert.Equal(constructorComparer, actual);
        }

        [Fact]
        public void ParameterToMemberComparerIsCorrectWhenInitializedWithTestFixture()
        {
            var builder = new Fixture();
            var sut = new MemberInitializationAssertion(builder);

            var actual = sut.ParameterToMemberComparer;

            var comparers = Assert.IsAssignableFrom<OrEqualityComparer<IReflectionElement>>(actual)
                .EqualityComparers.ToArray();
            Assert.Equal(2, comparers.Length);
            var comparers1 = Assert.IsAssignableFrom<ParameterToPropertyComparer>(comparers[0]);
            Assert.Same(builder, comparers1.Builder);
            var comparers2 = Assert.IsAssignableFrom<ParameterToFieldComparer>(comparers[1]);
            Assert.Same(builder, comparers2.Builder);
        }

        [Fact]
        public void MemberToParameterComparerIsCorrect()
        {
            var memberComparer = EqualityComparer<IReflectionElement>.Default;
            var sut = new MemberInitializationAssertion(
                EqualityComparer<IReflectionElement>.Default,
                memberComparer);

            var actual = sut.MemberToParameterComparer;

            Assert.Equal(memberComparer, actual);
        }

        [Fact]
        public void MemberToParameterComparerIsCorrectWhenInitializedWithTestFixture()
        {
            var fixture = new Fixture();
            var sut = new MemberInitializationAssertion(fixture);

            var actual = sut.MemberToParameterComparer;

            var comparers = Assert.IsAssignableFrom<OrEqualityComparer<IReflectionElement>>(actual)
                .EqualityComparers.ToArray();
            Assert.Equal(2, comparers.Length);
            var comparers1 = Assert.IsAssignableFrom<PropertyToParameterComparer>(comparers[0]);
            Assert.Same(fixture, comparers1.Builder);
            var comparers2 = Assert.IsAssignableFrom<FieldToParameterComparer>(comparers[1]);
            Assert.Same(fixture, comparers2.Builder);
        }

        [Fact]
        public void BuilderIsCorrect()
        {
            var builder = new Fixture();
            var sut = new MemberInitializationAssertion(builder);

            var actual = sut.Builder;

            Assert.Same(builder, actual);
        }

        [Fact]
        public void VerifyAssemblyCorrectlyVerifies()
        {
            // Fixture setup
            var sut = new Mock<MemberInitializationAssertion>(new Fixture()) { CallBase = true }.Object;

            var types = new List<MemberInfo>();
            sut.ToMock().Setup(x => x.Verify(It.IsAny<Type>())).Callback<Type>(types.Add);

            var assembly = typeof(TestBaseAttribute).Assembly;

            var expected = assembly.GetExportedTypes();

            // Exercise system
            sut.Verify(assembly);

            // Verify outcome
            Assert.Equal(expected, types);
        }

        [Fact]
        public void VerifyNullAssemblyThrows()
        {
            var sut = new MemberInitializationAssertion(new Fixture());
            Assert.Throws<ArgumentNullException>(() => sut.Verify((Assembly)null));
        }

        [Theory]
        [InlineData(typeof(SatisfiedConstructorDataAttribute.ClassForSatisfiedConstructors))]
        public void VerifySatisfiedTypeDoesNotThrow(Type type)
        {
            var sut = new MemberInitializationAssertion(new Fixture());
            Assert.DoesNotThrow(() => sut.Verify(type));
        }

        [Theory]
        [InlineData(typeof(SatisfiedFieldDataAttribute.ClassForSatisfiedFields))]
        [InlineData(typeof(SatisfiedPropertyDataAttribute.ClassForSatisfiedProperties))]
        [InlineData(typeof(UnsatisfiedConstructorDataAttribute.ClassForUnsatisfiedConstructors))]
        [InlineData(typeof(UnsatisfiedFieldDataAttribute.ClassForUnsatisfiedFields))]
        [InlineData(typeof(UnsatisfiedPropertyDataAttribute.ClassForUnsatisfiedProperties))]
        public void VerifyUnsatisfiedTypeThrows(Type type)
        {
            var sut = new MemberInitializationAssertion(new Fixture());
            Assert.Throws<MemberInitializationException>(() => sut.Verify(type));
        }

        [Theory]
        [SatisfiedFieldData]
        public void VerifySatisfiedFieldDoesNotThrow(FieldInfo field)
        {
            var sut = new MemberInitializationAssertion(new Fixture());
            Assert.DoesNotThrow(() => sut.Verify(field));
        }

        [Theory]
        [UnsatisfiedFieldData]
        public void VerifyUnsatisfiedFieldThrows(FieldInfo field)
        {
            var sut = new MemberInitializationAssertion(new Fixture());
            Assert.Throws<MemberInitializationException>(() => sut.Verify(field));
        }

        [Fact]
        public void VerifyNullFieldThrows()
        {
            var sut = new MemberInitializationAssertion(new Fixture());
            var exception = Assert.Throws<ArgumentNullException>(() => sut.Verify((FieldInfo)null));
            Assert.Equal("field", exception.ParamName);
        }

        [Fact]
        public void VerifyStaticFieldDoesNotThrow()
        {
            var sut = new MemberInitializationAssertion(new Fixture());
            var field = Fields.Select(() => ClassWithMembers.StaticField);
            Assert.DoesNotThrow(() => sut.Verify(field));
        }

        [Fact]
        public void VerifyEnumFieldDoesNotThrow()
        {
            var sut = new MemberInitializationAssertion(new Fixture());
            var field = typeof(Accessibilities).GetField("value__");
            Assert.DoesNotThrow(() => sut.Verify(field));
        }

        [Theory]
        [SatisfiedConstructorData]
        public void VerifySatisfiedConstructorDoesNotThrow(ConstructorInfo constructor)
        {
            var sut = new MemberInitializationAssertion(new Fixture());
            Assert.DoesNotThrow(() => sut.Verify(constructor));
        }

        [Theory]
        [UnsatisfiedConstructorData]
        public void VerifyUnsatisfiedConstructorThrows(ConstructorInfo constructor)
        {
            var sut = new MemberInitializationAssertion(new Fixture());
            Assert.Throws<MemberInitializationException>(() => sut.Verify(constructor));
        }

        [Fact]
        public void VerifyNullConstructorThrows()
        {
            var sut = new MemberInitializationAssertion(new Fixture());
            Assert.Throws<ArgumentNullException>(() => sut.Verify((ConstructorInfo)null));
        }

        [Fact]
        public void VerifyStaticConstructorDoesNotThrow()
        {
            // Fixture setup
            var sut = new MemberInitializationAssertion(new Fixture());
            var constructor = typeof(ClassWithMembers)
                .GetConstructors(BindingFlags.NonPublic | BindingFlags.Static)
                .Single();
            Assert.NotNull(constructor);

            // Exercise system and Verify outcome
            Assert.DoesNotThrow(() => sut.Verify(constructor));
        }

        [Theory]
        [SatisfiedPropertyData]
        public void VerifySatisfiedPropertyDoesNotThrow(PropertyInfo property)
        {
            var sut = new MemberInitializationAssertion(new Fixture());
            Assert.DoesNotThrow(() => sut.Verify(property));
        }

        [Theory]
        [UnsatisfiedPropertyData]
        public void VerifyUnsatisfiedPropertyThrows(PropertyInfo property)
        {
            var sut = new MemberInitializationAssertion(new Fixture());
            Assert.Throws<MemberInitializationException>(() => sut.Verify(property));
        }

        [Fact]
        public void VerifyNullPropertyThrows()
        {
            var sut = new MemberInitializationAssertion(new Fixture());
            var exception = Assert.Throws<ArgumentNullException>(() => sut.Verify((PropertyInfo)null));
            Assert.Equal("property", exception.ParamName);
        }

        [Fact]
        public void VerifyStaticSetPropertyDoesNotThrow()
        {
            // Fixture setup
            var sut = new MemberInitializationAssertion(new Fixture());
            var property = typeof(ClassWithMembers).GetProperty("StaticWriteOnlyProperty");
            Assert.NotNull(property);

            // Exercise system and Verify outcome
            Assert.DoesNotThrow(() => sut.Verify(property));
        }

        [Fact]
        public void VerifyStaticGetPropertyDoesNotThrow()
        {
            // Fixture setup
            var sut = new MemberInitializationAssertion(new Fixture());
            var property = typeof(ClassWithMembers).GetProperty("StaticReadOnlyProperty");
            Assert.NotNull(property);

            // Exercise system and Verify outcome
            Assert.DoesNotThrow(() => sut.Verify(property));
        }

        [Fact]
        public void VerifyInterfaceSetPropertyDoesNotThrow()
        {
            // Fixture setup
            var sut = new MemberInitializationAssertion(new Fixture());
            var property = typeof(IInterfaceWithMembers).GetProperty("SetProperty");
            Assert.NotNull(property);

            // Exercise system and Verify outcome
            Assert.DoesNotThrow(() => sut.Verify(property));
        }

        [Fact]
        public void VerifyInterfaceGetPropertyDoesNotThrow()
        {
            // Fixture setup
            var sut = new MemberInitializationAssertion(new Fixture());
            var property = typeof(IInterfaceWithMembers).GetProperty("GetProperty");
            Assert.NotNull(property);

            // Exercise system and Verify outcome
            Assert.DoesNotThrow(() => sut.Verify(property));
        }

        [Fact]
        public void VerifySetPropertyDoesNotThrow()
        {
            // Fixture setup
            var sut = new MemberInitializationAssertion(new Fixture());
            var property = typeof(ClassWithMembers).GetProperty("WriteOnlyProperty");
            Assert.NotNull(property);

            // Exercise system and Verify outcome
            Assert.DoesNotThrow(() => sut.Verify(property));
        }

        [Fact]
        public void VerifyPrivateGetPropertyDoesNotThrow()
        {
            // Fixture setup
            var sut = new MemberInitializationAssertion(new Fixture());
            var property = typeof(ClassWithMembers).GetProperty("PrivateGetProperty");
            Assert.NotNull(property);

            // Exercise system and Verify outcome
            Assert.DoesNotThrow(() => sut.Verify(property));
        }

        [Theory]
        [IndexerData]
        public void VerifyIndexerAlwaysDoesNotThrow(PropertyInfo indexer)
        {
            var sut = new MemberInitializationAssertion(new Fixture());
            Assert.DoesNotThrow(() => sut.Verify(indexer));
        }

        private class SatisfiedConstructorDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                return typeof(ClassForSatisfiedConstructors).GetConstructors()
                    .Concat(typeof(ClassForSatisfiedConstructorsWithIndexer).GetConstructors())
                    .Select(c => new object[] { c });
            }

            public class ClassForSatisfiedConstructors
            {
                public readonly string StringValue;
                public readonly int IntValue;

                public ClassForSatisfiedConstructors()
                {
                }

                public ClassForSatisfiedConstructors(string stringValue)
                {
                    this.StringValue = stringValue;
                }

                public ClassForSatisfiedConstructors(string stringValue, int intValue)
                {
                    this.StringValue = stringValue;
                    this.IntValue = intValue;
                }

                public ClassForSatisfiedConstructors(object objectValue, int intValue)
                {
                    this.ObjectValue = objectValue;
                    this.IntValue = intValue;
                }

                public ClassForSatisfiedConstructors(object objectValue, byte byteValue)
                {
                    this.ObjectValue = objectValue;
                    this.ByteValue = byteValue;
                }

                public object ObjectValue { get; set; }

                public byte ByteValue { get; set; }
            }

            public class ClassForSatisfiedConstructorsWithIndexer
            {
                public readonly int IntValue;

                public ClassForSatisfiedConstructorsWithIndexer()
                {
                }

                public ClassForSatisfiedConstructorsWithIndexer(object objectValue, int intValue)
                {
                    this.ObjectValue = objectValue;
                    this.IntValue = intValue;
                }

                public object ObjectValue { get; set; }

                public object this[int index]
                {
                    get
                    {
                        return new object();
                    }
                }
            }
        }

        private class UnsatisfiedConstructorDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                return typeof(ClassForUnsatisfiedConstructors).GetConstructors().Select(c => new object[] { c });
            }

            public class ClassForUnsatisfiedConstructors
            {
                public readonly string StringValue;

                public ClassForUnsatisfiedConstructors(string stringValue)
                {
                }

                public ClassForUnsatisfiedConstructors(string stringValue, int intValue)
                {
                    this.StringValue = stringValue;
                }

                public ClassForUnsatisfiedConstructors(int intValue, object objectValue)
                {
                    this.ObjectValue = objectValue;
                }

                public object ObjectValue { get; set; }
            }
        }

        private class SatisfiedFieldDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                return typeof(ClassForSatisfiedFields).GetFields().Select(c => new object[] { c });
            }

            public class ClassForSatisfiedFields
            {
                public readonly object ObjectValue;
                public readonly string StringValue;
                public readonly int IntValue;

                public ClassForSatisfiedFields(object objectValue, string stringValue)
                {
                    this.ObjectValue = objectValue;
                }

                public ClassForSatisfiedFields(string stringValue, int intValue)
                {
                    this.StringValue = stringValue;
                    this.IntValue = intValue;
                }
            }
        }

        private class UnsatisfiedFieldDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                return typeof(ClassForUnsatisfiedFields).GetFields().Select(c => new object[] { c });
            }

            public class ClassForUnsatisfiedFields
            {
#pragma warning disable 169, 649
                public readonly object ObjectValue;
                public readonly string StringValue;
                public readonly int IntValue;
#pragma warning restore 169, 649

                public ClassForUnsatisfiedFields(object objectValue, string stringValue)
                {
                }

                public ClassForUnsatisfiedFields(string stringValue, int intValue)
                {
                }
            }
        }

        private class SatisfiedPropertyDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                return typeof(ClassForSatisfiedProperties).GetProperties().Select(c => new object[] { c });
            }

            public class ClassForSatisfiedProperties
            {
                private readonly int intValue;

                public ClassForSatisfiedProperties(object objectValue, string stringValue)
                {
                    this.ObjectValue = objectValue;
                }

                public ClassForSatisfiedProperties(string stringValue, int intValue)
                {
                    this.StringValue = stringValue;
                    this.intValue = intValue;
                }

                public object ObjectValue { get; set; }

                public string StringValue { get; private set; }

                public int IntValue
                {
                    get
                    {
                        return this.intValue;
                    }
                }
            }
        }

        private class UnsatisfiedPropertyDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                return typeof(ClassForUnsatisfiedProperties).GetProperties().Select(c => new object[] { c });
            }

            public class ClassForUnsatisfiedProperties
            {
                public ClassForUnsatisfiedProperties(object objectValue, string stringValue)
                {
                }

                public ClassForUnsatisfiedProperties(string stringValue, int intValue)
                {
                }

                public object ObjectValue { get; set; }

                public string StringValue { get; private set; }

                public int IntValue
                {
                    get
                    {
                        return -1;
                    }
                }
            }
        }

        private class IndexerDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                return typeof(ClassForIndices).GetProperties().Select(c => new object[] { c });
            }

            public class ClassForIndices
            {
                public object this[int index]
                {
                    get { return new object(); }
                }
            }
        }
    }
}