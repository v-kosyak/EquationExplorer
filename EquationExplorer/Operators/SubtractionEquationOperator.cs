namespace EquationExplorer.Operators
{
    public class SubtractionEquationOperator<T>: EquationOperator<T>
    {
        public override T Evaluate(T left, T right)
        {
            dynamic dynLeft = left;
            return dynLeft - right;
        }

        public override string ToString()
        {
            return "-";
        }
    }
}
