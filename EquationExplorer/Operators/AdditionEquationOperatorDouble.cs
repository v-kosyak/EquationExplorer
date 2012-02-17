using System;

namespace EquationExplorer.Operators
{
    public class AdditionEquationOperatorDouble: EquationOperator<Double>
    {
        public override string ToString()
        {
            return "+";
        }

        public override double Evaluate(double left, double right)
        {
            return left + right;
        }
    }
}
