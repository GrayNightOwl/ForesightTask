using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace StringBuilderTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // arrange
            StringBuilder sb = new StringBuilder();

            // act
            var res = calc.Sum(2, 5);

            // assert
            Assert.AreEqual(7, res);
        }
    }
}
