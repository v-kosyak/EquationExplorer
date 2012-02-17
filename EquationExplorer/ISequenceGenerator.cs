using System.Collections.Generic;

namespace EquationExplorer
{
    public interface ISequenceGenerator
    {
        IEnumerable<IEnumerable<T>> GenerateOperators<T>(IEnumerable<T> sourceSequence, int count);
        IEnumerable<IEnumerable<T>> GenerateMembers<T>(IEnumerable<T> sourceSequence, int count);
    }
}
