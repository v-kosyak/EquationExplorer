using System;

namespace EquationExplorer.Operators
{
    public class DivisionEquationOperatorInt32: EquationOperator<Int32>
    {
        public override int Evaluate(int left, int right)
        {
            return left/right;
        }

        public override bool HasPriority(EquationOperator<int> equationOperator)
        {
            return equationOperator is AdditionEquationOperatorInt32 ||
                   equationOperator is SubstructionEquationOperatorInt32;
        }

        public override string ToString()
        {
            return "/";
        }
    }
}
