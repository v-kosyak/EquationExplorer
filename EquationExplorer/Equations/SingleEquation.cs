namespace EquationExplorer.Equations
{
    public class SingleEquation<T>: Equation<T>
    {
        private readonly T _member;

        public SingleEquation(T member = default(T))
        {
            _member = member;
        }

        public override T Value
        {
            get { return _member; }
        }

        public override string ToString()
        {
            return _member.ToString();
        }
    }
}
