using System;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents a weakly-typed test case that can be turned into an
    /// xUnit.net ITestCommand when returned from a test method adorned with
    /// the <see cref="FirstClassTheoremBaseAttribute" />.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification="This rule is suppressed to provide auto data to test method.")]
    public class TestCase<T1, T2> : ITestCase
    {
        private readonly Delegate _delegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="action">The test action.</param>
        public TestCase(Action<T1, T2> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (action.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite actions are not supported, set only one action operation.",
                    "action");
            }

            _delegate = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="func">The test function.</param>
        public TestCase(Func<T1, T2, object> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (func.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite functions are not supported, set only one function operation.",
                    "func");
            }

            _delegate = func;
        }

        /// <summary>
        /// Gets the test delegate.
        /// </summary>
        public Delegate Delegate
        {
            get
            {
                return _delegate;
            }
        }

        /// <summary>
        /// Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">
        /// The method adorned by a <see cref="FirstClassTheoremBaseAttribute" />.
        /// </param>
        /// <param name="testFixtureFactory">
        /// A test fixture factory to provide auto data.
        /// </param>
        /// <returns>
        /// An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory testFixtureFactory)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (testFixtureFactory == null)
            {
                throw new ArgumentNullException("testFixtureFactory");
            }

            var fixture = testFixtureFactory.Create(Delegate.Method);
            var arguments = new[]
            {
                fixture.Create(typeof(T1)),
                fixture.Create(typeof(T2))
            };
            return new FirstClassCommand(method, Delegate, arguments);
        }
    }

    /// <summary>
    /// Represents a weakly-typed test case that can be turned into an
    /// xUnit.net ITestCommand when returned from a test method adorned with
    /// the <see cref="FirstClassTheoremBaseAttribute" />.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification="This rule is suppressed to provide auto data to test method.")]
    public class TestCase<T1, T2, T3> : ITestCase
    {
        private readonly Delegate _delegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="action">The test action.</param>
        public TestCase(Action<T1, T2, T3> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (action.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite actions are not supported, set only one action operation.",
                    "action");
            }

            _delegate = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="func">The test function.</param>
        public TestCase(Func<T1, T2, T3, object> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (func.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite functions are not supported, set only one function operation.",
                    "func");
            }

            _delegate = func;
        }

        /// <summary>
        /// Gets the test delegate.
        /// </summary>
        public Delegate Delegate
        {
            get
            {
                return _delegate;
            }
        }

        /// <summary>
        /// Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">
        /// The method adorned by a <see cref="FirstClassTheoremBaseAttribute" />.
        /// </param>
        /// <param name="testFixtureFactory">
        /// A test fixture factory to provide auto data.
        /// </param>
        /// <returns>
        /// An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory testFixtureFactory)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (testFixtureFactory == null)
            {
                throw new ArgumentNullException("testFixtureFactory");
            }

            var fixture = testFixtureFactory.Create(Delegate.Method);
            var arguments = new[]
            {
                fixture.Create(typeof(T1)),
                fixture.Create(typeof(T2)),
                fixture.Create(typeof(T3))
            };
            return new FirstClassCommand(method, Delegate, arguments);
        }
    }

    /// <summary>
    /// Represents a weakly-typed test case that can be turned into an
    /// xUnit.net ITestCommand when returned from a test method adorned with
    /// the <see cref="FirstClassTheoremBaseAttribute" />.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification="This rule is suppressed to provide auto data to test method.")]
    public class TestCase<T1, T2, T3, T4> : ITestCase
    {
        private readonly Delegate _delegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="action">The test action.</param>
        public TestCase(Action<T1, T2, T3, T4> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (action.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite actions are not supported, set only one action operation.",
                    "action");
            }

            _delegate = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="func">The test function.</param>
        public TestCase(Func<T1, T2, T3, T4, object> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (func.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite functions are not supported, set only one function operation.",
                    "func");
            }

            _delegate = func;
        }

        /// <summary>
        /// Gets the test delegate.
        /// </summary>
        public Delegate Delegate
        {
            get
            {
                return _delegate;
            }
        }

        /// <summary>
        /// Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">
        /// The method adorned by a <see cref="FirstClassTheoremBaseAttribute" />.
        /// </param>
        /// <param name="testFixtureFactory">
        /// A test fixture factory to provide auto data.
        /// </param>
        /// <returns>
        /// An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory testFixtureFactory)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (testFixtureFactory == null)
            {
                throw new ArgumentNullException("testFixtureFactory");
            }

            var fixture = testFixtureFactory.Create(Delegate.Method);
            var arguments = new[]
            {
                fixture.Create(typeof(T1)),
                fixture.Create(typeof(T2)),
                fixture.Create(typeof(T3)),
                fixture.Create(typeof(T4))
            };
            return new FirstClassCommand(method, Delegate, arguments);
        }
    }

    /// <summary>
    /// Represents a weakly-typed test case that can be turned into an
    /// xUnit.net ITestCommand when returned from a test method adorned with
    /// the <see cref="FirstClassTheoremBaseAttribute" />.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification="This rule is suppressed to provide auto data to test method.")]
    public class TestCase<T1, T2, T3, T4, T5> : ITestCase
    {
        private readonly Delegate _delegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="action">The test action.</param>
        public TestCase(Action<T1, T2, T3, T4, T5> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (action.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite actions are not supported, set only one action operation.",
                    "action");
            }

            _delegate = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="func">The test function.</param>
        public TestCase(Func<T1, T2, T3, T4, T5, object> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (func.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite functions are not supported, set only one function operation.",
                    "func");
            }

            _delegate = func;
        }

        /// <summary>
        /// Gets the test delegate.
        /// </summary>
        public Delegate Delegate
        {
            get
            {
                return _delegate;
            }
        }

        /// <summary>
        /// Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">
        /// The method adorned by a <see cref="FirstClassTheoremBaseAttribute" />.
        /// </param>
        /// <param name="testFixtureFactory">
        /// A test fixture factory to provide auto data.
        /// </param>
        /// <returns>
        /// An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory testFixtureFactory)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (testFixtureFactory == null)
            {
                throw new ArgumentNullException("testFixtureFactory");
            }

            var fixture = testFixtureFactory.Create(Delegate.Method);
            var arguments = new[]
            {
                fixture.Create(typeof(T1)),
                fixture.Create(typeof(T2)),
                fixture.Create(typeof(T3)),
                fixture.Create(typeof(T4)),
                fixture.Create(typeof(T5))
            };
            return new FirstClassCommand(method, Delegate, arguments);
        }
    }

    /// <summary>
    /// Represents a weakly-typed test case that can be turned into an
    /// xUnit.net ITestCommand when returned from a test method adorned with
    /// the <see cref="FirstClassTheoremBaseAttribute" />.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification="This rule is suppressed to provide auto data to test method.")]
    public class TestCase<T1, T2, T3, T4, T5, T6> : ITestCase
    {
        private readonly Delegate _delegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="action">The test action.</param>
        public TestCase(Action<T1, T2, T3, T4, T5, T6> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (action.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite actions are not supported, set only one action operation.",
                    "action");
            }

            _delegate = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="func">The test function.</param>
        public TestCase(Func<T1, T2, T3, T4, T5, T6, object> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (func.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite functions are not supported, set only one function operation.",
                    "func");
            }

            _delegate = func;
        }

        /// <summary>
        /// Gets the test delegate.
        /// </summary>
        public Delegate Delegate
        {
            get
            {
                return _delegate;
            }
        }

        /// <summary>
        /// Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">
        /// The method adorned by a <see cref="FirstClassTheoremBaseAttribute" />.
        /// </param>
        /// <param name="testFixtureFactory">
        /// A test fixture factory to provide auto data.
        /// </param>
        /// <returns>
        /// An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory testFixtureFactory)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (testFixtureFactory == null)
            {
                throw new ArgumentNullException("testFixtureFactory");
            }

            var fixture = testFixtureFactory.Create(Delegate.Method);
            var arguments = new[]
            {
                fixture.Create(typeof(T1)),
                fixture.Create(typeof(T2)),
                fixture.Create(typeof(T3)),
                fixture.Create(typeof(T4)),
                fixture.Create(typeof(T5)),
                fixture.Create(typeof(T6))
            };
            return new FirstClassCommand(method, Delegate, arguments);
        }
    }

    /// <summary>
    /// Represents a weakly-typed test case that can be turned into an
    /// xUnit.net ITestCommand when returned from a test method adorned with
    /// the <see cref="FirstClassTheoremBaseAttribute" />.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification="This rule is suppressed to provide auto data to test method.")]
    public class TestCase<T1, T2, T3, T4, T5, T6, T7> : ITestCase
    {
        private readonly Delegate _delegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="action">The test action.</param>
        public TestCase(Action<T1, T2, T3, T4, T5, T6, T7> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (action.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite actions are not supported, set only one action operation.",
                    "action");
            }

            _delegate = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="func">The test function.</param>
        public TestCase(Func<T1, T2, T3, T4, T5, T6, T7, object> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (func.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite functions are not supported, set only one function operation.",
                    "func");
            }

            _delegate = func;
        }

        /// <summary>
        /// Gets the test delegate.
        /// </summary>
        public Delegate Delegate
        {
            get
            {
                return _delegate;
            }
        }

        /// <summary>
        /// Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">
        /// The method adorned by a <see cref="FirstClassTheoremBaseAttribute" />.
        /// </param>
        /// <param name="testFixtureFactory">
        /// A test fixture factory to provide auto data.
        /// </param>
        /// <returns>
        /// An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory testFixtureFactory)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (testFixtureFactory == null)
            {
                throw new ArgumentNullException("testFixtureFactory");
            }

            var fixture = testFixtureFactory.Create(Delegate.Method);
            var arguments = new[]
            {
                fixture.Create(typeof(T1)),
                fixture.Create(typeof(T2)),
                fixture.Create(typeof(T3)),
                fixture.Create(typeof(T4)),
                fixture.Create(typeof(T5)),
                fixture.Create(typeof(T6)),
                fixture.Create(typeof(T7))
            };
            return new FirstClassCommand(method, Delegate, arguments);
        }
    }

    /// <summary>
    /// Represents a weakly-typed test case that can be turned into an
    /// xUnit.net ITestCommand when returned from a test method adorned with
    /// the <see cref="FirstClassTheoremBaseAttribute" />.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification="This rule is suppressed to provide auto data to test method.")]
    public class TestCase<T1, T2, T3, T4, T5, T6, T7, T8> : ITestCase
    {
        private readonly Delegate _delegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="action">The test action.</param>
        public TestCase(Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (action.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite actions are not supported, set only one action operation.",
                    "action");
            }

            _delegate = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="func">The test function.</param>
        public TestCase(Func<T1, T2, T3, T4, T5, T6, T7, T8, object> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (func.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite functions are not supported, set only one function operation.",
                    "func");
            }

            _delegate = func;
        }

        /// <summary>
        /// Gets the test delegate.
        /// </summary>
        public Delegate Delegate
        {
            get
            {
                return _delegate;
            }
        }

        /// <summary>
        /// Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">
        /// The method adorned by a <see cref="FirstClassTheoremBaseAttribute" />.
        /// </param>
        /// <param name="testFixtureFactory">
        /// A test fixture factory to provide auto data.
        /// </param>
        /// <returns>
        /// An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory testFixtureFactory)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (testFixtureFactory == null)
            {
                throw new ArgumentNullException("testFixtureFactory");
            }

            var fixture = testFixtureFactory.Create(Delegate.Method);
            var arguments = new[]
            {
                fixture.Create(typeof(T1)),
                fixture.Create(typeof(T2)),
                fixture.Create(typeof(T3)),
                fixture.Create(typeof(T4)),
                fixture.Create(typeof(T5)),
                fixture.Create(typeof(T6)),
                fixture.Create(typeof(T7)),
                fixture.Create(typeof(T8))
            };
            return new FirstClassCommand(method, Delegate, arguments);
        }
    }

    /// <summary>
    /// Represents a weakly-typed test case that can be turned into an
    /// xUnit.net ITestCommand when returned from a test method adorned with
    /// the <see cref="FirstClassTheoremBaseAttribute" />.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification="This rule is suppressed to provide auto data to test method.")]
    public class TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9> : ITestCase
    {
        private readonly Delegate _delegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="action">The test action.</param>
        public TestCase(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (action.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite actions are not supported, set only one action operation.",
                    "action");
            }

            _delegate = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="func">The test function.</param>
        public TestCase(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, object> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (func.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite functions are not supported, set only one function operation.",
                    "func");
            }

            _delegate = func;
        }

        /// <summary>
        /// Gets the test delegate.
        /// </summary>
        public Delegate Delegate
        {
            get
            {
                return _delegate;
            }
        }

        /// <summary>
        /// Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">
        /// The method adorned by a <see cref="FirstClassTheoremBaseAttribute" />.
        /// </param>
        /// <param name="testFixtureFactory">
        /// A test fixture factory to provide auto data.
        /// </param>
        /// <returns>
        /// An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory testFixtureFactory)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (testFixtureFactory == null)
            {
                throw new ArgumentNullException("testFixtureFactory");
            }

            var fixture = testFixtureFactory.Create(Delegate.Method);
            var arguments = new[]
            {
                fixture.Create(typeof(T1)),
                fixture.Create(typeof(T2)),
                fixture.Create(typeof(T3)),
                fixture.Create(typeof(T4)),
                fixture.Create(typeof(T5)),
                fixture.Create(typeof(T6)),
                fixture.Create(typeof(T7)),
                fixture.Create(typeof(T8)),
                fixture.Create(typeof(T9))
            };
            return new FirstClassCommand(method, Delegate, arguments);
        }
    }

    /// <summary>
    /// Represents a weakly-typed test case that can be turned into an
    /// xUnit.net ITestCommand when returned from a test method adorned with
    /// the <see cref="FirstClassTheoremBaseAttribute" />.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification="This rule is suppressed to provide auto data to test method.")]
    public class TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : ITestCase
    {
        private readonly Delegate _delegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="action">The test action.</param>
        public TestCase(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (action.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite actions are not supported, set only one action operation.",
                    "action");
            }

            _delegate = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="func">The test function.</param>
        public TestCase(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, object> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (func.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite functions are not supported, set only one function operation.",
                    "func");
            }

            _delegate = func;
        }

        /// <summary>
        /// Gets the test delegate.
        /// </summary>
        public Delegate Delegate
        {
            get
            {
                return _delegate;
            }
        }

        /// <summary>
        /// Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">
        /// The method adorned by a <see cref="FirstClassTheoremBaseAttribute" />.
        /// </param>
        /// <param name="testFixtureFactory">
        /// A test fixture factory to provide auto data.
        /// </param>
        /// <returns>
        /// An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory testFixtureFactory)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (testFixtureFactory == null)
            {
                throw new ArgumentNullException("testFixtureFactory");
            }

            var fixture = testFixtureFactory.Create(Delegate.Method);
            var arguments = new[]
            {
                fixture.Create(typeof(T1)),
                fixture.Create(typeof(T2)),
                fixture.Create(typeof(T3)),
                fixture.Create(typeof(T4)),
                fixture.Create(typeof(T5)),
                fixture.Create(typeof(T6)),
                fixture.Create(typeof(T7)),
                fixture.Create(typeof(T8)),
                fixture.Create(typeof(T9)),
                fixture.Create(typeof(T10))
            };
            return new FirstClassCommand(method, Delegate, arguments);
        }
    }

    /// <summary>
    /// Represents a weakly-typed test case that can be turned into an
    /// xUnit.net ITestCommand when returned from a test method adorned with
    /// the <see cref="FirstClassTheoremBaseAttribute" />.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification="This rule is suppressed to provide auto data to test method.")]
    public class TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : ITestCase
    {
        private readonly Delegate _delegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="action">The test action.</param>
        public TestCase(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (action.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite actions are not supported, set only one action operation.",
                    "action");
            }

            _delegate = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="func">The test function.</param>
        public TestCase(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, object> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (func.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite functions are not supported, set only one function operation.",
                    "func");
            }

            _delegate = func;
        }

        /// <summary>
        /// Gets the test delegate.
        /// </summary>
        public Delegate Delegate
        {
            get
            {
                return _delegate;
            }
        }

        /// <summary>
        /// Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">
        /// The method adorned by a <see cref="FirstClassTheoremBaseAttribute" />.
        /// </param>
        /// <param name="testFixtureFactory">
        /// A test fixture factory to provide auto data.
        /// </param>
        /// <returns>
        /// An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory testFixtureFactory)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (testFixtureFactory == null)
            {
                throw new ArgumentNullException("testFixtureFactory");
            }

            var fixture = testFixtureFactory.Create(Delegate.Method);
            var arguments = new[]
            {
                fixture.Create(typeof(T1)),
                fixture.Create(typeof(T2)),
                fixture.Create(typeof(T3)),
                fixture.Create(typeof(T4)),
                fixture.Create(typeof(T5)),
                fixture.Create(typeof(T6)),
                fixture.Create(typeof(T7)),
                fixture.Create(typeof(T8)),
                fixture.Create(typeof(T9)),
                fixture.Create(typeof(T10)),
                fixture.Create(typeof(T11))
            };
            return new FirstClassCommand(method, Delegate, arguments);
        }
    }

    /// <summary>
    /// Represents a weakly-typed test case that can be turned into an
    /// xUnit.net ITestCommand when returned from a test method adorned with
    /// the <see cref="FirstClassTheoremBaseAttribute" />.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification="This rule is suppressed to provide auto data to test method.")]
    public class TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : ITestCase
    {
        private readonly Delegate _delegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="action">The test action.</param>
        public TestCase(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (action.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite actions are not supported, set only one action operation.",
                    "action");
            }

            _delegate = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="func">The test function.</param>
        public TestCase(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, object> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (func.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite functions are not supported, set only one function operation.",
                    "func");
            }

            _delegate = func;
        }

        /// <summary>
        /// Gets the test delegate.
        /// </summary>
        public Delegate Delegate
        {
            get
            {
                return _delegate;
            }
        }

        /// <summary>
        /// Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">
        /// The method adorned by a <see cref="FirstClassTheoremBaseAttribute" />.
        /// </param>
        /// <param name="testFixtureFactory">
        /// A test fixture factory to provide auto data.
        /// </param>
        /// <returns>
        /// An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory testFixtureFactory)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (testFixtureFactory == null)
            {
                throw new ArgumentNullException("testFixtureFactory");
            }

            var fixture = testFixtureFactory.Create(Delegate.Method);
            var arguments = new[]
            {
                fixture.Create(typeof(T1)),
                fixture.Create(typeof(T2)),
                fixture.Create(typeof(T3)),
                fixture.Create(typeof(T4)),
                fixture.Create(typeof(T5)),
                fixture.Create(typeof(T6)),
                fixture.Create(typeof(T7)),
                fixture.Create(typeof(T8)),
                fixture.Create(typeof(T9)),
                fixture.Create(typeof(T10)),
                fixture.Create(typeof(T11)),
                fixture.Create(typeof(T12))
            };
            return new FirstClassCommand(method, Delegate, arguments);
        }
    }

    /// <summary>
    /// Represents a weakly-typed test case that can be turned into an
    /// xUnit.net ITestCommand when returned from a test method adorned with
    /// the <see cref="FirstClassTheoremBaseAttribute" />.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification="This rule is suppressed to provide auto data to test method.")]
    public class TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : ITestCase
    {
        private readonly Delegate _delegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="action">The test action.</param>
        public TestCase(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (action.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite actions are not supported, set only one action operation.",
                    "action");
            }

            _delegate = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="func">The test function.</param>
        public TestCase(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, object> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (func.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite functions are not supported, set only one function operation.",
                    "func");
            }

            _delegate = func;
        }

        /// <summary>
        /// Gets the test delegate.
        /// </summary>
        public Delegate Delegate
        {
            get
            {
                return _delegate;
            }
        }

        /// <summary>
        /// Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">
        /// The method adorned by a <see cref="FirstClassTheoremBaseAttribute" />.
        /// </param>
        /// <param name="testFixtureFactory">
        /// A test fixture factory to provide auto data.
        /// </param>
        /// <returns>
        /// An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory testFixtureFactory)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (testFixtureFactory == null)
            {
                throw new ArgumentNullException("testFixtureFactory");
            }

            var fixture = testFixtureFactory.Create(Delegate.Method);
            var arguments = new[]
            {
                fixture.Create(typeof(T1)),
                fixture.Create(typeof(T2)),
                fixture.Create(typeof(T3)),
                fixture.Create(typeof(T4)),
                fixture.Create(typeof(T5)),
                fixture.Create(typeof(T6)),
                fixture.Create(typeof(T7)),
                fixture.Create(typeof(T8)),
                fixture.Create(typeof(T9)),
                fixture.Create(typeof(T10)),
                fixture.Create(typeof(T11)),
                fixture.Create(typeof(T12)),
                fixture.Create(typeof(T13))
            };
            return new FirstClassCommand(method, Delegate, arguments);
        }
    }

    /// <summary>
    /// Represents a weakly-typed test case that can be turned into an
    /// xUnit.net ITestCommand when returned from a test method adorned with
    /// the <see cref="FirstClassTheoremBaseAttribute" />.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification="This rule is suppressed to provide auto data to test method.")]
    public class TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : ITestCase
    {
        private readonly Delegate _delegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="action">The test action.</param>
        public TestCase(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (action.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite actions are not supported, set only one action operation.",
                    "action");
            }

            _delegate = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="func">The test function.</param>
        public TestCase(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, object> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (func.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite functions are not supported, set only one function operation.",
                    "func");
            }

            _delegate = func;
        }

        /// <summary>
        /// Gets the test delegate.
        /// </summary>
        public Delegate Delegate
        {
            get
            {
                return _delegate;
            }
        }

        /// <summary>
        /// Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">
        /// The method adorned by a <see cref="FirstClassTheoremBaseAttribute" />.
        /// </param>
        /// <param name="testFixtureFactory">
        /// A test fixture factory to provide auto data.
        /// </param>
        /// <returns>
        /// An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory testFixtureFactory)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (testFixtureFactory == null)
            {
                throw new ArgumentNullException("testFixtureFactory");
            }

            var fixture = testFixtureFactory.Create(Delegate.Method);
            var arguments = new[]
            {
                fixture.Create(typeof(T1)),
                fixture.Create(typeof(T2)),
                fixture.Create(typeof(T3)),
                fixture.Create(typeof(T4)),
                fixture.Create(typeof(T5)),
                fixture.Create(typeof(T6)),
                fixture.Create(typeof(T7)),
                fixture.Create(typeof(T8)),
                fixture.Create(typeof(T9)),
                fixture.Create(typeof(T10)),
                fixture.Create(typeof(T11)),
                fixture.Create(typeof(T12)),
                fixture.Create(typeof(T13)),
                fixture.Create(typeof(T14))
            };
            return new FirstClassCommand(method, Delegate, arguments);
        }
    }

    /// <summary>
    /// Represents a weakly-typed test case that can be turned into an
    /// xUnit.net ITestCommand when returned from a test method adorned with
    /// the <see cref="FirstClassTheoremBaseAttribute" />.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification="This rule is suppressed to provide auto data to test method.")]
    public class TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : ITestCase
    {
        private readonly Delegate _delegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="action">The test action.</param>
        public TestCase(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (action.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite actions are not supported, set only one action operation.",
                    "action");
            }

            _delegate = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="func">The test function.</param>
        public TestCase(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, object> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (func.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite functions are not supported, set only one function operation.",
                    "func");
            }

            _delegate = func;
        }

        /// <summary>
        /// Gets the test delegate.
        /// </summary>
        public Delegate Delegate
        {
            get
            {
                return _delegate;
            }
        }

        /// <summary>
        /// Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">
        /// The method adorned by a <see cref="FirstClassTheoremBaseAttribute" />.
        /// </param>
        /// <param name="testFixtureFactory">
        /// A test fixture factory to provide auto data.
        /// </param>
        /// <returns>
        /// An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory testFixtureFactory)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (testFixtureFactory == null)
            {
                throw new ArgumentNullException("testFixtureFactory");
            }

            var fixture = testFixtureFactory.Create(Delegate.Method);
            var arguments = new[]
            {
                fixture.Create(typeof(T1)),
                fixture.Create(typeof(T2)),
                fixture.Create(typeof(T3)),
                fixture.Create(typeof(T4)),
                fixture.Create(typeof(T5)),
                fixture.Create(typeof(T6)),
                fixture.Create(typeof(T7)),
                fixture.Create(typeof(T8)),
                fixture.Create(typeof(T9)),
                fixture.Create(typeof(T10)),
                fixture.Create(typeof(T11)),
                fixture.Create(typeof(T12)),
                fixture.Create(typeof(T13)),
                fixture.Create(typeof(T14)),
                fixture.Create(typeof(T15))
            };
            return new FirstClassCommand(method, Delegate, arguments);
        }
    }

    /// <summary>
    /// Represents a weakly-typed test case that can be turned into an
    /// xUnit.net ITestCommand when returned from a test method adorned with
    /// the <see cref="FirstClassTheoremBaseAttribute" />.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification="This rule is suppressed to provide auto data to test method.")]
    public class TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : ITestCase
    {
        private readonly Delegate _delegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="action">The test action.</param>
        public TestCase(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (action.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite actions are not supported, set only one action operation.",
                    "action");
            }

            _delegate = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="func">The test function.</param>
        public TestCase(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, object> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (func.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite functions are not supported, set only one function operation.",
                    "func");
            }

            _delegate = func;
        }

        /// <summary>
        /// Gets the test delegate.
        /// </summary>
        public Delegate Delegate
        {
            get
            {
                return _delegate;
            }
        }

        /// <summary>
        /// Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">
        /// The method adorned by a <see cref="FirstClassTheoremBaseAttribute" />.
        /// </param>
        /// <param name="testFixtureFactory">
        /// A test fixture factory to provide auto data.
        /// </param>
        /// <returns>
        /// An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory testFixtureFactory)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (testFixtureFactory == null)
            {
                throw new ArgumentNullException("testFixtureFactory");
            }

            var fixture = testFixtureFactory.Create(Delegate.Method);
            var arguments = new[]
            {
                fixture.Create(typeof(T1)),
                fixture.Create(typeof(T2)),
                fixture.Create(typeof(T3)),
                fixture.Create(typeof(T4)),
                fixture.Create(typeof(T5)),
                fixture.Create(typeof(T6)),
                fixture.Create(typeof(T7)),
                fixture.Create(typeof(T8)),
                fixture.Create(typeof(T9)),
                fixture.Create(typeof(T10)),
                fixture.Create(typeof(T11)),
                fixture.Create(typeof(T12)),
                fixture.Create(typeof(T13)),
                fixture.Create(typeof(T14)),
                fixture.Create(typeof(T15)),
                fixture.Create(typeof(T16))
            };
            return new FirstClassCommand(method, Delegate, arguments);
        }
    }

}

