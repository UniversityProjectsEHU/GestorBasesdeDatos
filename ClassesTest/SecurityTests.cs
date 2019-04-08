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
            Assert.AreEqual(false, existsPf2);

            db.Query("DROP DATABASE testDB;");
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            Database db = new Database("testDB2", "admin", "admin");
            string q = "CREATE SECURITY PROFILE user;";
            db.Query(q);
            string a = "ADD USER (sergio, 123, user);";
            db.Query(a);
            string profile = "user";
            string pathfileDATA = @"..//..//..//data//testDB2//profiles//" + profile + ".pf";
            using (StreamReader lineadef = File.OpenText(pathfileDATA))
            {
                string linea = "sergio 123";
                Assert.AreEqual(linea, lineadef.ReadLine());
            }
            
            string b = "ADD USER (lola, 123, user);";
            db.Query(b);

            string q2 = "DELETE USER sergio;";
            db.Query(q2);
            using (StreamReader lineadef = File.OpenText(pathfileDATA))
            {
                string linea2 = "lola 123";
                Assert.AreEqual(linea2, lineadef.ReadLine());
            }

            db.Query("DROP DATABASE testDB2;");
        }
    }
}
