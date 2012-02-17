using System.Collections.Generic;
using System.Linq;
using EquationExplorer.Combinatorics;

namespace EquationExplorer
{
    public class SequenceGenerator:ISequenceGenerator
    {
        public IEnumerable<IEnumerable<T>> GenerateOperators<T>(IEnumerable<T> sourceSequence, int count)
        {
            return new Variations<T>(sourceSequence.ToList(), count, GenerateOption.WithRepetition);
        }

        public IEnumerable<IEnumerable<T>> GenerateMembers<T>(IEnumerable<T> sourceSequence, int count)
        {
            return new Variations<T>(sourceSequence.ToList(), count, GenerateOption.WithoutRepetition);
        }
    }
}
