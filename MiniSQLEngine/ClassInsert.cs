
using System;
using System.Collections.Generic;
using System.IO;
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
            string rutaCompleta = @"..//..//..//data//"+dbname+"//"+aTable+".data";
            string texto="";
            for (int i = 0; i < values.Length; i++)
            {
                if (i == values.Length-1)
                {
                    texto = texto + values[i]+";";
                }
                else
                {
                    texto = texto + values[i]+",";

                }
            }

            using (StreamWriter file = new StreamWriter(rutaCompleta, true))
            {
                //se agrega información al documento
                file.WriteLine(texto); 
               

                file.Close();
            }
        }
    }
}
