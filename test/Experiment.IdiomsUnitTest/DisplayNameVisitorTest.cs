using System;
using System.Collections.Generic;
using System.Linq;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment
{
    public class DisplayNameVisitorTest
    {
        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new DisplayNameVisitor();
            Assert.IsAssignableFrom<IReflectionVisitor<IEnumerable<string>>>(sut);
        }

        [Fact]
        public void ValueIsCorrect()
        {
            var sut = new DisplayNameVisitor();
            var actual = sut.Value;
            Assert.Empty(actual);
        }

        [Fact]
        public void VisitAssemblyElementCollectsCorrectName()
        {
            var sut = new DisplayNameVisitor();
            var assembly = typeof(object).Assembly;
            var expected = assembly.ToElement().ToString();

            var actual = sut.Visit(assembly.ToElement());

            Assert.Equal(expected, actual.Value.Single());
        }

        [Fact]
        public void VisitNullAssemblyElementThrows()
        {
            var sut = new DisplayNameVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((AssemblyElement)null));
        }

        [Fact]
        public void VisitManyAssemblyElementsCollectsCorrectNames()
        {
            var sut = new DisplayNameVisitor();
            var assembly = typeof(object).Assembly;

            var actual1 = sut.Visit(assembly.ToElement());
            var actual2 = actual1.Visit(assembly.ToElement());

            Assert.Equal(2, actual2.Value.Count());
        }

        [Fact]
        public void VisitTypeElementCollectsCorrectName()
        {
            var sut = new DisplayNameVisitor();
            var type = typeof(string);
            var expected = type.ToElement().ToString();

            var actual = sut.Visit(type.ToElement());

            Assert.Equal(expected, actual.Value.Single());
        }

        [Fact]
        public void VisitManyTypeElementsCollectsCorrectNames()
        {
            var sut = new DisplayNameVisitor();
            var type = typeof(object);

            var actual1 = sut.Visit(type.ToElement());
            var actual2 = actual1.Visit(type.ToElement());

            Assert.Equal(2, actual2.Value.Count());
        }

        [Fact]
        public void VisitNullTypeElementThrows()
        {
            var sut = new DisplayNameVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((TypeElement)null));
        }

        [Fact]
        public void VisitConstructorInfoElementCollectsCorrectName()
        {
            var sut = new DisplayNameVisitor();
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
            var sut = new DisplayNameVisitor();
            var constructorInfo = typeof(string).GetConstructors().First();

            var actual1 = sut.Visit(constructorInfo.ToElement());
            var actual2 = actual1.Visit(constructorInfo.ToElement());

            Assert.Equal(2, actual2.Value.Count());
        }

        [Fact]
        public void VisitNullConstructorInfoElementThrows()
        {
            var sut = new DisplayNameVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((ConstructorInfoElement)null));
        }

        [Fact]
        public void VisitFieldInfoElementCollectsCorrectName()
        {
            var sut = new DisplayNameVisitor();
            var fieldInfo = typeof(TypeWithMembers).GetFields().First();
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
            var sut = new DisplayNameVisitor();
            var fieldInfo = typeof(TypeWithMembers).GetFields().First();

            var actual1 = sut.Visit(fieldInfo.ToElement());
            var actual2 = actual1.Visit(fieldInfo.ToElement());

            Assert.Equal(2, actual2.Value.Count());
        }

        [Fact]
        public void VisitNullFieldInfoElementThrows()
        {
            var sut = new DisplayNameVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((FieldInfoElement)null));
        }

        [Fact]
        public void VisitPropertyInfoElementCollectsCorrectName()
        {
            var sut = new DisplayNameVisitor();
            var propertyInfo = typeof(TypeWithMembers).GetProperties().First();
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
            var sut = new DisplayNameVisitor();
            var propertyInfo = typeof(TypeWithMembers).GetProperties().First();

            var actual1 = sut.Visit(propertyInfo.ToElement());
            var actual2 = actual1.Visit(propertyInfo.ToElement());

            Assert.Equal(2, actual2.Value.Count());
        }

        [Fact]
        public void VisitNullPropertyInfoElementThrows()
        {
            var sut = new DisplayNameVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((PropertyInfoElement)null));
        }

        [Fact]
        public void VisitMethodInfoElementCollectsCorrectName()
        {
            var sut = new DisplayNameVisitor();
            var methodInfo = typeof(TypeWithMembers).GetMethods().First();
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
            var sut = new DisplayNameVisitor();
            var propertyInfo = typeof(TypeWithMembers).GetMethods().First();

            var actual1 = sut.Visit(propertyInfo.ToElement());
            var actual2 = actual1.Visit(propertyInfo.ToElement());

            Assert.Equal(2, actual2.Value.Count());
        }

        [Fact]
        public void VisitNullMethodInfoElementThrows()
        {
            var sut = new DisplayNameVisitor();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((MethodInfoElement)null));
        }
    }
}