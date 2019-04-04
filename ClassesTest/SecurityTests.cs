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
            Database db = new Database("MyDB", "admin", "admin");
            string q = "CREATE SECURITY PROFILE user;";
            db.Query(q);
            string pathProfiles = @"..\\..\\..\\data\\mydb\\profiles\\user.pf";
            bool existsPf = File.Exists(pathProfiles);
            Assert.AreEqual(true, existsPf);

        }

    }
}
