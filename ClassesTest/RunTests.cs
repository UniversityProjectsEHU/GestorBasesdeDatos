using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQLEngine;
using System.Text.RegularExpressions;

namespace ClassesTest
{
    [TestClass]
    class TestCreateTable
    {
        public void RunTest()
        {
            string dbname = "myDB";
            string myTable = "aTable";
            string[] values = { "One", "Two", "Three", "Caramba" };
            ClassCreateTable aNewTable = new ClassCreateTable(myTable, values);
        }
    }
}

