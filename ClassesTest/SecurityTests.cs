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
    public class SecurityTests
    {
        [TestMethod]
        public void TestCreateDropProfile()
        {
            Database db = new Database("testDB", "admin", "admin");
            string q = "CREATE SECURITY PROFILE user;";
            db.Query(q);
            string pathProfiles = @"..\\..\\..\\data\\testDB\\profiles\\user.pf";
            //CreateProfile testing
            bool existsPf = File.Exists(pathProfiles);
            Assert.AreEqual(true, existsPf);

            string q2 = "DROP SECURITY PROFILE user;";
            db.Query(q2);
            //DropProfile testing
            bool existsPf2 = File.Exists(pathProfiles);
            Assert.AreEqual(false, existsPf);

            db.Query("DROP DATABASE testDB;");
        }
    }
}
