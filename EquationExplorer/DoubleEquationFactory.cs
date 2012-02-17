using System;

namespace EquationExplorer
{
    public class DoubleEquationFactory: IEquationFactory<Double>
    {
        public Func<string, double> CreateParser()
        {
            return Double.Parse;
        }

        public void AddAdditionOperator(EquationResolver<double> resolver)
        {
            resolver.AddOperator<Operators.AdditionEquationOperatorDouble>();
        }

        public void AddSubstructionOperator(EquationResolver<double> resolver)
        {
            resolver.AddOperator<Operators.SubstructionEquationOperatorDouble>();
        }

        public void AddMultiplicationOperator(EquationResolver<double> resolver)
        {
            resolver.AddOperator<Operators.MultiplyEquationOperatorDouble>();
        }

        public void AddDivisionOperator(EquationResolver<double> resolver)
        {
            resolver.AddOperator<Operators.DivisionEquationOperatorDouble>();
        }

        public EquationResolver<double> CreateResolver()
        {
            return new EquationResolver<double>();
        }
    }
}
