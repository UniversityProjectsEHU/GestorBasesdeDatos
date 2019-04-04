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
        [TestMethod]
        public void TestGrant()
        {
            string user = "admin";
            string pass = "admin";
            string dbname = "myDBTest";
            string queryCreate = "CREATE TABLE pTable (int id true, int age false);";
            string sec_priv = "myProfile";
            string priv_type = "SELECT";
            string queryGrant = "GRANT " + priv_type + " ON pTable TO " + sec_priv+";";
            string queryCreateProfile = "CREATE SECURITY PROFILE myProfile;";
            string path = @"..\\..\\..\\data\\" + dbname + "\\pTable.sec";
            string linea = sec_priv + "," + priv_type;
            Database db = new Database(dbname, user, pass);
            db.Query(queryCreate);
            db.Query(queryCreateProfile);
            db.Query(queryGrant);
            String[] lineadef = System.IO.File.ReadAllLines(path);
            string line = lineadef[0];
            Assert.AreEqual(line, linea);
        }
    }
}
