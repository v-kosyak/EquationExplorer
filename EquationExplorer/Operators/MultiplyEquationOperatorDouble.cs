using System;

namespace EquationExplorer.Operators
{
    public class MultiplyEquationOperatorDouble: EquationOperator<Double>
    {
        public override double Evaluate(double left, double right)
        {
            return left*right;
        }

        public override string ToString()
        {
            return "*";
        }

        //public override bool HasPriority(EquationOperator<double> equationOperator)
        //{
        //    return equationOperator is AdditionEquationOperatorDouble ||
        //           equationOperator is SubstructionEquationOperatorDouble;
        //}
    }
}
