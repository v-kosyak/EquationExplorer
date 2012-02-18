namespace EquationExplorer.Operators
{
    public class AdditionEquationOperator<T>: EquationOperator<T>
    {
        public override string ToString()
        {
            return "+";
        }

        public override T Evaluate(T left, T right)
        {
            dynamic dynLeft = left;
            return dynLeft + right;
        }
    }
}
