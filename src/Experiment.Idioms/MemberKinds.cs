using System;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    ///     Represents kinds of idiomatic members.
    /// </summary>
    [Flags]
    public enum MemberKinds
    {
        /// <summary>
        ///     None
        /// </summary>
        None = 0,

        /// <summary>
        ///     The instance field
        /// </summary>
        InstanceField = 1,

        /// <summary>
        ///     The instance constructor
        /// </summary>
        InstanceConstructor = 2,

        /// <summary>
        ///     The instance get property
        /// </summary>
        InstanceGetProperty = 4,

        /// <summary>
        ///     The instance set property
        /// </summary>
        InstanceSetProperty = 8,

        /// <summary>
        ///     The instance property
        /// </summary>
        InstanceProperty = InstanceGetProperty | InstanceSetProperty,

        /// <summary>
        ///     The instance method
        /// </summary>
        InstanceMethod = 0x10,

        /// <summary>
        ///     The instance event
        /// </summary>
        InstanceEvent = 0x20,

        /// <summary>
        ///     The static field
        /// </summary>
        StaticField = 0x40,

        /// <summary>
        ///     The static constructor
        /// </summary>
        StaticConstructor = 0x80,

        /// <summary>
        ///     The static get property
        /// </summary>
        StaticGetProperty = 0x100,

        /// <summary>
        ///     The static set property
        /// </summary>
        StaticSetProperty = 0x200,

        /// <summary>
        ///     The static property
        /// </summary>
        StaticProperty = StaticGetProperty | StaticSetProperty,

        /// <summary>
        ///     The static method
        /// </summary>
        StaticMethod = 0x400,

        /// <summary>
        ///     The static event
        /// </summary>
        StaticEvent = 0x800,

        /// <summary>
        ///     The field
        /// </summary>
        Field = InstanceField | StaticField,

        /// <summary>
        ///     The constructor
        /// </summary>
        Constructor = InstanceConstructor | StaticConstructor,

        /// <summary>
        ///     The get property
        /// </summary>
        GetProperty = InstanceGetProperty | StaticGetProperty,

        /// <summary>
        ///     The set property
        /// </summary>
        SetProperty = InstanceSetProperty | StaticSetProperty,

        /// <summary>
        ///     The property
        /// </summary>
        Property = InstanceProperty | StaticProperty,

        /// <summary>
        ///     The method
        /// </summary>
        Method = InstanceMethod | StaticMethod,

        /// <summary>
        ///     The event
        /// </summary>
        Event = InstanceEvent | StaticEvent,

        /// <summary>
        ///     All the instance members
        /// </summary>
        Instance = InstanceField | InstanceConstructor | InstanceProperty | InstanceMethod | InstanceEvent,

        /// <summary>
        ///     All the static members
        /// </summary>
        Static = StaticField | StaticConstructor | StaticProperty | StaticMethod | StaticEvent,

        /// <summary>
        ///     All the members
        /// </summary>
        All = Instance | Static,

        /// <summary>
        ///     The default members (All)
        /// </summary>
        Default = All
    }
}