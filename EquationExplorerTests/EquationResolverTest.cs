using System;
using System.Collections.Generic;
using System.Linq;
using EquationExplorer.Equations;
using EquationExplorer.Operators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EquationExplorer;
using NSubstitute;

namespace EquationExplorerTests
{
    [TestClass]
    public class EquationResolverTest
    {
        
        [TestMethod]
        public void EquationResolverAcceptsAvailableOperators()
        {
            var op1 = Substitute.For<EquationOperator<int>>();
            var op2 = Substitute.For<EquationOperator<int>>();

            var sut = new EquationResolver<int>();

            sut.AddOperator(op1);
            sut.AddOperator(op2);

            Assert.IsNotNull(sut.Operators);
            Assert.AreEqual(2, sut.Operators.Count);
            Assert.AreEqual(op1, sut.Operators[0]);
            Assert.AreEqual(op2, sut.Operators[1]);
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
            Action<IEnumerable<int>> assertMembers =
                members => CollectionAssert.AreEquivalent(new[] { 1, 2, 3 }, members.ToArray());
    
            var sequenceGenerator = Substitute.For<ISequenceGenerator>();
            sequenceGenerator.GenerateMembers(Arg.Do(assertMembers), 3);

            var sut = new EquationResolver<int>{ SequenceGenerator = sequenceGenerator };
            
            sut.Resolve(1, 2, 3);

            sequenceGenerator.Received().GenerateMembers(Arg.Any<IEnumerable<int>>(), 3);
        }

        [TestMethod]
        public void EquationResolverGeneratesSequencesForOperatorsOfValues()
        {
            var op1 = Substitute.For<EquationOperator<int>>();
            var op2 = Substitute.For<EquationOperator<int>>();

            var sequenceGenerator = Substitute.For<ISequenceGenerator>();
            Action<IEnumerable<EquationOperator<int>>> assertOperators =
                opers => CollectionAssert.AreEquivalent(new[] { op1, op2 }, opers.ToArray());
            sequenceGenerator.GenerateOperators(Arg.Do(assertOperators), 2);

            var sut = new EquationResolver<int>
            {
                SequenceGenerator = sequenceGenerator
            };

            sut.AddOperator(op1);
            sut.AddOperator(op2);

            sut.Resolve(1, 2, 3);


            sequenceGenerator.Received().GenerateOperators(Arg.Any<IEnumerable<EquationOperator<int>>>(), 2);
        }

        [TestMethod]
        public void EquationResolverCreatesEquationsForAllCombinations()
        {
            var memberSeq1 = Substitute.For<IEnumerable<int>>();
            var memberSeq2 = Substitute.For<IEnumerable<int>>();

            var operatorSeq1 = Substitute.For<IEnumerable<EquationOperator<int>>>();
            var operatorSeq2 = Substitute.For<IEnumerable<EquationOperator<int>>>();

            var equationFactory = Substitute.For<IEquationFactory>();

            var sequenceGenerator = Substitute.For<ISequenceGenerator>();
            sequenceGenerator.GenerateMembers(Arg.Any<IEnumerable<int>>(), 0).ReturnsForAnyArgs(new[] { memberSeq1, memberSeq2 });
            sequenceGenerator.GenerateOperators(Arg.Any<IEnumerable<EquationOperator<int>>>(), 0).ReturnsForAnyArgs(new[] { operatorSeq1, operatorSeq2 });

            var sut = new EquationResolver<int>
                          {
                              EquationFactory = equationFactory,
                              SequenceGenerator = sequenceGenerator
                          };

            sut.Resolve(1, 2);

            equationFactory.ReceivedWithAnyArgs(4).CreateEquation(Arg.Any<IEnumerable<int>>(), Arg.Any<IEnumerable<EquationOperator<int>>>());
        }

        [TestMethod]
        public void EquationResolverReturnsFilteredEquations()
        {
            var eq1 = Substitute.For<Equation<int>>();
            eq1.ToString().Returns("eq1");
            eq1.Value.Returns(2);

            var eq2 = Substitute.For<Equation<int>>();
            eq2.Value.Returns(3);

            var eq3 = Substitute.For<Equation<int>>();
            eq3.ToString().Returns("eq3");
            eq3.Value.Returns(2);

            var eq4 = Substitute.For<Equation<int>>();
            eq4.Value.Returns(3);

            var equationFactory = Substitute.For<IEquationFactory>();
            equationFactory.CreateEquation(Arg.Any<IEnumerable<int>>(), Arg.Any<IEnumerable<EquationOperator<int>>>()).Returns(eq1, eq2, eq3, eq4);

            var sequenceGenerator = Substitute.For<ISequenceGenerator>();
            sequenceGenerator.GenerateMembers(Arg.Any<IEnumerable<int>>(), Arg.Any<int>()).Returns(new IEnumerable<int>[2]);
            sequenceGenerator.GenerateOperators(Arg.Any<IEnumerable<EquationOperator<int>>>(), Arg.Any<int>()).Returns(new IEnumerable<EquationOperator<int>>[2]);

            var sut = new EquationResolver<int>
            {
                EquationFactory = equationFactory,
                SequenceGenerator = sequenceGenerator,
                Filter = equation => equation.Value.Equals(2)
            };

            var actual = sut.Resolve(1, 2);

            Assert.IsNotNull(actual);
            Assert.AreEqual(2, actual.Count());
            Assert.IsTrue(actual.Contains(eq1));
            Assert.IsTrue(actual.Contains(eq3));

        }
    }
}
