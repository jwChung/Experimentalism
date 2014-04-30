using System;
using System.Runtime.Serialization;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents a verification error when testing whether all references of
    /// a certain assembly are specified.
    /// </summary>
    public class RestrictiveReferenceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestrictiveReferenceException"/> class.
        /// </summary>
        public RestrictiveReferenceException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestrictiveReferenceException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        public RestrictiveReferenceException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestrictiveReferenceException"/> class.
        /// </summary>
        /// <param name="message">
        /// The exception message.
        /// </param>
        /// <param name="inner">
        /// The inner exception.
        /// </param>
        public RestrictiveReferenceException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestrictiveReferenceException" /> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo" /> that holds the serialized object
        /// data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext" /> that contains contextual information
        /// about the source or destination.
        /// </param>
        protected RestrictiveReferenceException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}