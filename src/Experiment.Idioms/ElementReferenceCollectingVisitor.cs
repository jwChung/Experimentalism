using Ploeh.Albedo;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents <see cref="IReflectionVisitor{T}" /> to collect references
    /// for a given assembly on only element level.
    /// </summary>
    public class ElementReferenceCollectingVisitor : ReferenceCollectingVisitor
    {
    }
}