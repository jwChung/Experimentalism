namespace Jwc.Experiment
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a verification error when testing whether certain assemblies are not exposed
    /// through public API.
    /// </summary>
    [Serializable]
    public class IndirectReferenceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndirectReferenceException" /> class.
        /// </summary>
        public IndirectReferenceException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndirectReferenceException" /> class.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        public IndirectReferenceException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndirectReferenceException" /> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="inner">
        /// The inner.
        /// </param>
        public IndirectReferenceException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndirectReferenceException" /> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains
        /// contextual information about the source or destination.
        /// </param>
        protected IndirectReferenceException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}