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
            throw new System.NotImplementedException();
        }
    }
}