
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class ClassInsert : Query
    {
        private string aTable;
        private string[] values;
        public ClassInsert(String table, String[] myArray)
        {
            aTable = table;
            values = myArray;
        }
        public string GetTable()
        {
            return aTable;
        }

        public string[] GetValues()
        {
            return values;
        }


        public override string getClass()
        {
            return "insert";
        }

        public override void Run(string dbname)
        {
            
        }
    }
}
