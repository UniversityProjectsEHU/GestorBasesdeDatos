using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQLEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesTest
{
    [TestClass]
    class SecurityTests
    {
        [TestMethod]
        public void TestCreateProfile()
        {
            string myDB = "myDB";
            ClassCreateDatabase db = new ClassCreateDatabase(myDB);
            db.Run(myDB);
            bool exists = Directory.Exists(@"..//..//..//data//myDB");
            //Testing CreateDatabase
            Assert.AreEqual(true, exists);

            string myTable = "myTable";
            string[] values = new string[2];
            values[0] = "column1 string true";
            values[1] = "column2 int false";
            ClassCreateTable ctable = new ClassCreateTable(myTable, values);
            ctable.Run(myDB);
            //Testing CreateTable
            bool existsDef = File.Exists(@"..//..//..//data//myDB//myTable.def");
            Assert.AreEqual(true, existsDef);
            bool existsData = File.Exists(@"..//..//..//data//myDB//myTable.data");
            Assert.AreEqual(true, existsData);

            ClassDropTable dtable = new ClassDropTable(myTable);
            dtable.Run(myDB);
            //Testing DropTable
            bool existsDefDrop = File.Exists(@"..//..//..//data//myDB//myTable.def");
            Assert.AreEqual(false, existsDefDrop);
            bool existsDataDrop = File.Exists(@"..//..//..//data//myDB//myTable.data");
            Assert.AreEqual(false, existsDataDrop);

            ClassDropDatabase drop = new ClassDropDatabase(myDB);
            drop.Run(myDB);
            //Testing DropDatabase
            bool existsDrop = Directory.Exists(@"..//..//..//data//myDB");
            Assert.AreEqual(false, existsDrop);
        }
    }
}
