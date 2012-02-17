using System;

namespace EquationExplorer.Operators
{
    public class AdditionEquationOperatorInt32: EquationOperator<Int32>
    {
        public override string ToString()
        {
            return "+";
        }

        public override int Evaluate(int left, int right)
        {
            return left + right;
        }
    }
}
