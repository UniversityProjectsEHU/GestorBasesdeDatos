using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parsing;
using MiniSQLEngine;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestManageDropTable()
        {
            string query = @"DROP TABLE table1";
            Class1 myClass = new Class1();
            ClassDropTable dt = myClass.ManageDropTable(query);
            String name = "table1";
            ClassDropTable dtExpected = new ClassDropTable(name);
            Assert.AreEqual(dt.GetName(), dtExpected.GetName());
        }

        [TestMethod]
        public void TestManageInsert()
        {
            Class1 myClass = new Class1();
            ClassInsert i = myClass.ManageInsert("INSERT INTO table1 VALUES (10,abc,8)");
            string[] values = new string[3];
            values[0] = "10";
            values[1] = "abc";
            values[2] = "8";
            ClassInsert iExpected = new ClassInsert("table1", values);
            Assert.AreEqual(i.GetTable(), iExpected.GetTable());
            Assert.AreEqual(i.GetValues(), iExpected.GetValues());
        }

        [TestMethod]
        public void TestManageSelect()
        {
            Class1 myClass = new Class1();
            ClassSelect s = myClass.ManageSelect("SELECT edad,curso FROM universidad WHERE edad>18");
            string[] columns = new string[2];
            columns[0] = "edad";
            columns[1] = "curso";
            ClassSelect sExpected = new ClassSelect(columns, "universidad", "edad>18");
            Assert.AreEqual(s.GetTable(), sExpected.GetTable());
            Assert.AreEqual(s.GetColumns(), sExpected.GetColumns());
            Assert.AreEqual(s.GetCondition(), sExpected.GetCondition());
        }

        [TestMethod]
        public void TestManageUpdate()
        {
            Class1 myClass = new Class1();
            ClassUpdate u = myClass.ManageUpdate("UPDATE table1 SET nombre=Ana,edad=20 WHERE nombre=Maria");
            string[] columns = new string[2];
            columns[0] = "nombre=Ana";
            columns[1] = "edad=20";
            ClassUpdate uExpected = new ClassUpdate("table1", columns, "nombre=Maria");
            Assert.AreEqual(u.GetTable(), uExpected.GetTable());
            Assert.AreEqual(u.GetColumns(), uExpected.GetColumns());
            Assert.AreEqual(u.GetCondition(), uExpected.GetCondition());
        }
    }
}
