using System;

namespace EquationExplorer.Operators
{
    public class DivisionEquationOperatorDouble: EquationOperator<Double>
    {
        public override double Evaluate(double left, double right)
        {
            return left/right;
        }

        //public override bool HasPriority(EquationOperator<double> equationOperator)
        //{
        //    return equationOperator is AdditionEquationOperatorDouble ||
        //           equationOperator is SubstructionEquationOperatorDouble;
        //}

        public override string ToString()
        {
            return "/";
        }
    }
}
