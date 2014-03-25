using System;
using System.Diagnostics;

namespace Jwc.Experiment
{
    public partial class TestCase
    {
        /* Contain just the typed overloads that are just pass-through to the real implementations.
         * They all have DebuggerStepThrough to ease debugging. */

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2>(Action<TArg1, TArg2> @delegate)
        {
            return new TestCase(@delegate, new object[0]);
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2>(TArg1 arg1, Action<TArg1, TArg2> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2>(TArg1 arg1, TArg2 arg2, Action<TArg1, TArg2> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3>(Action<TArg1, TArg2, TArg3> @delegate)
        {
            return new TestCase(@delegate, new object[0]);
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3>(TArg1 arg1, Action<TArg1, TArg2, TArg3> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3>(TArg1 arg1, TArg2 arg2, Action<TArg1, TArg2, TArg3> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3>(TArg1 arg1, TArg2 arg2, TArg3 arg3, Action<TArg1, TArg2, TArg3> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4>(Action<TArg1, TArg2, TArg3, TArg4> @delegate)
        {
            return new TestCase(@delegate, new object[0]);
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4>(TArg1 arg1, Action<TArg1, TArg2, TArg3, TArg4> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4>(TArg1 arg1, TArg2 arg2, Action<TArg1, TArg2, TArg3, TArg4> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4>(TArg1 arg1, TArg2 arg2, TArg3 arg3, Action<TArg1, TArg2, TArg3, TArg4> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, Action<TArg1, TArg2, TArg3, TArg4> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5>(Action<TArg1, TArg2, TArg3, TArg4, TArg5> @delegate)
        {
            return new TestCase(@delegate, new object[0]);
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5>(TArg1 arg1, Action<TArg1, TArg2, TArg3, TArg4, TArg5> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5>(TArg1 arg1, TArg2 arg2, Action<TArg1, TArg2, TArg3, TArg4, TArg5> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5>(TArg1 arg1, TArg2 arg2, TArg3 arg3, Action<TArg1, TArg2, TArg3, TArg4, TArg5> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, Action<TArg1, TArg2, TArg3, TArg4, TArg5> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, Action<TArg1, TArg2, TArg3, TArg4, TArg5> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> @delegate)
        {
            return new TestCase(@delegate, new object[0]);
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(TArg1 arg1, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(TArg1 arg1, TArg2 arg2, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(TArg1 arg1, TArg2 arg2, TArg3 arg3, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> @delegate)
        {
            return new TestCase(@delegate, new object[0]);
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(TArg1 arg1, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(TArg1 arg1, TArg2 arg2, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(TArg1 arg1, TArg2 arg2, TArg3 arg3, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> @delegate)
        {
            return new TestCase(@delegate, new object[0]);
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(TArg1 arg1, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(TArg1 arg1, TArg2 arg2, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(TArg1 arg1, TArg2 arg2, TArg3 arg3, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> @delegate)
        {
            return new TestCase(@delegate, new object[0]);
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(TArg1 arg1, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(TArg1 arg1, TArg2 arg2, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(TArg1 arg1, TArg2 arg2, TArg3 arg3, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> @delegate)
        {
            return new TestCase(@delegate, new object[0]);
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(TArg1 arg1, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(TArg1 arg1, TArg2 arg2, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(TArg1 arg1, TArg2 arg2, TArg3 arg3, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> @delegate)
        {
            return new TestCase(@delegate, new object[0]);
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(TArg1 arg1, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(TArg1 arg1, TArg2 arg2, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(TArg1 arg1, TArg2 arg2, TArg3 arg3, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> @delegate)
        {
            return new TestCase(@delegate, new object[0]);
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(TArg1 arg1, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(TArg1 arg1, TArg2 arg2, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(TArg1 arg1, TArg2 arg2, TArg3 arg3, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> @delegate)
        {
            return new TestCase(@delegate, new object[0]);
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(TArg1 arg1, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(TArg1 arg1, TArg2 arg2, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(TArg1 arg1, TArg2 arg2, TArg3 arg3, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> @delegate)
        {
            return new TestCase(@delegate, new object[0]);
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(TArg1 arg1, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(TArg1 arg1, TArg2 arg2, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(TArg1 arg1, TArg2 arg2, TArg3 arg3, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> @delegate)
        {
            return new TestCase(@delegate, new object[0]);
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>(TArg1 arg1, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>(TArg1 arg1, TArg2 arg2, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>(TArg1 arg1, TArg2 arg2, TArg3 arg3, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> @delegate)
        {
            return new TestCase(@delegate, new object[0]);
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16>(TArg1 arg1, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16>(TArg1 arg1, TArg2 arg2, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16>(TArg1 arg1, TArg2 arg2, TArg3 arg3, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15 });
        }

		/// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <returns>
        /// The created instance.
        /// </returns>
		[DebuggerStepThrough]
        public static ITestCase New<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14, TArg15 arg15, TArg16 arg16, Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16 });
        }

    }
}
