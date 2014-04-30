using System.Linq;
using System.Reflection;
using Moq;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment.Idioms2
{
    public class IdiomaticMemberAssertionTest
    {
        [Fact]
        public void SutIsIdiomaticMemberAssertion()
        {
            var sut = new Mock<IdiomaticMemberAssertion> { CallBase = true }.Object;
            Assert.IsAssignableFrom<IIdiomaticMemberAssertion>(sut);
        }

        [Fact]
        public void VerifyFieldMemberCallsVerifyField()
        {
            var sut = new Mock<IdiomaticMemberAssertion> { CallBase = true }.Object;
            var field = new Fields<TypeWithMembers>().Select(x => x.PublicField);

            sut.Verify((MemberInfo)field);

            sut.ToMock().Verify(x => x.Verify(field));
        }

        [Fact]
        public void VerifyConstructorMemberCallsVerifyConstructor()
        {
            var sut = new Mock<IdiomaticMemberAssertion> { CallBase = true }.Object;
            var constructor = Constructors.Select(() => new TypeWithMembers());

            sut.Verify((MemberInfo)constructor);

            sut.ToMock().Verify(x => x.Verify(constructor));
        }

        [Fact]
        public void VerifyPropertyMemberCallsVerifyProperty()
        {
            var sut = new Mock<IdiomaticMemberAssertion> { CallBase = true }.Object;
            var property = new Properties<TypeWithMembers>().Select(x => x.PublicProperty);

            sut.Verify((MemberInfo)property);

            sut.ToMock().Verify(x => x.Verify(property));
        }

        [Fact]
        public void VerifyMethodMemberCallsVerifyMethod()
        {
            var sut = new Mock<IdiomaticMemberAssertion> { CallBase = true }.Object;
            var method = new Methods<TypeWithMembers>().Select(x => x.PublicMethod());

            sut.Verify((MemberInfo)method);

            sut.ToMock().Verify(x => x.Verify(method));
        }

        [Fact]
        public void VerifyMethodMemberCallsVerifyEvent()
        {
            var sut = new Mock<IdiomaticMemberAssertion> { CallBase = true }.Object;
            var @event = typeof(TypeWithMembers).GetEvents().First();

            sut.Verify((MemberInfo)@event);

            sut.ToMock().Verify(x => x.Verify(@event));
        }
    }
}