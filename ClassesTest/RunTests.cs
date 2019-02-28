using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQLEngine;
using System.Text.RegularExpressions;
using System.IO;

namespace ClassesTest
{
    public class RunTests
    {
        [TestMethod]
        public void TestCreateDatabase()
        {
            string myDB = "myDB";
            ClassCreateDatabase db = new ClassCreateDatabase(myDB);
            db.Run(myDB);
            bool exists = Directory.Exists(@"..//..//..//data//myDB");
            Assert.AreEqual(true, exists);
        }

        [TestMethod]
        public void TestDropDatabase()
        {
            string myDB = "myDB";
            ClassCreateDatabase db = new ClassCreateDatabase(myDB);
            db.Run(myDB);
            ClassDropDatabase drop = new ClassDropDatabase(myDB);
            drop.Run(myDB);
            bool exists = Directory.Exists(@"..//..//..//data//myDB");
            Assert.AreEqual(false, exists);
        }

        [TestMethod]
        public void TestDropTable()
        {
            string myDB = "myDB";
            ClassCreateDatabase db = new ClassCreateDatabase(myDB);
            db.Run(myDB);
            string myTable = "myTable";
            string[] values = new string[2];
            values[1] = "column1 string true";
            values[2] = "column2 int false";
            ClassCreateTable ctable = new ClassCreateTable(myTable, values);
            ctable.Run(myDB);
            ClassDropTable dtable = new ClassDropTable(myTable);
            dtable.Run(myDB);
            bool existsDef = File.Exists(@"..//..//..//data//myDB//myTable.def");
            Assert.AreEqual(false, existsDef);
            bool existsData = File.Exists(@"..//..//..//data//myDB//myTable.data");
            Assert.AreEqual(false, existsData);
        }
    }
}

