using System;
using EquationExplorer.Equations;

namespace EquationExplorerTests.Mocks
{
    [Obsolete]
    public class EquationMock : Equation<int>
    {
        public string ToStringReturns { get; set; }
        public int ValueToGet { get; set; }

        public override string ToString()
        {
            return ToStringReturns;
        }

        public override int Value
        {
            get { return ValueToGet; }
        }
    }
}
