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
        /// The instance field
        /// </summary>
        Field = 1,

        /// <summary>
        /// The instance constructor
        /// </summary>
        Constructor = 2,

        /// <summary>
        /// The instance property
        /// </summary>
        Property = GetProperty | SetProperty,

        /// <summary>
        /// The instance get property
        /// </summary>
        GetProperty = 4,

        /// <summary>
        /// The instance set property
        /// </summary>
        SetProperty = 8,

        /// <summary>
        /// The instance method
        /// </summary>
        Method = 0x10,

        /// <summary>
        /// The instance event
        /// </summary>
        Event = 0x20,

        /// <summary>
        /// The static field
        /// </summary>
        StaticField = 0x40,

        /// <summary>
        /// The static constructor
        /// </summary>
        StaticConstructor = 0x80,

        /// <summary>
        /// The static property
        /// </summary>
        StaticProperty = StaticGetProperty | StaticSetProperty,

        /// <summary>
        /// The static get property
        /// </summary>
        StaticGetProperty = 0x100,

        /// <summary>
        /// The static set property
        /// </summary>
        StaticSetProperty = 0x200,

        /// <summary>
        /// The static method
        /// </summary>
        StaticMethod = 0x400,

        /// <summary>
        /// The static event
        /// </summary>
        StaticEvent = 0x800,

        /// <summary>
        /// All
        /// </summary>
        All = InstanceMembers | StaticMembers,

        /// <summary>
        /// The instance members
        /// </summary>
        InstanceMembers = Field | Constructor | Property | Method | Event,

        /// <summary>
        /// The instance members
        /// </summary>
        StaticMembers = StaticField | StaticConstructor | StaticProperty | StaticMethod | StaticEvent,

        /// <summary>
        /// The default
        /// </summary>
        Default = All
    }
}