using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MiniSQLEngine;
using System.Text.RegularExpressions;

namespace ClassesTest
{
    [TestClass]
    public class CreateDatabaseTest
    {
        [TestMethod]
        public void TestManageCreateDatabase()
        {
            string query = @"CREATE DATABASE myDB";
            ClassParsing myClass = new ClassParsing();
            string name = "myDB";
            ClassCreateDatabase myCreatedDB = new ClassCreateDatabase(name);

            Assert.AreEqual(myClass.ManageCreateDatabase(query).getName(), myCreatedDB.getName());
        }
    }

    [TestClass]
    public class CreateTableTest
    {
        [TestMethod]
        public void TestManageCreateTableTableName()
        {
            string query = @"CREATE TABLE myTable(column1 int true,column2 string false,column3 int false);";
            ClassParsing myClass = new ClassParsing();
            string name = "myTable";
            string[] values = { "column1 int true", "column2 string false", "column3 int false" };
            ClassCreateTable myCreatedTable = new ClassCreateTable(name, values);
            Assert.AreEqual(myClass.ManageCreateTable(query).getTableName(), myCreatedTable.getTableName());
        }
        [TestMethod]
        public void TestManageCreateTableTableValues()
        {
            string query = @"CREATE TABLE myTable(column1 int true,column2 string false,column3 int false);";
            ClassParsing myClass = new ClassParsing();
            string name = "myTable";
            string[] values = { "column1 int true", "column2 string false", "column3 int false" };
            string[] expectedValues = myClass.ManageCreateTable(query).getTableValues();
            for (int i = 0; i < values.Length; i++)
            {
                Assert.AreEqual(expectedValues[i], values[i]);
            }

        }
    }
    [TestClass]
    public class DeleteTest
    {
        [TestMethod]
        public void TestManageDeleteTable()
        {
            string query = @"CREATE TABLE myTable(column1 int,column2 String,column3 int);";
            ClassParsing myClass = new ClassParsing();
            string name = "myTable";
            string[] values = { "column1 int true", "column2 string false", "column3 int false" };
            ClassCreateTable myCreatedTable = new ClassCreateTable(name, values);
            Assert.AreEqual(myClass.ManageCreateTable(query).getTableName(), myCreatedTable.getTableName());
        }
        [TestMethod]
        public void TestManageDeleteCondition()
        {
            string query = @"DELETE FROM myTable WHERE id=1;";
            ClassParsing myClass = new ClassParsing();
            string name = "myTable";
            string condition = "id=1";
            ClassDelete myDeletedTable = new ClassDelete(name, condition);
            Assert.AreEqual(myClass.ManageDelete(query).getCondition(), myDeletedTable.getCondition());
        }
    }
    [TestClass]
    public class DropDatabase
    {
        [TestMethod]
        public void TestManageDropDatabase()
        {
            string query = @"DROP DATABASE myDB;";
            ClassParsing myClass = new ClassParsing();
            string name = "myDB";
            ClassDropDatabase myDroppedDatabase = new ClassDropDatabase(name);
            Assert.AreEqual(myClass.ManageDropDatabase(query).getDatabaseName(), myDroppedDatabase.getDatabaseName());
        }
    }



    [TestClass]
    public class ParsingTests
    {
        ClassParsing myClass = new ClassParsing();

        [TestMethod]
        public void TestManageDropTable()
        {
            string query = @"DROP TABLE table1;";
            ClassDropTable dt = myClass.ManageDropTable(query);
            String name = "table1";
            ClassDropTable dtExpected = new ClassDropTable(name);
            Assert.AreEqual(dtExpected.GetName(), dt.GetName());
        }

        [TestMethod]
        public void TestManageInsert()
        {
            ClassInsert i = myClass.ManageInsert("INSERT INTO table1 VALUES (10,abc,8);");
            string[] values = i.GetValues();
            Assert.AreEqual("table1", i.GetTable());
            Assert.AreEqual("10", i.GetValues()[0]);
            Assert.AreEqual("abc", i.GetValues()[1]);
            Assert.AreEqual("8", i.GetValues()[2]);
        }

        [TestMethod]
        public void TestManageSelect()
        {
            ClassSelect s = myClass.ManageSelect("SELECT edad,curso FROM universidad WHERE edad>18;");
            string[] columns = s.GetColumns();
            Assert.AreEqual("universidad", s.GetTable());
            Assert.AreEqual("edad", s.GetColumns()[0]);
            Assert.AreEqual("curso", s.GetColumns()[1]);
            Assert.AreEqual("edad>18", s.GetCondition());
        }

        [TestMethod]
        public void TestManageUpdate()
        {
            ClassUpdate u = myClass.ManageUpdate("UPDATE table1 SET nombre=Ana,edad=20 WHERE nombre=Maria;");
            string[] columns = u.GetColumns();
            Assert.AreEqual("table1", u.GetTable());
            Assert.AreEqual("nombre=Ana", u.GetColumns()[0]);
            Assert.AreEqual("edad=20", u.GetColumns()[1]);
            Assert.AreEqual("nombre=Maria", u.GetCondition());
        }
    }
}
