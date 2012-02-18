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
            resolver.AddOperator<Operators.AdditionEquationOperator<double>>();
        }

        public void AddSubstructionOperator(EquationResolver<double> resolver)
        {
            resolver.AddOperator<Operators.SubtractionEquationOperator<double>>();
        }

        public void AddMultiplicationOperator(EquationResolver<double> resolver)
        {
            resolver.AddOperator<Operators.MultiplyEquationOperator<Double>>();
        }

        public void AddDivisionOperator(EquationResolver<double> resolver)
        {
            resolver.AddOperator<Operators.DivisionEquationOperator<double>>();
        }

        public EquationResolver<double> CreateResolver()
        {
            return new EquationResolver<double>();
        }
    }
}
