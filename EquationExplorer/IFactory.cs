using System;

namespace EquationExplorer
{
    public interface IEquationFactory<T>
    {
        Func<string, T> CreateParser();

        void AddAdditionOperator(EquationResolver<T> resolver);

        void AddSubstructionOperator(EquationResolver<T> resolver);

        void AddMultiplicationOperator(EquationResolver<T> resolver);

        void AddDivisionOperator(EquationResolver<T> resolver);

        EquationResolver<T> CreateResolver();
    }
}
