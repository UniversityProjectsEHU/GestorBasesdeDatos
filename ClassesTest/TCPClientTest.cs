using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQLEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCPClientExample;

namespace ClassesTest
{
    [TestClass]
    public class TCPClientTest
    {
        [TestMethod]
        public void TestEncapsulate()
        {
            string query = "SELECT id FROM t1 WHERE id=1;";
            string result = "<Query>" + query + "</Query>";
            Assert.AreEqual(TCPClientExample.Program.encapsule(query), result);
        }
    }
}
