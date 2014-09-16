﻿[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1508:ClosingCurlyBracketsMustNotBePrecededByBlankLine", Justification = "The last line is generated by the code template.")]

namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents test cases.
    /// </summary>
    public sealed class TestCases
    {
        private TestCases()
        {
        }

        /// <summary>
        /// Returns test cases with arguments.
        /// </summary>
        /// <typeparam name="T1">
        /// A type of the first arguments.
        /// </typeparam>
        /// <param name="args1">
        /// The first arguments.
        /// </param>
        /// <returns>
        /// The new test cases with arguments.
        /// </returns>
        public static ITestCasesWithArgs<T1> WithArgs<T1>(IEnumerable<T1> args1)
        {
            return new TestCasesWithArgs<T1>(args1);
        }

        /// <summary>
        /// Returns test cases with arguments.
        /// </summary>
        /// <typeparam name="T1">
        /// A type of the first arguments.
        /// </typeparam>
        /// <typeparam name="T2">
        /// A type of the second arguments.
        /// </typeparam>
        /// <param name="args1">
        /// The first arguments.
        /// </param>
        /// <param name="args2">
        /// The second arguments.
        /// </param>
        /// <returns>
        /// The new test cases with arguments.
        /// </returns>
        public static ITestCasesWithArgs<T1, T2> WithArgs<T1, T2>(IEnumerable<T1> args1, IEnumerable<T2> args2)
        {
            return new TestCasesWithArgs<T1, T2>(args1, args2);
        }

        /// <summary>
        /// Returns test cases with arguments.
        /// </summary>
        /// <typeparam name="T1">
        /// A type of the first arguments.
        /// </typeparam>
        /// <typeparam name="T2">
        /// A type of the second arguments.
        /// </typeparam>
        /// <typeparam name="T3">
        /// A type of the third arguments.
        /// </typeparam>
        /// <param name="args1">
        /// The first arguments.
        /// </param>
        /// <param name="args2">
        /// The second arguments.
        /// </param>
        /// <param name="args3">
        /// The third arguments.
        /// </param>
        /// <returns>
        /// The new test cases with arguments.
        /// </returns>
        public static ITestCasesWithArgs<T1, T2, T3> WithArgs<T1, T2, T3>(IEnumerable<T1> args1, IEnumerable<T2> args2, IEnumerable<T3> args3)
        {
            return new TestCasesWithArgs<T1, T2, T3>(args1, args2, args3);
        }

        /// <summary>
        /// Returns test cases with arguments.
        /// </summary>
        /// <typeparam name="T1">
        /// A type of the first arguments.
        /// </typeparam>
        /// <typeparam name="T2">
        /// A type of the second arguments.
        /// </typeparam>
        /// <typeparam name="T3">
        /// A type of the third arguments.
        /// </typeparam>
        /// <typeparam name="T4">
        /// A type of the fourth arguments.
        /// </typeparam>
        /// <param name="args1">
        /// The first arguments.
        /// </param>
        /// <param name="args2">
        /// The second arguments.
        /// </param>
        /// <param name="args3">
        /// The third arguments.
        /// </param>
        /// <param name="args4">
        /// The fourth arguments.
        /// </param>
        /// <returns>
        /// The new test cases with arguments.
        /// </returns>
        public static ITestCasesWithArgs<T1, T2, T3, T4> WithArgs<T1, T2, T3, T4>(IEnumerable<T1> args1, IEnumerable<T2> args2, IEnumerable<T3> args3, IEnumerable<T4> args4)
        {
            return new TestCasesWithArgs<T1, T2, T3, T4>(args1, args2, args3, args4);
        }

        /// <summary>
        /// Returns test cases with arguments.
        /// </summary>
        /// <typeparam name="T1">
        /// A type of the first arguments.
        /// </typeparam>
        /// <typeparam name="T2">
        /// A type of the second arguments.
        /// </typeparam>
        /// <typeparam name="T3">
        /// A type of the third arguments.
        /// </typeparam>
        /// <typeparam name="T4">
        /// A type of the fourth arguments.
        /// </typeparam>
        /// <typeparam name="T5">
        /// A type of the fifth arguments.
        /// </typeparam>
        /// <param name="args1">
        /// The first arguments.
        /// </param>
        /// <param name="args2">
        /// The second arguments.
        /// </param>
        /// <param name="args3">
        /// The third arguments.
        /// </param>
        /// <param name="args4">
        /// The fourth arguments.
        /// </param>
        /// <param name="args5">
        /// The fifth arguments.
        /// </param>
        /// <returns>
        /// The new test cases with arguments.
        /// </returns>
        public static ITestCasesWithArgs<T1, T2, T3, T4, T5> WithArgs<T1, T2, T3, T4, T5>(IEnumerable<T1> args1, IEnumerable<T2> args2, IEnumerable<T3> args3, IEnumerable<T4> args4, IEnumerable<T5> args5)
        {
            return new TestCasesWithArgs<T1, T2, T3, T4, T5>(args1, args2, args3, args4, args5);
        }

        /// <summary>
        /// Returns test cases with arguments.
        /// </summary>
        /// <typeparam name="T1">
        /// A type of the first arguments.
        /// </typeparam>
        /// <typeparam name="T2">
        /// A type of the second arguments.
        /// </typeparam>
        /// <typeparam name="T3">
        /// A type of the third arguments.
        /// </typeparam>
        /// <typeparam name="T4">
        /// A type of the fourth arguments.
        /// </typeparam>
        /// <typeparam name="T5">
        /// A type of the fifth arguments.
        /// </typeparam>
        /// <typeparam name="T6">
        /// A type of the sixth arguments.
        /// </typeparam>
        /// <param name="args1">
        /// The first arguments.
        /// </param>
        /// <param name="args2">
        /// The second arguments.
        /// </param>
        /// <param name="args3">
        /// The third arguments.
        /// </param>
        /// <param name="args4">
        /// The fourth arguments.
        /// </param>
        /// <param name="args5">
        /// The fifth arguments.
        /// </param>
        /// <param name="args6">
        /// The sixth arguments.
        /// </param>
        /// <returns>
        /// The new test cases with arguments.
        /// </returns>
        public static ITestCasesWithArgs<T1, T2, T3, T4, T5, T6> WithArgs<T1, T2, T3, T4, T5, T6>(IEnumerable<T1> args1, IEnumerable<T2> args2, IEnumerable<T3> args3, IEnumerable<T4> args4, IEnumerable<T5> args5, IEnumerable<T6> args6)
        {
            return new TestCasesWithArgs<T1, T2, T3, T4, T5, T6>(args1, args2, args3, args4, args5, args6);
        }

        /// <summary>
        /// Returns test cases with arguments.
        /// </summary>
        /// <typeparam name="T1">
        /// A type of the first arguments.
        /// </typeparam>
        /// <typeparam name="T2">
        /// A type of the second arguments.
        /// </typeparam>
        /// <typeparam name="T3">
        /// A type of the third arguments.
        /// </typeparam>
        /// <typeparam name="T4">
        /// A type of the fourth arguments.
        /// </typeparam>
        /// <typeparam name="T5">
        /// A type of the fifth arguments.
        /// </typeparam>
        /// <typeparam name="T6">
        /// A type of the sixth arguments.
        /// </typeparam>
        /// <typeparam name="T7">
        /// A type of the seventh arguments.
        /// </typeparam>
        /// <param name="args1">
        /// The first arguments.
        /// </param>
        /// <param name="args2">
        /// The second arguments.
        /// </param>
        /// <param name="args3">
        /// The third arguments.
        /// </param>
        /// <param name="args4">
        /// The fourth arguments.
        /// </param>
        /// <param name="args5">
        /// The fifth arguments.
        /// </param>
        /// <param name="args6">
        /// The sixth arguments.
        /// </param>
        /// <param name="args7">
        /// The seventh arguments.
        /// </param>
        /// <returns>
        /// The new test cases with arguments.
        /// </returns>
        public static ITestCasesWithArgs<T1, T2, T3, T4, T5, T6, T7> WithArgs<T1, T2, T3, T4, T5, T6, T7>(IEnumerable<T1> args1, IEnumerable<T2> args2, IEnumerable<T3> args3, IEnumerable<T4> args4, IEnumerable<T5> args5, IEnumerable<T6> args6, IEnumerable<T7> args7)
        {
            return new TestCasesWithArgs<T1, T2, T3, T4, T5, T6, T7>(args1, args2, args3, args4, args5, args6, args7);
        }

        /// <summary>
        /// Returns test cases with arguments.
        /// </summary>
        /// <typeparam name="T1">
        /// A type of the first arguments.
        /// </typeparam>
        /// <typeparam name="T2">
        /// A type of the second arguments.
        /// </typeparam>
        /// <typeparam name="T3">
        /// A type of the third arguments.
        /// </typeparam>
        /// <typeparam name="T4">
        /// A type of the fourth arguments.
        /// </typeparam>
        /// <typeparam name="T5">
        /// A type of the fifth arguments.
        /// </typeparam>
        /// <typeparam name="T6">
        /// A type of the sixth arguments.
        /// </typeparam>
        /// <typeparam name="T7">
        /// A type of the seventh arguments.
        /// </typeparam>
        /// <typeparam name="T8">
        /// A type of the eighth arguments.
        /// </typeparam>
        /// <param name="args1">
        /// The first arguments.
        /// </param>
        /// <param name="args2">
        /// The second arguments.
        /// </param>
        /// <param name="args3">
        /// The third arguments.
        /// </param>
        /// <param name="args4">
        /// The fourth arguments.
        /// </param>
        /// <param name="args5">
        /// The fifth arguments.
        /// </param>
        /// <param name="args6">
        /// The sixth arguments.
        /// </param>
        /// <param name="args7">
        /// The seventh arguments.
        /// </param>
        /// <param name="args8">
        /// The eighth arguments.
        /// </param>
        /// <returns>
        /// The new test cases with arguments.
        /// </returns>
        public static ITestCasesWithArgs<T1, T2, T3, T4, T5, T6, T7, T8> WithArgs<T1, T2, T3, T4, T5, T6, T7, T8>(IEnumerable<T1> args1, IEnumerable<T2> args2, IEnumerable<T3> args3, IEnumerable<T4> args4, IEnumerable<T5> args5, IEnumerable<T6> args6, IEnumerable<T7> args7, IEnumerable<T8> args8)
        {
            return new TestCasesWithArgs<T1, T2, T3, T4, T5, T6, T7, T8>(args1, args2, args3, args4, args5, args6, args7, args8);
        }

        /// <summary>
        /// Returns test cases with arguments.
        /// </summary>
        /// <typeparam name="T1">
        /// A type of the first arguments.
        /// </typeparam>
        /// <typeparam name="T2">
        /// A type of the second arguments.
        /// </typeparam>
        /// <typeparam name="T3">
        /// A type of the third arguments.
        /// </typeparam>
        /// <typeparam name="T4">
        /// A type of the fourth arguments.
        /// </typeparam>
        /// <typeparam name="T5">
        /// A type of the fifth arguments.
        /// </typeparam>
        /// <typeparam name="T6">
        /// A type of the sixth arguments.
        /// </typeparam>
        /// <typeparam name="T7">
        /// A type of the seventh arguments.
        /// </typeparam>
        /// <typeparam name="T8">
        /// A type of the eighth arguments.
        /// </typeparam>
        /// <typeparam name="T9">
        /// A type of the ninth arguments.
        /// </typeparam>
        /// <param name="args1">
        /// The first arguments.
        /// </param>
        /// <param name="args2">
        /// The second arguments.
        /// </param>
        /// <param name="args3">
        /// The third arguments.
        /// </param>
        /// <param name="args4">
        /// The fourth arguments.
        /// </param>
        /// <param name="args5">
        /// The fifth arguments.
        /// </param>
        /// <param name="args6">
        /// The sixth arguments.
        /// </param>
        /// <param name="args7">
        /// The seventh arguments.
        /// </param>
        /// <param name="args8">
        /// The eighth arguments.
        /// </param>
        /// <param name="args9">
        /// The ninth arguments.
        /// </param>
        /// <returns>
        /// The new test cases with arguments.
        /// </returns>
        public static ITestCasesWithArgs<T1, T2, T3, T4, T5, T6, T7, T8, T9> WithArgs<T1, T2, T3, T4, T5, T6, T7, T8, T9>(IEnumerable<T1> args1, IEnumerable<T2> args2, IEnumerable<T3> args3, IEnumerable<T4> args4, IEnumerable<T5> args5, IEnumerable<T6> args6, IEnumerable<T7> args7, IEnumerable<T8> args8, IEnumerable<T9> args9)
        {
            return new TestCasesWithArgs<T1, T2, T3, T4, T5, T6, T7, T8, T9>(args1, args2, args3, args4, args5, args6, args7, args8, args9);
        }

        /// <summary>
        /// Returns test cases with arguments.
        /// </summary>
        /// <typeparam name="T1">
        /// A type of the first arguments.
        /// </typeparam>
        /// <typeparam name="T2">
        /// A type of the second arguments.
        /// </typeparam>
        /// <typeparam name="T3">
        /// A type of the third arguments.
        /// </typeparam>
        /// <typeparam name="T4">
        /// A type of the fourth arguments.
        /// </typeparam>
        /// <typeparam name="T5">
        /// A type of the fifth arguments.
        /// </typeparam>
        /// <typeparam name="T6">
        /// A type of the sixth arguments.
        /// </typeparam>
        /// <typeparam name="T7">
        /// A type of the seventh arguments.
        /// </typeparam>
        /// <typeparam name="T8">
        /// A type of the eighth arguments.
        /// </typeparam>
        /// <typeparam name="T9">
        /// A type of the ninth arguments.
        /// </typeparam>
        /// <typeparam name="T10">
        /// A type of the tenth arguments.
        /// </typeparam>
        /// <param name="args1">
        /// The first arguments.
        /// </param>
        /// <param name="args2">
        /// The second arguments.
        /// </param>
        /// <param name="args3">
        /// The third arguments.
        /// </param>
        /// <param name="args4">
        /// The fourth arguments.
        /// </param>
        /// <param name="args5">
        /// The fifth arguments.
        /// </param>
        /// <param name="args6">
        /// The sixth arguments.
        /// </param>
        /// <param name="args7">
        /// The seventh arguments.
        /// </param>
        /// <param name="args8">
        /// The eighth arguments.
        /// </param>
        /// <param name="args9">
        /// The ninth arguments.
        /// </param>
        /// <param name="args10">
        /// The tenth arguments.
        /// </param>
        /// <returns>
        /// The new test cases with arguments.
        /// </returns>
        public static ITestCasesWithArgs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> WithArgs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(IEnumerable<T1> args1, IEnumerable<T2> args2, IEnumerable<T3> args3, IEnumerable<T4> args4, IEnumerable<T5> args5, IEnumerable<T6> args6, IEnumerable<T7> args7, IEnumerable<T8> args8, IEnumerable<T9> args9, IEnumerable<T10> args10)
        {
            return new TestCasesWithArgs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(args1, args2, args3, args4, args5, args6, args7, args8, args9, args10);
        }

        /// <summary>
        /// Returns test cases with arguments.
        /// </summary>
        /// <typeparam name="T1">
        /// A type of the first arguments.
        /// </typeparam>
        /// <typeparam name="T2">
        /// A type of the second arguments.
        /// </typeparam>
        /// <typeparam name="T3">
        /// A type of the third arguments.
        /// </typeparam>
        /// <typeparam name="T4">
        /// A type of the fourth arguments.
        /// </typeparam>
        /// <typeparam name="T5">
        /// A type of the fifth arguments.
        /// </typeparam>
        /// <typeparam name="T6">
        /// A type of the sixth arguments.
        /// </typeparam>
        /// <typeparam name="T7">
        /// A type of the seventh arguments.
        /// </typeparam>
        /// <typeparam name="T8">
        /// A type of the eighth arguments.
        /// </typeparam>
        /// <typeparam name="T9">
        /// A type of the ninth arguments.
        /// </typeparam>
        /// <typeparam name="T10">
        /// A type of the tenth arguments.
        /// </typeparam>
        /// <typeparam name="T11">
        /// A type of the eleventh arguments.
        /// </typeparam>
        /// <param name="args1">
        /// The first arguments.
        /// </param>
        /// <param name="args2">
        /// The second arguments.
        /// </param>
        /// <param name="args3">
        /// The third arguments.
        /// </param>
        /// <param name="args4">
        /// The fourth arguments.
        /// </param>
        /// <param name="args5">
        /// The fifth arguments.
        /// </param>
        /// <param name="args6">
        /// The sixth arguments.
        /// </param>
        /// <param name="args7">
        /// The seventh arguments.
        /// </param>
        /// <param name="args8">
        /// The eighth arguments.
        /// </param>
        /// <param name="args9">
        /// The ninth arguments.
        /// </param>
        /// <param name="args10">
        /// The tenth arguments.
        /// </param>
        /// <param name="args11">
        /// The eleventh arguments.
        /// </param>
        /// <returns>
        /// The new test cases with arguments.
        /// </returns>
        public static ITestCasesWithArgs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> WithArgs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(IEnumerable<T1> args1, IEnumerable<T2> args2, IEnumerable<T3> args3, IEnumerable<T4> args4, IEnumerable<T5> args5, IEnumerable<T6> args6, IEnumerable<T7> args7, IEnumerable<T8> args8, IEnumerable<T9> args9, IEnumerable<T10> args10, IEnumerable<T11> args11)
        {
            return new TestCasesWithArgs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(args1, args2, args3, args4, args5, args6, args7, args8, args9, args10, args11);
        }

        /// <summary>
        /// Returns test cases with arguments.
        /// </summary>
        /// <typeparam name="T1">
        /// A type of the first arguments.
        /// </typeparam>
        /// <typeparam name="T2">
        /// A type of the second arguments.
        /// </typeparam>
        /// <typeparam name="T3">
        /// A type of the third arguments.
        /// </typeparam>
        /// <typeparam name="T4">
        /// A type of the fourth arguments.
        /// </typeparam>
        /// <typeparam name="T5">
        /// A type of the fifth arguments.
        /// </typeparam>
        /// <typeparam name="T6">
        /// A type of the sixth arguments.
        /// </typeparam>
        /// <typeparam name="T7">
        /// A type of the seventh arguments.
        /// </typeparam>
        /// <typeparam name="T8">
        /// A type of the eighth arguments.
        /// </typeparam>
        /// <typeparam name="T9">
        /// A type of the ninth arguments.
        /// </typeparam>
        /// <typeparam name="T10">
        /// A type of the tenth arguments.
        /// </typeparam>
        /// <typeparam name="T11">
        /// A type of the eleventh arguments.
        /// </typeparam>
        /// <typeparam name="T12">
        /// A type of the twelfth arguments.
        /// </typeparam>
        /// <param name="args1">
        /// The first arguments.
        /// </param>
        /// <param name="args2">
        /// The second arguments.
        /// </param>
        /// <param name="args3">
        /// The third arguments.
        /// </param>
        /// <param name="args4">
        /// The fourth arguments.
        /// </param>
        /// <param name="args5">
        /// The fifth arguments.
        /// </param>
        /// <param name="args6">
        /// The sixth arguments.
        /// </param>
        /// <param name="args7">
        /// The seventh arguments.
        /// </param>
        /// <param name="args8">
        /// The eighth arguments.
        /// </param>
        /// <param name="args9">
        /// The ninth arguments.
        /// </param>
        /// <param name="args10">
        /// The tenth arguments.
        /// </param>
        /// <param name="args11">
        /// The eleventh arguments.
        /// </param>
        /// <param name="args12">
        /// The twelfth arguments.
        /// </param>
        /// <returns>
        /// The new test cases with arguments.
        /// </returns>
        public static ITestCasesWithArgs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> WithArgs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(IEnumerable<T1> args1, IEnumerable<T2> args2, IEnumerable<T3> args3, IEnumerable<T4> args4, IEnumerable<T5> args5, IEnumerable<T6> args6, IEnumerable<T7> args7, IEnumerable<T8> args8, IEnumerable<T9> args9, IEnumerable<T10> args10, IEnumerable<T11> args11, IEnumerable<T12> args12)
        {
            return new TestCasesWithArgs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(args1, args2, args3, args4, args5, args6, args7, args8, args9, args10, args11, args12);
        }

        /// <summary>
        /// Returns test cases with arguments.
        /// </summary>
        /// <typeparam name="T1">
        /// A type of the first arguments.
        /// </typeparam>
        /// <typeparam name="T2">
        /// A type of the second arguments.
        /// </typeparam>
        /// <typeparam name="T3">
        /// A type of the third arguments.
        /// </typeparam>
        /// <typeparam name="T4">
        /// A type of the fourth arguments.
        /// </typeparam>
        /// <typeparam name="T5">
        /// A type of the fifth arguments.
        /// </typeparam>
        /// <typeparam name="T6">
        /// A type of the sixth arguments.
        /// </typeparam>
        /// <typeparam name="T7">
        /// A type of the seventh arguments.
        /// </typeparam>
        /// <typeparam name="T8">
        /// A type of the eighth arguments.
        /// </typeparam>
        /// <typeparam name="T9">
        /// A type of the ninth arguments.
        /// </typeparam>
        /// <typeparam name="T10">
        /// A type of the tenth arguments.
        /// </typeparam>
        /// <typeparam name="T11">
        /// A type of the eleventh arguments.
        /// </typeparam>
        /// <typeparam name="T12">
        /// A type of the twelfth arguments.
        /// </typeparam>
        /// <typeparam name="T13">
        /// A type of the thirteenth arguments.
        /// </typeparam>
        /// <param name="args1">
        /// The first arguments.
        /// </param>
        /// <param name="args2">
        /// The second arguments.
        /// </param>
        /// <param name="args3">
        /// The third arguments.
        /// </param>
        /// <param name="args4">
        /// The fourth arguments.
        /// </param>
        /// <param name="args5">
        /// The fifth arguments.
        /// </param>
        /// <param name="args6">
        /// The sixth arguments.
        /// </param>
        /// <param name="args7">
        /// The seventh arguments.
        /// </param>
        /// <param name="args8">
        /// The eighth arguments.
        /// </param>
        /// <param name="args9">
        /// The ninth arguments.
        /// </param>
        /// <param name="args10">
        /// The tenth arguments.
        /// </param>
        /// <param name="args11">
        /// The eleventh arguments.
        /// </param>
        /// <param name="args12">
        /// The twelfth arguments.
        /// </param>
        /// <param name="args13">
        /// The thirteenth arguments.
        /// </param>
        /// <returns>
        /// The new test cases with arguments.
        /// </returns>
        public static ITestCasesWithArgs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> WithArgs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(IEnumerable<T1> args1, IEnumerable<T2> args2, IEnumerable<T3> args3, IEnumerable<T4> args4, IEnumerable<T5> args5, IEnumerable<T6> args6, IEnumerable<T7> args7, IEnumerable<T8> args8, IEnumerable<T9> args9, IEnumerable<T10> args10, IEnumerable<T11> args11, IEnumerable<T12> args12, IEnumerable<T13> args13)
        {
            return new TestCasesWithArgs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(args1, args2, args3, args4, args5, args6, args7, args8, args9, args10, args11, args12, args13);
        }

        /// <summary>
        /// Returns test cases with arguments.
        /// </summary>
        /// <typeparam name="T1">
        /// A type of the first arguments.
        /// </typeparam>
        /// <typeparam name="T2">
        /// A type of the second arguments.
        /// </typeparam>
        /// <typeparam name="T3">
        /// A type of the third arguments.
        /// </typeparam>
        /// <typeparam name="T4">
        /// A type of the fourth arguments.
        /// </typeparam>
        /// <typeparam name="T5">
        /// A type of the fifth arguments.
        /// </typeparam>
        /// <typeparam name="T6">
        /// A type of the sixth arguments.
        /// </typeparam>
        /// <typeparam name="T7">
        /// A type of the seventh arguments.
        /// </typeparam>
        /// <typeparam name="T8">
        /// A type of the eighth arguments.
        /// </typeparam>
        /// <typeparam name="T9">
        /// A type of the ninth arguments.
        /// </typeparam>
        /// <typeparam name="T10">
        /// A type of the tenth arguments.
        /// </typeparam>
        /// <typeparam name="T11">
        /// A type of the eleventh arguments.
        /// </typeparam>
        /// <typeparam name="T12">
        /// A type of the twelfth arguments.
        /// </typeparam>
        /// <typeparam name="T13">
        /// A type of the thirteenth arguments.
        /// </typeparam>
        /// <typeparam name="T14">
        /// A type of the fourteenth arguments.
        /// </typeparam>
        /// <param name="args1">
        /// The first arguments.
        /// </param>
        /// <param name="args2">
        /// The second arguments.
        /// </param>
        /// <param name="args3">
        /// The third arguments.
        /// </param>
        /// <param name="args4">
        /// The fourth arguments.
        /// </param>
        /// <param name="args5">
        /// The fifth arguments.
        /// </param>
        /// <param name="args6">
        /// The sixth arguments.
        /// </param>
        /// <param name="args7">
        /// The seventh arguments.
        /// </param>
        /// <param name="args8">
        /// The eighth arguments.
        /// </param>
        /// <param name="args9">
        /// The ninth arguments.
        /// </param>
        /// <param name="args10">
        /// The tenth arguments.
        /// </param>
        /// <param name="args11">
        /// The eleventh arguments.
        /// </param>
        /// <param name="args12">
        /// The twelfth arguments.
        /// </param>
        /// <param name="args13">
        /// The thirteenth arguments.
        /// </param>
        /// <param name="args14">
        /// The fourteenth arguments.
        /// </param>
        /// <returns>
        /// The new test cases with arguments.
        /// </returns>
        public static ITestCasesWithArgs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> WithArgs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(IEnumerable<T1> args1, IEnumerable<T2> args2, IEnumerable<T3> args3, IEnumerable<T4> args4, IEnumerable<T5> args5, IEnumerable<T6> args6, IEnumerable<T7> args7, IEnumerable<T8> args8, IEnumerable<T9> args9, IEnumerable<T10> args10, IEnumerable<T11> args11, IEnumerable<T12> args12, IEnumerable<T13> args13, IEnumerable<T14> args14)
        {
            return new TestCasesWithArgs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(args1, args2, args3, args4, args5, args6, args7, args8, args9, args10, args11, args12, args13, args14);
        }

        /// <summary>
        /// Returns test cases with arguments.
        /// </summary>
        /// <typeparam name="T1">
        /// A type of the first arguments.
        /// </typeparam>
        /// <typeparam name="T2">
        /// A type of the second arguments.
        /// </typeparam>
        /// <typeparam name="T3">
        /// A type of the third arguments.
        /// </typeparam>
        /// <typeparam name="T4">
        /// A type of the fourth arguments.
        /// </typeparam>
        /// <typeparam name="T5">
        /// A type of the fifth arguments.
        /// </typeparam>
        /// <typeparam name="T6">
        /// A type of the sixth arguments.
        /// </typeparam>
        /// <typeparam name="T7">
        /// A type of the seventh arguments.
        /// </typeparam>
        /// <typeparam name="T8">
        /// A type of the eighth arguments.
        /// </typeparam>
        /// <typeparam name="T9">
        /// A type of the ninth arguments.
        /// </typeparam>
        /// <typeparam name="T10">
        /// A type of the tenth arguments.
        /// </typeparam>
        /// <typeparam name="T11">
        /// A type of the eleventh arguments.
        /// </typeparam>
        /// <typeparam name="T12">
        /// A type of the twelfth arguments.
        /// </typeparam>
        /// <typeparam name="T13">
        /// A type of the thirteenth arguments.
        /// </typeparam>
        /// <typeparam name="T14">
        /// A type of the fourteenth arguments.
        /// </typeparam>
        /// <typeparam name="T15">
        /// A type of the fifteenth arguments.
        /// </typeparam>
        /// <param name="args1">
        /// The first arguments.
        /// </param>
        /// <param name="args2">
        /// The second arguments.
        /// </param>
        /// <param name="args3">
        /// The third arguments.
        /// </param>
        /// <param name="args4">
        /// The fourth arguments.
        /// </param>
        /// <param name="args5">
        /// The fifth arguments.
        /// </param>
        /// <param name="args6">
        /// The sixth arguments.
        /// </param>
        /// <param name="args7">
        /// The seventh arguments.
        /// </param>
        /// <param name="args8">
        /// The eighth arguments.
        /// </param>
        /// <param name="args9">
        /// The ninth arguments.
        /// </param>
        /// <param name="args10">
        /// The tenth arguments.
        /// </param>
        /// <param name="args11">
        /// The eleventh arguments.
        /// </param>
        /// <param name="args12">
        /// The twelfth arguments.
        /// </param>
        /// <param name="args13">
        /// The thirteenth arguments.
        /// </param>
        /// <param name="args14">
        /// The fourteenth arguments.
        /// </param>
        /// <param name="args15">
        /// The fifteenth arguments.
        /// </param>
        /// <returns>
        /// The new test cases with arguments.
        /// </returns>
        public static ITestCasesWithArgs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> WithArgs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(IEnumerable<T1> args1, IEnumerable<T2> args2, IEnumerable<T3> args3, IEnumerable<T4> args4, IEnumerable<T5> args5, IEnumerable<T6> args6, IEnumerable<T7> args7, IEnumerable<T8> args8, IEnumerable<T9> args9, IEnumerable<T10> args10, IEnumerable<T11> args11, IEnumerable<T12> args12, IEnumerable<T13> args13, IEnumerable<T14> args14, IEnumerable<T15> args15)
        {
            return new TestCasesWithArgs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(args1, args2, args3, args4, args5, args6, args7, args8, args9, args10, args11, args12, args13, args14, args15);
        }

        /// <summary>
        /// Returns test cases with arguments.
        /// </summary>
        /// <typeparam name="T1">
        /// A type of the first arguments.
        /// </typeparam>
        /// <typeparam name="T2">
        /// A type of the second arguments.
        /// </typeparam>
        /// <typeparam name="T3">
        /// A type of the third arguments.
        /// </typeparam>
        /// <typeparam name="T4">
        /// A type of the fourth arguments.
        /// </typeparam>
        /// <typeparam name="T5">
        /// A type of the fifth arguments.
        /// </typeparam>
        /// <typeparam name="T6">
        /// A type of the sixth arguments.
        /// </typeparam>
        /// <typeparam name="T7">
        /// A type of the seventh arguments.
        /// </typeparam>
        /// <typeparam name="T8">
        /// A type of the eighth arguments.
        /// </typeparam>
        /// <typeparam name="T9">
        /// A type of the ninth arguments.
        /// </typeparam>
        /// <typeparam name="T10">
        /// A type of the tenth arguments.
        /// </typeparam>
        /// <typeparam name="T11">
        /// A type of the eleventh arguments.
        /// </typeparam>
        /// <typeparam name="T12">
        /// A type of the twelfth arguments.
        /// </typeparam>
        /// <typeparam name="T13">
        /// A type of the thirteenth arguments.
        /// </typeparam>
        /// <typeparam name="T14">
        /// A type of the fourteenth arguments.
        /// </typeparam>
        /// <typeparam name="T15">
        /// A type of the fifteenth arguments.
        /// </typeparam>
        /// <typeparam name="T16">
        /// A type of the sixteenth arguments.
        /// </typeparam>
        /// <param name="args1">
        /// The first arguments.
        /// </param>
        /// <param name="args2">
        /// The second arguments.
        /// </param>
        /// <param name="args3">
        /// The third arguments.
        /// </param>
        /// <param name="args4">
        /// The fourth arguments.
        /// </param>
        /// <param name="args5">
        /// The fifth arguments.
        /// </param>
        /// <param name="args6">
        /// The sixth arguments.
        /// </param>
        /// <param name="args7">
        /// The seventh arguments.
        /// </param>
        /// <param name="args8">
        /// The eighth arguments.
        /// </param>
        /// <param name="args9">
        /// The ninth arguments.
        /// </param>
        /// <param name="args10">
        /// The tenth arguments.
        /// </param>
        /// <param name="args11">
        /// The eleventh arguments.
        /// </param>
        /// <param name="args12">
        /// The twelfth arguments.
        /// </param>
        /// <param name="args13">
        /// The thirteenth arguments.
        /// </param>
        /// <param name="args14">
        /// The fourteenth arguments.
        /// </param>
        /// <param name="args15">
        /// The fifteenth arguments.
        /// </param>
        /// <param name="args16">
        /// The sixteenth arguments.
        /// </param>
        /// <returns>
        /// The new test cases with arguments.
        /// </returns>
        public static ITestCasesWithArgs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> WithArgs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(IEnumerable<T1> args1, IEnumerable<T2> args2, IEnumerable<T3> args3, IEnumerable<T4> args4, IEnumerable<T5> args5, IEnumerable<T6> args6, IEnumerable<T7> args7, IEnumerable<T8> args8, IEnumerable<T9> args9, IEnumerable<T10> args10, IEnumerable<T11> args11, IEnumerable<T12> args12, IEnumerable<T13> args13, IEnumerable<T14> args14, IEnumerable<T15> args15, IEnumerable<T16> args16)
        {
            return new TestCasesWithArgs<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(args1, args2, args3, args4, args5, args6, args7, args8, args9, args10, args11, args12, args13, args14, args15, args16);
        }

    }
}