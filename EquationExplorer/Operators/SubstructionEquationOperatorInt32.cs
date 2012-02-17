using System;

namespace EquationExplorer.Operators
{
    public class SubstructionEquationOperatorInt32: EquationOperator<Int32>
    {
        public override int Evaluate(int left, int right)
        {
            return left - right;
        }

        public override string ToString()
        {
            return "-";
        }
    }
}
