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
            string op;
            string[] elements;
            if (condition.Contains("="))
            {
                elements = condition.Split('=');
                op = "=";
            }
            else if (condition.Contains("<"))
            {
                elements = condition.Split('<');
                op = "<";
            }
            else
            {
                elements = condition.Split('>');
                op = ">";
            }
            //I read all the file
            string allFile = System.IO.File.ReadAllText("..//..//..//data//" + dbname + "//" + table + ".def");
            string []splittedFile = allFile.Split(',');
            string []numcolumns=;
            int cont = 0;
            for (int i = 0; i < splittedFile.Length; i++)
            {
                for (int j = 0; j < columns.Length; j++)
                {
                    if (splittedFile[i].Contains(columns[j]))
                    {
                        numcolumns[cont] = columns[j] + " in " + i;
                    }
                }
            }
            result = "result";
        }
       
    }
}
