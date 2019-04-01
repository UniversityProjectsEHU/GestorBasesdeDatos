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
            string myDB = "myDB";
            ClassCreateDatabase db = new ClassCreateDatabase(myDB);
            db.Run(myDB);
            bool exists = Directory.Exists(@"..//..//..//data//myDB");
        
            Assert.AreEqual(true, exists);
            Query q = new Query("CREATE SECURITY PROFILE user;",myDB);
            q.run();
            string pathProfiles = @"..\\..\\..\\data\\mydb\\profiles\\user";
            Assert.AreEqual(true, pathProfiles);

        }
    }
}
