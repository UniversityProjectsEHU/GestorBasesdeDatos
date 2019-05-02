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
            db.Query(q, db);
            string pathProfiles = @"..\\..\\..\\data\\testDB\\profiles\\user.pf";
            //CreateProfile testing
            bool existsPf = File.Exists(pathProfiles);
            Assert.AreEqual(true, existsPf);

            string q2 = "DROP SECURITY PROFILE user;";
            db.Query(q2, db);
            //DropProfile testing
            bool existsPf2 = File.Exists(pathProfiles);
            Assert.AreEqual(false, existsPf2);

            db.Query("DROP DATABASE testDB;", db);
        }
        [TestMethod]
        public void TestGrant()
        {
            string user = "admin";
            string pass = "admin";
            string dbname = "myDBTest";
            string queryCreate = "CREATE TABLE pTable (int id true, int age false);";
            string queryDrop = "DROP DATABASE myDBTest";
            string sec_priv = "myProfile";
            string sec_priv2 = "myProfile2";
            string priv_type = "SELECT";
            string priv_type2 = "UPDATE";
            string queryGrant = "GRANT SELECT ON pTable TO myProfile;";
            string queryGrantUpdate = "GRANT " + priv_type2 + " ON pTable TO " + sec_priv + ";";
            string queryProfileDoesNotExist = "GRANT " + priv_type + " ON pTable TO " + sec_priv2 + ";";
            string queryCreateProfile = "CREATE SECURITY PROFILE myProfile;";
            string path = @"..\\..\\..\\data\\" + dbname + "\\pTable.sec";
            string linea = sec_priv + "," + priv_type;
            string linea2 = sec_priv + "," + priv_type + "/" + priv_type2;
            string profDoesNotExist = Constants.SecurityProfileDoesNotExist;
            Database db = new Database(dbname, user, pass);
            db.Query(queryCreate, db);
            db.Query(queryCreateProfile, db);
            db.Query(queryGrant, db);
            db.Query(queryGrantUpdate, db);
            String[] lineadef = System.IO.File.ReadAllLines(path);
            string line = lineadef[1];
            Assert.AreEqual(line, linea2);
            Assert.AreEqual(db.Query(queryProfileDoesNotExist, db), profDoesNotExist);
            db.Query(queryDrop, db);
        }
        [TestMethod]
        public void TestRevoke()
        {
            string user = "admin";
            string pass = "admin";
            string dbname = "myDBTest";
            string queryCreate = "CREATE TABLE pTable (int id true, int age false);";
            string queryDrop = "DROP DATABASE myDBTest;";
            string sec_priv = "myProfile";
            string sec_priv2 = "myProfile2";
            string priv_type = "SELECT";
            string priv_type2 = "UPDATE";
            string priv_type3 = "DELETE";
            string queryGrantSelect = "GRANT "+ priv_type + " ON pTable TO " + sec_priv + ";";
            string queryGrantUpdate = "GRANT " + priv_type2 + " ON pTable TO " + sec_priv + ";";
            string queryRevoke = "REVOKE " + priv_type2 + " ON pTable TO " + sec_priv + ";";
            string queryRevokeNotExists = "REVOKE " + priv_type3 + " ON pTable TO " + sec_priv + ";";
            string queryCreateProfile = "CREATE SECURITY PROFILE myProfile;";
            string path = @"..\\..\\..\\data\\" + dbname + "\\pTable.sec";
            string linea = sec_priv + "," + priv_type;
            string Error = "ERROR: ";
            string RevDoesNotExist = Constants.SecurityPrivilegeRevoked;
            Database db = new Database(dbname, user, pass);
            db.Query(queryCreate, db);
            db.Query(queryCreateProfile, db);
            db.Query(queryGrantSelect, db);
            db.Query(queryGrantUpdate, db);
            db.Query(queryRevoke, db);
            Assert.AreEqual(db.Query(queryRevokeNotExists, db), RevDoesNotExist);
            String[] lineadef = System.IO.File.ReadAllLines(path);
            string line = lineadef[1];
            Assert.AreEqual(line, linea);
            db.Query(queryDrop, db);
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            Database db = new Database("testDB2", "admin", "admin");
            string q = "CREATE SECURITY PROFILE user;";
            db.Query(q, db);
            string a = "ADD USER ('sergio', '123', user);";
            db.Query(a, db);
            string profile = "user";
            string pathfileDATA = @"..//..//..//data//testDB2//profiles//" + profile + ".pf";
            using (StreamReader lineadef = File.OpenText(pathfileDATA))
            {
                string linea = "sergio,123";
                Assert.AreEqual(linea, lineadef.ReadLine());
            }
            
            string b = "ADD USER ('lola', '123', user);";
            db.Query(b, db);

            string q2 = "DELETE USER sergio;";
            db.Query(q2, db);
            using (StreamReader lineadef = File.OpenText(pathfileDATA))
            {
                string linea2 = "lola,123";
                Assert.AreEqual(linea2, lineadef.ReadLine());
            }

            db.Query("DROP DATABASE testDB2;", db);
        }
    }
}
