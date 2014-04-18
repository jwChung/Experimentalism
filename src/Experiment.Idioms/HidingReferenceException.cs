using System;
using System.Runtime.Serialization;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents a verification error when testing whether specified
    /// assemblies are not referenced from any elements.
    /// </summary>
    [Serializable]
    public class HidingReferenceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HidingReferenceException"/> class.
        /// </summary>
        public HidingReferenceException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HidingReferenceException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public HidingReferenceException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HidingReferenceException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public HidingReferenceException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HidingReferenceException"/> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo" /> that holds the serialized object
        /// data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext" /> that contains contextual
        /// information about the source or destination.
        /// </param>
        protected HidingReferenceException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}