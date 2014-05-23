using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public void ValueIsCorrectWhenInitializedWithMemberKinds()
        {
            var memberKinds = new[] { MemberKinds.InstanceProperty, MemberKinds.StaticField };
            var sut = new MemberKindCollector(memberKinds);

            var actual = sut.Value;

            Assert.Equal(memberKinds, actual);
        }

        [Fact]
        public void VisitFieldInfoElementCollectsCorrectMemberKind()
        {
            var sut = new MemberKindCollector();
            var constructorInfoElement = typeof(ClassWithMembers).GetConstructors().First().ToElement();
            var fieldInfoElement = new Fields<ClassWithMembers>().Select(x => x.PublicField).ToElement();

            var actual = sut.Visit(constructorInfoElement).Visit(fieldInfoElement);

            Assert.NotSame(sut, actual);
            Assert.Equal(new[] { MemberKinds.InstanceConstructor, MemberKinds.InstanceField }, actual.Value);
        }

        [Fact]
        public void VisitNullFieldInfoElementThrows()
        {
            var sut = new MemberKindCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((FieldInfoElement)null));
        }

        [Fact]
        public void VisitConstructorInfoElementCollectsCorrectMemberKind()
        {
            var sut = new MemberKindCollector();
            var fieldInfoElement = new Fields<ClassWithMembers>().Select(x => x.PublicField).ToElement();
            var constructorInfoElement = typeof(ClassWithMembers).GetConstructors().First().ToElement();

            var actual = sut.Visit(fieldInfoElement).Visit(constructorInfoElement);

            Assert.NotSame(sut, actual);
            Assert.Equal(new[] { MemberKinds.InstanceField, MemberKinds.InstanceConstructor }, actual.Value);
        }

        [Fact]
        public void VisitNullConstructorInfoElementThrows()
        {
            var sut = new MemberKindCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((ConstructorInfoElement)null));
        }

        [Fact]
        public void VisitGetPropertyInfoElementCollectsCorrectMemberKind()
        {
            var sut = new MemberKindCollector();
            var fieldInfoElement = new Fields<ClassWithMembers>().Select(x => x.PublicField).ToElement();
            var getPropertyInfoElement = typeof(ClassWithMembers)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .First(p => p.GetSetMethod(true) == null && p.GetGetMethod(true) != null)
                .ToElement();

            var actual = sut.Visit(fieldInfoElement).Visit(getPropertyInfoElement);

            Assert.NotSame(sut, actual);
            Assert.Equal(new[] { MemberKinds.InstanceField, MemberKinds.InstanceGetProperty }, actual.Value);
        }

        [Fact]
        public void VisitSetPropertyInfoElementCollectsCorrectMemberKind()
        {
            var sut = new MemberKindCollector();
            var fieldInfoElement = new Fields<ClassWithMembers>().Select(x => x.PublicField).ToElement();
            var setPropertyInfoElement = typeof(ClassWithMembers)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .First(p => p.GetGetMethod(true) == null && p.GetSetMethod(true) != null)
                .ToElement();

            var actual = sut.Visit(fieldInfoElement).Visit(setPropertyInfoElement);

            Assert.NotSame(sut, actual);
            Assert.Equal(new[] { MemberKinds.InstanceField, MemberKinds.InstanceSetProperty }, actual.Value);
        }

        [Fact]
        public void VisitGetSetPropertyInfoElementCollectsCorrectMemberKind()
        {
            var sut = new MemberKindCollector();
            var fieldInfoElement = new Fields<ClassWithMembers>().Select(x => x.PublicField).ToElement();
            var getSetPropertyInfoElement = typeof(ClassWithMembers)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .First(p => p.GetGetMethod(true) != null && p.GetSetMethod(true) != null)
                .ToElement();

            var actual = sut.Visit(fieldInfoElement).Visit(getSetPropertyInfoElement);

            Assert.NotSame(sut, actual);
            Assert.Equal(new[] { MemberKinds.InstanceField, MemberKinds.InstanceProperty }, actual.Value);
        }

        [Fact]
        public void VisitNullPropertyInfoElementThrows()
        {
            var sut = new MemberKindCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((PropertyInfoElement)null));
        }

        [Fact]
        public void VisitMethodInfoElementCollectsCorrectMemberKind()
        {
            var sut = new MemberKindCollector();
            var fieldInfoElement = new Fields<ClassWithMembers>().Select(x => x.PublicField).ToElement();
            var methodInfoElement = typeof(ClassWithMembers)
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .First().ToElement();

            var actual = sut.Visit(fieldInfoElement).Visit(methodInfoElement);

            Assert.NotSame(sut, actual);
            Assert.Equal(new[] { MemberKinds.InstanceField, MemberKinds.InstanceMethod }, actual.Value);
        }

        [Fact]
        public void VisitNullMethodInfoElementThrows()
        {
            var sut = new MemberKindCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((MethodInfoElement)null));
        }

        [Fact]
        public void VisitEventInfoElementCollectsCorrectMemberKind()
        {
            var sut = new MemberKindCollector();
            var fieldInfoElement = new Fields<ClassWithMembers>().Select(x => x.PublicField).ToElement();
            var eventInfoElement = typeof(ClassWithMembers)
                .GetEvents(BindingFlags.Instance | BindingFlags.Public)
                .First().ToElement();

            var actual = sut.Visit(fieldInfoElement).Visit(eventInfoElement);

            Assert.NotSame(sut, actual);
            Assert.Equal(new[] { MemberKinds.InstanceField, MemberKinds.InstanceEvent }, actual.Value);
        }

        [Fact]
        public void VisitNullEventInfoElementThrows()
        {
            var sut = new MemberKindCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((EventInfoElement)null));
        }

        [Fact]
        public void VisitNonPublicPropertyInfoElementCollectsCorrectMemberKind()
        {
            var sut = new MemberKindCollector();
            var propertyInfoElement = typeof(ClassWithMembers)
                .GetProperties(BindingFlags.NonPublic | BindingFlags.Instance)
                .First(p => p.GetGetMethod(true) != null && p.GetSetMethod(true) != null)
                .ToElement();

            var actual = sut.Visit(propertyInfoElement);

            Assert.NotSame(sut, actual);
            Assert.Equal(new[] { MemberKinds.InstanceProperty }, actual.Value);
        }

        [Fact]
        public void VisitStaticFieldInfoElementCollectsCorrectMemberKind()
        {
            var sut = new MemberKindCollector(new[] { MemberKinds.StaticEvent });
            var fieldInfoElement = typeof(ClassWithMembers)
                .GetFields(BindingFlags.Static | BindingFlags.Public)
                .First().ToElement();

            var actual = sut.Visit(fieldInfoElement);

            Assert.NotSame(sut, actual);
            Assert.Equal(new[] { MemberKinds.StaticEvent, MemberKinds.StaticField }, actual.Value);
        }

        [Fact]
        public void VisitStaticConstructorInfoElementCollectsCorrectMemberKind()
        {
            var sut = new MemberKindCollector(new[] { MemberKinds.StaticEvent });
            var constructorInfoElement = typeof(ClassWithMembers)
                .GetConstructors(BindingFlags.Static | BindingFlags.NonPublic)
                .First().ToElement();

            var actual = sut.Visit(constructorInfoElement);

            Assert.NotSame(sut, actual);
            Assert.Equal(new[] { MemberKinds.StaticEvent, MemberKinds.StaticConstructor }, actual.Value);
        }

        [Fact]
        public void VisitStaticGetPropertyInfoElementCollectsCorrectMemberKind()
        {
            var sut = new MemberKindCollector(new[] { MemberKinds.InstanceField });
            var getPropertyInfoElement = typeof(ClassWithMembers)
                .GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .First(p => p.GetSetMethod(true) == null && p.GetGetMethod(true) != null)
                .ToElement();

            var actual = sut.Visit(getPropertyInfoElement);

            Assert.NotSame(sut, actual);
            Assert.Equal(new[] { MemberKinds.InstanceField, MemberKinds.StaticGetProperty }, actual.Value);
        }

        [Fact]
        public void VisitStaticSetPropertyInfoElementCollectsCorrectMemberKind()
        {
            var sut = new MemberKindCollector(new[] { MemberKinds.InstanceField });
            var setPropertyInfoElement = typeof(ClassWithMembers)
                .GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .First(p => p.GetGetMethod(true) == null && p.GetSetMethod(true) != null)
                .ToElement();

            var actual = sut.Visit(setPropertyInfoElement);

            Assert.NotSame(sut, actual);
            Assert.Equal(new[] { MemberKinds.InstanceField, MemberKinds.StaticSetProperty }, actual.Value);
        }

        [Fact]
        public void VisitStaticMethodInfoElementCollectsCorrectMemberKind()
        {
            var sut = new MemberKindCollector(new[] { MemberKinds.InstanceField });
            var methodInfoElement = typeof(ClassWithMembers)
                .GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .First().ToElement();

            var actual = sut.Visit(methodInfoElement);

            Assert.NotSame(sut, actual);
            Assert.Equal(new[] { MemberKinds.InstanceField, MemberKinds.StaticMethod }, actual.Value);
        }

        [Fact]
        public void VisitStaticEventInfoElementCollectsCorrectMemberKind()
        {
            var sut = new MemberKindCollector(new[] { MemberKinds.InstanceField });
            var eventInfoElement = typeof(ClassWithMembers)
                .GetEvents(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .First().ToElement();

            var actual = sut.Visit(eventInfoElement);

            Assert.NotSame(sut, actual);
            Assert.Equal(new[] { MemberKinds.InstanceField, MemberKinds.StaticEvent }, actual.Value);
        }
    }
}