using EquationExplorer;
using EquationExplorer.Equations;
using EquationExplorer.Operators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EquationExplorerTests
{
    [TestClass]
    public class EquationFactoryTest
    {
        [TestMethod]
        public void EquationFactoryCanCreateSimpleEquation()
        {
            var opMock = new Mock<EquationOperator<int>>().Object;

            var sut = new EquationFactory();

            var actual = sut.CreateEquation(new[] {1, 2}, new[] {opMock});

            Assert.IsNotNull(actual);

            Assert.IsInstanceOfType(actual, typeof(CompositeEquation<int>));

            var composite = (CompositeEquation<int>) actual;

            AssertSinqleEquation(composite.Left, 1);
            AssertSinqleEquation(composite.Right, 2);

            Assert.AreEqual(opMock, composite.Operator);

        }

        [TestMethod]
        public void EquationFactoryCanCreateComplexEquation()
        {
            var opMock = new Mock<EquationOperator<int>>();
            opMock.SetupAllProperties();
            opMock.Object.DefaultPriority = false;
            var equationOperator = opMock.Object;

            var prioMock = new Mock<EquationOperator<int>>();
            prioMock.SetupAllProperties();
            prioMock.Object.DefaultPriority = true;
            var prioOperator = prioMock.Object;

            var sut = new EquationFactory();

            var actual = sut.CreateEquation(new[] { 1, 2, 3, 4 }, new[] { equationOperator, prioOperator, equationOperator });

            Assert.IsNotNull(actual);

            Assert.IsInstanceOfType(actual, typeof(CompositeEquation<int>));

            var composite = (CompositeEquation<int>)actual;

            AssertSinqleEquation(composite.Right, 4);
            Assert.AreEqual(equationOperator, composite.Operator);

            var compositeInner = composite.Left as CompositeEquation<int>;
            Assert.IsNotNull(compositeInner);

            AssertSinqleEquation(compositeInner.Left, 1);
            Assert.AreEqual(equationOperator, compositeInner.Operator);

            var compositeInnerest = compositeInner.Right as CompositeEquation<int>;
            Assert.IsNotNull(compositeInnerest);
            AssertSinqleEquation(compositeInnerest.Left, 2);
            AssertSinqleEquation(compositeInnerest.Right, 3);

            Assert.AreEqual(prioOperator, compositeInnerest.Operator);
        }

        private static void AssertSinqleEquation<T>(Equation<T> equation, T value)
        {
            Assert.IsNotNull(equation);
            Assert.IsInstanceOfType(equation, typeof(SingleEquation<T>));
            Assert.AreEqual(value, equation.Value);
        }
    }
}
