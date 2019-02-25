using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public override string getClass()
        {
            return "delete";
        }

        public override void Run(string dbname)
        {
            string pathfileDEF = @"..//..//..//data//" + dbname + "//" + Table + ".def";
            string line1;
            List<string> columns = new List<string>();
            string attr = null;
            string symbol = null;
            string cond = null;


            Match at = Regex.Match(Condition, Constants.regExConditionAttribute);
            if (at.Success)
            {
                attr = at.Groups[1].Value;
            }
            Match val = Regex.Match(Condition, Constants.regExConditionValue);
            if (val.Success)
            {
                string value = val.Groups[1].Value;
                symbol = value.Substring(0,1);
                cond = value.Substring(1);
            }

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

            int index = columns.IndexOf(attr);
            

            string pathfileDATA = @"..//..//..//data//" + dbname + "//" + Table + ".data";
            string line2 = "";
     
            using (StreamReader sr2 = new StreamReader(pathfileDATA))
            {
                while ((line2 = sr2.ReadLine()) != null)                
                {
                    string[] data = line2.Split(',');
                    if (symbol.Equals("="))
                    { 
                        if (data[index].Equals(cond))
                        {
                            //Borrar linea
                        }
                    }
                    if (symbol.Equals(">"))
                    {
                        //if ((int) data[index] > (int)cond))
                        {
                            //Borrar linea
                        }
                    }
                    if (symbol.Equals("<"))
                    {
                        //if ((int) data[index] < (int)cond))
                        {
                            //Borrar linea
                        }
                    }
                }

            }
    }
}

