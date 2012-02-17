using Microsoft.VisualStudio.TestTools.UnitTesting;
using EquationExplorer.Equations;

namespace EquationExplorerTests
{
    /// <summary>
    /// Summary description for SingleEquationMemeberTest
    /// </summary>
    [TestClass]
    public class SingleEquationMemeberTest
    {
       [TestMethod]
       public void SingleEquationRequiresMember()
       {
           var sut = new SingleEquation<int>(2);

           Assert.IsNotNull(sut);
           Assert.AreEqual(2, sut.Value);
       }

       [TestMethod]
       public void SingleEquationToString()
       {
           var sut = new SingleEquation<int>(3);

           Assert.AreEqual("3", sut.ToString());
       }
    }
}
