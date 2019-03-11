using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class ClassCreateTable : Query
    {
        private string aTable;
        private string[] values;
        private string result;
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
            string pathfileDEF = @"..//..//..//data//" + dbname +"//"+ aTable + ".def";
            string pathfileDATA = @"..//..//..//data//" + dbname + "//" + aTable + ".data";

            if (File.Exists(pathfileDATA) || File.Exists(pathfileDEF))
            {
                result = Constants.TableAlreadyExists;
            }

            else
            {
                using (FileStream stream1 = System.IO.File.Create(pathfileDATA))
                {

                    using (FileStream stream = File.Create(pathfileDEF))
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            if (i != (values.Length - 1))
                            {
                                string actual = values[i] + ",";
                                byte[] info = new UTF8Encoding(true).GetBytes(actual);
                                stream.Write(info, 0, info.Length);
                            }
                            else
                            {
                                string actual = values[i];
                                byte[] info = new UTF8Encoding(true).GetBytes(actual);
                                stream.Write(info, 0, info.Length);
                            }
                        }
                        byte[] info2 = new UTF8Encoding(true).GetBytes(";");
                        stream.Write(info2, 0, info2.Length);
                    }
                }
                result = Constants.CreateTableSuccess;
            }
        }
        public string getTableName()
        {
            return aTable;
        }
        public string[] getTableValues()
        {
            return values;
        }
        public string getResult()
        {
            return result;
        }
    }
}
