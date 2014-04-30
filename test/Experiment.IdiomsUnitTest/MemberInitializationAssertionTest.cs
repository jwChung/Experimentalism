using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment.Idioms
{
    public class MemberInitializationAssertionTest
    {
        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new MemberInitializationAssertion(
                EqualityComparer<IReflectionElement>.Default,
                EqualityComparer<IReflectionElement>.Default);
            Assert.IsAssignableFrom<IIdiomaticMemberAssertion>(sut);
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
        public void ParameterToMemberComparerIsCorrectWhenInitializedWithTestFixture()
        {
            var testFixture = new DelegatingTestFixture();
            var sut = new MemberInitializationAssertion(testFixture);

            var actual = sut.ParameterToMemberComparer;

            var comparers = Assert.IsAssignableFrom<OrEqualityComparer<IReflectionElement>>(actual)
                .EqualityComparers.ToArray();
            Assert.Equal(2, comparers.Length);
            var comparers1 = Assert.IsAssignableFrom<ParameterToPropertyComparer>(comparers[0]);
            Assert.Equal(testFixture, comparers1.TestFixture);
            var comparers2 = Assert.IsAssignableFrom<ParameterToFieldComparer>(comparers[1]);
            Assert.Equal(testFixture, comparers2.TestFixture);
        }

        [Fact]
        public void MemberToParameterComparerIsCorrectWhenInitializedWithTestFixture()
        {
            var testFixture = new DelegatingTestFixture();
            var sut = new MemberInitializationAssertion(testFixture);

            var actual = sut.MemberToParameterComparer;

            var comparers = Assert.IsAssignableFrom<OrEqualityComparer<IReflectionElement>>(actual)
                .EqualityComparers.ToArray();
            Assert.Equal(2, comparers.Length);
            var comparers1 = Assert.IsAssignableFrom<PropertyToParameterComparer>(comparers[0]);
            Assert.Equal(testFixture, comparers1.TestFixture);
            var comparers2 = Assert.IsAssignableFrom<FieldToParameterComparer>(comparers[1]);
            Assert.Equal(testFixture, comparers2.TestFixture);
        }

        [Fact]
        public void TestFixtureIsCorrect()
        {
            var testFixture = new DelegatingTestFixture();
            var sut = new MemberInitializationAssertion(testFixture);

            var actual = sut.TestFixture;

            Assert.Equal(testFixture, actual);
        }

        [Theory]
        [SatisfiedConstructorData]
        public void VerifySatisfiedConstructorDoesNotThrow(ConstructorInfo constructor)
        {
            var sut = new MemberInitializationAssertion(new FakeTestFixture());
            Assert.DoesNotThrow(() => sut.Verify(constructor));
        }

        [Theory]
        [UnsatisfiedConstructorData]
        public void VerifyUnsatisfiedConstructorThrows(ConstructorInfo constructor)
        {
            var sut = new MemberInitializationAssertion(new FakeTestFixture());
            Assert.Throws<MemberInitializationException>(() => sut.Verify(constructor));
        }

        [Theory]
        [SatisfiedFieldData]
        public void VerifySatisfiedFieldDoesNotThrow(FieldInfo field)
        {
            var sut = new MemberInitializationAssertion(new FakeTestFixture());
            Assert.DoesNotThrow(() => sut.Verify(field));
        }

        [Theory]
        [UnsatisfiedFieldData]
        public void VerifyUnsatisfiedFieldThrows(FieldInfo field)
        {
            var sut = new MemberInitializationAssertion(new FakeTestFixture());
            Assert.Throws<MemberInitializationException>(() => sut.Verify(field));
        }

        [Theory]
        [SatisfiedPropertyData]
        public void VerifySatisfiedPropertyDoesNotThrow(PropertyInfo property)
        {
            var sut = new MemberInitializationAssertion(new FakeTestFixture());
            Assert.DoesNotThrow(() => sut.Verify(property));
        }

        [Theory]
        [UnsatisfiedPropertyData]
        public void VerifyUnsatisfiedPropertyThrows(PropertyInfo property)
        {
            var sut = new MemberInitializationAssertion(new FakeTestFixture());
            Assert.Throws<MemberInitializationException>(() => sut.Verify(property));
        }

        [Fact]
        public void VerifyNullConstructorThrows()
        {
            var sut = new MemberInitializationAssertion(new DelegatingTestFixture());
            Assert.Throws<ArgumentNullException>(() => sut.Verify((ConstructorInfo)null));
        }

        private class SatisfiedConstructorDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                return typeof(ClassForSatisfiedConstructors).GetConstructors().Select(c => new object[] { c });
            }

            private class ClassForSatisfiedConstructors
            {
                public readonly string StringValue;
                public readonly int IntValue;

                public ClassForSatisfiedConstructors()
                {
                }

                public ClassForSatisfiedConstructors(string stringValue)
                {
                    StringValue = stringValue;
                }

                public ClassForSatisfiedConstructors(string stringValue, int intValue)
                {
                    StringValue = stringValue;
                    IntValue = intValue;
                }

                public ClassForSatisfiedConstructors(object objectValue, int intValue)
                {
                    ObjectValue = objectValue;
                    IntValue = intValue;
                }

                public ClassForSatisfiedConstructors(object objectValue, byte byteValue)
                {
                    ObjectValue = objectValue;
                    ByteValue = byteValue;
                }

                public object ObjectValue
                {
                    get;
                    set;
                }

                public byte ByteValue
                {
                    get;
                    set;
                }
            }
        }

        private class UnsatisfiedConstructorDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                return typeof(ClassForUnsatisfiedConstructors).GetConstructors().Select(c => new object[] { c });
            }

            private class ClassForUnsatisfiedConstructors
            {
                public readonly string StringValue;

                public ClassForUnsatisfiedConstructors(string stringValue)
                {
                }

                public ClassForUnsatisfiedConstructors(string stringValue, int intValue)
                {
                    StringValue = stringValue;
                }

                public ClassForUnsatisfiedConstructors(int intValue, object objectValue)
                {
                    ObjectValue = objectValue;
                }

                public object ObjectValue
                {
                    get;
                    set;
                }
            }
        }

        private class SatisfiedFieldDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                return typeof(ClassForSatisfiedFields).GetFields().Select(c => new object[] { c });
            }

            private class ClassForSatisfiedFields
            {
                public readonly object ObjectValue;
                public readonly string StringValue;
                public readonly int IntValue;

                public ClassForSatisfiedFields(object objectValue, string stringValue)
                {
                    ObjectValue = objectValue;
                }

                public ClassForSatisfiedFields(string stringValue, int intValue)
                {
                    StringValue = stringValue;
                    IntValue = intValue;
                }
            }
        }

        private class UnsatisfiedFieldDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                return typeof(ClassForUnsatisfiedFields).GetFields().Select(c => new object[] { c });
            }

            private class ClassForUnsatisfiedFields
            {
                public readonly object ObjectValue;
                public readonly string StringValue;
                public readonly int IntValue;

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

            private class ClassForSatisfiedProperties
            {
                private readonly int _intValue;

                public ClassForSatisfiedProperties(object objectValue, string stringValue)
                {
                    ObjectValue = objectValue;
                }

                public ClassForSatisfiedProperties(string stringValue, int intValue)
                {
                    StringValue = stringValue;
                    _intValue = intValue;
                }

                public object ObjectValue
                {
                    get;
                    set;
                }

                public string StringValue
                {
                    get;
                    private set;
                }

                public int IntValue
                {
                    get
                    {
                        return _intValue;
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

            private class ClassForUnsatisfiedProperties
            {
                public ClassForUnsatisfiedProperties(object objectValue, string stringValue)
                {
                }

                public ClassForUnsatisfiedProperties(string stringValue, int intValue)
                {
                }

                public object ObjectValue
                {
                    get;
                    set;
                }

                public string StringValue
                {
                    get;
                    private set;
                }

                public int IntValue
                {
                    get
                    {
                        return 10;
                    }
                }
            }
        }
    }
}