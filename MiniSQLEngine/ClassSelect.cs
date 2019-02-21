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
        private string result;
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
        public string getResult()
        {
            return result;
        }

        public override string getClass()
        {
            return "select";
        }

        public override void Run(string dbname)
        {
            //Here we save the result into a variable so we can then pick it
            result = "result";
        }
       
    }
}
