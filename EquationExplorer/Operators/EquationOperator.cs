﻿using System;

namespace EquationExplorer.Operators
{
    public abstract class EquationOperator<T> : IComparable<EquationOperator<T>>
    {
        public abstract T Evaluate(T left, T right);

        public virtual bool HasPriority(EquationOperator<T> equationOperator)
        {
            return DefaultPriority;
        }

        public int CompareTo(EquationOperator<T> other)
        {
            return ToString().CompareTo(other.ToString());
        }

        public bool DefaultPriority { get; set; }
    }
}
