using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQLEngine;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
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
