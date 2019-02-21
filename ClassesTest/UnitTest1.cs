using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parsing;
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
            Class1 myClass = new Class1();
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
            Class1 myClass = new Class1();
            string name = "myTable";
            string[] values = { "column1 int true", "column2 string false", "column3 int false" };
            ClassCreateTable myCreatedTable = new ClassCreateTable(name, values);
            Assert.AreEqual(myClass.ManageCreateTable(query).getTableName(), myCreatedTable.getTableName());
        }
        [TestMethod]
        public void TestManageCreateTableTableValues()
        {
            string query = @"CREATE TABLE myTable(column1 int true,column2 string false,column3 int false);";
            Class1 myClass = new Class1();
            string name = "myTable";
            string[] values = { "column1 int true", "column2 string false", "column3 int false" };
            string[] expectedValues = myClass.ManageCreateTable(query).getTableValues();
            ClassCreateTable myCreatedTable = new ClassCreateTable(name, values);
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
            Class1 myClass = new Class1();
            string name = "myTable";
            string[] values = { "column1 int true", "column2 string false", "column3 int false" };
            ClassCreateTable myCreatedTable = new ClassCreateTable(name, values);
            Assert.AreEqual(myClass.ManageCreateTable(query).getTableName(), myCreatedTable.getTableName());
        }
        [TestMethod]
        public void TestManageDeleteCondition()
        {
            string query = @"DELETE FROM myTable WHERE id=1;";
            Class1 myClass = new Class1();
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
            Class1 myClass = new Class1();
            string name = "myDB";
            ClassDropDatabase myDroppedDatabase = new ClassDropDatabase(name);
            Assert.AreEqual(myClass.ManageDropDatabase(query).getDatabaseName(), myDroppedDatabase.getDatabaseName());
        }
    }
    [TestClass]
    public class DeleteTest
    {
        [TestMethod]
        public void TestManageDeleteTable()
        {
            string query = @"CREATE TABLE myTable(column1 int,column2 String,column3 int);";
            Class1 myClass = new Class1();
            string name = "myTable";
            string[] values = { "column1 int true", "column2 string false", "column3 int false" };
            ClassCreateTable myCreatedTable = new ClassCreateTable(name, values);
            Assert.AreEqual(myClass.ManageCreateTable(query).getTableName(), myCreatedTable.getTableName());
        }
        [TestMethod]
        public void TestManageDeleteCondition()
        {
            string query = @"DELETE FROM myTable WHERE id=1;";
            Class1 myClass = new Class1();
            string name = "myTable";
            string condition = "id=1";
            ClassDelete myDeletedTable = new ClassDelete(name, condition);
            Assert.AreEqual(myClass.ManageDelete(query).getCondition(), myDeletedTable.getCondition());
        }
    }
}