using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class ClassUpdate : Query
    {
        private string Table,Condition;
        private string[] Column;
        
        public ClassUpdate(string pTable,string[] pColumn,string pCondition)
        {
            Table = pTable;
            Column = pColumn;
            Condition = pCondition;
        }
        public string GetTable()
        {
            return Table;
        }

        public string[] GetColumns()
        {
            return Column;
        }
        public string GetCondition()
        {
            return Condition;
        }
        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
}

