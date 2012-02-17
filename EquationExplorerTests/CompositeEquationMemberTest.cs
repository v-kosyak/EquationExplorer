using EquationExplorer;
using EquationExplorer.Equations;
using EquationExplorer.Operators;
using EquationExplorerTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
            var opMock = new Mock<EquationOperator<int>>();
           
            var sut = new CompositeEquation<int>(left, opMock.Object, right);

            Assert.AreEqual(left, sut.Left);
            Assert.AreEqual(right, sut.Right);
            Assert.AreEqual(opMock.Object, sut.Operator);
        }

        [TestMethod]
        public void CompositeEquationCanContainCompositeMembers()
        {
            var opMock = new Mock<EquationOperator<long>>();

            var left = new CompositeEquation<long>(new SingleEquation<long>(1), opMock.Object,
                                                        new SingleEquation<long>(2));
            var right = new CompositeEquation<long>(new SingleEquation<long>(1), opMock.Object,
                                                        new SingleEquation<long>(2));

            var sut = new CompositeEquation<long>(left, opMock.Object, right);

            Assert.AreEqual(left, sut.Left);
            Assert.AreEqual(right, sut.Right);
            Assert.AreEqual(opMock.Object, sut.Operator);
        }

        [TestMethod]
        public void CompositeEquationHasStringPresentation()
        {
            var left = new EquationMock{ToStringReturns = "l"};

            var right = new EquationMock {ToStringReturns = "r"};

            var op = new EquationOperatorMock {ToStringReturns = "op"};

            var sut = new CompositeEquation<int>(left, op, right);

            Assert.AreEqual("(lopr)", sut.ToString());
        }

        [TestMethod]
        public void CompositeEquationEvaluatesWithOperator()
        {
            var leftMock = new Mock<Equation<int>>();
            leftMock.SetupGet(left => left.Value).Returns(1);

            var rightMock = new Mock<Equation<int>>();
            rightMock.SetupGet(right => right.Value).Returns(18);

            var opMock = new Mock<EquationOperator<int>>();
            opMock.Setup(op => op.Evaluate(1, 18)).Returns(3);

            var sut = new CompositeEquation<int>(leftMock.Object, opMock.Object, rightMock.Object);

            var actual = sut.Value;

            Assert.AreEqual(3, actual);
        }
    }
}
