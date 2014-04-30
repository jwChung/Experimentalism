using System;
using System.Collections.Generic;
using System.Linq;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class MemberKindCollectorTest
    {
        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new MemberKindCollector();
            Assert.IsAssignableFrom<IReflectionVisitor<IEnumerable<MemberKinds>>>(sut);
        }

        [Fact]
        public void ValueIsCorrect()
        {
            var sut = new MemberKindCollector();
            var actual = sut.Value;
            Assert.Empty(actual);
        }

        [Fact]
        public void VisitFieldInfoElementCollectsCorrectMemberTypes()
        {
            var sut = new MemberKindCollector();
            var constructorInfoElement = typeof(ClassWithMembers).GetConstructors().First().ToElement();
            var fieldInfoElement = typeof(ClassWithMembers).GetFields().First().ToElement();

            var actual = sut.Visit(constructorInfoElement).Visit(fieldInfoElement);

            Assert.NotSame(sut, actual);
            Assert.Equal(new[] { MemberKinds.Constructor, MemberKinds.Field }, actual.Value);
        }

        [Fact]
        public void VisitNullFieldInfoElementThrows()
        {
            var sut = new MemberKindCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((FieldInfoElement)null));
        }

        [Fact]
        public void VisitConstructorInfoElementCollectsCorrectMemberTypes()
        {
            var sut = new MemberKindCollector();
            var fieldInfoElement = typeof(ClassWithMembers).GetFields().First().ToElement();
            var constructorInfoElement = typeof(ClassWithMembers).GetConstructors().First().ToElement();

            var actual = sut.Visit(fieldInfoElement).Visit(constructorInfoElement);

            Assert.NotSame(sut, actual);
            Assert.Equal(new[] { MemberKinds.Field, MemberKinds.Constructor }, actual.Value);
        }

        [Fact]
        public void VisitNullConstructorInfoElementThrows()
        {
            var sut = new MemberKindCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((ConstructorInfoElement)null));
        }

        [Fact]
        public void VisitGetPropertyInfoElementCollectsCorrectMemberTypes()
        {
            var sut = new MemberKindCollector();
            var fieldInfoElement = typeof(ClassWithMembers).GetFields().First().ToElement();
            var getPropertyInfoElement = typeof(ClassWithMembers).GetProperties()
                .First(p => p.GetSetMethod() == null).ToElement();

            var actual = sut.Visit(fieldInfoElement).Visit(getPropertyInfoElement);

            Assert.NotSame(sut, actual);
            Assert.Equal(new[] { MemberKinds.Field, MemberKinds.GetProperty }, actual.Value);
        }

        [Fact]
        public void VisitSetPropertyInfoElementCollectsCorrectMemberTypes()
        {
            var sut = new MemberKindCollector();
            var fieldInfoElement = typeof(ClassWithMembers).GetFields().First().ToElement();
            var setPropertyInfoElement = typeof(ClassWithMembers).GetProperties()
                .First(p => p.GetGetMethod() == null).ToElement();

            var actual = sut.Visit(fieldInfoElement).Visit(setPropertyInfoElement);

            Assert.NotSame(sut, actual);
            Assert.Equal(new[] { MemberKinds.Field, MemberKinds.SetProperty }, actual.Value);
        }

        [Fact]
        public void VisitGetSetPropertyInfoElementCollectsCorrectMemberTypes()
        {
            var sut = new MemberKindCollector();
            var fieldInfoElement = typeof(ClassWithMembers).GetFields().First().ToElement();
            var getSetPropertyInfoElement = typeof(ClassWithMembers).GetProperties()
                .First(p => p.GetGetMethod() != null && p.GetSetMethod() != null).ToElement();

            var actual = sut.Visit(fieldInfoElement).Visit(getSetPropertyInfoElement);

            Assert.NotSame(sut, actual);
            Assert.Equal(new[] { MemberKinds.Field, MemberKinds.Property }, actual.Value);
        }

        [Fact]
        public void VisitNullPropertyInfoElementThrows()
        {
            var sut = new MemberKindCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((PropertyInfoElement)null));
        }

        [Fact]
        public void VisitMethodInfoElementCollectsCorrectMemberTypes()
        {
            var sut = new MemberKindCollector();
            var fieldInfoElement = typeof(ClassWithMembers).GetFields().First().ToElement();
            var methodInfoElement = typeof(ClassWithMembers).GetMethods().First().ToElement();

            var actual = sut.Visit(fieldInfoElement).Visit(methodInfoElement);

            Assert.NotSame(sut, actual);
            Assert.Equal(new[] { MemberKinds.Field, MemberKinds.Method }, actual.Value);
        }

        [Fact]
        public void VisitNullMethodInfoElementThrows()
        {
            var sut = new MemberKindCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((MethodInfoElement)null));
        }

        [Fact]
        public void VisitEventInfoElementCollectsCorrectMemberTypes()
        {
            var sut = new MemberKindCollector();
            var fieldInfoElement = typeof(ClassWithMembers).GetFields().First().ToElement();
            var eventInfoElement = typeof(ClassWithMembers).GetEvents().First().ToElement();

            var actual = sut.Visit(fieldInfoElement).Visit(eventInfoElement);

            Assert.NotSame(sut, actual);
            Assert.Equal(new[] { MemberKinds.Field, MemberKinds.Event }, actual.Value);
        }

        [Fact]
        public void VisitNullEventInfoElementThrows()
        {
            var sut = new MemberKindCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((EventInfoElement)null));
        }
    }
}