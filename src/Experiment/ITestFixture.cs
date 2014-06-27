using System;

namespace Jwc.Experiment
{
    /// <summary>
    /// test fixture를 표현.
    /// </summary>
    public interface ITestFixture
    {
        /// <summary>
        /// request를 통해 테스트에 필요한 specimen를 만듦.
        /// </summary>
        /// <param name="request">
        /// specimen을 만들기 위해 필요한 정보를 제공. 일반적으로 <see cref="Type" />을 많이 활용.
        /// </param>
        /// <returns>
        /// 만들어진 specimen 객체.
        /// </returns>
        object Create(object request);
    }
}