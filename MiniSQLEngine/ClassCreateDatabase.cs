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
            tableName = pName;
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
            try
            {
                string path = @"..//..//..//data//" + tableName;
                //string path = @"C:\Users\docencia\source\repos\sergioyeahmen\Si-funciona-no-lo-toques\data\" + tableName;

                System.IO.Directory.CreateDirectory(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
               
            }

        }
    }
}
