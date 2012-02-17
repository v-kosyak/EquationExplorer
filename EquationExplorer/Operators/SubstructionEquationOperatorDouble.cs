using System;

namespace EquationExplorer.Operators
{
    public class SubstructionEquationOperatorDouble: EquationOperator<Double>
    {
        public override double Evaluate(double left, double right)
        {
            return left - right;
        }

        public override string ToString()
        {
            return "-";
        }
    }
}
