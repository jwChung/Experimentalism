using System;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents kinds of idiomatic member.
    /// </summary>
    [Flags]
    public enum MemberKinds
    {
        /// <summary>
        /// The none
        /// </summary>
        None = 0,

        /// <summary>
        /// The field
        /// </summary>
        Field = 1,

        /// <summary>
        /// The constructor
        /// </summary>
        Constructor = 2,

        /// <summary>
        /// The get property
        /// </summary>
        GetProperty = 4,

        /// <summary>
        /// The set property
        /// </summary>
        SetProperty = 8,

        /// <summary>
        /// The property
        /// </summary>
        Property = GetProperty | SetProperty,

        /// <summary>
        /// The method
        /// </summary>
        Method = 0x10,

        /// <summary>
        /// The event
        /// </summary>
        Event = 0x20,

        /// <summary>
        /// All
        /// </summary>
        All = Field | Constructor | Property | Method | Event,

        /// <summary>
        /// The default
        /// </summary>
        Default = All
    }
}