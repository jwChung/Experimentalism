﻿namespace Jwc.Experiment
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// test fixture를 표현.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "The warnings are from Korean.")]
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

        /// <summary>
        /// Creates a specimen.
        /// </summary>
        /// <typeparam name="T">
        /// A type of the specimen.
        /// </typeparam>
        /// <returns>
        /// The speicmen being created.
        /// </returns>
        [Obsolete("Do not use this method. Instead install AutoFixture package and use the members of IFixture to customize test fixture.")]
        T Create<T>();

        /// <summary>
        /// Freezes a specified specimen.
        /// </summary>
        /// <typeparam name="T">
        /// A type of the specimen.
        /// </typeparam>
        /// <param name="specimen">
        /// The specimen to be frozen.
        /// </param>
        [Obsolete("Do not use this method. Instead install AutoFixture package and use the members of IFixture to customize test fixture.")]
        void Freeze<T>(T specimen);
    }
}