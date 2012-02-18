namespace EquationExplorer.Operators
{
    public class DivisionEquationOperator<T>: EquationOperator<T>
    {
        public override T Evaluate(T left, T right)
        {
            dynamic dynLeft = left;
            return dynLeft / right;
        }

        public override string ToString()
        {
            return "/";
        }
    }
}
