﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment.Idioms
{
    public class ConstructingMemberAssertionTest
    {
        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new ConstructingMemberAssertion(
                EqualityComparer<IReflectionElement>.Default,
                EqualityComparer<IReflectionElement>.Default);
            Assert.IsAssignableFrom<IReflectionVisitor<object>>(sut);
        }

        [Fact]
        public void ParameterToMemberComparerIsCorrect()
        {
            var constructorComparer = new DelegatingReflectionElementComparer();
            var sut = new ConstructingMemberAssertion(
                   constructorComparer,
                   EqualityComparer<IReflectionElement>.Default);

            var actual = sut.ParameterToMemberComparer;

            Assert.Equal(constructorComparer, actual);
        }

        [Fact]
        public void MemberToParameterComparerIsCorrect()
        {
            var memberComparer = EqualityComparer<IReflectionElement>.Default;
            var sut = new ConstructingMemberAssertion(
                   EqualityComparer<IReflectionElement>.Default,
                   memberComparer);

            var actual = sut.MemberToParameterComparer;

            Assert.Equal(memberComparer, actual);
        }

        [Fact]
        public void InitializeWithNullParameterToMemberComparerThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new ConstructingMemberAssertion(
                    null,
                    EqualityComparer<IReflectionElement>.Default));
        }

        [Fact]
        public void InitializeWithNullMemberToParameterComparerThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new ConstructingMemberAssertion(
                    EqualityComparer<IReflectionElement>.Default,
                    null));
        }

        [Fact]
        public void VisitNotSatisfiedConstructorInfoElementInspectsAllPublicFields()
        {
            // Fixture setup
            var memberInfos = new List<MemberInfo>();
            var constructorComparer = new DelegatingReflectionElementComparer
            {
                OnEquals = (x, y) =>
                {
                    var fieldInfoElement = y as FieldInfoElement;
                    if (fieldInfoElement != null)
                        memberInfos.Add(fieldInfoElement.FieldInfo);
                    return false;
                }
            };

            var sut = new ConstructingMemberAssertion(
                   constructorComparer,
                   EqualityComparer<IReflectionElement>.Default);

            var constructorInfoElement = Constructors
                .Select(() => new TypeWithMembers("string", 1234)).ToElement();

            var expected = new MemberInfo[]
            {
                new Fields<TypeWithMembers>().Select(x => x.OtherPublicField),
                new Fields<TypeWithMembers>().Select(x => x.PublicField)
            };

            // Exercise system and Verify outcome
            Assert.Throws<ConstructingMemberException>(() => sut.Visit(constructorInfoElement));
            Assert.Equal(expected, memberInfos.OrderBy(mi => mi.Name).Distinct());
        }

        [Fact]
        public void VisitNotSatisfiedConstructorInfoElementInspectsAllPublicProperties()
        {
            // Fixture setup
            var memberInfos = new List<MemberInfo>();
            var constructorComparer = new DelegatingReflectionElementComparer
            {
                OnEquals = (x, y) =>
                {
                    var propertyInfoElement = y as PropertyInfoElement;
                    if (propertyInfoElement != null)
                        memberInfos.Add(propertyInfoElement.PropertyInfo);
                    return false;
                }
            };

            var sut = new ConstructingMemberAssertion(
                   constructorComparer,
                   EqualityComparer<IReflectionElement>.Default);

            var constructorInfoElement = Constructors
                .Select(() => new TypeWithMembers("string", 1234)).ToElement();

            var expected = new MemberInfo[]
            {
                new Properties<TypeWithMembers>().Select(x => x.PublicProperty),
                new Properties<TypeWithMembers>().Select(x => x.ReadOnlyProperty)
            };

            // Exercise system and Verify outcome
            Assert.Throws<ConstructingMemberException>(() => sut.Visit(constructorInfoElement));
            Assert.Equal(expected, memberInfos.OrderBy(mi => mi.Name).Distinct());
        }

        [Fact]
        public void VisitSatisfiedConstructorInfoElementDoesNotThrow()
        {
            // Fixture setup
            var sut = new ConstructingMemberAssertion(
                new ParameterToPropertyComparer(new FakeTestFixture()), 
                EqualityComparer<IReflectionElement>.Default);

            var constructorInfoElement = Constructors
                .Select(() => new TypeForTestConstructor(null, null, 0)).ToElement();
            
            // Exercise system
            var actual = sut.Visit(constructorInfoElement);

            // Verify outcome
            Assert.Equal(sut, actual);
        }

        [Theory]
        [NotSatisfiedConstructorInfoElementData]
        public void VisitNotSatisfiedConstructorInfoElementThrows(
            ConstructorInfoElement constructorInfoElement)
        {
            var sut = new ConstructingMemberAssertion(
                new ParameterToPropertyComparer(new FakeTestFixture()),
                EqualityComparer<IReflectionElement>.Default);
            Assert.Throws<ConstructingMemberException>(() => sut.Visit(constructorInfoElement));
        }

        [Fact]
        public void VisitNullConstructorInfoElementThrows()
        {
            var sut = new ConstructingMemberAssertion(
                EqualityComparer<IReflectionElement>.Default,
                EqualityComparer<IReflectionElement>.Default);
            Assert.Throws<ArgumentNullException>(() => sut.Visit((ConstructorInfoElement)null));
        }

        [Fact]
        public void ValueThrowsNotSupportedException()
        {
            var sut = new ConstructingMemberAssertion(
                EqualityComparer<IReflectionElement>.Default,
                EqualityComparer<IReflectionElement>.Default);
            Assert.Throws<NotSupportedException>(() => sut.Value);
        }

        [Fact]
        public void VisitNullFieldInfoElementThrows()
        {
            var sut = new ConstructingMemberAssertion(
                EqualityComparer<IReflectionElement>.Default,
                EqualityComparer<IReflectionElement>.Default);
            Assert.Throws<ArgumentNullException>(() => sut.Visit((FieldInfoElement)null));
        }

        [Fact]
        public void VisitNotSatisfiedFieldInfoElementThrows()
        {
            var sut = new ConstructingMemberAssertion(
                EqualityComparer<IReflectionElement>.Default,
                new FieldToParameterComparer(new FakeTestFixture()));
            FieldInfoElement fieldInfoElement = new Fields<TypeForTestField>()
                .Select(x => x.NotSatisfied).ToElement();
            Assert.Throws<ConstructingMemberException>(() => sut.Visit(fieldInfoElement));
        }

        [Theory]
        [SatisfiedFieldInfoElementData]
        public void VisitSatisfiedFieldInfoElementDoesNotThrow(
            FieldInfoElement fieldInfoElement)
        {
            var sut = new ConstructingMemberAssertion(
                EqualityComparer<IReflectionElement>.Default,
                new FieldToParameterComparer(new FakeTestFixture()));
            var actual = sut.Visit(fieldInfoElement);
            Assert.Equal(sut, actual);
        }

        [Fact]
        public void VisitNullPropertyInfoElementThrows()
        {
            var sut = new ConstructingMemberAssertion(
                EqualityComparer<IReflectionElement>.Default,
                EqualityComparer<IReflectionElement>.Default);
            Assert.Throws<ArgumentNullException>(() => sut.Visit((PropertyInfoElement)null));
        }

        [Fact]
        public void VisitNotSatisfiedPropertyInfoElementThrows()
        {
            var sut = new ConstructingMemberAssertion(
                EqualityComparer<IReflectionElement>.Default,
                new PropertyToParameterComparer(new FakeTestFixture()));
            PropertyInfoElement propertyInfoElement = new Properties<TypeForTestProperty>()
                .Select(x => x.NotSatisfied).ToElement();
            Assert.Throws<ConstructingMemberException>(() => sut.Visit(propertyInfoElement));
        }

        [Theory]
        [SatisfiedPropertyInfoElementData]
        public void VisitSatisfiedPropertyInfoElementDoesNotThrow(
            PropertyInfoElement propertyFieldInfoElement)
        {
            var sut = new ConstructingMemberAssertion(
                EqualityComparer<IReflectionElement>.Default,
                new PropertyToParameterComparer(new FakeTestFixture()));
            var actual = sut.Visit(propertyFieldInfoElement);
            Assert.Equal(sut, actual);
        }

        private class NotSatisfiedConstructorInfoElementDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                yield return new object[]
                {
                    Constructors.Select(() => new TypeForTestConstructor(null, 0)).ToElement()
                };
                yield return new object[]
                {
                    Constructors.Select(() => new TypeForTestConstructor(0, null)).ToElement()
                };
                yield return new object[]
                {
                    Constructors.Select(() => new TypeForTestConstructor(0, 0.1)).ToElement()
                };
            }
        }

        private class SatisfiedFieldInfoElementDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                yield return new object[]
                {
                    new Fields<TypeForTestField>().Select(x => x.Satisfied1).ToElement()
                };
                yield return new object[]
                {
                    new Fields<TypeForTestField>().Select(x => x.Satisfied2).ToElement()
                };
            }
        }

        private class SatisfiedPropertyInfoElementDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                yield return new object[]
                {
                    new Properties<TypeForTestProperty>().Select(x => x.Satisfied1).ToElement()
                };
                yield return new object[]
                {
                    new Properties<TypeForTestProperty>().Select(x => x.Satisfied2).ToElement()
                };
            }
        }

        private class TypeForTestConstructor
        {
            private readonly string _stringValue;
            private readonly object _objectValue;
            private readonly int _intValue;

            public TypeForTestConstructor(string stringValue, object objectValue, int intValue)
            {
                _stringValue = stringValue;
                _objectValue = objectValue;
                _intValue = intValue;
            }

            public TypeForTestConstructor(object objectValue, int intValue)
            {
            }

            public TypeForTestConstructor(int intValue, string stringValue)
            {
                _stringValue = stringValue;
            }

            public TypeForTestConstructor(int intValue, double doubleValue)
            {
                _intValue = intValue;
            }

            public int IntValue
            {
                get
                {
                    return _intValue;
                }
            }

            public string StringValue
            {
                get
                {
                    return _stringValue;
                }
            }

            public object ObjectValue
            {
                get
                {
                    return _objectValue;
                }
            }
        }

        private class TypeForTestField
        {
            public object NotSatisfied;

            public object Satisfied1;

            public object Satisfied2;

            public TypeForTestField()
            {
            }

            public TypeForTestField(string arg1, object arg2)
            {
            }

            public TypeForTestField(int value)
            {
                Satisfied1 = value;
            }

            public TypeForTestField(string arg1, int arg2, object arg3)
            {
                Satisfied2 = arg2;
            }

            protected internal TypeForTestField(object arg1, string arg2)
            {
                NotSatisfied = arg1;
            }

            protected TypeForTestField(string arg1, int arg2)
            {
                NotSatisfied = arg2;
            }

            internal TypeForTestField(object arg)
            {
                NotSatisfied = arg;
            }

            private TypeForTestField(string arg)
            {
                NotSatisfied = arg;
            }
        }

        private class TypeForTestProperty
        {
            public TypeForTestProperty()
            {
            }

            public TypeForTestProperty(string arg1, object arg2)
            {
            }

            public TypeForTestProperty(int value)
            {
                Satisfied1 = value;
            }

            public TypeForTestProperty(string arg1, int arg2, object arg3)
            {
                Satisfied2 = arg2;
            }

            protected internal TypeForTestProperty(object arg1, string arg2)
            {
                NotSatisfied = arg1;
            }

            protected TypeForTestProperty(string arg1, int arg2)
            {
                NotSatisfied = arg2;
            }

            internal TypeForTestProperty(object arg)
            {
                NotSatisfied = arg;
            }

            private TypeForTestProperty(string arg)
            {
                NotSatisfied = arg;
            }

            public object NotSatisfied
            {
                get;
                set;
            }

            public object Satisfied1
            {
                get;
                set;
            }

            public object Satisfied2
            {
                get;
                set;
            }
        }
    }
}