using System;

namespace Jwc.Experiment
{
    /// <summary>
    /// Specifies flags than represents accessibilities.
    /// </summary>
    [Flags]
    public enum Accessibilities
    {
        /// <summary>
        /// The none accessibility.
        /// </summary>
        None = 0,

        /// <summary>
        /// The public accessibility.
        /// </summary>
        Public = 1,

        /// <summary>
        /// The protected accessibility.
        /// </summary>
        Protected = 2,

        /// <summary>
        /// The internal accessibility.
        /// </summary>
        Internal = 4,

        /// <summary>
        /// The private accessibility.
        /// </summary>
        Private = 8,

        /// <summary>
        /// The protected or internal accessibility.
        /// </summary>
        ProtectedInternal = Protected | Internal,

        /// <summary>
        /// The public or protected accessibility.
        /// </summary>
        Exposed = Public | Protected,

        /// <summary>
        /// The all accessibilities.
        /// </summary>
        All = Public | Protected | Internal | Private,

        /// <summary>
        /// The default accessibilities.
        /// </summary>
        Default = Public
    }
}