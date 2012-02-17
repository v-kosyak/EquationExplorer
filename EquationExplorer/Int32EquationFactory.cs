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
            resolver.AddOperator<Operators.AdditionEquationOperatorInt32>();
        }

        public void AddSubstructionOperator(EquationResolver<int> resolver)
        {
            resolver.AddOperator<Operators.SubstructionEquationOperatorInt32>();
        }

        public void AddMultiplicationOperator(EquationResolver<int> resolver)
        {
            resolver.AddOperator<Operators.MultiplyEquationOperatorInt32>();
        }

        public void AddDivisionOperator(EquationResolver<int> resolver)
        {
            resolver.AddOperator<Operators.DivisionEquationOperatorInt32>();
        }

        public EquationResolver<int> CreateResolver()
        {
            return new EquationResolver<int>();
        }
    }
}
