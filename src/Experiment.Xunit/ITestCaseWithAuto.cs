﻿[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1508:ClosingCurlyBracketsMustNotBePrecededByBlankLine", Justification = "The last line is generated by the code template.")]

namespace Jwc.Experiment.Xunit
{
    using System;

    /// <summary>
    /// Represents a test case with auto arguments.
    /// </summary>
    /// <typeparam name="T1">
    /// A type of the first argument.
    /// </typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "This rules is suppressed to pass many arguments to a test method.")]
    public interface ITestCaseWithAuto<T1> : IFluentInterface
    {
        /// <summary>
        /// Creates a test case with auto arguments.
        /// </summary>
        /// <param name="delegator">
        /// A delegator representing the actual test method.
        /// </param>
        /// <returns>
        /// The new test case.
        /// </returns>
        ITestCase Create(Action<T1> delegator);
    }

    /// <summary>
    /// Represents a test case with auto arguments.
    /// </summary>
    /// <typeparam name="T1">
    /// A type of the first argument.
    /// </typeparam>
    /// <typeparam name="T2">
    /// A type of the second argument.
    /// </typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "This rules is suppressed to pass many arguments to a test method.")]
    public interface ITestCaseWithAuto<T1, T2> : IFluentInterface
    {
        /// <summary>
        /// Creates a test case with auto arguments.
        /// </summary>
        /// <param name="delegator">
        /// A delegator representing the actual test method.
        /// </param>
        /// <returns>
        /// The new test case.
        /// </returns>
        ITestCase Create(Action<T1, T2> delegator);
    }

    /// <summary>
    /// Represents a test case with auto arguments.
    /// </summary>
    /// <typeparam name="T1">
    /// A type of the first argument.
    /// </typeparam>
    /// <typeparam name="T2">
    /// A type of the second argument.
    /// </typeparam>
    /// <typeparam name="T3">
    /// A type of the third argument.
    /// </typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "This rules is suppressed to pass many arguments to a test method.")]
    public interface ITestCaseWithAuto<T1, T2, T3> : IFluentInterface
    {
        /// <summary>
        /// Creates a test case with auto arguments.
        /// </summary>
        /// <param name="delegator">
        /// A delegator representing the actual test method.
        /// </param>
        /// <returns>
        /// The new test case.
        /// </returns>
        ITestCase Create(Action<T1, T2, T3> delegator);
    }

    /// <summary>
    /// Represents a test case with auto arguments.
    /// </summary>
    /// <typeparam name="T1">
    /// A type of the first argument.
    /// </typeparam>
    /// <typeparam name="T2">
    /// A type of the second argument.
    /// </typeparam>
    /// <typeparam name="T3">
    /// A type of the third argument.
    /// </typeparam>
    /// <typeparam name="T4">
    /// A type of the fourth argument.
    /// </typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "This rules is suppressed to pass many arguments to a test method.")]
    public interface ITestCaseWithAuto<T1, T2, T3, T4> : IFluentInterface
    {
        /// <summary>
        /// Creates a test case with auto arguments.
        /// </summary>
        /// <param name="delegator">
        /// A delegator representing the actual test method.
        /// </param>
        /// <returns>
        /// The new test case.
        /// </returns>
        ITestCase Create(Action<T1, T2, T3, T4> delegator);
    }

    /// <summary>
    /// Represents a test case with auto arguments.
    /// </summary>
    /// <typeparam name="T1">
    /// A type of the first argument.
    /// </typeparam>
    /// <typeparam name="T2">
    /// A type of the second argument.
    /// </typeparam>
    /// <typeparam name="T3">
    /// A type of the third argument.
    /// </typeparam>
    /// <typeparam name="T4">
    /// A type of the fourth argument.
    /// </typeparam>
    /// <typeparam name="T5">
    /// A type of the fifth argument.
    /// </typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "This rules is suppressed to pass many arguments to a test method.")]
    public interface ITestCaseWithAuto<T1, T2, T3, T4, T5> : IFluentInterface
    {
        /// <summary>
        /// Creates a test case with auto arguments.
        /// </summary>
        /// <param name="delegator">
        /// A delegator representing the actual test method.
        /// </param>
        /// <returns>
        /// The new test case.
        /// </returns>
        ITestCase Create(Action<T1, T2, T3, T4, T5> delegator);
    }

    /// <summary>
    /// Represents a test case with auto arguments.
    /// </summary>
    /// <typeparam name="T1">
    /// A type of the first argument.
    /// </typeparam>
    /// <typeparam name="T2">
    /// A type of the second argument.
    /// </typeparam>
    /// <typeparam name="T3">
    /// A type of the third argument.
    /// </typeparam>
    /// <typeparam name="T4">
    /// A type of the fourth argument.
    /// </typeparam>
    /// <typeparam name="T5">
    /// A type of the fifth argument.
    /// </typeparam>
    /// <typeparam name="T6">
    /// A type of the sixth argument.
    /// </typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "This rules is suppressed to pass many arguments to a test method.")]
    public interface ITestCaseWithAuto<T1, T2, T3, T4, T5, T6> : IFluentInterface
    {
        /// <summary>
        /// Creates a test case with auto arguments.
        /// </summary>
        /// <param name="delegator">
        /// A delegator representing the actual test method.
        /// </param>
        /// <returns>
        /// The new test case.
        /// </returns>
        ITestCase Create(Action<T1, T2, T3, T4, T5, T6> delegator);
    }

    /// <summary>
    /// Represents a test case with auto arguments.
    /// </summary>
    /// <typeparam name="T1">
    /// A type of the first argument.
    /// </typeparam>
    /// <typeparam name="T2">
    /// A type of the second argument.
    /// </typeparam>
    /// <typeparam name="T3">
    /// A type of the third argument.
    /// </typeparam>
    /// <typeparam name="T4">
    /// A type of the fourth argument.
    /// </typeparam>
    /// <typeparam name="T5">
    /// A type of the fifth argument.
    /// </typeparam>
    /// <typeparam name="T6">
    /// A type of the sixth argument.
    /// </typeparam>
    /// <typeparam name="T7">
    /// A type of the seventh argument.
    /// </typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "This rules is suppressed to pass many arguments to a test method.")]
    public interface ITestCaseWithAuto<T1, T2, T3, T4, T5, T6, T7> : IFluentInterface
    {
        /// <summary>
        /// Creates a test case with auto arguments.
        /// </summary>
        /// <param name="delegator">
        /// A delegator representing the actual test method.
        /// </param>
        /// <returns>
        /// The new test case.
        /// </returns>
        ITestCase Create(Action<T1, T2, T3, T4, T5, T6, T7> delegator);
    }

    /// <summary>
    /// Represents a test case with auto arguments.
    /// </summary>
    /// <typeparam name="T1">
    /// A type of the first argument.
    /// </typeparam>
    /// <typeparam name="T2">
    /// A type of the second argument.
    /// </typeparam>
    /// <typeparam name="T3">
    /// A type of the third argument.
    /// </typeparam>
    /// <typeparam name="T4">
    /// A type of the fourth argument.
    /// </typeparam>
    /// <typeparam name="T5">
    /// A type of the fifth argument.
    /// </typeparam>
    /// <typeparam name="T6">
    /// A type of the sixth argument.
    /// </typeparam>
    /// <typeparam name="T7">
    /// A type of the seventh argument.
    /// </typeparam>
    /// <typeparam name="T8">
    /// A type of the eighth argument.
    /// </typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "This rules is suppressed to pass many arguments to a test method.")]
    public interface ITestCaseWithAuto<T1, T2, T3, T4, T5, T6, T7, T8> : IFluentInterface
    {
        /// <summary>
        /// Creates a test case with auto arguments.
        /// </summary>
        /// <param name="delegator">
        /// A delegator representing the actual test method.
        /// </param>
        /// <returns>
        /// The new test case.
        /// </returns>
        ITestCase Create(Action<T1, T2, T3, T4, T5, T6, T7, T8> delegator);
    }

    /// <summary>
    /// Represents a test case with auto arguments.
    /// </summary>
    /// <typeparam name="T1">
    /// A type of the first argument.
    /// </typeparam>
    /// <typeparam name="T2">
    /// A type of the second argument.
    /// </typeparam>
    /// <typeparam name="T3">
    /// A type of the third argument.
    /// </typeparam>
    /// <typeparam name="T4">
    /// A type of the fourth argument.
    /// </typeparam>
    /// <typeparam name="T5">
    /// A type of the fifth argument.
    /// </typeparam>
    /// <typeparam name="T6">
    /// A type of the sixth argument.
    /// </typeparam>
    /// <typeparam name="T7">
    /// A type of the seventh argument.
    /// </typeparam>
    /// <typeparam name="T8">
    /// A type of the eighth argument.
    /// </typeparam>
    /// <typeparam name="T9">
    /// A type of the ninth argument.
    /// </typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "This rules is suppressed to pass many arguments to a test method.")]
    public interface ITestCaseWithAuto<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IFluentInterface
    {
        /// <summary>
        /// Creates a test case with auto arguments.
        /// </summary>
        /// <param name="delegator">
        /// A delegator representing the actual test method.
        /// </param>
        /// <returns>
        /// The new test case.
        /// </returns>
        ITestCase Create(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> delegator);
    }

    /// <summary>
    /// Represents a test case with auto arguments.
    /// </summary>
    /// <typeparam name="T1">
    /// A type of the first argument.
    /// </typeparam>
    /// <typeparam name="T2">
    /// A type of the second argument.
    /// </typeparam>
    /// <typeparam name="T3">
    /// A type of the third argument.
    /// </typeparam>
    /// <typeparam name="T4">
    /// A type of the fourth argument.
    /// </typeparam>
    /// <typeparam name="T5">
    /// A type of the fifth argument.
    /// </typeparam>
    /// <typeparam name="T6">
    /// A type of the sixth argument.
    /// </typeparam>
    /// <typeparam name="T7">
    /// A type of the seventh argument.
    /// </typeparam>
    /// <typeparam name="T8">
    /// A type of the eighth argument.
    /// </typeparam>
    /// <typeparam name="T9">
    /// A type of the ninth argument.
    /// </typeparam>
    /// <typeparam name="T10">
    /// A type of the tenth argument.
    /// </typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "This rules is suppressed to pass many arguments to a test method.")]
    public interface ITestCaseWithAuto<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : IFluentInterface
    {
        /// <summary>
        /// Creates a test case with auto arguments.
        /// </summary>
        /// <param name="delegator">
        /// A delegator representing the actual test method.
        /// </param>
        /// <returns>
        /// The new test case.
        /// </returns>
        ITestCase Create(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> delegator);
    }

    /// <summary>
    /// Represents a test case with auto arguments.
    /// </summary>
    /// <typeparam name="T1">
    /// A type of the first argument.
    /// </typeparam>
    /// <typeparam name="T2">
    /// A type of the second argument.
    /// </typeparam>
    /// <typeparam name="T3">
    /// A type of the third argument.
    /// </typeparam>
    /// <typeparam name="T4">
    /// A type of the fourth argument.
    /// </typeparam>
    /// <typeparam name="T5">
    /// A type of the fifth argument.
    /// </typeparam>
    /// <typeparam name="T6">
    /// A type of the sixth argument.
    /// </typeparam>
    /// <typeparam name="T7">
    /// A type of the seventh argument.
    /// </typeparam>
    /// <typeparam name="T8">
    /// A type of the eighth argument.
    /// </typeparam>
    /// <typeparam name="T9">
    /// A type of the ninth argument.
    /// </typeparam>
    /// <typeparam name="T10">
    /// A type of the tenth argument.
    /// </typeparam>
    /// <typeparam name="T11">
    /// A type of the eleventh argument.
    /// </typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "This rules is suppressed to pass many arguments to a test method.")]
    public interface ITestCaseWithAuto<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : IFluentInterface
    {
        /// <summary>
        /// Creates a test case with auto arguments.
        /// </summary>
        /// <param name="delegator">
        /// A delegator representing the actual test method.
        /// </param>
        /// <returns>
        /// The new test case.
        /// </returns>
        ITestCase Create(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> delegator);
    }

    /// <summary>
    /// Represents a test case with auto arguments.
    /// </summary>
    /// <typeparam name="T1">
    /// A type of the first argument.
    /// </typeparam>
    /// <typeparam name="T2">
    /// A type of the second argument.
    /// </typeparam>
    /// <typeparam name="T3">
    /// A type of the third argument.
    /// </typeparam>
    /// <typeparam name="T4">
    /// A type of the fourth argument.
    /// </typeparam>
    /// <typeparam name="T5">
    /// A type of the fifth argument.
    /// </typeparam>
    /// <typeparam name="T6">
    /// A type of the sixth argument.
    /// </typeparam>
    /// <typeparam name="T7">
    /// A type of the seventh argument.
    /// </typeparam>
    /// <typeparam name="T8">
    /// A type of the eighth argument.
    /// </typeparam>
    /// <typeparam name="T9">
    /// A type of the ninth argument.
    /// </typeparam>
    /// <typeparam name="T10">
    /// A type of the tenth argument.
    /// </typeparam>
    /// <typeparam name="T11">
    /// A type of the eleventh argument.
    /// </typeparam>
    /// <typeparam name="T12">
    /// A type of the twelfth argument.
    /// </typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "This rules is suppressed to pass many arguments to a test method.")]
    public interface ITestCaseWithAuto<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : IFluentInterface
    {
        /// <summary>
        /// Creates a test case with auto arguments.
        /// </summary>
        /// <param name="delegator">
        /// A delegator representing the actual test method.
        /// </param>
        /// <returns>
        /// The new test case.
        /// </returns>
        ITestCase Create(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> delegator);
    }

    /// <summary>
    /// Represents a test case with auto arguments.
    /// </summary>
    /// <typeparam name="T1">
    /// A type of the first argument.
    /// </typeparam>
    /// <typeparam name="T2">
    /// A type of the second argument.
    /// </typeparam>
    /// <typeparam name="T3">
    /// A type of the third argument.
    /// </typeparam>
    /// <typeparam name="T4">
    /// A type of the fourth argument.
    /// </typeparam>
    /// <typeparam name="T5">
    /// A type of the fifth argument.
    /// </typeparam>
    /// <typeparam name="T6">
    /// A type of the sixth argument.
    /// </typeparam>
    /// <typeparam name="T7">
    /// A type of the seventh argument.
    /// </typeparam>
    /// <typeparam name="T8">
    /// A type of the eighth argument.
    /// </typeparam>
    /// <typeparam name="T9">
    /// A type of the ninth argument.
    /// </typeparam>
    /// <typeparam name="T10">
    /// A type of the tenth argument.
    /// </typeparam>
    /// <typeparam name="T11">
    /// A type of the eleventh argument.
    /// </typeparam>
    /// <typeparam name="T12">
    /// A type of the twelfth argument.
    /// </typeparam>
    /// <typeparam name="T13">
    /// A type of the thirteenth argument.
    /// </typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "This rules is suppressed to pass many arguments to a test method.")]
    public interface ITestCaseWithAuto<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : IFluentInterface
    {
        /// <summary>
        /// Creates a test case with auto arguments.
        /// </summary>
        /// <param name="delegator">
        /// A delegator representing the actual test method.
        /// </param>
        /// <returns>
        /// The new test case.
        /// </returns>
        ITestCase Create(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> delegator);
    }

    /// <summary>
    /// Represents a test case with auto arguments.
    /// </summary>
    /// <typeparam name="T1">
    /// A type of the first argument.
    /// </typeparam>
    /// <typeparam name="T2">
    /// A type of the second argument.
    /// </typeparam>
    /// <typeparam name="T3">
    /// A type of the third argument.
    /// </typeparam>
    /// <typeparam name="T4">
    /// A type of the fourth argument.
    /// </typeparam>
    /// <typeparam name="T5">
    /// A type of the fifth argument.
    /// </typeparam>
    /// <typeparam name="T6">
    /// A type of the sixth argument.
    /// </typeparam>
    /// <typeparam name="T7">
    /// A type of the seventh argument.
    /// </typeparam>
    /// <typeparam name="T8">
    /// A type of the eighth argument.
    /// </typeparam>
    /// <typeparam name="T9">
    /// A type of the ninth argument.
    /// </typeparam>
    /// <typeparam name="T10">
    /// A type of the tenth argument.
    /// </typeparam>
    /// <typeparam name="T11">
    /// A type of the eleventh argument.
    /// </typeparam>
    /// <typeparam name="T12">
    /// A type of the twelfth argument.
    /// </typeparam>
    /// <typeparam name="T13">
    /// A type of the thirteenth argument.
    /// </typeparam>
    /// <typeparam name="T14">
    /// A type of the fourteenth argument.
    /// </typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "This rules is suppressed to pass many arguments to a test method.")]
    public interface ITestCaseWithAuto<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : IFluentInterface
    {
        /// <summary>
        /// Creates a test case with auto arguments.
        /// </summary>
        /// <param name="delegator">
        /// A delegator representing the actual test method.
        /// </param>
        /// <returns>
        /// The new test case.
        /// </returns>
        ITestCase Create(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> delegator);
    }

    /// <summary>
    /// Represents a test case with auto arguments.
    /// </summary>
    /// <typeparam name="T1">
    /// A type of the first argument.
    /// </typeparam>
    /// <typeparam name="T2">
    /// A type of the second argument.
    /// </typeparam>
    /// <typeparam name="T3">
    /// A type of the third argument.
    /// </typeparam>
    /// <typeparam name="T4">
    /// A type of the fourth argument.
    /// </typeparam>
    /// <typeparam name="T5">
    /// A type of the fifth argument.
    /// </typeparam>
    /// <typeparam name="T6">
    /// A type of the sixth argument.
    /// </typeparam>
    /// <typeparam name="T7">
    /// A type of the seventh argument.
    /// </typeparam>
    /// <typeparam name="T8">
    /// A type of the eighth argument.
    /// </typeparam>
    /// <typeparam name="T9">
    /// A type of the ninth argument.
    /// </typeparam>
    /// <typeparam name="T10">
    /// A type of the tenth argument.
    /// </typeparam>
    /// <typeparam name="T11">
    /// A type of the eleventh argument.
    /// </typeparam>
    /// <typeparam name="T12">
    /// A type of the twelfth argument.
    /// </typeparam>
    /// <typeparam name="T13">
    /// A type of the thirteenth argument.
    /// </typeparam>
    /// <typeparam name="T14">
    /// A type of the fourteenth argument.
    /// </typeparam>
    /// <typeparam name="T15">
    /// A type of the fifteenth argument.
    /// </typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "This rules is suppressed to pass many arguments to a test method.")]
    public interface ITestCaseWithAuto<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : IFluentInterface
    {
        /// <summary>
        /// Creates a test case with auto arguments.
        /// </summary>
        /// <param name="delegator">
        /// A delegator representing the actual test method.
        /// </param>
        /// <returns>
        /// The new test case.
        /// </returns>
        ITestCase Create(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> delegator);
    }

    /// <summary>
    /// Represents a test case with auto arguments.
    /// </summary>
    /// <typeparam name="T1">
    /// A type of the first argument.
    /// </typeparam>
    /// <typeparam name="T2">
    /// A type of the second argument.
    /// </typeparam>
    /// <typeparam name="T3">
    /// A type of the third argument.
    /// </typeparam>
    /// <typeparam name="T4">
    /// A type of the fourth argument.
    /// </typeparam>
    /// <typeparam name="T5">
    /// A type of the fifth argument.
    /// </typeparam>
    /// <typeparam name="T6">
    /// A type of the sixth argument.
    /// </typeparam>
    /// <typeparam name="T7">
    /// A type of the seventh argument.
    /// </typeparam>
    /// <typeparam name="T8">
    /// A type of the eighth argument.
    /// </typeparam>
    /// <typeparam name="T9">
    /// A type of the ninth argument.
    /// </typeparam>
    /// <typeparam name="T10">
    /// A type of the tenth argument.
    /// </typeparam>
    /// <typeparam name="T11">
    /// A type of the eleventh argument.
    /// </typeparam>
    /// <typeparam name="T12">
    /// A type of the twelfth argument.
    /// </typeparam>
    /// <typeparam name="T13">
    /// A type of the thirteenth argument.
    /// </typeparam>
    /// <typeparam name="T14">
    /// A type of the fourteenth argument.
    /// </typeparam>
    /// <typeparam name="T15">
    /// A type of the fifteenth argument.
    /// </typeparam>
    /// <typeparam name="T16">
    /// A type of the sixteenth argument.
    /// </typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "This rules is suppressed to pass many arguments to a test method.")]
    public interface ITestCaseWithAuto<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : IFluentInterface
    {
        /// <summary>
        /// Creates a test case with auto arguments.
        /// </summary>
        /// <param name="delegator">
        /// A delegator representing the actual test method.
        /// </param>
        /// <returns>
        /// The new test case.
        /// </returns>
        ITestCase Create(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> delegator);
    }

}
