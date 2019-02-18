using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class ClassSelect : Query
    {
        private string []columns;
        private string table;
        private string condition;
        public ClassSelect(string [] columns,string table,string condition)
        {
            this.columns = columns;
            this.table = table;
            this.condition = condition;
        }
        public string GetTable()
        {
            return table;
        }

        public string[] GetColumns()
        {
            return columns;
        }

        public string GetCondition()
        {
            return condition;
        }
        public override void Run()
        {
           
        }
    }
}
