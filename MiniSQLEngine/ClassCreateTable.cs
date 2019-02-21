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

        public override string getClass()
        {
            return "createtable";
        }

        public override void Run(string dbname)
        {
            //Pendiente de cambios
            string pathfileDEF = @"..//..//..//data//" + dbname +"//"+ aTable + ".def";
            string pathfileDATA = @"..//..//..//data//" + dbname + "//" + aTable + ".data";
            System.IO.File.Create(pathfileDEF);
            System.IO.File.Create(pathfileDATA);

            string info = "";
            for (int i = 0; i < values.Length; i++)
            {
                info = info + "," + values[i];
            }

            System.IO.File.WriteAllText(pathfileDEF, info);
        }
    }
}
