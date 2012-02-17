using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EquationExplorer.Equations;
using EquationExplorer.Operators;

namespace EquationExplorer
{
    public class EquationResolver<T>
    {
        private readonly IList<EquationOperator<T>> _operators = new List<EquationOperator<T>>();

        public Predicate<Equation<T>> Filter { get; set; }

        public void AddOperator<U>() where U: EquationOperator<T>, new()
        {
            AddOperator(new U());
            AddOperator(new U{DefaultPriority = true});
        }

        public void AddOperator(EquationOperator<T> equationOperator)
        {
            if (equationOperator == null)
                throw new ArgumentNullException("equationOperator");

            _operators.Add(equationOperator);
        }

        public ReadOnlyCollection<EquationOperator<T>> Operators 
        { 
            get { 
                return new ReadOnlyCollection<EquationOperator<T>>(_operators); 
            }
        }

        private bool IsValid(Equation<T> equation)
        {
            try
            {
                return Filter == null || Filter(equation);
            }
            catch
            {
                return false;
            }
            
        }

        public bool AllVariations { get; set; }

        public IEnumerable<Equation<T>> Resolve(params T[] values)
        {
            if (values == null)
                throw new ArgumentNullException("values");

            if (values.Length < 2)
                throw new ArgumentException("At least 2 memebers required", "values");

            var startSubsetCount = AllVariations ? 2 : values.Length;

            var result = new List<Equation<T>>();

            for (int subsetCount = startSubsetCount; subsetCount <= values.Length; subsetCount++)
            {
                var operatorsSequences = SequenceGenerator.GenerateOperators(Operators, subsetCount - 1);

                result.AddRange(from memberSequence in SequenceGenerator.GenerateMembers(values, subsetCount).AsParallel()
                                from operatorSequence in operatorsSequences
                                select EquationFactory.CreateEquation(memberSequence, operatorSequence)
                                into equation where IsValid(equation) select equation);
            }

            return result.Distinct();
        }

        private IEquationFactory _equationFactory;

        public IEquationFactory EquationFactory 
        {
            get { return _equationFactory ?? (_equationFactory = new EquationFactory()); }
            set { _equationFactory = value; }
        }

        private ISequenceGenerator _sequenceGenerator;
        public ISequenceGenerator SequenceGenerator 
        {
            get { return _sequenceGenerator ?? (_sequenceGenerator = new SequenceGenerator()); }
            set { _sequenceGenerator = value; }
        }
    }
}
