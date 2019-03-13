using System;
using MiniSQLEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MiniSQLEngine
{
    public class ClassCreateDatabase : Query
    {
        private string tableName;
        private string result;
        public ClassCreateDatabase(string pName)
        {
            tableName = pName;
        }
        public override string getResult()
        {
            return result;
        }
        public override string getClass()
        {
            return "createdb";
        }

        public string getName()
        {
            return tableName;
        }
        public override void Run(string dbname)
        {
            Boolean hayerror = false;
            if (Directory.Exists("..//..//..//data//" + dbname))
            {
                result = Constants.Error;
                hayerror = true;
            }

            if (hayerror == false)
            {
                try
                {
                    string path = @"..\\..\\..\\data\\" + tableName;
                    //string path = @"C:\Users\docencia\source\repos\sergioyeahmen\Si-funciona-no-lo-toques\data\" + tableName;

                    System.IO.Directory.CreateDirectory(path);
                    result = Constants.CreateDatabaseSuccess;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);

                }
            }
        }
    }
}
