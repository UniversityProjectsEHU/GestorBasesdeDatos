using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class ClassDelete : Query
    {
        private string table, condition;

        public ClassDelete(string pTable,string pCondition)
        {
            table = pTable;
            condition = pCondition;
        }
        public override void Run()
        {
            throw new NotImplementedException();
        }
        public string GetTableName()
        {
            return table;
        }
        public string getCondition()
        {
            return condition;
        }
    }
}

