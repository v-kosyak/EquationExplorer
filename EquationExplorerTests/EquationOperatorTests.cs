using EquationExplorer.Operators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EquationExplorerTests
{
    [TestClass]
    public class EquationOperatorTests
    {
        [TestMethod]
        public void AdditionEquationTest()
        {
            //Arrange
            var sut = new AdditionEquationOperator<double>();

            //Act
            var actual = sut.Evaluate(3, 5);

            //Assert
            Assert.AreEqual(8, actual);
        }

        [TestMethod]
        public void DivisionEquationTest()
        {
            //Arrange
            var sut = new DivisionEquationOperator<double>();

            //Act
            var actual = sut.Evaluate(5, 2);

            //Assert
            Assert.AreEqual(2.5, actual);
        }
    }
}
