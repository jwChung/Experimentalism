using System;
using System.Collections.Generic;
using System.Linq;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class DisplayNameCollectorTest
    {
        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new DisplayNameCollector();
            Assert.IsAssignableFrom<IReflectionVisitor<IEnumerable<string>>>(sut);
        }

        [Fact]
        public void ValueIsCorrect()
        {
            var sut = new DisplayNameCollector();
            var actual = sut.Value;
            Assert.Empty(actual);
        }

        [Fact]
        public void VisitAssemblyElementCollectsCorrectName()
        {
            var sut = new DisplayNameCollector();
            var assembly = typeof(object).Assembly;
            var expected = assembly.ToElement().ToString();

            var actual = sut.Visit(assembly.ToElement());

            Assert.Equal(expected, actual.Value.Single());
        }

        [Fact]
        public void VisitNullAssemblyElementThrows()
        {
            var sut = new DisplayNameCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((AssemblyElement)null));
        }

        [Fact]
        public void VisitManyAssemblyElementsCollectsCorrectNames()
        {
            var sut = new DisplayNameCollector();
            var assembly = typeof(object).Assembly;

            var actual1 = sut.Visit(assembly.ToElement());
            var actual2 = actual1.Visit(assembly.ToElement());

            Assert.Equal(2, actual2.Value.Count());
        }

        [Fact]
        public void VisitTypeElementCollectsCorrectName()
        {
            var sut = new DisplayNameCollector();
            var type = typeof(string);
            var expected = type.ToElement().ToString();

            var actual = sut.Visit(type.ToElement());

            Assert.Equal(expected, actual.Value.Single());
        }

        [Fact]
        public void VisitManyTypeElementsCollectsCorrectNames()
        {
            var sut = new DisplayNameCollector();
            var type = typeof(object);

            var actual1 = sut.Visit(type.ToElement());
            var actual2 = actual1.Visit(type.ToElement());

            Assert.Equal(2, actual2.Value.Count());
        }

        [Fact]
        public void VisitNullTypeElementThrows()
        {
            var sut = new DisplayNameCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((TypeElement)null));
        }

        [Fact]
        public void VisitConstructorInfoElementCollectsCorrectName()
        {
            var sut = new DisplayNameCollector();
            var constructorInfo = typeof(string).GetConstructors().First();
            var expected = string.Format(
                "[[{0}][{1}]] (constructor)",
                constructorInfo.ReflectedType,
                constructorInfo);

            var actual = sut.Visit(constructorInfo.ToElement());

            Assert.Equal(expected, actual.Value.Single());
        }

        [Fact]
        public void VisitManyConstructorInfoElementsCollectsCorrectNames()
        {
            var sut = new DisplayNameCollector();
            var constructorInfo = typeof(string).GetConstructors().First();

            var actual1 = sut.Visit(constructorInfo.ToElement());
            var actual2 = actual1.Visit(constructorInfo.ToElement());

            Assert.Equal(2, actual2.Value.Count());
        }

        [Fact]
        public void VisitNullConstructorInfoElementThrows()
        {
            var sut = new DisplayNameCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((ConstructorInfoElement)null));
        }

        [Fact]
        public void VisitFieldInfoElementCollectsCorrectName()
        {
            var sut = new DisplayNameCollector();
            var fieldInfo = typeof(ClassWithMembers).GetFields().First();
            var expected = string.Format(
                "[[{0}][{1}]] (field)",
                fieldInfo.ReflectedType,
                fieldInfo);

            var actual = sut.Visit(fieldInfo.ToElement());

            Assert.Equal(expected, actual.Value.Single());
        }

        [Fact]
        public void VisitManyFieldInfoElementsCollectsCorrectNames()
        {
            var sut = new DisplayNameCollector();
            var fieldInfo = typeof(ClassWithMembers).GetFields().First();

            var actual1 = sut.Visit(fieldInfo.ToElement());
            var actual2 = actual1.Visit(fieldInfo.ToElement());

            Assert.Equal(2, actual2.Value.Count());
        }

        [Fact]
        public void VisitNullFieldInfoElementThrows()
        {
            var sut = new DisplayNameCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((FieldInfoElement)null));
        }

        [Fact]
        public void VisitPropertyInfoElementCollectsCorrectName()
        {
            var sut = new DisplayNameCollector();
            var propertyInfo = typeof(ClassWithMembers).GetProperties().First();
            var expected = string.Format(
                "[[{0}][{1}]] (property)",
                propertyInfo.ReflectedType,
                propertyInfo);

            var actual = sut.Visit(propertyInfo.ToElement());

            Assert.Equal(expected, actual.Value.Single());
        }

        [Fact]
        public void VisitManyPropertyInfoElementsCollectsCorrectNames()
        {
            var sut = new DisplayNameCollector();
            var propertyInfo = typeof(ClassWithMembers).GetProperties().First();

            var actual1 = sut.Visit(propertyInfo.ToElement());
            var actual2 = actual1.Visit(propertyInfo.ToElement());

            Assert.Equal(2, actual2.Value.Count());
        }

        [Fact]
        public void VisitNullPropertyInfoElementThrows()
        {
            var sut = new DisplayNameCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((PropertyInfoElement)null));
        }

        [Fact]
        public void VisitMethodInfoElementCollectsCorrectName()
        {
            var sut = new DisplayNameCollector();
            var methodInfo = typeof(ClassWithMembers).GetMethods().First();
            var expected = string.Format(
                "[[{0}][{1}]] (method)",
                methodInfo.ReflectedType,
                methodInfo);

            var actual = sut.Visit(methodInfo.ToElement());

            Assert.Equal(expected, actual.Value.Single());
        }

        [Fact]
        public void VisitManyMethodInfoElementsCollectsCorrectNames()
        {
            var sut = new DisplayNameCollector();
            var methodInfo = typeof(ClassWithMembers).GetMethods().First();

            var actual1 = sut.Visit(methodInfo.ToElement());
            var actual2 = actual1.Visit(methodInfo.ToElement());

            Assert.Equal(2, actual2.Value.Count());
        }

        [Fact]
        public void VisitNullMethodInfoElementThrows()
        {
            var sut = new DisplayNameCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((MethodInfoElement)null));
        }

        [Fact]
        public void VisitEventInfoElementCollectsCorrectName()
        {
            var sut = new DisplayNameCollector();
            var eventInfo = typeof(ClassWithMembers).GetEvents().First();
            var expected = string.Format(
                "[[{0}][{1}]] (event)",
                eventInfo.ReflectedType,
                eventInfo);

            var actual = sut.Visit(eventInfo.ToElement());

            Assert.Equal(expected, actual.Value.Single());
        }

        [Fact]
        public void VisitManyEventInfoElementsCollectsCorrectNames()
        {
            var sut = new DisplayNameCollector();
            var eventInfo = typeof(ClassWithMembers).GetEvents().First();

            var actual1 = sut.Visit(eventInfo.ToElement());
            var actual2 = actual1.Visit(eventInfo.ToElement());

            Assert.Equal(2, actual2.Value.Count());
        }

        [Fact]
        public void VisitNullEventInfoElementThrows()
        {
            var sut = new DisplayNameCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((EventInfoElement)null));
        }
    }
}