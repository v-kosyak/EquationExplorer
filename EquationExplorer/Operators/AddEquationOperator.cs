using System;

namespace EquationExplorer.Operators
{
    public class AddEquationOperator<T>: EquationOperator<T>
    {
        #region Overrides of EquationOperator<T>

        public override T Evaluate(T left, T right)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
