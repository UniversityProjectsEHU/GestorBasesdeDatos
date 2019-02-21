using System;
using MiniSQLEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class ClassCreateDatabase : Query
    {
        private string tableName; 
        public ClassCreateDatabase(string pName)
        {
            pName = tableName;
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
            string path = @"..//..//..//data//" + tableName;
            System.IO.Directory.CreateDirectory(path);

        }
    }
}
