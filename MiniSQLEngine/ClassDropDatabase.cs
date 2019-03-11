using MiniSQLEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class ClassDropDatabase : Query
    {
        private string name;
        private string result;

        public ClassDropDatabase(string pName)
        {
            name = pName;
        }

        public override string getClass()
        {
            return "dropdb";
        }
        public override string getResult()
        {
            return result;
        }

        public override void Run(string dbname)
        {
            Boolean hayerror = false;
            if (Directory.Exists("..//..//..//data//" + dbname)==false)
            {
                result = Constants.Error;
                hayerror = true;
            }

            if (hayerror == false)
            {
                string path = @"..//..//..//data//" + dbname;
                System.IO.Directory.Delete(path, true);
                result = Constants.DeleteDatabaseSuccess;
            }
        }
        public string getDatabaseName()
        {
            return name;
        }
    }
}
