using EquationExplorer;
using EquationExplorer.Equations;
using EquationExplorer.Operators;
using EquationExplorerTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace EquationExplorerTests
{
    [TestClass]
    public class CompositeEquationTest
    {
        [TestMethod]
        public void CompositeEquationConsistsOfTwoMembersAndAnOperator()
        {
            var left = new SingleEquation<int>(1);
            var right = new SingleEquation<int>(2);
            var op = Substitute.For<EquationOperator<int>>();
           
            var sut = new CompositeEquation<int>(left, op, right);

            Assert.AreEqual(left, sut.Left);
            Assert.AreEqual(right, sut.Right);
            Assert.AreEqual(op, sut.Operator);
        }

        [TestMethod]
        public void CompositeEquationCanContainCompositeMembers()
        {
            var op   = Substitute.For<EquationOperator<long>>();

            var left = new CompositeEquation<long>(new SingleEquation<long>(1), op,
                                                        new SingleEquation<long>(2));
            var right = new CompositeEquation<long>(new SingleEquation<long>(1), op,
                                                        new SingleEquation<long>(2));

            var sut = new CompositeEquation<long>(left, op, right);

            Assert.AreEqual(left, sut.Left);
            Assert.AreEqual(right, sut.Right);
            Assert.AreEqual(op, sut.Operator);
        }

        [TestMethod]
        public void CompositeEquationHasStringPresentation()
        {
            var left = Substitute.For<Equation<int>>();
            left.ToString().Returns("l");

            var right = Substitute.For<Equation<int>>();
            right.ToString().Returns("r");

            var op = Substitute.For<EquationOperator<int>>();
            op.ToString().Returns("op");

            var sut = new CompositeEquation<int>(left, op, right);

            Assert.AreEqual("(lopr)", sut.ToString());
        }

        [TestMethod]
        public void CompositeEquationEvaluatesWithOperator()
        {
            var left = Substitute.For<Equation<int>>();
            left.Value.Returns(1);

            var right = Substitute.For<Equation<int>>();
            right.Value.Returns(18);

            var op = Substitute.For<EquationOperator<int>>();
            op.Evaluate(1, 18).Returns(3);

            var sut = new CompositeEquation<int>(left, op, right);

            var actual = sut.Value;

            Assert.AreEqual(3, actual);
        }
    }
}
