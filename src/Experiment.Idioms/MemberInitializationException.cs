using System;
using System.Runtime.Serialization;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents a verification error when testing whether members (property or field)
    /// are correctly intialized by a constructor.
    /// </summary>
    [Serializable]
    public class MemberInitializationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberInitializationException"/> class.
        /// </summary>
        public MemberInitializationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberInitializationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        public MemberInitializationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberInitializationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The exception message.
        /// </param>
        /// <param name="inner">
        /// The inner exception.
        /// </param>
        public MemberInitializationException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberInitializationException" /> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo" /> that holds the serialized object
        /// data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext" /> that contains contextual information
        /// about the source or destination.
        /// </param>
        protected MemberInitializationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}