using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class ClassDelete : Query
    {
        private string Table, Condition;
        public ClassDelete(string pTable,string pCondition)
        {
            Table = pTable;
            Condition = pCondition;
        }
        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
}

