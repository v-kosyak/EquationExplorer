using System;

namespace EquationExplorer
{
    public class Int32EquationFactory: IEquationFactory<Int32>
    {
        public Func<string, int> CreateParser()
        {
            return Int32.Parse;
        }

        public void AddAdditionOperator(EquationResolver<int> resolver)
        {
            resolver.AddOperator<Operators.AdditionEquationOperator<Int32>>();
        }

        public void AddSubstructionOperator(EquationResolver<int> resolver)
        {
            resolver.AddOperator<Operators.SubtractionEquationOperator<int>>();
        }

        public void AddMultiplicationOperator(EquationResolver<int> resolver)
        {
            resolver.AddOperator<Operators.MultiplyEquationOperator<int>>();
        }

        public void AddDivisionOperator(EquationResolver<int> resolver)
        {
            resolver.AddOperator<Operators.DivisionEquationOperator<int>>();
        }

        public EquationResolver<int> CreateResolver()
        {
            return new EquationResolver<int>();
        }
    }
}
