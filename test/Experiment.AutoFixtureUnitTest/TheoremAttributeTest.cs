﻿using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Ploeh.AutoFixture.Xunit;
using Xunit;

namespace Jwc.Experiment
{
    public class TheoremAttributeTest
    {
        [Fact]
        public void SutIsTheoremAttribute()
        {
            var sut = new TheoremAttribute();
            Assert.IsAssignableFrom<NaiveTheoremAttribute>(sut);
        }

        [Fact]
        public void FixtureTypeIsCorrect()
        {
            var sut = new TheoremAttribute();
            var actual = sut.FixtureType;
            Assert.Equal(typeof(TestFixtureAdapter), actual);
        }

        [Fact]
        public void FixtureFactoryIsCorrect()
        {
            var sut = new TheoremAttribute();
            var dummyMethod = typeof(object).GetMethod("ToString");

            var actual = sut.FixtureFactory(dummyMethod);

            var adapter = Assert.IsType<TestFixtureAdapter>(actual);
            var context = Assert.IsType<SpecimenContext>(adapter.SpecimenContext);
            Assert.IsType<Fixture>(context.Builder);
        }

        [Fact]
        public void FixtureFactoryAlwaysCreatesNewInstance()
        {
            var sut = new TheoremAttribute();
            var dummyMethod = typeof(object).GetMethod("ToString");

            var actual = sut.FixtureFactory(dummyMethod);

            Assert.NotNull(actual);
            Assert.NotSame(sut.FixtureFactory(dummyMethod), actual);
        }

        [Fact]
        public void FixtureFactoryReflectsCustomizeAttribute()
        {
            var sut = new TheoremAttribute();
            var actual = sut.FixtureFactory(GetType().GetMethod("FrozenTest"));
            Assert.Same(actual.Create(typeof(string)), actual.Create(typeof(string)));
        }

        [Fact]
        public void FixtureFactoryReflectsComplexCustomizeAttributes()
        {
            var sut = new TheoremAttribute();

            var actual = sut.FixtureFactory(GetType().GetMethod("PersonTest"));

            var name = (string)actual.Create(typeof(string));
            var age = (int)actual.Create(typeof(int));
            var person = (Person)actual.Create(typeof(Person));
            var other = actual.Create(typeof(object));
            Assert.Same(name, person.Name);
            Assert.Equal(age, person.Age);
            Assert.NotNull(other);
        }

        public void FrozenTest([Frozen] string arg)
        {
        }

        public void PersonTest([Frozen] string name, [Frozen] int age, Person person, object other)
        {
        }

        public class Person
        {
            private readonly string _name;
            private readonly int _age;

            public Person(string name, int age)
            {
                _name = name;
                _age = age;
            }

            public string Name
            {
                get
                {
                    return _name;
                }
            }

            public int Age
            {
                get
                {
                    return _age;
                }
            }
        }
    }
}