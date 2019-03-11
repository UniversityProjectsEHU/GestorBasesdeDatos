
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
        private string result;
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

        public override string getResult()
        {
            return result;
        }

        public override void Run(string dbname)
        {
            string pathfileDATA = @"..//..//..//data//" + dbname + "//" + aTable + ".data";

            if (!File.Exists(pathfileDATA))
            {
                result = Constants.TableDoesNotExist;
            }

            else
            {
                string pathfileDEF = @"..//..//..//data//" + dbname + "//" + aTable + ".def";
                string line1;
                List<string> columns = new List<string>();

                using (StreamReader sr = new StreamReader(pathfileDEF))
                {
                    while ((line1 = sr.ReadLine()) != null)
                    {
                        string[] parts = line1.Split(',');
                        foreach (string element in parts)
                        {
                            string[] atributes = element.Split(' ');
                            string type = atributes[0];
                            columns.Add(type);

                        }

                    }
                }

                string texto = "";
                for (int i = 0; i < values.Length; i++)
                {
                    if (i == values.Length - 1)
                    {
                        texto = texto + values[i];
                    }
                    else
                    {
                        texto = texto + values[i] + ",";

                    }
                }

                using (StreamWriter file = File.AppendText(pathfileDATA))
                {
                    //Data added to the document
                    file.WriteLine(texto);
                    file.Close();
                    result = Constants.InsertSuccess;
                }
            }
        }
    }
}
