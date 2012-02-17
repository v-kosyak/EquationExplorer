using System;
using EquationExplorer.Operators;

namespace EquationExplorer.Equations
{
    public class CompositeEquation<T>: Equation<T>
    {
        private readonly Equation<T> _left;
        private readonly Equation<T> _right;
        private readonly EquationOperator<T> _operator;

        public CompositeEquation(Equation<T> left, EquationOperator<T> equationOperator, Equation<T> right)
        {
            //TODO: validate input parameters

            _left = left;
            _right = right;
            _operator = equationOperator;
        }

        public Equation<T> Left
        {
            get { return _left; }
        }

        public Equation<T> Right
        {
            get { return _right; }
        }

        public EquationOperator<T> Operator
        {
            get { return _operator; }
        }

        public override T Value
        {
            get { return Operator.Evaluate(Left.Value, Right.Value); }
        }

        public override string ToString()
        {
            return String.Concat("(", Left.ToString(), Operator.ToString(), Right.ToString(), ")");
        }
    }
}
