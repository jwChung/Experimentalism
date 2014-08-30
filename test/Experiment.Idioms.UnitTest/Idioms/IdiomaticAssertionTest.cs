namespace Jwc.Experiment.Idioms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Jwc.Experiment.Xunit;
    using Moq;
    using global::Xunit;

    public class IdiomaticAssertionTest
    {
        [Fact]
        public void SutIsIdiomaticAssemblyAssertion()
        {
            var sut = new Mock<IdiomaticAssertion> { CallBase = true }.Object;
            Assert.IsAssignableFrom<IIdiomaticAssemblyAssertion>(sut);
        }

        [Fact]
        public void SutIsIdiomaticTypeAssertion()
        {
            var sut = new Mock<IdiomaticAssertion> { CallBase = true }.Object;
            Assert.IsAssignableFrom<IIdiomaticTypeAssertion>(sut);
        }

        [Fact]
        public void SutIsIdiomaticMemberAssertion()
        {
            var sut = new Mock<IdiomaticAssertion> { CallBase = true }.Object;
            Assert.IsAssignableFrom<IdiomaticMemberAssertion>(sut);
        }

        [Fact]
        public void VerifyAssemblyCorrectlyVerifies()
        {
            var sut = new Mock<IdiomaticAssertion> { CallBase = true }.Object;
            var types = new List<MemberInfo>();
            sut.ToMock().Setup(x => x.Verify(It.IsAny<Type>())).Callback<Type>(types.Add);
            var assembly = typeof(TestAttribute).Assembly;
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
    }
}