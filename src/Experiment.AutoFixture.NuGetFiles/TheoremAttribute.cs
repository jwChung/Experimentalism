using System;
using Jwc.Experiment;
using Ploeh.AutoFixture;

namespace Jwc.NuGetFiles
{
    /// <summary>
    /// 이 attribute는 method위에 선언되어 해당 method가 test case라는 것을
    /// 지칭하게 되며, non-parameterized test 뿐 아니라 parameterized test에도
    /// 사용될 수 있다.
    /// Parameterized test에 대해 이 attribute는 AutoFixture library를 이용하여
    /// auto data기능을 제공한다.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TheoremAttribute : AutoFixtureTheoremAttribute
    {
        /// <summary>
        /// Creates the fixture.
        /// </summary>
        /// <returns>The new fixture instance.</returns>
        protected override IFixture CreateFixture()
        {
            return new Fixture();
        }
    }
}