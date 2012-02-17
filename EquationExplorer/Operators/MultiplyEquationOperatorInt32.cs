using System;

namespace EquationExplorer.Operators
{
    public class MultiplyEquationOperatorInt32: EquationOperator<Int32>
    {
        public override int Evaluate(int left, int right)
        {
            return left*right;
        }

        public override string ToString()
        {
            return "*";
        }

        public override bool HasPriority(EquationOperator<int> equationOperator)
        {
            return equationOperator is AdditionEquationOperatorInt32 ||
                   equationOperator is SubstructionEquationOperatorInt32;
        }
    }
}
