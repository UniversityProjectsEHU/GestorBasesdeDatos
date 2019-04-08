using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniSQLEngine;
using System.Text.RegularExpressions;
using System.IO;

namespace ClassesTest
{
    [TestClass]
    public class RunTests
    {
        [TestMethod]
        public void TestCreateDropDatabaseTable()
        {
            string myDB = "mytestDB";
            string user = "admin";
            string pass = "admin";
            Database db = new Database(myDB, user, pass);
            bool exists = Directory.Exists(@"..//..//..//data//" + myDB);
            bool existsProf = Directory.Exists(@"..//..//..//data//"+myDB+"//profiles");
            bool existsPf = File.Exists(@"..//..//..//data//" + myDB + "//profiles//admin.pf");
            //Testing CreateDatabase
            Assert.AreEqual(true, exists);
            Assert.AreEqual(true, existsProf);
            Assert.AreEqual(true, existsPf);

            string myTable = "myTable";
            string[] values = new string[2];
            values[0] = "column1 string true";
            values[1] = "column2 int false";
            db.Query("CREATE TABLE " + myTable + "(" + values[0] + "," + values[1] + ");");
            //Testing CreateTable
            bool existsDef = File.Exists(@"..//..//..//data//" + myDB + "//myTable.def");
            Assert.AreEqual(true, existsDef);
            bool existsData = File.Exists(@"..//..//..//data//" + myDB + "//myTable.data");
            Assert.AreEqual(true, existsData);

            db.Query("DROP TABLE " + myTable + ";");
            //Testing DropTable
            bool existsDefDrop = File.Exists(@"..//..//..//data//" + myDB + "//myTable.def");
            Assert.AreEqual(false, existsDefDrop);
            bool existsDataDrop = File.Exists(@"..//..//..//data//" + myDB + "//myTable.data");
            Assert.AreEqual(false, existsDataDrop);

            db.Query("DROP DATABASE " + myDB +";");
            //Testing DropDatabase
            bool existsDrop = Directory.Exists(@"..//..//..//data//" + myDB + "");
            Assert.AreEqual(false, existsDrop);
        }
        [TestMethod]
        public void ExecuteTest()
        {
            string dbname = "myDB";
            //string dbname1 = "myDB1";
            string myTable = "thisTable";
            string[] values = { "One String true", "Two String false", "Three String false", "Caramba String false" };
            string[] valuesToInsert = { "One", "Two", "Three", "Caramba" };
            //string pathfileDEF = @"..//..//..//data//" + dbname + "//" + myTable + ".def";
            string rutaCompleta = @"..//..//..//data//" + dbname + "//" + myTable + ".data";
            /*string queryCreateDB = @"CREATE DATABASE myDB;";
            string queryCreateTable = @"CREATE TABLE myTable (column1 int true,column2 string false,column3 int false);";
            string queryInsert = @"INSERT INTO table1 VALUES (10,abc,8);";
            */
            ClassCreateDatabase newDB = new ClassCreateDatabase(dbname);
            ClassCreateTable newTable = new ClassCreateTable(myTable, values);
            ClassInsert inserted = new ClassInsert(myTable, valuesToInsert,null);
            newDB.Run(dbname);
            bool exists = Directory.Exists(@"..//..//..//data//" + dbname);
            Assert.AreEqual(true, exists);
            newTable.Run(dbname);
            /*bool existsTables = File.Exists(@"..//..//..//data//"+dbname+"//"+myTable+".data");
            Assert.AreEqual(true, existsTables);
            */
            inserted.Run(dbname);
            Assert.AreNotSame(0, rutaCompleta.Length);
            /*string fullPath = @"..//..//..//data//myDB//thisTable.data";
            StreamWriter file = new StreamWriter(fullPath, true);
            for (int i = 0; i < valuesToInsert.Length; i++)
            {
                file.WriteLine(valuesToInsert[i]);
            }*/
        }
        [TestMethod]
        public void TestUpdate()
        {
            //Database
            string myDB = "DBTestUpdate";
            string myTable = "tUpdate";
            Database db = new Database(myDB, "admin", "admin");
            //Table
            String Table = "CREATE TABLE tUpdate(column1 string true,column2 int false);";
            db.Query(Table);
            //Insert
            String Insert = "INSERT INTO tUpdate VALUES (hola,7);";
            db.Query(Insert);

            //Update 1
            string Update1 = "UPDATE tUpdate SET column1=lola,column2=5 WHERE column1=hola;";
            db.Query(Update1);
            //Testing update 1
            String[] lineas = System.IO.File.ReadAllLines("..//..//..//data//" + myDB + "//" + myTable + ".data");
            string actual = "lola,5";
            Assert.AreEqual(lineas[0], actual);

            //Update 2
            string Update2 = "UPDATE tUpdate SET column1=antonio,column2=7 WHERE column2>3;";
            db.Query(Update2);
            
            //Testing update 2
            String[] lineas2 = System.IO.File.ReadAllLines("..//..//..//data//" + myDB + "//" + myTable + ".data");
            string actual2 = "antonio,7";
            Assert.AreEqual(lineas2[0], actual2);

            ////Update 3
            string Update3 = "UPDATE tUpdate SET column1=francisco,column2=9 WHERE column2<20;";
            db.Query(Update3);
            
            //Testing update 3
            String[] lineas3 = System.IO.File.ReadAllLines("..//..//..//data//" + myDB + "//" + myTable + ".data");
            string actual3 = "francisco,9";
            Assert.AreEqual(lineas3[0], actual3);

            ////Update 2
            string Update4 = "UPDATE tUpdate SET column1=ana WHERE column2=9;";
            db.Query(Update4);
           
            //Testing update 2
            String[] lineas4 = System.IO.File.ReadAllLines("..//..//..//data//" + myDB + "//" + myTable + ".data");
            string actual4 = "ana,9";
            Assert.AreEqual(lineas4[0], actual4);

            db.Query("DROP DATABASE" + myDB + ";");

        }

        [TestMethod]
        public void TestDelete()
        {
            string myDB = "DBTestDelete";
            Database db = new Database(myDB, "admin", "admin");


            string myTable = "t1";
            string[] values = new string[2];
            values[0] = "name string true";
            values[1] = "edad int false";
            String q = "CREATE TABLE t1(name string true,edad int false);";
            db.Query(q);

            string[] insert1 = new string[2];
            insert1[0] = "Asier";
            insert1[1] = "1";

            string[] insert2 = new string[2];
            insert2[0] = "Leire";
            insert2[1] = "3";

            string[] insert3 = new string[2];
            insert3[0] = "Navarro";
            insert3[1] = "5";

            string[] insert4 = new string[2];
            insert4[0] = "Hernando";
            insert4[1] = "2";

            string[] insert5 = new string[2];
            insert5[0] = "Josu";
            insert5[1] = "4";

            db.Query("INSERT INTO t1 VALUES (Asier,1);");
            db.Query("INSERT INTO t1 VALUES (Leire,3);");
            db.Query("INSERT INTO t1 VALUES (Navarro,5);");
            db.Query("INSERT INTO t1 VALUES (Hernando,2);");
            db.Query("INSERT INTO t1 VALUES (Josu,4);");


           

            db.Query("DELETE FROM t1 WHERE edad>3;");
            

            string pathfileDATA = @"..//..//..//data//" + myDB + "//" + myTable + ".data";
            String[] lines = System.IO.File.ReadAllLines(pathfileDATA);
            //Testing Delete with >
            Assert.AreEqual(3, lines.Length);
            Assert.AreEqual(insert1[0] + "," + insert1[1], lines[0]);
            Assert.AreEqual(insert2[0] + "," + insert2[1], lines[1]);
            Assert.AreEqual(insert4[0] + "," + insert4[1], lines[2]);

           
            db.Query("DELETE FROM t1 WHERE name=Leire;");

            String[] lines2 = System.IO.File.ReadAllLines(pathfileDATA);
            //Testing Delete with string
            Assert.AreEqual(2, lines2.Length);
            Assert.AreEqual(insert1[0] + "," + insert1[1], lines2[0]);
            Assert.AreEqual(insert4[0] + "," + insert4[1], lines2[1]);

           
            db.Query("DELETE FROM t1 WHERE edad<2;");

            String[] lines3 = System.IO.File.ReadAllLines(pathfileDATA);
            //Testing Delete with <
            Assert.AreEqual(1, lines3.Length);
            Assert.AreEqual(insert4[0] + "," + insert4[1], lines3[0]);
            db.Query("DROP DATABASE" + myDB +";");
        }
        [TestMethod]
        public void fullTestWithSelect()
        {
            string dbname = "myDBFull";
            string user = "admin";
            string pass = "admin";
            string myTable = "thisTable111";
            string queryCreateDB = "CREATE DATABASE myDBFull;";
            string queryDropDB = "DROP DATABASE myDBFull;";
            //When doing a CREATE TABLE query, the definition of the columns have no spaces between commas
            string queryCreateTable = "CREATE TABLE thisTable111 (id int true,name String false,age int false);";
            string queryInsert = "INSERT INTO thisTable111 VALUES (1,Alejandra,37);";
            string queryInsert2 = "INSERT INTO thisTable111 VALUES (2,Paco,60);";
            string querySelect = "SELECT id,name FROM thisTable111 WHERE age<50;";
            string querySelectNotExists = "SELECT address FROM thisTable111 WHERE id=1;";
            string querySelectAll = "SELECT * FROM thisTable111 WHERE age>10;";
            string[] values = { "id int true", "name String false", "edad int false" };
            string[] valuesToInsert = { "1", "Alejandra", "36" };
            string[] valuesToInsert2 = { "2", "Paco", "60" };
            string message = "The result for the Query '" + querySelect + "' is: id 1 name Alejandra;";
            string messageAll = "The result for the Query '" + querySelectAll + "' is: id 1 name Alejandra age 37; id 2 name Paco age 60;";
            string messageNotExists = "ERROR: Column does not exist";
            Database db = new Database(dbname, user,pass);
            /*ClassCreateDatabase newDB = new ClassCreateDatabase(dbname);
            ClassCreateTable newTable = new ClassCreateTable(myTable, values);
            ClassInsert newInsert1 = new ClassInsert(myTable, valuesToInsert);
            ClassInsert newInsert2 = new ClassInsert(myTable, valuesToInsert2);
            newDB.Run(dbname);
            newTable.Run(dbname);
            newInsert1.Run(dbname);
            newInsert2.Run(dbname);
            */
            /*Testing the output of the select query, this query mustn't show the "Empty Query" message when 
            there's no matches to the query typed in console, it should show the name of the columns without any content*/
            string querySelectNoMatches = "SELECT id FROM thisTable111 WHERE age>65;";
            string querySelectNoWhere = "SELECT id FROM thisTable111;";
            string querySelectAllNoWhere = "SELECT * FROM thisTable111;";
            string messageNoMatches = "The result for the Query '" + querySelectNoMatches + "' is: id;";
            string messageNoWhere = "The result for the Query '" + querySelectNoWhere + "' is: id 1; id 2;";
            string messageAllNoWhere = "The result for the Query '" + querySelectAllNoWhere + "' is: id 1 name Alejandra age 37; id 2 name Paco age 60;";
            db.Query(queryDropDB);
            db.Query(queryCreateDB);
            db.Query(queryCreateTable);
            db.Query(queryInsert);
            db.Query(queryInsert2);
            Assert.AreEqual(db.Query(querySelect), message);
            Assert.AreEqual(db.Query(querySelectNotExists), messageNotExists);
            Assert.AreEqual(db.Query(querySelectAll), messageAll);
            Assert.AreEqual(db.Query(querySelectNoMatches), messageNoMatches);
            Assert.AreEqual(db.Query(querySelectNoWhere), messageNoWhere);
            Assert.AreEqual(db.Query(querySelectAllNoWhere), messageAllNoWhere);

        }
        [TestMethod]
        public void testCreateWithAdmin()
        {
            string dbname = "myDBFull";
            string user = "admin";
            string pass = "admin";
            string queryCreateDB = "CREATE DATABASE myDBFull;";
            string queryDropDB = "DROP DATABASE myDBFull;";
            string queryCreate = "CREATE TABLE pTable (int id true, int age false);";
            string line = "admin,DELETE/INSERT/SELECT/UPDATE";
            string path = @"..//..//..//data//" + dbname + "//pTable.sec";
            Database db = new Database(dbname,user,pass);
            db.Query(queryCreateDB);
            db.Query(queryCreate);
            String[] lineadef = System.IO.File.ReadAllLines(path);
            string uno = lineadef[0];
            Assert.AreEqual(uno, line);
            db.Query(queryDropDB);
        }
        [TestMethod]
        public void testDropWithAdmin()
        {
            string dbname = "myDBFull";
            string user = "admin";
            string pass = "admin";
            string queryCreateDB = "CREATE DATABASE myDBFull;";
            string queryDropDB = "DROP DATABASE myDBFull;";
            string queryCreate = "CREATE TABLE pTable (int id true, int age false);";
            string queryDrop = "DROP TABLE pTable;";
            string line = "admin,DELETE/INSERT/SELECT/UPDATE";
            string path = @"..//..//..//data//" + dbname + "//pTable.sec";
            Database db = new Database(dbname, user,pass);
            db.Query(queryCreateDB);
            db.Query(queryCreate);
            Assert.AreEqual(true, File.Exists(path));
            db.Query(queryDrop);
            Assert.AreEqual(false, File.Exists(path));
            db.Query(queryDropDB);
        }
    }
}

