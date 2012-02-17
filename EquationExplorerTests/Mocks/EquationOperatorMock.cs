using EquationExplorer.Operators;

namespace EquationExplorerTests.Mocks
{
    public class EquationOperatorMock: EquationOperator<int>
    {
        public string ToStringReturns { get; set; }

        public override string ToString()
        {
            return ToStringReturns;
        }

        public override int Evaluate(int left, int right)
        {
            throw new System.NotImplementedException();
        }
    }
}
