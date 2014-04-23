﻿using System;
using System.Runtime.Serialization;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents a verification error thrown when testing whether an assembly
    /// references only given assemblies.
    /// </summary>
    [Serializable]
    public class RestrictingReferenceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestrictingReferenceException"/> class.
        /// </summary>
        public RestrictingReferenceException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestrictingReferenceException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public RestrictingReferenceException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestrictingReferenceException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public RestrictingReferenceException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestrictingReferenceException"/> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo" /> that holds the serialized object data about the
        /// exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext" /> that contains contextual information about the
        ///  source or destination.
        /// </param>
        protected RestrictingReferenceException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}