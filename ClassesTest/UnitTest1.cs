using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parsing;
using MiniSQLEngine;
using System.Text.RegularExpressions;

namespace ClassesTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestManageCreateDatabase()
        {
            String query = @"CREATE DATABASE myDB";
            Class1 myClass = new Class1();
            String name = "myDB";
            ClassCreateDatabase myCreatedDB = new ClassCreateDatabase(name);

            Assert.AreEqual(myClass.ManageCreateDatabase(query).getName(), myCreatedDB.getName());
    }
    }
}
