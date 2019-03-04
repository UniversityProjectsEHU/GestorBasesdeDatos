using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQLEngine;
using System.Text.RegularExpressions;
using System.IO;

namespace ClassesTest
{
    [TestClass]
    public class RunTests
    {
        [TestMethod]
        public void ExecuteTest()
        {
            string dbname = "myDB";
            string dbname1 = "myDB1";
            string myTable = "thisTable";
            string[] values = { "One String true", "Two String false", "Three String false", "Caramba String false" };
            string[] valuesToInsert = { "One", "Two", "Three", "Caramba" };
            string pathfileDEF = @"..//..//..//data//" + dbname + "//" + myTable + ".def";
            /*string queryCreateDB = @"CREATE DATABASE myDB;";
            string queryCreateTable = @"CREATE TABLE myTable (column1 int true,column2 string false,column3 int false);";
            string queryInsert = @"INSERT INTO table1 VALUES (10,abc,8);";
            */
            ClassCreateDatabase newDB = new ClassCreateDatabase(dbname);
            ClassCreateTable newTable = new ClassCreateTable(myTable, values);
            ClassInsert inserted = new ClassInsert(myTable, valuesToInsert);
            newDB.Run(dbname);
            bool exists = Directory.Exists(@"..//..//..//data//"+dbname);
            Assert.AreEqual(true, exists);
            newTable.Run(dbname);
            bool existsTables = File.Exists(@"..//..//..//data//"+dbname+"//"+myTable+".data");
            Assert.AreEqual(true, existsTables);
            inserted.Run(dbname);
            /*string fullPath = @"..//..//..//data//myDB//thisTable.data";
            StreamWriter file = new StreamWriter(fullPath, true);
            for (int i = 0; i < valuesToInsert.Length; i++)
            {
                file.WriteLine(valuesToInsert[i]);
            }*/
            }
        }
            
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

