using System.Linq;
using EquationExplorer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EquationExplorerTests
{
    [TestClass]
    public class SequenceGeneratorTest
    {
        [TestMethod]
        public void SequenceGeneratorProducesVariationsWithoutRepetitionForMemebers()
        {
            var values = new []{1, 2};

            var sut = new SequenceGenerator();

            var actual = sut.GenerateMembers(values, 2);

            Assert.IsNotNull(actual);
            Assert.AreEqual(2, actual.Count());
            CollectionAssert.AreEquivalent(new []{1,2}, actual.ToList()[0].ToArray());
            CollectionAssert.AreEquivalent(new []{2,1}, actual.ToList()[1].ToArray());
        }

        [TestMethod]
        public void SequenceGeneratorProducesVariationsWithRepetitionForOperations()
        {
            var values = new[] { 1, 2 };

            var sut = new SequenceGenerator();

            var actual = sut.GenerateOperators(values, 2);

            Assert.IsNotNull(actual);
            CollectionAssert.AreEquivalent(new[] { 1, 1 }, actual.ToList()[0].ToArray());
            CollectionAssert.AreEquivalent(new[] { 1, 2 }, actual.ToList()[1].ToArray());
            CollectionAssert.AreEquivalent(new[] { 2, 1 }, actual.ToList()[2].ToArray());
            CollectionAssert.AreEquivalent(new[] { 2, 2 }, actual.ToList()[3].ToArray());
        }
    }
}
