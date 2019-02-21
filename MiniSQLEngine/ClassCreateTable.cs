using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class ClassCreateTable : Query
    {
        private string aTable;
        private string[] values;

        public ClassCreateTable(String table, String[] myArray)
        {
            aTable = table;
            values = myArray;
        }
        public override void Run()
        {
            throw new NotImplementedException();
        }
        public string getTableName()
        {
            return aTable;
        }
        public string[] getTableValues()
        {
            return values;
        }
    }
}
