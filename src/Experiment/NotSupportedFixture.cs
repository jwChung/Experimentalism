using System;

namespace Jwc.Experiment
{
    /// <summary>
    /// Auto data를 제공하지 않는다는 것을 보여주기 위한 class.
    /// </summary>
    public class NotSupportedFixture : ITestFixture
    {
        /// <summary>
        /// 이 메소드를 호출하게 되면 <see cref="NotSupportedException"/>이 발생함.
        /// </summary>
        public object Create(object request)
        {
            throw new NotSupportedException(
                "Parameterized test에 auto data기능을 구현하기 위해서는 ITestFixture를 직접구현하여 " +
                "TheoremAttribute에 제공하여야 함. NotSupportedFixture는 TheoremAttribute의 " +
                "기본 ITestFixture임.");
        }
    }
}