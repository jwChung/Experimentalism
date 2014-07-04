﻿using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Jwc.Experiment.Xunit
{
    /// <summary>
    /// Represents a weakly-typed test case that can be turned into an xUnit.net ITestCommand when
    /// returned from a test method adorned with the <see cref="FirstClassTestAttribute" />.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1508:ClosingCurlyBracketsMustNotBePrecededByBlankLine", Justification = "The is automatically generated.")]
    public partial class TestCase : ITestCase
    {
        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2>(Action<TArg1, TArg2> action)
        {
            return new TestCase(action);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <param name="displayParameterName">
        /// A name of the parameter.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2>(Action<TArg1, TArg2> action, string displayParameterName)
        {
            return new TestCase(action, displayParameterName);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3>(Action<TArg1, TArg2, TArg3> action)
        {
            return new TestCase(action);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <param name="displayParameterName">
        /// A name of the parameter.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3>(Action<TArg1, TArg2, TArg3> action, string displayParameterName)
        {
            return new TestCase(action, displayParameterName);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4>(Action<TArg1, TArg2, TArg3, TArg4> action)
        {
            return new TestCase(action);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <param name="displayParameterName">
        /// A name of the parameter.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4>(Action<TArg1, TArg2, TArg3, TArg4> action, string displayParameterName)
        {
            return new TestCase(action, displayParameterName);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <typeparam name="TArg5">A type of the fifth argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4, TArg5>(Action<TArg1, TArg2, TArg3, TArg4, TArg5> action)
        {
            return new TestCase(action);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <typeparam name="TArg5">A type of the fifth argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <param name="displayParameterName">
        /// A name of the parameter.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4, TArg5>(Action<TArg1, TArg2, TArg3, TArg4, TArg5> action, string displayParameterName)
        {
            return new TestCase(action, displayParameterName);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <typeparam name="TArg5">A type of the fifth argument.</typeparam>
        /// <typeparam name="TArg6">A type of the sixth argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> action)
        {
            return new TestCase(action);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <typeparam name="TArg5">A type of the fifth argument.</typeparam>
        /// <typeparam name="TArg6">A type of the sixth argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <param name="displayParameterName">
        /// A name of the parameter.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> action, string displayParameterName)
        {
            return new TestCase(action, displayParameterName);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <typeparam name="TArg5">A type of the fifth argument.</typeparam>
        /// <typeparam name="TArg6">A type of the sixth argument.</typeparam>
        /// <typeparam name="TArg7">A type of the seventh argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> action)
        {
            return new TestCase(action);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <typeparam name="TArg5">A type of the fifth argument.</typeparam>
        /// <typeparam name="TArg6">A type of the sixth argument.</typeparam>
        /// <typeparam name="TArg7">A type of the seventh argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <param name="displayParameterName">
        /// A name of the parameter.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> action, string displayParameterName)
        {
            return new TestCase(action, displayParameterName);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <typeparam name="TArg5">A type of the fifth argument.</typeparam>
        /// <typeparam name="TArg6">A type of the sixth argument.</typeparam>
        /// <typeparam name="TArg7">A type of the seventh argument.</typeparam>
        /// <typeparam name="TArg8">A type of the eighth argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> action)
        {
            return new TestCase(action);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <typeparam name="TArg5">A type of the fifth argument.</typeparam>
        /// <typeparam name="TArg6">A type of the sixth argument.</typeparam>
        /// <typeparam name="TArg7">A type of the seventh argument.</typeparam>
        /// <typeparam name="TArg8">A type of the eighth argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <param name="displayParameterName">
        /// A name of the parameter.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> action, string displayParameterName)
        {
            return new TestCase(action, displayParameterName);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <typeparam name="TArg5">A type of the fifth argument.</typeparam>
        /// <typeparam name="TArg6">A type of the sixth argument.</typeparam>
        /// <typeparam name="TArg7">A type of the seventh argument.</typeparam>
        /// <typeparam name="TArg8">A type of the eighth argument.</typeparam>
        /// <typeparam name="TArg9">A type of the ninth argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> action)
        {
            return new TestCase(action);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <typeparam name="TArg5">A type of the fifth argument.</typeparam>
        /// <typeparam name="TArg6">A type of the sixth argument.</typeparam>
        /// <typeparam name="TArg7">A type of the seventh argument.</typeparam>
        /// <typeparam name="TArg8">A type of the eighth argument.</typeparam>
        /// <typeparam name="TArg9">A type of the ninth argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <param name="displayParameterName">
        /// A name of the parameter.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> action, string displayParameterName)
        {
            return new TestCase(action, displayParameterName);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <typeparam name="TArg5">A type of the fifth argument.</typeparam>
        /// <typeparam name="TArg6">A type of the sixth argument.</typeparam>
        /// <typeparam name="TArg7">A type of the seventh argument.</typeparam>
        /// <typeparam name="TArg8">A type of the eighth argument.</typeparam>
        /// <typeparam name="TArg9">A type of the ninth argument.</typeparam>
        /// <typeparam name="TArg10">A type of the tenth argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> action)
        {
            return new TestCase(action);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <typeparam name="TArg5">A type of the fifth argument.</typeparam>
        /// <typeparam name="TArg6">A type of the sixth argument.</typeparam>
        /// <typeparam name="TArg7">A type of the seventh argument.</typeparam>
        /// <typeparam name="TArg8">A type of the eighth argument.</typeparam>
        /// <typeparam name="TArg9">A type of the ninth argument.</typeparam>
        /// <typeparam name="TArg10">A type of the tenth argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <param name="displayParameterName">
        /// A name of the parameter.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> action, string displayParameterName)
        {
            return new TestCase(action, displayParameterName);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <typeparam name="TArg5">A type of the fifth argument.</typeparam>
        /// <typeparam name="TArg6">A type of the sixth argument.</typeparam>
        /// <typeparam name="TArg7">A type of the seventh argument.</typeparam>
        /// <typeparam name="TArg8">A type of the eighth argument.</typeparam>
        /// <typeparam name="TArg9">A type of the ninth argument.</typeparam>
        /// <typeparam name="TArg10">A type of the tenth argument.</typeparam>
        /// <typeparam name="TArg11">A type of the eleventh argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> action)
        {
            return new TestCase(action);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <typeparam name="TArg5">A type of the fifth argument.</typeparam>
        /// <typeparam name="TArg6">A type of the sixth argument.</typeparam>
        /// <typeparam name="TArg7">A type of the seventh argument.</typeparam>
        /// <typeparam name="TArg8">A type of the eighth argument.</typeparam>
        /// <typeparam name="TArg9">A type of the ninth argument.</typeparam>
        /// <typeparam name="TArg10">A type of the tenth argument.</typeparam>
        /// <typeparam name="TArg11">A type of the eleventh argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <param name="displayParameterName">
        /// A name of the parameter.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> action, string displayParameterName)
        {
            return new TestCase(action, displayParameterName);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <typeparam name="TArg5">A type of the fifth argument.</typeparam>
        /// <typeparam name="TArg6">A type of the sixth argument.</typeparam>
        /// <typeparam name="TArg7">A type of the seventh argument.</typeparam>
        /// <typeparam name="TArg8">A type of the eighth argument.</typeparam>
        /// <typeparam name="TArg9">A type of the ninth argument.</typeparam>
        /// <typeparam name="TArg10">A type of the tenth argument.</typeparam>
        /// <typeparam name="TArg11">A type of the eleventh argument.</typeparam>
        /// <typeparam name="TArg12">A type of the twelfth argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> action)
        {
            return new TestCase(action);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <typeparam name="TArg5">A type of the fifth argument.</typeparam>
        /// <typeparam name="TArg6">A type of the sixth argument.</typeparam>
        /// <typeparam name="TArg7">A type of the seventh argument.</typeparam>
        /// <typeparam name="TArg8">A type of the eighth argument.</typeparam>
        /// <typeparam name="TArg9">A type of the ninth argument.</typeparam>
        /// <typeparam name="TArg10">A type of the tenth argument.</typeparam>
        /// <typeparam name="TArg11">A type of the eleventh argument.</typeparam>
        /// <typeparam name="TArg12">A type of the twelfth argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <param name="displayParameterName">
        /// A name of the parameter.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> action, string displayParameterName)
        {
            return new TestCase(action, displayParameterName);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <typeparam name="TArg5">A type of the fifth argument.</typeparam>
        /// <typeparam name="TArg6">A type of the sixth argument.</typeparam>
        /// <typeparam name="TArg7">A type of the seventh argument.</typeparam>
        /// <typeparam name="TArg8">A type of the eighth argument.</typeparam>
        /// <typeparam name="TArg9">A type of the ninth argument.</typeparam>
        /// <typeparam name="TArg10">A type of the tenth argument.</typeparam>
        /// <typeparam name="TArg11">A type of the eleventh argument.</typeparam>
        /// <typeparam name="TArg12">A type of the twelfth argument.</typeparam>
        /// <typeparam name="TArg13">A type of the thirteenth argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> action)
        {
            return new TestCase(action);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <typeparam name="TArg5">A type of the fifth argument.</typeparam>
        /// <typeparam name="TArg6">A type of the sixth argument.</typeparam>
        /// <typeparam name="TArg7">A type of the seventh argument.</typeparam>
        /// <typeparam name="TArg8">A type of the eighth argument.</typeparam>
        /// <typeparam name="TArg9">A type of the ninth argument.</typeparam>
        /// <typeparam name="TArg10">A type of the tenth argument.</typeparam>
        /// <typeparam name="TArg11">A type of the eleventh argument.</typeparam>
        /// <typeparam name="TArg12">A type of the twelfth argument.</typeparam>
        /// <typeparam name="TArg13">A type of the thirteenth argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <param name="displayParameterName">
        /// A name of the parameter.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> action, string displayParameterName)
        {
            return new TestCase(action, displayParameterName);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <typeparam name="TArg5">A type of the fifth argument.</typeparam>
        /// <typeparam name="TArg6">A type of the sixth argument.</typeparam>
        /// <typeparam name="TArg7">A type of the seventh argument.</typeparam>
        /// <typeparam name="TArg8">A type of the eighth argument.</typeparam>
        /// <typeparam name="TArg9">A type of the ninth argument.</typeparam>
        /// <typeparam name="TArg10">A type of the tenth argument.</typeparam>
        /// <typeparam name="TArg11">A type of the eleventh argument.</typeparam>
        /// <typeparam name="TArg12">A type of the twelfth argument.</typeparam>
        /// <typeparam name="TArg13">A type of the thirteenth argument.</typeparam>
        /// <typeparam name="TArg14">A type of the fourteenth argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> action)
        {
            return new TestCase(action);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <typeparam name="TArg5">A type of the fifth argument.</typeparam>
        /// <typeparam name="TArg6">A type of the sixth argument.</typeparam>
        /// <typeparam name="TArg7">A type of the seventh argument.</typeparam>
        /// <typeparam name="TArg8">A type of the eighth argument.</typeparam>
        /// <typeparam name="TArg9">A type of the ninth argument.</typeparam>
        /// <typeparam name="TArg10">A type of the tenth argument.</typeparam>
        /// <typeparam name="TArg11">A type of the eleventh argument.</typeparam>
        /// <typeparam name="TArg12">A type of the twelfth argument.</typeparam>
        /// <typeparam name="TArg13">A type of the thirteenth argument.</typeparam>
        /// <typeparam name="TArg14">A type of the fourteenth argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <param name="displayParameterName">
        /// A name of the parameter.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> action, string displayParameterName)
        {
            return new TestCase(action, displayParameterName);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <typeparam name="TArg5">A type of the fifth argument.</typeparam>
        /// <typeparam name="TArg6">A type of the sixth argument.</typeparam>
        /// <typeparam name="TArg7">A type of the seventh argument.</typeparam>
        /// <typeparam name="TArg8">A type of the eighth argument.</typeparam>
        /// <typeparam name="TArg9">A type of the ninth argument.</typeparam>
        /// <typeparam name="TArg10">A type of the tenth argument.</typeparam>
        /// <typeparam name="TArg11">A type of the eleventh argument.</typeparam>
        /// <typeparam name="TArg12">A type of the twelfth argument.</typeparam>
        /// <typeparam name="TArg13">A type of the thirteenth argument.</typeparam>
        /// <typeparam name="TArg14">A type of the fourteenth argument.</typeparam>
        /// <typeparam name="TArg15">A type of the fifteenth argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> action)
        {
            return new TestCase(action);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <typeparam name="TArg5">A type of the fifth argument.</typeparam>
        /// <typeparam name="TArg6">A type of the sixth argument.</typeparam>
        /// <typeparam name="TArg7">A type of the seventh argument.</typeparam>
        /// <typeparam name="TArg8">A type of the eighth argument.</typeparam>
        /// <typeparam name="TArg9">A type of the ninth argument.</typeparam>
        /// <typeparam name="TArg10">A type of the tenth argument.</typeparam>
        /// <typeparam name="TArg11">A type of the eleventh argument.</typeparam>
        /// <typeparam name="TArg12">A type of the twelfth argument.</typeparam>
        /// <typeparam name="TArg13">A type of the thirteenth argument.</typeparam>
        /// <typeparam name="TArg14">A type of the fourteenth argument.</typeparam>
        /// <typeparam name="TArg15">A type of the fifteenth argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <param name="displayParameterName">
        /// A name of the parameter.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> action, string displayParameterName)
        {
            return new TestCase(action, displayParameterName);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <typeparam name="TArg5">A type of the fifth argument.</typeparam>
        /// <typeparam name="TArg6">A type of the sixth argument.</typeparam>
        /// <typeparam name="TArg7">A type of the seventh argument.</typeparam>
        /// <typeparam name="TArg8">A type of the eighth argument.</typeparam>
        /// <typeparam name="TArg9">A type of the ninth argument.</typeparam>
        /// <typeparam name="TArg10">A type of the tenth argument.</typeparam>
        /// <typeparam name="TArg11">A type of the eleventh argument.</typeparam>
        /// <typeparam name="TArg12">A type of the twelfth argument.</typeparam>
        /// <typeparam name="TArg13">A type of the thirteenth argument.</typeparam>
        /// <typeparam name="TArg14">A type of the fourteenth argument.</typeparam>
        /// <typeparam name="TArg15">A type of the fifteenth argument.</typeparam>
        /// <typeparam name="TArg16">A type of the sixteenth argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> action)
        {
            return new TestCase(action);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg1">A type of the first argument.</typeparam>
        /// <typeparam name="TArg2">A type of the second argument.</typeparam>
        /// <typeparam name="TArg3">A type of the third argument.</typeparam>
        /// <typeparam name="TArg4">A type of the fourth argument.</typeparam>
        /// <typeparam name="TArg5">A type of the fifth argument.</typeparam>
        /// <typeparam name="TArg6">A type of the sixth argument.</typeparam>
        /// <typeparam name="TArg7">A type of the seventh argument.</typeparam>
        /// <typeparam name="TArg8">A type of the eighth argument.</typeparam>
        /// <typeparam name="TArg9">A type of the ninth argument.</typeparam>
        /// <typeparam name="TArg10">A type of the tenth argument.</typeparam>
        /// <typeparam name="TArg11">A type of the eleventh argument.</typeparam>
        /// <typeparam name="TArg12">A type of the twelfth argument.</typeparam>
        /// <typeparam name="TArg13">A type of the thirteenth argument.</typeparam>
        /// <typeparam name="TArg14">A type of the fourteenth argument.</typeparam>
        /// <typeparam name="TArg15">A type of the fifteenth argument.</typeparam>
        /// <typeparam name="TArg16">A type of the sixteenth argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <param name="displayParameterName">
        /// A name of the parameter.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> action, string displayParameterName)
        {
            return new TestCase(action, displayParameterName);
        }

    }
}
