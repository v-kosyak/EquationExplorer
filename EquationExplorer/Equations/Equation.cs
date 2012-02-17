using System;

namespace EquationExplorer.Equations
{
    public abstract class Equation<T> : IEquatable<Equation<T>>
    {
        public abstract T Value { get; }

        public bool Equals(Equation<T> other)
        {
            return ToString().Equals(other.ToString());
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
