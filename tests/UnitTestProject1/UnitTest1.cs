using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQLEngine;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MiniSQLParse1()
        {
            Engine engine = new Engine();
            string result;
            engine.Query("SELECT * FROM table1",out result);
            Assert.AreEqual("resultado",result);
        }
    }
}
