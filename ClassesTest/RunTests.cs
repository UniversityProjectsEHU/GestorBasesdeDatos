using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQLEngine;
using System.Text.RegularExpressions;
using System.IO;

namespace ClassesTest
{
    [TestClass]
    public class TestCreateBothWithInsert
    {
        [TestMethod]
        public void ExecuteTest()
        {
            string dbname = "myDB";
            string dbname1 = "myDB1";
            string myTable = "thisTable";
            string[] values = { "One String true", "Two String false", "Three String false", "Caramba String false" };
            string[] valuesToInsert = { "One", "Two", "Three", "Caramba" };
            /*string queryCreateDB = @"CREATE DATABASE myDB;";
            string queryCreateTable = @"CREATE TABLE myTable (column1 int true,column2 string false,column3 int false);";
            string queryInsert = @"INSERT INTO table1 VALUES (10,abc,8);";
            */
            ClassCreateDatabase newDB = new ClassCreateDatabase(dbname);
            ClassCreateTable newTable = new ClassCreateTable(myTable, values);
            ClassInsert inserted = new ClassInsert(myTable, valuesToInsert);
            newDB.Run(dbname);
            bool exists = Directory.Exists(@"..//..//..//data//thisTable");
            Assert.AreEqual(true, exists);
            newTable.Run(dbname);
            bool existsTables = Directory.Exists(@"..//..//..//data//myDB//thisTable.data");
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



