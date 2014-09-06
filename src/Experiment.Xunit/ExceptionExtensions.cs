namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Reflection;

    internal static class ExceptionExtensions
    {
        public static Exception UnwrapTargetInvocationException(this Exception exception)
        {
            var e = exception as TargetInvocationException;
            if (e == null)
                return exception;

            ((Action)Delegate.CreateDelegate(
                typeof(Action),
                exception.InnerException,
                "InternalPreserveStackTrace"))
                .Invoke();

            if (exception.InnerException != null)
                return UnwrapTargetInvocationException(exception.InnerException);

            return exception;
        }
    }
}
