using System.Collections.Generic;
using EquationExplorer.Operators;

namespace EquationExplorer
{
    public interface IEquationFactory
    {
        Equations.Equation<T> CreateEquation<T>(IEnumerable<T> members, IEnumerable<EquationOperator<T>> operators);
    }
}
