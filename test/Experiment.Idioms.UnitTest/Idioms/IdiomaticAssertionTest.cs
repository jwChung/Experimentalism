namespace Jwc.Experiment.Idioms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Jwc.Experiment.Xunit;
    using Moq;
    using Ploeh.Albedo;
    using global::Xunit;

    public class IdiomaticAssertionTest
    {
        [Fact]
        public void SutIsIdiomaticAssemblyAssertion()
        {
            var sut = new Mock<IdiomaticAssertion> { CallBase = true }.Object;
            Assert.IsAssignableFrom<IIdiomaticAssertion>(sut);
        }

        [Fact]
        public void VerifyAssemblyCorrectlyVerifies()
        {
            var sut = new Mock<IdiomaticAssertion> { CallBase = true }.Object;
            var types = new List<MemberInfo>();
            sut.ToMock().Setup(x => x.Verify(It.IsAny<Type>())).Callback<Type>(types.Add);
            var assembly = typeof(TestBaseAttribute).Assembly;
            var expected = assembly.GetExportedTypes();

            sut.Verify(assembly);

            Assert.Equal(expected, types);
        }

        [Fact]
        public void VerifyNullAssemblyThrows()
        {
            var sut = new Mock<IdiomaticAssertion> { CallBase = true }.Object;
            Assert.Throws<ArgumentNullException>(() => sut.Verify((Assembly)null));
        }

        [Fact]
        public void VerifyTypeVerifiesCorrectMembers()
        {
            var sut = new Mock<IdiomaticAssertion> { CallBase = true }.Object;
            var members = new List<MemberInfo>();
            sut.ToMock().Setup(x => x.Verify(It.IsAny<MemberInfo>())).Callback<MemberInfo>(members.Add);
            var type = typeof(ClassWithMembers);
            var expected = new IdiomaticMembers(type);

            sut.Verify(type);

            Assert.Equal(expected.OrderBy(m => m.Name), members.OrderBy(m => m.Name));
        }

        [Fact]
        public void VerifyNullTypeThrows()
        {
            var sut = new Mock<IdiomaticAssertion> { CallBase = true }.Object;
            Assert.Throws<ArgumentNullException>(() => sut.Verify((Type)null));
        }

        [Fact]
        public void VerifyFieldMemberCallsVerifyField()
        {
            var sut = new Mock<IdiomaticAssertion> { CallBase = true }.Object;
            var field = new Fields<ClassWithMembers>().Select(x => x.PublicField);

            sut.Verify((MemberInfo)field);

            sut.ToMock().Verify(x => x.Verify(field));
        }

        [Fact]
        public void VerifyConstructorMemberCallsVerifyConstructor()
        {
            var sut = new Mock<IdiomaticAssertion> { CallBase = true }.Object;
            var constructor = Constructors.Select(() => new ClassWithMembers());

            sut.Verify((MemberInfo)constructor);

            sut.ToMock().Verify(x => x.Verify(constructor));
        }

        [Fact]
        public void VerifyPropertyMemberCallsVerifyProperty()
        {
            var sut = new Mock<IdiomaticAssertion> { CallBase = true }.Object;
            var property = new Properties<ClassWithMembers>().Select(x => x.PublicProperty);

            sut.Verify((MemberInfo)property);

            sut.ToMock().Verify(x => x.Verify(property));
        }

        [Fact]
        public void VerifyMethodMemberCallsVerifyMethod()
        {
            var sut = new Mock<IdiomaticAssertion> { CallBase = true }.Object;
            var method = new Methods<ClassWithMembers>().Select(x => x.PublicMethod());

            sut.Verify((MemberInfo)method);

            sut.ToMock().Verify(x => x.Verify(method));
        }

        [Fact]
        public void VerifyEventMemberCallsVerifyEvent()
        {
            var sut = new Mock<IdiomaticAssertion> { CallBase = true }.Object;
            var @event = typeof(ClassWithMembers).GetEvents().First();

            sut.Verify((MemberInfo)@event);

            sut.ToMock().Verify(x => x.Verify(@event));
        }
    }
}