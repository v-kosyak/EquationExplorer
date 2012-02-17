using System.Collections.Generic;
using EquationExplorer.Equations;
using EquationExplorer.Operators;

namespace EquationExplorer
{
    public class EquationFactory: IEquationFactory
    {
        public Equation<T> CreateEquation<T>(IEnumerable<T> members, IEnumerable<EquationOperator<T>> operators)
        {
            var operatorsQueue = new Queue<EquationOperator<T>>(operators);
            var membersQueue = new Queue<T>(members);

            Equation<T> result = new SingleEquation<T>(membersQueue.Dequeue());

            while (membersQueue.Count != 0)
            {
                var equationOperator = operatorsQueue.Dequeue();
                var newSingle = new SingleEquation<T>(membersQueue.Dequeue());

                var compositeResult = result as CompositeEquation<T>;

                if (compositeResult != null && equationOperator.HasPriority(compositeResult.Operator))
                {
                    var right = new CompositeEquation<T>(compositeResult.Right, equationOperator, newSingle);
                    result = new CompositeEquation<T>(compositeResult.Left, compositeResult.Operator, right);
                }
                else
                    result = new CompositeEquation<T>(result, equationOperator, newSingle);
            }

            return result;
        }
    }
}
