using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EquationExplorer.Equations;
using EquationExplorer.Operators;
using EquationExplorerTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EquationExplorer;
using Moq;

namespace EquationExplorerTests
{
    [TestClass]
    public class EquationResolverTest
    {
        
        [TestMethod]
        public void EquationResolverAcceptsAvailableOperators()
        {
            var op1Mock = new Mock<EquationOperator<int>>();
            var op2Mock = new Mock<EquationOperator<int>>();

            var sut = new EquationResolver<int>();

            sut.AddOperator(op1Mock.Object);
            sut.AddOperator(op2Mock.Object);

            Assert.IsNotNull(sut.Operators);
            Assert.AreEqual(2, sut.Operators.Count);
            Assert.AreEqual(op1Mock.Object, sut.Operators[0]);
            Assert.AreEqual(op2Mock.Object, sut.Operators[1]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EquationResolverDoesNotAcceptNullOperators()
        {
            new EquationResolver<int>().AddOperator(null);
        }

        [TestMethod]
        public void EquationResolverGeneratesSequencesForEquationMembers()
        {

            var sequenceGeneratorMock = new Mock<ISequenceGenerator>();

            var sut = new EquationResolver<int>
                          {
                              SequenceGenerator = sequenceGeneratorMock.Object
                          };

            sut.Resolve(1, 2, 3);

            sequenceGeneratorMock.Verify(seqGen => seqGen.GenerateMembers(new[] { 1, 2, 3 }, 3), Times.Once());
        }

        [TestMethod]
        public void EquationResolverGeneratesSequencesForOperatorsOfValues()
        {
            var op1 = new Mock<EquationOperator<int>>().Object;
            var op2 = new Mock<EquationOperator<int>>().Object;

            var sequenceGeneratorMock = new Mock<ISequenceGenerator>();
            var sut = new EquationResolver<int>
            {
                SequenceGenerator = sequenceGeneratorMock.Object
            };

            sut.AddOperator(op1);
            sut.AddOperator(op2);

            sut.Resolve(1, 2, 3);

            sequenceGeneratorMock.Verify(seqGen => seqGen.GenerateOperators(new[] { op1, op2 }, 2), Times.Once());
        }

        [TestMethod]
        public void EquationResolverCreatesEquationsForAllCombinations()
        {
            var memberSeq1 = new Mock<IEnumerable<int>>().Object;
            var memberSeq2 = new Mock<IEnumerable<int>>().Object;

            var operatorSeq1 = new Mock<IEnumerable<EquationOperator<int>>>().Object;
            var operatorSeq2 = new Mock<IEnumerable<EquationOperator<int>>>().Object;

            var equationFactoryMock = new Mock<IEquationFactory>();

            var sequenceGeneratorMock = new Mock<ISequenceGenerator>();
            sequenceGeneratorMock.Setup(seqGen => seqGen.GenerateMembers(It.IsAny<IEnumerable<int>>(), It.IsAny<int>())).Returns(new[] { memberSeq1, memberSeq2 });
            sequenceGeneratorMock.Setup(seqGen => seqGen.GenerateOperators(It.IsAny<IEnumerable<EquationOperator<int>>>(), It.IsAny<int>())).Returns(new[] { operatorSeq1, operatorSeq2 });

            var sut = new EquationResolver<int>
                          {
                              EquationFactory = equationFactoryMock.Object,
                              SequenceGenerator = sequenceGeneratorMock.Object
                          };

            sut.Resolve(1, 2);

            equationFactoryMock.Verify(factory => factory.CreateEquation(It.IsAny<IEnumerable<int>>(), It.IsAny<IEnumerable<EquationOperator<int>>>()), Times.Exactly(4));
        }

        [TestMethod]
        public void EquationResolverReturnsFilteredEquations()
        {
            var eq1Mock = new EquationMock {ToStringReturns = "eq1", ValueToGet = 2};

            var eq2Mock = new Mock<Equation<int>>();
            eq2Mock.SetupGet(eq => eq.Value).Returns(3);

            var eq3Mock = new EquationMock { ToStringReturns = "eq3", ValueToGet = 2 };

            var eq4Mock = new Mock<Equation<int>>();
            eq4Mock.SetupGet(eq => eq.Value).Returns(3);

            var expectedEquationsQueue =
                new Queue<Equation<int>>(new[] {eq1Mock, eq2Mock.Object, eq3Mock, eq4Mock.Object});

            var equationFactoryMock = new Mock<IEquationFactory>();
            equationFactoryMock.Setup(factory => factory.CreateEquation(It.IsAny<IEnumerable<int>>(), It.IsAny<IEnumerable<EquationOperator<int>>>())).Returns(expectedEquationsQueue.Dequeue);

            var sequenceGeneratorMock = new Mock<ISequenceGenerator>();
            sequenceGeneratorMock.Setup(seqGen => seqGen.GenerateMembers(It.IsAny<IEnumerable<int>>(), It.IsAny<int>())).Returns(new IEnumerable<int>[2]);
            sequenceGeneratorMock.Setup(seqGen => seqGen.GenerateOperators(It.IsAny<IEnumerable<EquationOperator<int>>>(), It.IsAny<int>())).Returns(new IEnumerable<EquationOperator<int>>[2]);

            var sut = new EquationResolver<int>
            {
                EquationFactory = equationFactoryMock.Object,
                SequenceGenerator = sequenceGeneratorMock.Object,
                Filter = equation => equation.Value.Equals(2)
            };

            var actual = sut.Resolve(1, 2);

            Assert.IsNotNull(actual);
            Assert.AreEqual(2, actual.Count());
            Assert.IsTrue(actual.Contains(eq1Mock));
            Assert.IsTrue(actual.Contains(eq3Mock));

        }
    }
}
